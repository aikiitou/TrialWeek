using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 定数
    const float MOVE_SPEED = 1.0f;


    // 変数

    // デバッグ用
    [SerializeField]
    private float debugSpeed = 1.0f;

    [SerializeField]
    private float debugJumpPower = 1.0f;

    // 物理演算用
    Rigidbody rigidBody = null;

    // 入力取得用
    private bool moveFront = false;　
    private bool moveBack = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool isJump = false;

    // 移動用
    private Vector3 moveVector = Vector3.zero;
    private Vector3 normalizedVector = Vector3.zero;
    private Vector3 velocity = Vector3.zero;


    // 関数

    private void keyInput() // キーボード入力取得
    {
        // 前後の入力
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

        // 左右の入力
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

        // ジャンプの入力
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJump)
            {
                Debug.Log("ジャンプ");
                jump();
            }

        }
    }

    private void moveVectorSet() // 移動用のベクトルの設定
    {
        // Z軸の移動ベクトル設定
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

        // X軸のベクトルの移動設定
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

        // 移動ベクトルの正規化
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
        Debug.Log("着地");
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        keyInput(); // キー入力の取得
        moveVectorSet(); // 移動用のベクトルの設定
    }
    private void FixedUpdate()
    {
        velocity = normalizedVector * debugSpeed;
        velocity = velocity - rigidBody.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x, -debugSpeed, debugSpeed),0.0f,Mathf.Clamp(velocity.z,-debugSpeed,debugSpeed));

        rigidBody.AddForce(velocity / Time.fixedDeltaTime,ForceMode.Force);
    }
}
