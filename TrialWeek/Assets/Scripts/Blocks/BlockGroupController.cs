using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupController : MonoBehaviour
{
    const int NONE = 0;

    Vector3 direction = new(0, 0, 3.0f);
    int memberNum = 0;
    bool isStop = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            Move();
        }

        //�q�I�u�W�F�N�g�����Ȃ���͍폜����
        if(memberNum == NONE)
        {
            Destroy(gameObject);
        }
    }

    public void SetIsStop(bool is_stop)
    {
        isStop = is_stop;
    }

    private void Move()
    {
        transform.Translate(direction * Time.deltaTime);
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
