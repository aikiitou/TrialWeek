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

        //子オブジェクトがいなけれは削除する
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

    //子オブジェクトが増えた時に呼び出される
    public void JoinMember(int add_member_value)
    {
        memberNum += add_member_value;
    }

    //子オブジェクトが増えた時に呼び出される
    public void BreakAwayMember(int sub_member_value)
    {
        memberNum -= sub_member_value;
    }
}
