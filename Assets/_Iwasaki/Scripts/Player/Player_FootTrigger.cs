using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FootTrigger : MonoBehaviour
{
    // �ڒn����
    public bool OnGround = false;

    private const int cap = 4;

    // �g���K�[�ɐڐG���Ă���I�u�W�F�N�g�̔z��
    private List<GameObject> objs = new List<GameObject>(cap);



    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            if (objs.Count < cap)
            {
                // �v���C���[�^�O����Ȃ��Ċ���
                // �z��ɋ󂫂�����Δz��ɒǉ�����.
                objs.Add(other.gameObject);
                Debug.Log("�ڐG���܂���:" + other.name);
                OnGround = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // �^�O��Player�������牽�����Ȃ�.
        if (other.CompareTag("Player")) return;

        // ���X�g�������ς��ɂȂ��Ă����牽�����Ȃ�.
        if (objs.Count >= cap) return;

        // ���X�g�Ɋ܂܂�Ă��Ȃ�������ǉ�.
        if (!objs.Contains(other.gameObject))
        {
            objs.Add(other.gameObject);
            Debug.Log("�ǉ����܂���:" + other.name);
            OnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �g���K�[����O�ꂽ�I�u�W�F�N�g�����X�g�ɂ��邩����.
        int num = objs.IndexOf(other.gameObject);

        if (num != -1)
        {
            // �����ɂ��������烊�X�g����폜.
            objs.RemoveAt(num);

            // ���X�g����ɂȂ�����ڒn�����false�ɂ���.
            if(objs.Count <= 0)
            {
                Debug.Log("�ڒn���Ă��܂���");
                OnGround = false;
            }
        }
    }
}
