using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // 定数
    
    // 移動用
    private const float MOVE_SPEED = 1.0f;
    
    // ジャンプ用
    private const float JUMP_POWER = 7.0f;

    // バレット発射用
    private const float FIRE_INTERVAL_TIME = 1.0f;
    private const int MAX_BULLET_NUM = 8;
    private const float RELOAD_TIME = 3.0f;


    // 変数

    // デバッグ用
    [SerializeField]
    private float debugSpeed = 1.0f;

    [SerializeField]
    private float debugJumpPower = 7.0f;

    [SerializeField]
    private float debugFireInterval = 1.0f;

    [SerializeField]
    private float debugReloadtime = 3.0f;

    [SerializeField]
    private float debugMoveAngle = 30.0f;

    [SerializeField]
    private int debugLife = 5;

    // 物理演算用
    Rigidbody rigidBody = null;

    // 入力取得用
    private bool isInputFront = false;
    private bool isInputBack = false;
    private bool isInputLeft = false;
    private bool isInputRight = false;
    private bool isJump = false;

    // 移動用
    private Vector3 moveVector = Vector3.zero;
    private Vector3 normalizedVector = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private float frontDeg = 90;
    private float frontRad = 0;

    // バレット発射用
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private GameObject firePoint = null;
    private float fireIntervalTimer = 0.0f;
    private int currentBulletNum = 0;
    private bool isReload = false;
    private bool canReload = true;

    // ライフ用
    private int currentLife = 0;

    // クリア判定用
    private bool isClear = false;


    // 関数

    private void keyInput() // キーボード入力取得
    {
        // 前後の入力
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            isInputFront = false;
            isInputBack = false ;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            isInputFront = true;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            isInputBack = true;
        }
        else
        {
            isInputFront = false;
            isInputBack = false;
        }

        // 左右の入力
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            isInputLeft = false;
            isInputRight = false ;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            isInputLeft = true;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            isInputRight = true;
        }
        else
        {
            isInputLeft = false;
            isInputRight = false;
        }

        // ジャンプの入力
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJump && !isReload)
            {
                Debug.Log("ジャンプ");
                jump(); // ジャンプ
            }

        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            canReload = false;
        }
    }

    private void mouseInput() // マウスの入力の取得
    {
        fireIntervalTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            // 発射間隔のチェックとリロード中のチェック
            if (fireIntervalTimer > debugFireInterval && !isReload)
            {
                fireIntervalTimer = 0;
                bulletFire(); // 弾の発射(生成)
            }
        }
    }

    private void moveVectorSet() // 移動用のベクトルの設定
    {
        // Z軸の移動ベクトル設定
        if(isInputFront)
        {
            moveVector.x = Mathf.Cos(FrontRad);
            moveVector.z = Mathf.Sin(FrontRad);
        }
        else if(isInputBack)
        {
            moveVector.x = Mathf.Cos(FrontRad) * -1;
            moveVector.z = Mathf.Sin(FrontRad) * -1;

        }
        else
        {
            moveVector = Vector3.zero;
        }

        // X軸のベクトルの移動設定
        if(isInputLeft)
        {
            frontDeg += debugMoveAngle * Time.deltaTime;
        }
        else if(isInputRight)
        {
            frontDeg -= debugMoveAngle * Time.deltaTime;
        }

        transform.eulerAngles = new Vector3(0.0f, 90 - frontDeg, 0.0f);

        // 移動角度を度からラジアンに変換
        frontRad = frontDeg * Mathf.Deg2Rad;


        // 移動ベクトルの正規化
        normalizedVector = moveVector.normalized;
    }

    private void jump()　// ジャンプ
    {
        isJump = true;
        rigidBody.AddForce(Vector3.up * debugJumpPower, ForceMode.Impulse);
    }

    private void bulletFire() // 弾の発射(生成)
    {
        Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
        currentBulletNum--;
    }

    private IEnumerator bulletReload() // リロード
    {
        isReload = true;
        yield return new WaitForSeconds(debugReloadtime);
        currentBulletNum = MAX_BULLET_NUM;
        isReload = false;
        canReload = true;
    }

    private void sceneController() // シーン遷移
    {
        if(currentLife == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }

        if(isClear)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }

    public int CurrentBulletNum { get => currentBulletNum; }
    public float FrontRad { get => frontRad;}

    public int CurrentLife {  get => currentLife; }

    private void OnCollisionEnter(Collision collision)
    {
        isJump = false;

        if (collision.gameObject.tag == "DeadZone")
        {
            currentLife--;
        }

        if(collision.gameObject.tag == "Finish")
        {
            isClear = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        fireIntervalTimer = debugFireInterval;
        currentBulletNum = MAX_BULLET_NUM;
        currentLife = debugLife;
    }
    private void Update()
    {
        keyInput(); // キー入力の取得
        mouseInput(); // マウスの入力の取得
        moveVectorSet(); // 移動用のベクトルの設定


        if (currentBulletNum == 0 || !canReload)
        {
            if (isReload)
            {
                return;
            }
            StartCoroutine(bulletReload()); // リロード
        }

        sceneController(); // シーン遷移
    }
    private void FixedUpdate()
    {
        velocity = normalizedVector * debugSpeed;
        velocity = velocity - rigidBody.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x, -debugSpeed, debugSpeed),0.0f,Mathf.Clamp(velocity.z,-debugSpeed,debugSpeed));

        rigidBody.AddForce(velocity / Time.fixedDeltaTime,ForceMode.Force);
    }
}
