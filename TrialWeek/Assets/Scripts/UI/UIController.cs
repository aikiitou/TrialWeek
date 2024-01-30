using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // 残弾表示用のGameObject取得
    [SerializeField] private GameObject bulletPrefab = null;

    // bulletPrefabList     インデックス指定用
    private List<GameObject> bulletList = new List<GameObject>();

    // UI表示時の座標を配列で取得
    [SerializeField] private Vector3[] uiPos = null;

    // 弾listのインデックス
    private int currentIndex = MAX_INDEX;

    // 定数宣言
    const int MAX_INDEX = 7;    // 要素のインデックス最大値　要素数は8だから
    const int EMPTY = -1;       // 空の時

    // 取得用変数
    [SerializeField] private GameObject player = null;
    private PlayerController playerController = null;

    // playerの現在の残弾
    private int currentBullet = 0;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        currentBullet = playerController.CurrentBulletNum;
    }

    void Update()
    {
        // リロード時に残弾数更新
        if (playerController.IsReload)
        {
            currentBullet = playerController.CurrentBulletNum;
        }
        // UIを生成する
        // 保存した残弾数より現在の残弾数が違うとき
        if (playerController.CurrentBulletNum > currentBullet)
        {
            // 補充
            activeUI(bulletPrefab);
        }
        if (playerController.CurrentBulletNum < currentBullet)
        {
            // ref  参照渡しのような機能をもたらす
            // 減少
            decreaseUI(ref currentIndex);
        }
    }

    // UIを生成する
    private void activeUI(GameObject obj_)
    {
        // リストクリア
        bulletList.Clear();
        // index初期化
        currentIndex = MAX_INDEX;

        // 子オブジェクト(UI)生成
        for (int i = 0; i < uiPos.Length; i++)
        {
            // UIをInstantiate　且つ　listに挿入 => 減少時のindex取得のため
            bulletList.Add(Instantiate(obj_, uiPos[i], Quaternion.identity, this.transform));  // transformは親指定のため
        }
        currentBullet = playerController.CurrentBulletNum;
    }

    // UIを削除
    private void decreaseUI(ref int num_)
    {
        // index != -1 のとき
        if (num_ != EMPTY)
        {
            // 削除
            Destroy(bulletList[num_]);

            // listから要素削除
            bulletList.RemoveAt(num_);
            num_--;
        }
        currentBullet = playerController.CurrentBulletNum;
    }

}
