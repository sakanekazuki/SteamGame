using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    [SerializeField]
    private Vector3 localGravity;
    private Rigidbody rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }



    private void FixedUpdate()
    {
        rb.AddForce(localGravity, ForceMode.Acceleration);
    }
}
