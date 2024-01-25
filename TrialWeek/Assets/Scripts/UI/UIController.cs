using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // 残弾表示用のGameObject取得
    [SerializeField] private GameObject bulletPrefab = null;

    // bulletPrefab
    private List<GameObject> bullets = new List<GameObject>();

    // UI表示時の座標を配列で取得
    [SerializeField] private Vector3[] uiPos = null;

    // 弾listのインデックス
    private int currentIndex = MAX_INDEX;

    // リセット可能かどうかを表すflag
    bool isResetAble = true;

    // 定数宣言
    const int MAX_INDEX = 7;
    const int EMPTY = -1;

    void Update()
    {
        // フラグ判定
        setResetFlag();

        // 一度でuiPos.Lengthまで
        if (Input.GetKey(KeyCode.Space) && isResetAble)
        {
            activeUI(bulletPrefab);
        }

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
        if (bullets.Count == 0)
            isResetAble = true;
        else
            isResetAble = false;
    }

    // UIを表示する
    private void activeUI(GameObject obj_)
    {
        // リストクリア
        bullets.Clear();
        // index初期化
        currentIndex = MAX_INDEX;

        // 親オブジェクト指定
        var parent = transform;
        // 子オブジェクト(UI)生成
        for (int i = 0; i < uiPos.Length; i++)
        {
            // UIをInstantiate　且つ　listに挿入 => 減少時のため
            bullets.Add(Instantiate(obj_, uiPos[i], Quaternion.identity, parent));
        }
    }

    // UIを非表示に
    private void decreaseUI(ref int num_)
    {
        if (num_ != EMPTY)
        {
            bullets[num_].SetActive(false);

            // listから要素削除
            bullets.RemoveAt(num_);
            num_--;
        }
    }

}
