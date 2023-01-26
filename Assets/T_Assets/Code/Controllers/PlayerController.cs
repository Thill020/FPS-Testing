using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float rotX;

    [SerializeField] private float playerSpeed = 1500.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float turnSpeed = 4.0f;
    [SerializeField] private float minTurnAngle = -90.0f;
    [SerializeField] private float maxTurnAngle = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseAiming();
        Movement();
    }

    void MouseAiming()
    {
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }

    void Movement()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //Changes the height position of the player..
        if (Input.GetButtonDown("Jump") /*&& groundedPlayer*/)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        Vector3 forward = transform.forward;
        controller.SimpleMove(forward * playerSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        controller.SimpleMove(transform.right * playerSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
        
    }
}
