using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPOV : MonoBehaviour
{
    private GameObject fpsBody;
    public float rotY;
    public float rotX;
    public float sensitivity;
    private float horizontalMouse;
    private float verticalMouse;
    // Start is called before the first frame update
    void Start()
    {
        fpsBody = GameObject.Find("First Person Body");
        rotY = gameObject.transform.rotation.y;
        rotX = gameObject.transform.rotation.x;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        //Takes mouse input
        horizontalMouse = Input.GetAxis("Mouse X");
        verticalMouse = Input.GetAxis("Mouse Y");
        
        //calculates camera rotations based on mouse movement
        rotY += horizontalMouse * Time.deltaTime * sensitivity;
        rotX -= verticalMouse * Time.deltaTime * sensitivity;

        if (rotX >=90)
        {
            rotX = 90;
        } else if(rotX <= -90)
        {
            rotX = -90;
        }
        //print(horizontalInput);

        
        //camera rotates
        gameObject.transform.localEulerAngles = new Vector3(rotX, 0, 0);
        fpsBody.transform.localEulerAngles = new Vector3(fpsBody.transform.rotation.x, rotY, fpsBody.transform.rotation.z);
    }
}
