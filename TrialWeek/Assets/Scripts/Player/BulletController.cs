using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // �萔

    private float ALIVE_TIME = 5.0f;
    private float MOVE_SPEED = 3.0f;

    // �ϐ�

    // �f�o�b�O�p
    [SerializeField]
    private float debugAliveTime = 5.0f;

    [SerializeField]
    private float debugMoveSpeed = 3.0f;

    // �������Z�p
    private Rigidbody rigidBody = null;

    // �ړ��p
    [SerializeField]
    private GameObject playerObject = null;
    private Vector3 moveVector = Vector3.zero;
    private Vector3 normalizedVector = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private float frontRad = 0.0f;
    private bool isMove = true;

    // �֐�

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Block" || collision.gameObject.tag == "Bullet")
        {
            isMove = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            isMove = true;
        }
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, debugAliveTime);
        playerObject = GameObject.Find("Player");
        if(playerObject == null )
        {
            Debug.Log("null");
        }
        frontRad = playerObject.GetComponent<PlayerController>().FrontRad;
        Debug.Log("�p�x�F" + (frontRad * Mathf.Rad2Deg).ToString());
    }

    private void FixedUpdate()
    {
        moveVector.x = Mathf.Cos(frontRad);
        moveVector.z = Mathf.Sin(frontRad);

        normalizedVector = moveVector.normalized;

        velocity = normalizedVector * debugMoveSpeed;
        velocity = velocity - rigidBody.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x,velocity.x,debugMoveSpeed), 0.0f, Mathf.Clamp(velocity.z, velocity.z, debugMoveSpeed));

        if(isMove)
        {
            rigidBody.AddForce(velocity / Time.fixedDeltaTime, ForceMode.Force);
        }
        else
        {
            rigidBody.velocity = Vector3.forward * debugMoveSpeed;
        }
    }
}
