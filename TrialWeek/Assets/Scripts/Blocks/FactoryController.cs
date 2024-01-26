using System.Collections;
using System.Collections.Generic;
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
        
    }

    public GameObject ActiveBlock(bool is_group = false, int memger_num = 1)
    {
        GameObject obj = null;
        if (blocks.Count == NONE)
        {
            if (is_group)
            {
                obj = new GameObject("Blocks");
                obj.AddComponent<BlockGroupController>();
            }
            else
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
            }
            else
            {
                obj = Instantiate(block);
            }
            obj = blocks[0];
            obj.transform.parent = null;
            obj.SetActive(true);
            blocks.Remove(obj);
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