using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // �c�e�\���p��GameObject�擾
    [SerializeField] private GameObject bulletPrefab = null;

    // bulletPrefab
    private List<GameObject> bullets = new List<GameObject>();

    // UI�\�����̍��W��z��Ŏ擾
    [SerializeField] private Vector3[] uiPos = null;

    // �elist�̃C���f�b�N�X
    private int currentIndex = MAX_INDEX;

    // ���Z�b�g�\���ǂ�����\��flag
    bool isResetAble = true;

    // �萔�錾
    const int MAX_INDEX = 7;
    const int EMPTY = -1;

    void Update()
    {
        // �t���O����
        setResetFlag();

        // ��x��uiPos.Length�܂�
        if (Input.GetKey(KeyCode.Space) && isResetAble)
        {
            activeUI(bulletPrefab);
        }

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
        if (bullets.Count == 0)
            isResetAble = true;
        else
            isResetAble = false;
    }

    // UI��\������
    private void activeUI(GameObject obj_)
    {
        // ���X�g�N���A
        bullets.Clear();
        // index������
        currentIndex = MAX_INDEX;

        // �e�I�u�W�F�N�g�w��
        var parent = transform;
        // �q�I�u�W�F�N�g(UI)����
        for (int i = 0; i < uiPos.Length; i++)
        {
            // UI��Instantiate�@���@list�ɑ}�� => �������̂���
            bullets.Add(Instantiate(obj_, uiPos[i], Quaternion.identity, parent));
        }
    }

    // UI���\����
    private void decreaseUI(ref int num_)
    {
        if (num_ != EMPTY)
        {
            bullets[num_].SetActive(false);

            // list����v�f�폜
            bullets.RemoveAt(num_);
            num_--;
        }
    }

}
