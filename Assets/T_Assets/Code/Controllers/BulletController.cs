using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float bulletSpeed = 10.0f;
    [SerializeField] private float secondTillDestroy = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            return;
        transform.rotation = player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        secondTillDestroy -= Time.deltaTime;

        if (secondTillDestroy < 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Damage Player
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
