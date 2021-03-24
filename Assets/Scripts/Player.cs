using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 6;

    public Camera camera;
    public float mouseSense = 2;
    public float limitAngle = 50;

    private Vector3 move;
    private float hMove;
    private float vMove;
    private float rotateX;
    private float rotateY;
    private Vector3 currentRotation;

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterController = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        //Character movement with the keyboard
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");

        move = transform.forward * vMove + transform.right * hMove;
        characterController.Move(speed * Time.deltaTime * move);

        //Character rotatation with the mouse
        rotateX = Input.GetAxis("Mouse X");
        rotateY = Input.GetAxis("Mouse Y");

        characterController.transform.Rotate(0, rotateX * mouseSense, 0);
        camera.transform.Rotate(-rotateY*mouseSense, 0,0);

        currentRotation = camera.transform.localEulerAngles;

        if(currentRotation.x > 180)
        {
            currentRotation.x -= 360;
        }

        currentRotation.x = Mathf.Clamp(currentRotation.x, -limitAngle, limitAngle);
        camera.transform.localRotation = Quaternion.Euler(currentRotation);

        //Raycast to objects to active hightlight and get click collect
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "Collectable")
            {
                Outline outline = hit.transform.gameObject.GetComponent<Outline>();
                if(outline == null)
                {
                    outline = hit.transform.gameObject.AddComponent<Outline>();
                }

                outline.enabled = true;

                if(Input.GetMouseButtonDown(0))
                {
                    GameController.COLLECT.Invoke(hit.transform);
                }
            }
        }

    }
}
