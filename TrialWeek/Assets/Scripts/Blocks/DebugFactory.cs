using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFactory : MonoBehaviour
{
    [SerializeField]
    GameObject[] spornes = null;
    [SerializeField]
    GameObject block = null;

    float timer_01 = 0;
    float timer_02 = 0;
    float sponeTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer_01 += Time.deltaTime;
        timer_02 += Time.deltaTime;
        if(timer_01 > sponeTime) 
        {
            for(int i = 0;i < spornes.Length; i++)
            {
                Vector3 pos = spornes[i].transform.position;

                Instantiate(block,pos,Quaternion.identity);
            }
            timer_01 = 0;
        }
        if(timer_02 > 15.0f)
        {
            Destroy(gameObject);
        }
    }
}
