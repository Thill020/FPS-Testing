using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool groundedPlayer;
    private float rotX;

    [SerializeField] private float playerSpeed = 3000.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float turnSpeed = 4.0f;
    [SerializeField] private float minTurnAngle = -90.0f;
    [SerializeField] private float maxTurnAngle = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
/*        controller = gameObject.AddComponent<CharacterController>();
*/        rb = gameObject.GetComponent<Rigidbody>();
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
        rb.AddForce(transform.forward * playerSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        rb.AddForce(transform.right * playerSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

        if (!groundedPlayer)
            return;

        //Changes the height position of the player..
        if (Input.GetAxis("Jump") > 0)
        {
            Debug.Log("Player Jumpped");
            rb.AddForce(0f, jumpHeight, 0f, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (groundedPlayer)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundedPlayer = true;
        }
    }
}
