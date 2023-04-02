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

    // �J�����̐e
    private Transform CameraPitch;
    private Transform CameraRoll;

    // �J�����̉�]�l
    private float rx = 0.0f;
    private float ry = 0.0f;

    // �J�����ړ��̈��S��(�J�������������擾�o���Ă����true)
    private bool CameraSafety = true;

    // �ړ��pvelocity
    private Vector3 vel_move;

    // �J������]�p���͒l
    private Vector2 delta;

    // �����ړ������x
    private const float speed_walk = 30.0f;

    // �󒆈ړ������x
    private const float speed_airwalk = 10.0f;

    // �W�����v��
    private const float impulse_jump = 8.0f;

    // �J�����̉���]���x(100�����{�ɂ���\��)
    private const float speed_cam_turn = 100.0f;

    // �J�����̏c��]���x(100�����{�ɂ���\��)
    private const float speed_cam_roll = 100.0f;

    // �J�����̏c��]�Ō�����p�x���
    private const float limit_cam_upper = 50.0f;

    // �J�����̏c��]�Ō�����p�x����
    private const float limit_cam_downer = -50.0f;





    private void Awake()
    {
        // �e�v�f�̎擾.
        pi = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        // �����̎q����v�f���擾.
        ft = transform.Find("FootTrigger").GetComponent<Player_FootTrigger>();

        // �J�����̐e�������擾.
        CameraPitch = transform.Find("CameraPitch");
        CameraRoll = CameraPitch.Find("CameraRoll");

        // �J�����̈��S���𔽉f.
        if(CameraPitch == null || CameraRoll == null)
        {
            Debug.Log("�J�������������擾�o���܂���");
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

        // �f���Q�[�g�Ɋe��֐���ǉ�.
        pi.onActionTriggered += Action_Move;
        pi.onActionTriggered += Action_Jump;
        pi.onActionTriggered += Action_CameraRotate;
    }



    private void OnDisable()
    {
        // ��A�N�e�B�u���ɑ����Ă����珈���R�X�g�ɂȂ邽��
        // �f���Q�[�g����e��֐����폜.
        pi.onActionTriggered -= Action_Move;
        pi.onActionTriggered -= Action_Jump;
        pi.onActionTriggered -= Action_CameraRotate;
    }





    /*
     * FixedUpdate�֐����ɓ����֐�.
     */

    // �ړ��p�֐�.
    private void FU_Move()
    {
        // �����x���󋵂ɉ����Ď擾.
        float acceleration = 
            ft.OnGround ? speed_walk : speed_airwalk;

        // �����Ă�������ɉ����Ĉړ�������ύX.
        //vel_move =
        //    CameraPitch.forward * vel_move.z +
        //    CameraPitch.right * vel_move.x;
        Vector3 vel =
            CameraPitch.forward * vel_move.z +
            CameraPitch.right * vel_move.x;

        // �����ɂ��ړ�.
        rb.AddForce(vel * acceleration, ForceMode.Acceleration);
    }



    // �J�����̉�]�p�֐�.
    private void FU_CameraRotate()
    {
        // �J�����̋��������S����Ȃ�������s��Ȃ�.
        if (!CameraSafety) return;

        // �J�����̉���].
        //CameraPitch.Rotate(new Vector3(0.0f, delta.x, 0.0f));

        // �J�����̏c��]�͂��ꂼ�ꐧ����t����.

        // �܂��͓����v�Z���ăN���b�s���O.
        rx -= delta.y * speed_cam_roll * 0.01f;
        ry += delta.x * speed_cam_turn * 0.01f;
        rx = Mathf.Clamp(rx, limit_cam_downer, limit_cam_upper);

        CameraPitch.rotation = Quaternion.identity
            * Quaternion.Euler(0.0f, ry, 0.0f);
        CameraRoll.rotation = CameraPitch.rotation
            * Quaternion.Euler(rx, 0.0f, 0.0f);
    }





    /*
     * InputAction�p�֐�.
     */

    // ���ʈړ�
    private void Action_Move(InputAction.CallbackContext context)
    {
        // Move�ȊO�͏������s��Ȃ�.
        if (context.action.name != "Move") return;

        // InputAction��context������͒l���擾����.
        Vector2 input = context.ReadValue<Vector2>();

        // Vector3�ɕϊ����ēn��.
        vel_move = new Vector3(input.x, 0.0f, input.y);
    }



    // �W�����v
    private void Action_Jump(InputAction.CallbackContext context)
    {
        // Jump�ȊO�͏������s��Ȃ�.
        if (context.action.name != "Jump") return;

        // �ڒn���肪false�ɂȂ��Ă����牽�����Ȃ�.
        if (!ft.OnGround) return;

        if (context.started)
        {
            Debug.Log("Jump!");
            rb.AddForce(new Vector3(0.0f, impulse_jump, 0.0f), ForceMode.VelocityChange);
        }
    }



    // �J�����̉�]
    private void Action_CameraRotate(InputAction.CallbackContext context)
    {
        // CameraRotate�ȊO�͏������s��Ȃ�.
        if (context.action.name != "CameraRotate") return;

        // InputAction��context������͒l���擾���Ă��̂܂ܓn��.
        delta = context.ReadValue<Vector2>();
    }



    // �U��
    private void Action_Attack(InputAction.CallbackContext context)
    {
        // Attack�ȊO�͏������s��Ȃ�.
        if (context.action.name != "Attack") return;
    }



    // �J�����̃��b�N�I��
    private void Action_Lockon(InputAction.CallbackContext context)
    {
        // Lockon�ȊO�͏������s��Ȃ�.
        if (context.action.name != "Lockon") return;
    }
}
