using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Object References")]
    //Object references
    private CharacterController characterController;
    public Camera cameraPlayer;
    
    [Header("Options")]
    //Keyboard movement options
    public float speed = 6;

    //Mouse movement options
    public float mouseSense = 2;
    public float limitAngle = 50;

    //Private variables
    private Vector3 move;
    private float hMove;
    private float vMove;
    private float rotateX;
    private float rotateY;
    private Vector3 currentRotation;
    private Transform lastHit; 

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
        
        //Apply gravity to the Character
        if (characterController.isGrounded == false)
        {
            //Add our gravity Vector
            move += Physics.gravity;
        }

        //Move the character
        characterController.Move(speed * Time.deltaTime * move);

        //Character rotatation with the mouse
        rotateX = Input.GetAxis("Mouse X");
        rotateY = Input.GetAxis("Mouse Y");

        characterController.transform.Rotate(0, rotateX * mouseSense, 0);
        cameraPlayer.transform.Rotate(-rotateY*mouseSense, 0,0);

        currentRotation = cameraPlayer.transform.localEulerAngles;

        if(currentRotation.x > 180)
        {
            currentRotation.x -= 360;
        }

        currentRotation.x = Mathf.Clamp(currentRotation.x, -limitAngle, limitAngle);
        cameraPlayer.transform.localRotation = Quaternion.Euler(currentRotation);
        
        //Get mouse button press and check if the lastHit is a Collectable to collect it
        if(Input.GetMouseButtonDown(0))
        {
            if(lastHit.transform.tag == "Collectable")
            {

                GameController.Instance.COLLECT.Invoke(lastHit.transform);

            }
        }
    }

    private void FixedUpdate() {
        //Raycast to objects to active hightlight and get click collect
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            //If raycast hit is not the same do
            if(lastHit != hit.transform)
            {
                GameController.Instance.RAYCAST_PLAYER_HIT.Invoke(hit.transform);


                //Save the last collectable that was triggered
                lastHit = hit.transform;
            }
        }
        
    }
}
