using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // �c�e�\���p��GameObject�擾
    [SerializeField] private GameObject bulletPrefab = null;

    // bulletPrefabList     �C���f�b�N�X�w��p
    private List<GameObject> bulletList = new List<GameObject>();

    // UI�\�����̍��W��z��Ŏ擾
    [SerializeField] private Vector3[] uiPos = null;

    // �elist�̃C���f�b�N�X
    private int currentIndex = MAX_INDEX;

    // ���Z�b�g�\���ǂ�����\��flag
    private bool isResetAble = true;

    // �萔�錾
    const int MAX_INDEX = 7;    // �v�f�̃C���f�b�N�X�ő�l�@�v�f����8������
    const int EMPTY = -1;       // ��̎�

    void Update()
    {
        // �t���O����
        setResetFlag();

        // UI�𐶐�����
        // ��x��uiPos.Length�܂�
        if (Input.GetKey(KeyCode.Space) && isResetAble)
        {
            activeUI(bulletPrefab);
        }
        // UI���폜����
        if(Input.GetKeyUp(KeyCode.Return))
        {
            // ref  �Q�Ɠn���̂悤�ȋ@�\�������炷
            decreaseUI(ref currentIndex);
        }
    }

    // �t���O���胁�\�b�h
    private void setResetFlag()
    {
        // list����̂Ƃ����Z�b�g�\
        if (bulletList.Count == 0)
            isResetAble = true;
        else
            isResetAble = false;
    }

    // UI�𐶐�����
    private void activeUI(GameObject obj_)
    {
        // ���X�g�N���A
        bulletList.Clear();
        // index������
        currentIndex = MAX_INDEX;

        // �q�I�u�W�F�N�g(UI)����
        for (int i = 0; i < uiPos.Length; i++)
        {
            // UI��Instantiate�@���@list�ɑ}�� => ��������index�擾�̂���
            bulletList.Add(Instantiate(obj_, uiPos[i], Quaternion.identity, this.transform));  // transform�͐e�w��̂���
        }
    }

    // UI���폜
    private void decreaseUI(ref int num_)
    {
        // index != -1 �̂Ƃ�
        if (num_ != EMPTY)
        {
            // �폜
            Destroy(bulletList[num_]);

            // list����v�f�폜
            bulletList.RemoveAt(num_);
            num_--;
        }
    }

}
