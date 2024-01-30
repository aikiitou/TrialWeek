
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField]
    GameObject manager = null;
    const int NONE_LINK = 1;
    const float NONE = 0.0f;

    BlockManagerController blockManager = null;
    BlockController linkController = null;
    Rigidbody rigidbody = null;
    int hitCounter = 0;
    int linkCounter = 0;
    int mass = 1;
    float speed = 3.0f;
    float speed = 5.0f;
    bool isGroup = false;
    int counter = 0;

    public int Mass { get => mass; }
    public bool IsGroup { get => isGroup; }

    // Start is called before the first frame update
    void Start()
    {
        blockManager = manager.GetComponent<BlockManagerController>();
        rigidbody = GetComponent<Rigidbody>();
        Vector3 direction = new(0, 0, speed);
        rigidbody.AddForce(direction);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(IsStop());
        if (!IsStop())
        {
            rigidbody.isKinematic = false;
            if (transform.parent == null)
            if(transform.parent == null)
            {
                Move();
            }
            counter = 0;
        }
        else
        {
            if (counter == 0)
            if(counter == 0)
            {
                //rigidbody.isKinematic = false;
                Vector3 direction = new(0, 0, speed);
                rigidbody.AddForce(direction);
            }
            counter++;
            if (IsStop())
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation  //Rotation��S�ăI��
                | RigidbodyConstraints.FreezePositionZ;  //Position��Y�̂݃I��
            }
            else
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;  //Rotation��S�ăI��
                Vector3 direction = new(0, 0, speed);
                rigidbody.AddForce(direction);
            }
        if (IsStop())
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation  //Rotation��S�ăI��
            | RigidbodyConstraints.FreezePositionZ;  //Position��Y�̂݃I��
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;  //Rotation��S�ăI��
            Vector3 direction = new(0, 0, speed);
            rigidbody.AddForce(direction);
        }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                hitCounter++;
                rigidbody.isKinematic = true;
                if (transform.parent != null)
                {
                    Debug.Log("Enter");
                    Debug.Log(hitCounter);
                    Debug.Log(mass);
                    Debug.Log(IsStop());
                    GameObject parent = transform.parent.gameObject;
                    parent.GetComponent<BlockGroupController>().SetIsStop(IsStop());
                }
            }
            else if (collision.gameObject.tag == "Block")  //�Փˑ��肪BlockObject�ł���
            {

            else if(collision.gameObject.tag == "Block")  //�Փˑ��肪BlockObject�ł���
            {
                Debug.Log("�Փ�");
                linkCounter++;
                linkController = collision.gameObject.GetComponent<BlockController>();
            else if(collision.gameObject.name == "Block")  //�Փˑ��肪BlockObject�ł���
            {
                Debug.Log("�Փ�");
                linkCounter++;
                linkController = collision.gameObject.GetComponent<BlockController>();
            }
            else if (collision.gameObject.name == "Block")  //�Փˑ��肪BlockObject�ł���
            {
                Debug.Log("�Փ�");
                linkCounter++;
                linkController = collision.gameObject.GetComponent<BlockController>();
                Vector3 collisionPos = collision.transform.position;

                if (linkController.IsGroup)
                {
                    transform.parent = collision.transform.parent;
                    GameObject parent = transform.parent.gameObject;
                    parent.GetComponent<BlockGroupController>().SetIsStop(linkController.IsStop());
                }
                else
                {

                }
                else
                {
                    Debug.Log("g");
                    GameObject parent = blockManager.CreateBlockGroup();
                    parent.GetComponent<BlockGroupController>().JoinMember(linkCounter);
                    transform.parent = parent.transform;
                    collision.transform.parent = parent.transform;

                    int add_mas = linkController.Mass;

                }
                mass++;
                isGroup = true;
            }
            else if (collision.gameObject.tag == "DeadZone")
            {
                Destroy(gameObject);
                    int add_mas = linkCotroller.Mass;

                }
                mass++;
                isGroup = true;
            }
            else if (collision.gameObject.tag == "DeadZone")
            {
                Destroy(gameObject);
                int add_mas = linkController.Mass;

                mass += add_mas;
            }
                isGroup = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                hitCounter--;
                if (transform.parent != null)
                {
                    Debug.Log("Exit");
                    Debug.Log(mass);
                    Debug.Log(hitCounter);
                    Debug.Log(IsStop());
                    GameObject parent = transform.parent.gameObject;
                    parent.GetComponent<BlockGroupController>().SetIsStop(IsStop());
                }
            }
            else if (collision.gameObject.name == "Block")
            {
                linkCounter--;
                mass--;
                Vector3 collisionPos = collision.transform.position;
                if (collisionPos.y > transform.position.y || collisionPos.z < transform.position.z)
                {
                    int sub_mass = collision.gameObject.GetComponent<BlockController>().Mass;
                }
                if (collision.transform.parent != null)
                {
                    GameObject parent = collision.transform.parent.gameObject;
                    parent.GetComponent<BlockGroupController>().BreakAwayMember(linkCounter + 1);
                }
                if (linkCounter == NONE)
                {
                    isGroup = false;
                    transform.parent = null;
                }
            }
        }
    }

    private void Move()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    public bool IsStop()
    {
        return hitCounter >= mass;

        
    }
}