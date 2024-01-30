using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // �萔
    
    // �ړ��p
    private const float MOVE_SPEED = 1.0f;
    
    // �W�����v�p
    private const float JUMP_POWER = 7.0f;

    // �o���b�g���˗p
    private const float FIRE_INTERVAL_TIME = 1.0f;
    private const int MAX_BULLET_NUM = 8;
    private const float RELOAD_TIME = 3.0f;


    // �ϐ�

    // �f�o�b�O�p
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

    // �������Z�p
    Rigidbody rigidBody = null;

    // ���͎擾�p
    private bool isInputFront = false;
    private bool isInputBack = false;
    private bool isInputLeft = false;
    private bool isInputRight = false;
    private bool isJump = false;

    // �ړ��p
    private Vector3 moveVector = Vector3.zero;
    private Vector3 normalizedVector = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private float frontDeg = 90;
    private float frontRad = 0;

    // �o���b�g���˗p
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private GameObject firePoint = null;
    private float fireIntervalTimer = 0.0f;
    private int currentBulletNum = 0;
    private bool isReload = false;
    private bool canReload = true;

    // ���C�t�p
    private int currentLife = 0;

    // �N���A����p
    private bool isClear = false;


    // �֐�

    private void keyInput() // �L�[�{�[�h���͎擾
    {
        // �O��̓���
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

        // ���E�̓���
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

        // �W�����v�̓���
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJump && !isReload)
            {
                Debug.Log("�W�����v");
                jump(); // �W�����v
            }

        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            canReload = false;
        }
    }

    private void mouseInput() // �}�E�X�̓��͂̎擾
    {
        fireIntervalTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            // ���ˊԊu�̃`�F�b�N�ƃ����[�h���̃`�F�b�N
            if (fireIntervalTimer > debugFireInterval && !isReload)
            {
                fireIntervalTimer = 0;
                bulletFire(); // �e�̔���(����)
            }
        }
    }

    private void moveVectorSet() // �ړ��p�̃x�N�g���̐ݒ�
    {
        // Z���̈ړ��x�N�g���ݒ�
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

        // X���̃x�N�g���̈ړ��ݒ�
        if(isInputLeft)
        {
            frontDeg += debugMoveAngle * Time.deltaTime;
        }
        else if(isInputRight)
        {
            frontDeg -= debugMoveAngle * Time.deltaTime;
        }

        transform.eulerAngles = new Vector3(0.0f, 90 - frontDeg, 0.0f);

        // �ړ��p�x��x���烉�W�A���ɕϊ�
        frontRad = frontDeg * Mathf.Deg2Rad;


        // �ړ��x�N�g���̐��K��
        normalizedVector = moveVector.normalized;
    }

    private void jump()�@// �W�����v
    {
        isJump = true;
        rigidBody.AddForce(Vector3.up * debugJumpPower, ForceMode.Impulse);
    }

    private void bulletFire() // �e�̔���(����)
    {
        Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
        currentBulletNum--;
    }

    private IEnumerator bulletReload() // �����[�h
    {
        isReload = true;
        yield return new WaitForSeconds(debugReloadtime);
        currentBulletNum = MAX_BULLET_NUM;
        isReload = false;
        canReload = true;
    }

    private void sceneController() // �V�[���J��
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
        keyInput(); // �L�[���͂̎擾
        mouseInput(); // �}�E�X�̓��͂̎擾
        moveVectorSet(); // �ړ��p�̃x�N�g���̐ݒ�


        if (currentBulletNum == 0 || !canReload)
        {
            if (isReload)
            {
                return;
            }
            StartCoroutine(bulletReload()); // �����[�h
        }

        sceneController(); // �V�[���J��
    }
    private void FixedUpdate()
    {
        velocity = normalizedVector * debugSpeed;
        velocity = velocity - rigidBody.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x, -debugSpeed, debugSpeed),0.0f,Mathf.Clamp(velocity.z,-debugSpeed,debugSpeed));

        rigidBody.AddForce(velocity / Time.fixedDeltaTime,ForceMode.Force);
    }
}
