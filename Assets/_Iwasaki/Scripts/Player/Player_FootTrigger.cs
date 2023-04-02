using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FootTrigger : MonoBehaviour
{
    // 接地判定
    public bool OnGround = false;

    private const int cap = 4;

    // トリガーに接触しているオブジェクトの配列
    private List<GameObject> objs = new List<GameObject>(cap);



    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            if (objs.Count < cap)
            {
                // プレイヤータグじゃなくて且つ
                // 配列に空きがあれば配列に追加する.
                objs.Add(other.gameObject);
                Debug.Log("接触しました:" + other.name);
                OnGround = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // タグがPlayerだったら何もしない.
        if (other.CompareTag("Player")) return;

        // リストがいっぱいになっていたら何もしない.
        if (objs.Count >= cap) return;

        // リストに含まれていなかったら追加.
        if (!objs.Contains(other.gameObject))
        {
            objs.Add(other.gameObject);
            Debug.Log("追加しました:" + other.name);
            OnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // トリガーから外れたオブジェクトがリストにあるか検索.
        int num = objs.IndexOf(other.gameObject);

        if (num != -1)
        {
            // 検索にかかったらリストから削除.
            objs.RemoveAt(num);

            // リストが空になったら接地判定をfalseにする.
            if(objs.Count <= 0)
            {
                Debug.Log("接地していません");
                OnGround = false;
            }
        }
    }
}
