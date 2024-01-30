using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;

public class FactoryController : MonoBehaviour
{
    [SerializeField]
    GameObject block = null;
    [SerializeField]
    const int WAVE_CREATE_VELUE = 6;
    [SerializeField]
    int MaxBlockNum = 150;

    const float NONE = 0.0f;
    List<GameObject> blocks = null;

    // Start is called before the first frame update
    void Start()
    {
        blocks = new List<GameObject>(MaxBlockNum);

        for (int i = 0; i < MaxBlockNum; i++)
        {
            GameObject obj = Instantiate(block);

            obj.transform.parent = transform;
            obj.SetActive(false);
            obj.name = block.name;

            blocks.Add(obj);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ActiveBlock();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ActiveBlock(true, 2);
        }
    }

    public GameObject ActiveBlock(bool is_group = false, int memger_num = 1)
    {
        GameObject obj = null;
        if (blocks.Count == NONE)//待機状態のオブジェクト配列の要素数が0のとき
        {
            if (is_group)//blockを結合した状態で出現させるとき
            {
                obj = new GameObject("Blocks");
                obj.AddComponent<BlockGroupController>();
                for (int i = 0; i < memger_num; i++)
                {
                    GameObject block = ActiveBlock();
                    block.transform.parent = obj.transform;
                }
            }
            else//blockを単体で出現させるとき
            {
                obj = Instantiate(block);
            }
        }
        else
        {
            if (is_group)
            {
                obj = new GameObject("Blocks");
                obj.AddComponent<BlockGroupController>();
                for (int i = 0; i < memger_num; i++)
                {
                    GameObject block = ActiveBlock();
                    block.transform.parent = obj.transform;                    
                }
            }
            else
            {
                obj = Instantiate(block);
                obj = blocks[0];
                obj.transform.parent = null;
                obj.SetActive(true);
                blocks.Remove(obj);
            }
        }
        return obj;
    }

    public void StoreBlock(GameObject obj)
    {
        if (obj.name != block.name)
            return;

        if (blocks.Count >= MaxBlockNum)
        {
            MaxBlockNum++;
        }

        obj.transform.parent = transform;
        obj.SetActive(false);
        obj.name = block.name;

        blocks.Add(obj);
    }
}