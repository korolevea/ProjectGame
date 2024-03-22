using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed;
    public float rotation_speed;
    public float jump;
    public Transform groundCheckerTransform;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * speed);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.back * speed);
        }
    }

    public void RotateRight()
    {
        transform.Rotate(rotation_speed, 0, 0);
    }
    public void RotateLeft()
    {
        transform.Rotate(-rotation_speed, 0, 0);
    }
    void Jump ()
    {
        rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
    }
}
