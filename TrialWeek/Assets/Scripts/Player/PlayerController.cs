using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �萔
    const float MOVE_SPEED = 1.0f;


    // �ϐ�

    // �f�o�b�O�p
    [SerializeField]
    private float debugSpeed = 1.0f;

    [SerializeField]
    private float debugJumpPower = 1.0f;

    // �������Z�p
    Rigidbody rigidBody = null;

    // ���͎擾�p
    private bool moveFront = false;�@
    private bool moveBack = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool isJump = false;

    // �ړ��p
    private Vector3 moveVector = Vector3.zero;
    private Vector3 normalizedVector = Vector3.zero;
    private Vector3 velocity = Vector3.zero;


    // �֐�

    private void keyInput() // �L�[�{�[�h���͎擾
    {
        // �O��̓���
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            moveFront = false;
            moveBack = false ;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            moveFront = true;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            moveBack = true;
        }
        else
        {
            moveFront = false;
            moveBack = false;
        }

        // ���E�̓���
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            moveLeft = false;
            moveRight = false ;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            moveRight = true;
        }
        else
        {
            moveLeft = false;
            moveRight = false;
        }

        // �W�����v�̓���
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJump)
            {
                Debug.Log("�W�����v");
                jump();
            }

        }
    }

    private void moveVectorSet() // �ړ��p�̃x�N�g���̐ݒ�
    {
        // Z���̈ړ��x�N�g���ݒ�
        if(moveFront)
        {
            moveVector.z = 1.0f;
        }
        else if(moveBack)
        {
            moveVector.z = -1.0f;
        }
        else
        {
            moveVector.z = 0.0f;
        }

        // X���̃x�N�g���̈ړ��ݒ�
        if(moveLeft)
        {
            moveVector.x = -1.0f;
        }
        else if(moveRight)
        {
            moveVector.x = 1.0f;
        }
        else
        {
            moveVector.x = 0.0f;
        }

        // �ړ��x�N�g���̐��K��
        normalizedVector = moveVector.normalized;
    }

    private void jump()
    {
        isJump = true;
        rigidBody.AddForce(Vector3.up * debugJumpPower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJump = false;
        Debug.Log("���n");
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        keyInput(); // �L�[���͂̎擾
        moveVectorSet(); // �ړ��p�̃x�N�g���̐ݒ�
    }
    private void FixedUpdate()
    {
        velocity = normalizedVector * debugSpeed;
        velocity = velocity - rigidBody.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x, -debugSpeed, debugSpeed),0.0f,Mathf.Clamp(velocity.z,-debugSpeed,debugSpeed));

        rigidBody.AddForce(velocity / Time.fixedDeltaTime,ForceMode.Force);
    }
}
