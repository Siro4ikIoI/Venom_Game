using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomData
{
    public GameObject roomPrefab;
    public bool topExit, bottomExit, leftExit, rightExit;
}

public class RoomInstance : MonoBehaviour
{
    public RoomData roomData; // Связь с данными комнаты
}

public class LevelGen : MonoBehaviour
{
    [Header("Основные комнаты")]
    public List<RoomData> roomTemplates;
    public GameObject startRoomPrefab;
    public GameObject endRoomPrefab;

    [Header("Тупики (по 1 выходу)")]
    public List<RoomData> deadEndRooms;

    [Header("Настройки генерации")]
    public int maxRooms = 20;
    public Vector2Int gridSize = new Vector2Int(10, 10);
    public Vector2Int startPos = Vector2Int.zero;

    private Dictionary<Vector2Int, GameObject> placedRooms = new Dictionary<Vector2Int, GameObject>();
    private List<Vector2Int> roomPositions = new List<Vector2Int>();

    private List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        Queue<Vector2Int> frontier = new Queue<Vector2Int>();

        // --- 1. Стартовая комната ---
        var startRoom = Instantiate(startRoomPrefab, GetWorldPosition(startPos), Quaternion.identity);
        placedRooms[startPos] = startRoom;
        roomPositions.Add(startPos);

        // --- 2. Первая обязательная комната рядом со стартом ---
        Vector2Int firstRoomPos = startPos + Vector2Int.down;
        placedRooms[firstRoomPos] = null; // Резервируем
        frontier.Enqueue(firstRoomPos);
        roomPositions.Add(firstRoomPos);

        // --- 3. Генерация остальных комнат ---
        while (frontier.Count > 0 && placedRooms.Count < maxRooms)
        {
            Vector2Int currentPos = frontier.Dequeue();

            List<Vector2Int> neighbors = GetOccupiedNeighbors(currentPos);
            RoomData roomData = PickRoomForNeighbors(neighbors, currentPos);
            if (roomData == null) continue;

            GameObject roomGO = Instantiate(roomData.roomPrefab, GetWorldPosition(currentPos), Quaternion.identity);
            roomGO.AddComponent<RoomInstance>().roomData = roomData;
            placedRooms[currentPos] = roomGO;

            // Добавляем новые фронтиры
            foreach (Vector2Int dir in directions)
            {
                Vector2Int nextPos = currentPos + dir;
                if (!placedRooms.ContainsKey(nextPos) && IsInBounds(nextPos) && Random.value > 0.3f)
                {
                    frontier.Enqueue(nextPos);
                    placedRooms[nextPos] = null; // Резерв
                    roomPositions.Add(nextPos);
                }
            }
        }

        // --- 4. Поиск финальной комнаты ---
        Vector2Int endPos = FindFarEndRoomPosition();
        if (endPos != Vector2Int.zero)
        {
            Vector2Int neighborDir = GetSingleNeighborDirection(endPos);
            Quaternion rot = GetRotationToDirection(-neighborDir);
            placedRooms[endPos] = Instantiate(endRoomPrefab, GetWorldPosition(endPos), rot);
            Debug.Log($"Финальная комната на {endPos}");
        }

        // --- 5. Заполняем пустоты тупиками ---
        FillEmptySpacesWithDeadEnds();

