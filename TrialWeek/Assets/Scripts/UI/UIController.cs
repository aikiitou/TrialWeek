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

    // リセット可能かどうかを表すflag
    private bool isResetAble = true;

    // 定数宣言
    const int MAX_INDEX = 7;    // 要素のインデックス最大値　要素数は8だから
    const int EMPTY = -1;       // 空の時

    void Update()
    {
        // フラグ判定
        setResetFlag();

        // UIを生成する
        // 一度でuiPos.Lengthまで
        if (Input.GetKey(KeyCode.Space) && isResetAble)
        {
            activeUI(bulletPrefab);
        }
        // UIを削除する
        if(Input.GetKeyUp(KeyCode.Return))
        {
            // ref  参照渡しのような機能をもたらす
            decreaseUI(ref currentIndex);
        }
    }

    // フラグ判定メソッド
    private void setResetFlag()
    {
        // listが空のときリセット可能
        if (bulletList.Count == 0)
            isResetAble = true;
        else
            isResetAble = false;
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
    }

}
