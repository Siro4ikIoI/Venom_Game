using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_forward : MonoBehaviour
{
    public TohScreen TohScreen;
    public Joystick Joystick_p;
    public float Speed_Player;
    public bool mobil;

    public Slider stamina_sl;
    public float max_stamina = 10;

    private float xhR = 0f;
    private float yhR = 0f;



    private Camera Main_Camera;
    private Rigidbody rigidbody_pl;
    private CharacterController characterController;
    private float timer = 0;
    private bool isCrouching = false;
    private float standart_pl_y;
    private float standart_pl_y_half;
    private float standart_speed;
    private float standart_camera_y;
    private float standart_camera_x;
    void Start()
    {
        standart_speed = Speed_Player;
        Main_Camera = Camera.main;
        standart_camera_y = Main_Camera.transform.position.y;
        standart_camera_x = Main_Camera.transform.position.x;
        characterController = GetComponent<CharacterController>();
        standart_pl_y = characterController.height;
        standart_pl_y_half = characterController.height / 2;
        stamina_sl.maxValue = max_stamina;
        //rigidbody_pl = GetComponent<Rigidbody>();

        if (!mobil)
        {
            TohScreen.gameObject.SetActive(false);
            Joystick_p.gameObject.SetActive(false);
        }
        else
        {
            TohScreen.gameObject.SetActive(true);
            Joystick_p.gameObject.SetActive(true);
        }
    }

    public void Sit_down(bool ch)
    {
        isCrouching = !isCrouching;
        if (ch)
            characterController.height = standart_pl_y;
            //transform.localScale = new Vector3(transform.localScale.x, standart_pl_y, transform.localScale.z);
        else
            characterController.height = standart_pl_y_half;
        //transform.localScale = new Vector3(transform.localScale.x, standart_pl_y_half, transform.localScale.z);
    }

    public void Sit_down_MOBIl()
    {
        isCrouching = !isCrouching;
        if (isCrouching)
            transform.localScale = new Vector3(transform.localScale.x, standart_pl_y, transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x, standart_pl_y_half, transform.localScale.z);
    }

    public void Run_Vasy_run()
    {
        Speed_Player = standart_speed * 2;
        max_stamina -= Time.deltaTime;
        stamina_sl.value = max_stamina;
    }

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
                            yhR = Mathf.Clamp(yhR, -90, 87);
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
            characterController.Move(transform.TransformVector(moveVektor));
            //transform.Translate(moveVektor, Space.World);
            //rigidbody_pl.AddForce(moveVektor);
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            xhR += mouseX;
            yhR += mouseY;
            yhR = Mathf.Clamp(yhR, -90, 87);

            Main_Camera.transform.localRotation = Quaternion.Euler(-yhR, xhR, 0);
            Vector3 moveVektor = (new Vector3(Main_Camera.transform.forward.x, 0f, Main_Camera.transform.forward.z) * Input.GetAxis("Vertical") + Main_Camera.transform.right * Input.GetAxis("Horizontal")) * Time.deltaTime * Speed_Player;
            //transform.Translate(moveVektor, Space.World);
            //rigidbody_pl.AddForce(moveVektor*50);
            //rigidbody_pl.MovePosition(transform.position + transform.TransformVector(moveVektor*2));
            characterController.Move(transform.TransformVector(moveVektor));

            if (Input.GetKey(KeyCode.LeftControl))
            {
                Sit_down(false);
            }
            else
            {
                Sit_down(true);
            }

            if (Input.GetKey(KeyCode.LeftShift) && max_stamina > 1)
            {
                Run_Vasy_run();
            }
            else
            {
                Speed_Player = standart_speed;
                if (stamina_sl.maxValue >= max_stamina)
                {
                    max_stamina += Time.deltaTime * 0.5f;
                    stamina_sl.value = max_stamina;
                }

            }
        }

        //if (!characterController.isGrounded) {
        //    characterController.Move(new Vector3(0f, -6f, 0f));
        //}
        
        // -----------Качание камеры------------------

        if (characterController.velocity.magnitude >= 0.1f)
        {
            print("555");
            print(characterController.velocity.magnitude);
            timer += Time.deltaTime * 5;
            Main_Camera.transform.localPosition = new Vector3(standart_camera_x + Mathf.Sin(timer * 1.2f) * 0.25f / 2, standart_camera_y + Mathf.Sin(timer*2.4f) * 0.2f / 2, Main_Camera.transform.localPosition.z);
            //Main_Camera.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Cos(timer * 1.2f) * 1f);
        }
        else
        {
            timer = 0;
            Main_Camera.transform.localPosition = new Vector3(Mathf.Lerp(Main_Camera.transform.localPosition.x, standart_camera_x, 0.1f), Mathf.Lerp(Main_Camera.transform.localPosition.y, standart_camera_y, 0.1f), Main_Camera.transform.localPosition.z);
        }
    }

    private void FixedUpdate()
    {
        if (!characterController.isGrounded) {
            characterController.Move(new Vector3(0f, -6f, 0f));
        }
    }
}