        Debug.Log($"Итоговое количество комнат: {placedRooms.Count}");
    }

    // --- Вспомогательные функции ---

    Vector3 GetWorldPosition(Vector2Int pos) => new Vector3(pos.x * gridSize.x, 0, pos.y * gridSize.y);

    bool IsInBounds(Vector2Int pos) => pos.x >= -gridSize.x / 2 && pos.x <= gridSize.x / 2 && pos.y >= -gridSize.y / 2 && pos.y <= gridSize.y / 2;

    List<Vector2Int> GetOccupiedNeighbors(Vector2Int pos)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        foreach (var dir in directions)
            if (placedRooms.ContainsKey(pos + dir)) neighbors.Add(dir);
        return neighbors;
    }

    RoomData PickRoomForNeighbors(List<Vector2Int> neighbors, Vector2Int currentPos)
    {
        List<RoomData> candidates = roomTemplates.FindAll(room =>
            (!neighbors.Contains(Vector2Int.up) || room.bottomExit) &&
            (!neighbors.Contains(Vector2Int.down) || room.topExit) &&
            (!neighbors.Contains(Vector2Int.left) || room.rightExit) &&
            (!neighbors.Contains(Vector2Int.right) || room.leftExit)
        );

        if (candidates.Count > 0)
            return candidates[Random.Range(0, candidates.Count)];

        Debug.LogWarning($"Нет подходящей комнаты для {currentPos}");
        return null;
    }

    Vector2Int FindFarEndRoomPosition()
    {
        Vector2Int farthest = Vector2Int.zero;
        float maxDist = 0;

        foreach (Vector2Int pos in placedRooms.Keys)
        {
            if (placedRooms[pos] != null) continue;
            if (GetOccupiedNeighbors(pos).Count == 1)
            {
                float dist = Vector2Int.Distance(startPos, pos);
                if (dist > maxDist)
                {
                    maxDist = dist;
                    farthest = pos;
                }
            }
        }
        return farthest;
    }

    Vector2Int GetSingleNeighborDirection(Vector2Int pos)
    {
        foreach (Vector2Int dir in directions)
            if (placedRooms.ContainsKey(pos + dir)) return dir;
        return Vector2Int.zero;
    }

    Quaternion GetRotationToDirection(Vector2Int dir)
    {
        if (dir == Vector2Int.up) return Quaternion.Euler(0, 0, 0);
        if (dir == Vector2Int.right) return Quaternion.Euler(0, 90, 0);
        if (dir == Vector2Int.down) return Quaternion.Euler(0, 180, 0);
        if (dir == Vector2Int.left) return Quaternion.Euler(0, 270, 0);
        return Quaternion.identity;
    }

    // --- Тупики ---

    void FillEmptySpacesWithDeadEnds()
    {
        List<Vector2Int> newPositions = new List<Vector2Int>(); // Временный список

        foreach (var roomPos in roomPositions)
        {
            GameObject roomObj = placedRooms[roomPos];
            if (roomObj == null) continue;

            RoomInstance roomInstance = roomObj.GetComponent<RoomInstance>();
            if (roomInstance == null || roomInstance.roomData == null) continue;

            RoomData data = roomInstance.roomData;

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighborPos = roomPos + dir;
                if (placedRooms.ContainsKey(neighborPos)) continue;

                bool hasExit = false;
                if (dir == Vector2Int.up && data.topExit) hasExit = true;
                if (dir == Vector2Int.down && data.bottomExit) hasExit = true;
                if (dir == Vector2Int.left && data.leftExit) hasExit = true;
                if (dir == Vector2Int.right && data.rightExit) hasExit = true;

                if (hasExit && IsInBounds(neighborPos))
                {
                    
                }

                PlaceDeadEnd(neighborPos, -dir);
                newPositions.Add(neighborPos); // Добавляем во временный список
            }
        }

        // Добавляем новые позиции после завершения цикла
        roomPositions.AddRange(newPositions);
    }

    void PlaceDeadEnd(Vector2Int position, Vector2Int toDirection)
    {
        RoomData deadEnd = deadEndRooms.Find(room =>
            (toDirection == Vector2Int.up && room.bottomExit) ||
            (toDirection == Vector2Int.down && room.topExit) ||
            (toDirection == Vector2Int.left && room.rightExit) ||
            (toDirection == Vector2Int.right && room.leftExit)
        );

        if (deadEnd != null)
        {
            GameObject room = Instantiate(deadEnd.roomPrefab, GetWorldPosition(position), Quaternion.identity);
            room.AddComponent<RoomInstance>().roomData = deadEnd;
            placedRooms[position] = room;
            Debug.Log($"Добавлен тупик в {position}");
        }
        else
        {
            Debug.LogWarning($"Нет тупика для {position} в направлении {toDirection}");
        }
    }
}
