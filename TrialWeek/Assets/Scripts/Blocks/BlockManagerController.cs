using UnityEngine;

public class BlockManagerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateBlockGroup()
    {
        GameObject obj = new GameObject("Blocks");
        obj.AddComponent<BlockGroupController>();
        return obj;
    }
}
