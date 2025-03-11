using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_forward : MonoBehaviour
{
    public TohScreen TohScreen;
    public Joystick Joystick_p;
    public float Speed_Player;
    public bool mobil;
    private float xhR = 0f;
    private float yhR = 0f;

    private Camera Main_Camera;
    void Start()
    {
        Main_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mobil)
        {
            float mouseX = 0;
            float mouseY = 0;

            if (TohScreen.pressed)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.fingerId == TohScreen.finger)
                    {
                        if (touch.phase == TouchPhase.Moved)
                        {
                            mouseX += touch.deltaPosition.x * Time.deltaTime * TohScreen.fingerSpeed;
                            mouseY += touch.deltaPosition.y * Time.deltaTime * TohScreen.fingerSpeed;
                            xhR += mouseX;
                            yhR += mouseY;
                            Main_Camera.transform.localRotation = Quaternion.Euler(-yhR, xhR, 0);
                        }
                        if (touch.phase == TouchPhase.Stationary)
                        {
                            mouseX = 0;
                        }
                    }
                }
            }

            float hor_input = 0f;
            float ver_input = 0f;

            hor_input = Joystick_p.Horizontal;
            ver_input = Joystick_p.Vertical;
            Vector3 moveVektor = (new Vector3(Main_Camera.transform.forward.x, 0f, Main_Camera.transform.forward.z) * ver_input + Main_Camera.transform.right * hor_input) * Time.deltaTime * Speed_Player;
            transform.Translate(moveVektor, Space.World);
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            xhR += mouseX;
            yhR += mouseY;

            Main_Camera.transform.localRotation = Quaternion.Euler(-yhR, xhR, 0);
            Vector3 moveVektor = (new Vector3(Main_Camera.transform.forward.x, 0f, Main_Camera.transform.forward.z) * Input.GetAxis("Vertical") + Main_Camera.transform.right * Input.GetAxis("Horizontal")) * Time.deltaTime * Speed_Player;
            transform.Translate(moveVektor, Space.World);
        }

        
    }
}
