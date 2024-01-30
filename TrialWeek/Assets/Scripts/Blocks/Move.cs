using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 direction = new(0, 0, 10.0f);

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = Vector3.zero;
            Moved(direction);
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            rb.velocity = Vector3.zero;
            Moved(-direction);
        }
    }

    private void Moved(Vector3 direction_)
    {
        transform.Translate(direction_ * Time.deltaTime);
    }
}
