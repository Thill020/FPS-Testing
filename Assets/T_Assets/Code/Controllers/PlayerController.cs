using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    private Rigidbody rb;
    private bool groundedPlayer;
    private float rotX;
    [SerializeField] private float playerSpeed = 3000.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float turnSpeed = 4.0f;
    private float minTurnAngle = -90.0f;
    private float maxTurnAngle = 90.0f;

    [Header("Weapon Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseAiming();
        Movement();
        SummonBullet();
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
        rb.AddForce(transform.forward * playerSpeed * Input.GetAxis("Vertical") * Time.deltaTime, ForceMode.Acceleration);
        rb.AddForce(transform.right   * playerSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, ForceMode.Acceleration);

        if (rb.velocity.magnitude > 1)
        {
            rb.velocity.Normalize();
        }

        if (!groundedPlayer)
            return;

        //Changes the height position of the player..
        if (Input.GetAxis("Jump") > 0)
        {
            Debug.Log("Player Jumpped");
            rb.AddForce(0f, jumpHeight, 0f, ForceMode.Impulse);
            groundedPlayer = false;
        }
    }

    void SummonBullet()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0))
            return;

        Instantiate(bulletPrefab, bulletSpawnPoint.transform);

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
