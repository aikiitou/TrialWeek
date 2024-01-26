using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupController : MonoBehaviour
{
    const int NONE = 0;

    Vector3 direction = new(0, 0, 3.0f);
    int memberNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Group�ړ�");
            Move(direction);
        }


        //�q�I�u�W�F�N�g�����Ȃ���͍폜����
        if(memberNum == NONE)
        {
            Destroy(gameObject);
        }
    }

    private void Move(Vector3 direction_)
    {
        transform.Translate(direction_ * Time.deltaTime);
    }

    //�q�I�u�W�F�N�g�����������ɌĂяo�����
    public void JoinMember(int add_member_value)
    {
        memberNum += add_member_value;
    }

    //�q�I�u�W�F�N�g�����������ɌĂяo�����
    public void BreakAwayMember(int sub_member_value)
    {
        memberNum -= sub_member_value;
    }
}
