using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    private bool grounded = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!grounded)
            return;

        /*transform.Translate(Vector3.up * 3);*/
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            rb.AddForce(0f, 15f, 0f, ForceMode.Impulse);
            grounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (grounded)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }
    }
}
