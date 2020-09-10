using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour {

    public float speed = 20f;
    private PlayerMotor motor;
    public float lookSensitivity = 5f;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        // Calculate movment velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        // x vector
        Vector3 moveHorizontal = transform.right * xMov;

        // z vector
        Vector3 moveVertical = transform.forward * zMov;

        // Final movement vector
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        // Apply momvent
        motor.Move(velocity);

        //Calculate rotation velocity as a 3D vector (turning around)
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        //Aplly rotation
        motor.Rotate(rotation);


        //Calculate rotation velocity as a 3D vector (turning around)
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        //Aplly Camera rotation
        motor.RotateCamera(cameraRotation);

    }

}
