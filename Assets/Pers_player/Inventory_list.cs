using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_list : MonoBehaviour
{
    public static Inventory_list instante { get; set; }
    [Header("Инвентарь")]
    public Transform PL_place_obg;
    public List<Object_haracter> list_inventory = new List<Object_haracter>();
    public List<GameObject> list_Button = new List<GameObject>();


    private Camera Main_Camera;
    public int indx_now_obj = 4;


    // Создать в редакторе в списке пустой список из null
    void Start()
    {
        instante = this;
        Main_Camera = Camera.main;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray main_center = Main_Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(main_center, out hit))
            {
                if (hit.transform.GetComponent<Object_haracter>() != null)
                    Add_list(hit.transform.GetComponent<Object_haracter>());
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Drop_obj();
        }

        if (Input.mouseScrollDelta.y >= 0) {
            
        }
    }


    public void Add_list(Object_haracter object_game)
    {
        for (int i = 0; i < 4; i++)
        {
            if (list_inventory[i] == null)
            {
                object_game.transform.rotation = object_game.savedRotation;
                list_inventory[i] = object_game;
                object_game.gameObject.SetActive(false);
                list_inventory[i].transform.SetParent(PL_place_obg);
                Click_obj(list_inventory.IndexOf(object_game));
                break;
            }
        }

        Update_list_inventory();
    }

    public void Update_list_inventory()
    {
        for (int i = 0; i < list_inventory.Count; i++)
        {
            if (list_inventory[i] != null)
            {
                list_Button[i].gameObject.transform.Find("Image").gameObject.GetComponent<Image>().sprite = list_inventory[i].GetComponent<Object_haracter>().sprite_obj; 
            }

        }
    }

    public void Click_obj(int indx)
    {
        if (list_inventory[indx] != null)
        {
            list_inventory[indx].gameObject.SetActive(true);
            list_inventory[indx].transform.position = PL_place_obg.position;

            list_inventory[indx].GetComponent<Rigidbody>().isKinematic = true;
            indx_now_obj = indx;
        }
    }

    public void Drop_obj()
    {
        if (list_inventory[indx_now_obj] != null)
        {
            list_inventory[indx_now_obj].transform.SetParent(null);
            list_inventory[indx_now_obj].GetComponent<Rigidbody>().isKinematic = false;
            list_Button[indx_now_obj].gameObject.transform.Find("Image").GetComponent<Image>().sprite = null;
            list_inventory[indx_now_obj] = null;

            Update_list_inventory();
        }

    }
}
