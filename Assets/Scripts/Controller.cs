using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float speed = 3;
    float jumpPower = 300;
    float horizontalRotationSpeed = 100;
    float verticalRotationSpeed = 100;
    float horizontalMouseDrag = 1.0f;
    float verticalMouseDrag = 1.0f;

    public GameObject actorCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            DoJump(1);
        }

        if (Input.GetKey("w"))
        {
            MoveForward(1);
        }

        if (Input.GetKey("a"))
        {
            MoveLeft(1);
        }

        if (Input.GetKey("s"))
        {
            MoveBack(1);
        }

        if (Input.GetKey("d"))
        {
            MoveRight(1);
        }

        if (Input.GetKey("left"))
        {
            RotateLeft();
        }

        if (Input.GetKey("right"))
        {
            RotateRight();
        }

        if (Input.GetAxis("Mouse X") != 0)
        {
            //Code for action on mouse moving left/right
            transform.Rotate(0.0f, horizontalRotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X") * horizontalMouseDrag, 0.0f, Space.Self);
        }

        if(actorCamera != null)
        {
            if (Input.GetKey("up"))
            {
                float xAngle = actorCamera.transform.eulerAngles.x;
                if (xAngle < 180 || xAngle > 275)
                {
                    actorCamera.transform.Rotate(-1f * verticalRotationSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
                }
            }

            if (Input.GetKey("down"))
            {
                float xAngle = actorCamera.transform.eulerAngles.x;
                if (xAngle < 85 || xAngle > 180)
                {
                    actorCamera.transform.Rotate(1f * verticalRotationSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
                }
            }

            if (Input.GetAxis("Mouse Y") > 0)
            {
                //Code for action on mouse moving up
                float xAngle = actorCamera.transform.eulerAngles.x;
                if (xAngle < 180 || xAngle > 275)
                {
                    actorCamera.transform.Rotate(Input.GetAxis("Mouse Y") * verticalRotationSpeed * Time.deltaTime * -verticalMouseDrag, 0.0f, 0.0f, Space.Self);
                }
            }

            if (Input.GetAxis("Mouse Y") < 0)
            {
                //Code for action on mouse moving down
                float xAngle = actorCamera.transform.eulerAngles.x;
                if (xAngle < 85 || xAngle > 180)
                {
                    actorCamera.transform.Rotate(Input.GetAxis("Mouse Y") * verticalRotationSpeed * Time.deltaTime * -verticalMouseDrag, 0.0f, 0.0f, Space.Self);
                }
            }
        }
    }

    public void MoveForward(float power)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime * power);
    }

    public void MoveBack(float power)
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime * power);
    }

    public void MoveLeft(float power)
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime * power);
    }

    public void MoveRight(float power)
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime * power);
    }

    public void MoveNorth(float power)
    {
        transform.position += Vector3.forward * speed * Time.deltaTime * power;
    }

    public void MoveSouth(float power)
    {
        transform.position += Vector3.back * speed * Time.deltaTime * power;
    }

    public void MoveEast(float power)
    {
        transform.position += Vector3.left * speed * Time.deltaTime * power;
    }

    public void MoveWest(float power)
    {
        transform.position += Vector3.right * speed * Time.deltaTime * power;
    }

    public void RotateLeft()
    {
        transform.Rotate(0.0f, -1f * horizontalRotationSpeed * Time.deltaTime, 0.0f, Space.Self);
    }

    public void RotateRight()
    {
        transform.Rotate(0.0f, 1f * horizontalRotationSpeed * Time.deltaTime, 0.0f, Space.Self);
    }

    public void DoJump(float power)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower * power);
    }
}
