using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 direction = new(0, 0, 3.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Moved(direction);
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            Moved(-direction);
        }
    }

    private void Moved(Vector3 direction_)
    {
        transform.Translate(direction_ * Time.deltaTime);
    }
}
