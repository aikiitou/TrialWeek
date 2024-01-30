using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFactory : MonoBehaviour
{
    [SerializeField]
    GameObject[] spornes = null;
    [SerializeField]
    GameObject block = null;

    float timer = 0;
    [SerializeField]
    float sponeTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = sponeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > sponeTime) 
        {
            for(int i = 0;i < spornes.Length; i++)
            {
                Vector3 pos = spornes[i].transform.position;

                Instantiate(block,pos,Quaternion.identity);
            }
            timer = 0;
        }
    }
}
