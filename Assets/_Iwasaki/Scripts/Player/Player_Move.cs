using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Move : MonoBehaviour
{
    // PlayerInput
    private PlayerInput pi;

    // RigidBody
    private Rigidbody rb;

    // Player_FootTrigger
    private Player_FootTrigger ft;

    // カメラの親
    private Transform CameraPitch;
    private Transform CameraRoll;

    // カメラの回転値
    private float rx = 0.0f;
    private float ry = 0.0f;

    // カメラ移動の安全性(カメラが正しく取得出来ていればtrue)
    private bool CameraSafety = true;

    // 移動用velocity
    private Vector3 vel_move;

    // カメラ回転用入力値
    private Vector2 delta;

    // 歩き移動加速度
    private const float speed_walk = 30.0f;

    // 空中移動加速度
    private const float speed_airwalk = 10.0f;

    // ジャンプ力
    private const float impulse_jump = 8.0f;

    // カメラの横回転感度(100が等倍にする予定)
    private const float speed_cam_turn = 100.0f;

    // カメラの縦回転感度(100が等倍にする予定)
    private const float speed_cam_roll = 100.0f;

    // カメラの縦回転で向ける角度上限
    private const float limit_cam_upper = 50.0f;

    // カメラの縦回転で向ける角度下限
    private const float limit_cam_downer = -50.0f;





    private void Awake()
    {
        // 各要素の取得.
        pi = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        // 自分の子から要素を取得.
        ft = transform.Find("FootTrigger").GetComponent<Player_FootTrigger>();

        // カメラの親たちを取得.
        CameraPitch = transform.Find("CameraPitch");
        CameraRoll = CameraPitch.Find("CameraRoll");

        // カメラの安全性を反映.
        if(CameraPitch == null || CameraRoll == null)
        {
            Debug.Log("カメラが正しく取得出来ません");
            CameraSafety = false;
        }
    }



    private void FixedUpdate()
    {
        FU_Move();
        FU_CameraRotate();
    }



    private void OnEnable()
    {
        if (pi == null) return;

        // デリゲートに各種関数を追加.
        pi.onActionTriggered += Action_Move;
        pi.onActionTriggered += Action_Jump;
        pi.onActionTriggered += Action_CameraRotate;
    }



    private void OnDisable()
    {
        // 非アクティブ中に走っていたら処理コストになるため
        // デリゲートから各種関数を削除.
        pi.onActionTriggered -= Action_Move;
        pi.onActionTriggered -= Action_Jump;
        pi.onActionTriggered -= Action_CameraRotate;
    }





    /*
     * FixedUpdate関数内に入れる関数.
     */

    // 移動用関数.
    private void FU_Move()
    {
        // 加速度を状況に応じて取得.
        float acceleration = 
            ft.OnGround ? speed_walk : speed_airwalk;

        // 向いている方向に応じて移動方向を変更.
        //vel_move =
        //    CameraPitch.forward * vel_move.z +
        //    CameraPitch.right * vel_move.x;
        Vector3 vel =
            CameraPitch.forward * vel_move.z +
            CameraPitch.right * vel_move.x;

        // 物理による移動.
        rb.AddForce(vel * acceleration, ForceMode.Acceleration);
    }



    // カメラの回転用関数.
    private void FU_CameraRotate()
    {
        // カメラの挙動が安全じゃなかったら行わない.
        if (!CameraSafety) return;

        // カメラの横回転.
        //CameraPitch.Rotate(new Vector3(0.0f, delta.x, 0.0f));

        // カメラの縦回転はそれぞれ制限を付ける.

        // まずは内部計算してクリッピング.
        rx -= delta.y * speed_cam_roll * 0.01f;
        ry += delta.x * speed_cam_turn * 0.01f;
        rx = Mathf.Clamp(rx, limit_cam_downer, limit_cam_upper);

        CameraPitch.rotation = Quaternion.identity
            * Quaternion.Euler(0.0f, ry, 0.0f);
        CameraRoll.rotation = CameraPitch.rotation
            * Quaternion.Euler(rx, 0.0f, 0.0f);
    }





    /*
     * InputAction用関数.
     */

    // 平面移動
    private void Action_Move(InputAction.CallbackContext context)
    {
        // Move以外は処理を行わない.
        if (context.action.name != "Move") return;

        // InputActionのcontextから入力値を取得する.
        Vector2 input = context.ReadValue<Vector2>();

        // Vector3に変換して渡す.
        vel_move = new Vector3(input.x, 0.0f, input.y);
    }



    // ジャンプ
    private void Action_Jump(InputAction.CallbackContext context)
    {
        // Jump以外は処理を行わない.
        if (context.action.name != "Jump") return;

        // 接地判定がfalseになっていたら何もしない.
        if (!ft.OnGround) return;

        if (context.started)
        {
            Debug.Log("Jump!");
            rb.AddForce(new Vector3(0.0f, impulse_jump, 0.0f), ForceMode.VelocityChange);
        }
    }



    // カメラの回転
    private void Action_CameraRotate(InputAction.CallbackContext context)
    {
        // CameraRotate以外は処理を行わない.
        if (context.action.name != "CameraRotate") return;

        // InputActionのcontextから入力値を取得してそのまま渡す.
        delta = context.ReadValue<Vector2>();
    }



    // 攻撃
    private void Action_Attack(InputAction.CallbackContext context)
    {
        // Attack以外は処理を行わない.
        if (context.action.name != "Attack") return;
    }



    // カメラのロックオン
    private void Action_Lockon(InputAction.CallbackContext context)
    {
        // Lockon以外は処理を行わない.
        if (context.action.name != "Lockon") return;
    }
}
