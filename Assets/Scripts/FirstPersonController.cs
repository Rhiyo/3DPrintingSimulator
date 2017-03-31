using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

    public float movementSpeed = 10;
    public float mouseSensitivity = 5;

    public float verticalRotation = 0;
    public float headTiltRange = 60;

    public float jumpSpeed = 5;

    public float verticalVelocity = 0;

    public float distance = 1f;
    public float smooth = 10;

    CharacterController charController;

    private Interactable interactedObj;
    Collider interactedCol;

    public GameObject carriedObject;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        charController = GetComponent< CharacterController > ();
    }
	
	// Update is called once per frame
	void Update () {

        //View Rotation
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -headTiltRange, headTiltRange);
        
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0,0);


        //Movement
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float strafeSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        if (charController.isGrounded && Input.GetButton("Jump"))
        {
            verticalVelocity = jumpSpeed;
        }else if (!charController.isGrounded)
        {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 speed = new Vector3(strafeSpeed, verticalVelocity, forwardSpeed);

        speed = transform.rotation * speed;

        charController.Move(speed * Time.deltaTime);

        //Message
        int screenX = Screen.width / 2;
        int screenY = Screen.height / 2;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenX, screenY));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && carriedObject == null)
        {
            Interactable hitObj = hit.collider.GetComponent<Interactable>();
            if (hitObj != null)
            {
                if (hit.distance <= hitObj.interactionDistance)
                {
                    interactedObj = hitObj;
                    interactedCol = hit.collider;
                    if (hitObj.GetComponent<ItemScript>() != null)
                        if (Input.GetKeyDown(KeyCode.E))
                            PickupObject();
                }
                else
                    interactedObj = null;
            }
            else
                interactedObj = null;
        }
        else
            interactedObj = null;

        //Carry object
        if (carriedObject != null)
        {
            

            carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position,
                Camera.main.transform.position + Camera.main.transform.forward * distance,
                Time.deltaTime * smooth);
            if (Input.GetKeyUp(KeyCode.E))
            {
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject = null;
            }
        }

    }

    //Getters
    public Interactable GetInteractableObj()
    {
        return interactedObj;
    }

    public void PickupObject()
    {
        if (!interactedCol.gameObject.GetComponent<ItemScript>().locked) {
            carriedObject = interactedCol.gameObject;
            Rigidbody objBody = carriedObject.GetComponent<Rigidbody>();
            objBody.useGravity = false;
        }
    }
}
