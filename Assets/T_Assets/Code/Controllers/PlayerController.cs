using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool groundedPlayer;

    [Header("Player Movement")]
    [SerializeField] private float playerSpeed = 3000.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float turnSpeed = 4.0f;

    [Header("Camera Settings")]
    [SerializeField] private GameObject playerCamera;
    private float minTurnAngle = -90.0f;
    private float maxTurnAngle = 90.0f;
    private float rotX;
    private float rotY;

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
        MouseAiming2();
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
    void MouseAiming2()
    {
        rotY = Input.GetAxis("Mouse X") * turnSpeed;
        rotX = Input.GetAxis("Mouse Y") * turnSpeed;

        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        transform.Rotate(0, rotY, 0);

        playerCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x - rotX, transform.eulerAngles.y, 0);

    }

    void Movement()
    {
        rb.AddForce(transform.forward * Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime);
        rb.AddForce(transform.right * Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime);

        if (rb.velocity.magnitude > 1)
        {
            rb.velocity.Normalize();
        }

        if (!groundedPlayer)
            return;

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

        GameObject bullet;
        bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);
        bullet.transform.rotation = Quaternion.LookRotation(bulletSpawnPoint.transform.forward, bulletSpawnPoint.transform.up);
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
