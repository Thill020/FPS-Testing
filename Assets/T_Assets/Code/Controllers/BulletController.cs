using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float bulletSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime);

    }
}
