using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DamageZoneController : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    Rigidbody rb;
    Vector3 vector = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vector = transform.forward * speed;
    }

    void FixedUpdate()
    {
        rb.velocity = vector;
    }
}
