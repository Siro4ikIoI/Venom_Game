using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerForward : MonoBehaviour
{
    [Header("References")]
    public TohScreen tohScreen;
    public Joystick joystick;
    private Camera mainCamera;
    private Rigidbody playerRigidbody;
    private Animator playerAnim;
    public Animator playerAnimCanvos;
    public GameObject deathPanel;
    public RuntimeAnimatorController playerController;



    [Header("Settings")]
    public float playerSpeed = 5f;
    public bool isMobile;

    private float rotationX = 0f;
    private float rotationY = 0f;

    public bool _isDeath = false;

    void Start()
    {
        mainCamera = Camera.main;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isMobile)
        {
            HandleMobileInput();
        }
        else
        {
            HandlePCInput();
        }

        if (_isDeath) PlayerDeath();
    }

    private void HandleMobileInput()
    {
        float mouseX = 0;
        float mouseY = 0;

        if (tohScreen.pressed)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == tohScreen.finger)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        mouseX += touch.deltaPosition.x * Time.deltaTime * tohScreen.fingerSpeed;
                        mouseY += touch.deltaPosition.y * Time.deltaTime * tohScreen.fingerSpeed;
                        rotationX += mouseX;
                        rotationY += mouseY;
                        mainCamera.transform.localRotation = Quaternion.Euler(-rotationY, rotationX, 0);
                    }
                }
            }
        }

        Vector3 moveVector = CalculateMovement(joystick.Horizontal, joystick.Vertical);
        playerRigidbody.AddForce(moveVector);
    }

    private void HandlePCInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX += mouseX;
        rotationY += mouseY;
        mainCamera.transform.localRotation = Quaternion.Euler(-rotationY, rotationX, 0);

        Vector3 moveVector = CalculateMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(moveVector, Space.World);
    }

    private Vector3 CalculateMovement(float horizontal, float vertical)
    {
        Vector3 forwardMovement = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z) * vertical;
        Vector3 rightMovement = mainCamera.transform.right * horizontal;
        return (forwardMovement + rightMovement) * Time.deltaTime * playerSpeed;
    }

    public void PlayerDeath()
    {
        mainCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameObject.AddComponent<Animator>();
        playerAnim = GetComponent<Animator>();
        playerAnim.runtimeAnimatorController = playerController;
        deathPanel.SetActive(true);
        playerAnim.SetBool("deaeth", _isDeath);
        playerAnimCanvos.SetBool("death", _isDeath);
        _isDeath = false;
    }
}