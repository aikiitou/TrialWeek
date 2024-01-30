using System.Diagnostics.Tracing;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField]
    GameObject manager = null;
    const int NONE_LINK = 1;
    const float NONE = 0.0f;

    BlockManagerController blockManager = null;
    BlockController linkCotroller = null;
    Rigidbody rigidbody = null;
    int hitCounter = 0;
    int linkCounter = 0;
    int mass = 1;
    float speed = 5.0f;
    bool isGroup = false;

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
        if (IsStop())
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation  //Rotationを全てオン
            | RigidbodyConstraints.FreezePositionZ;  //PositionのYのみオン
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;  //Rotationを全てオン
            Vector3 direction = new(0, 0, speed);
            rigidbody.AddForce(direction);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Bullet")
            {
                hitCounter++;
            }
            else if(collision.gameObject.name == "Block")  //衝突相手がBlockObjectである
            {
                Debug.Log("衝突");
                linkCounter++;
                linkCotroller = collision.gameObject.GetComponent<BlockController>();
                Vector3 collisionPos = collision.transform.position;

                if (linkCotroller.IsGroup)
                {
                    transform.parent = collision.transform.parent;

                }
                else
                {
                    Debug.Log("g");
                    GameObject parent = blockManager.CreateBlockGroup();
                    parent.GetComponent<BlockGroupController>().JoinMember(linkCounter);

                    transform.parent = parent.transform;
                    collision.transform.parent = parent.transform;

                    int add_mas = linkCotroller.Mass;

                    mass += add_mas;
                }
                isGroup = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                hitCounter--;
            }
            else if (collision.gameObject.name == "Block")
            {
                linkCounter--;
                Vector3 collisionPos = collision.transform.position;
                if (collisionPos.y > transform.position.y || collisionPos.z < transform.position.z)
                {
                    int sub_mass = collision.gameObject.GetComponent<BlockController>().Mass;
                    mass -= sub_mass;
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

    private void Move(Vector3 direction_)
    {
        rigidbody.velocity += direction_ * Time.deltaTime;
    }

    bool IsStop()
    {
        return hitCounter >= mass;
    }
}
