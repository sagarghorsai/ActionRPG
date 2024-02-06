using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Serialized")]
    public float moveSpeed;
    public float jumpForce;

    public Rigidbody rb;

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;
        dir *=moveSpeed;
        dir.y = rb.velocity.y;
        rb.velocity = dir;

    }

    void Jump() 
    {
        if (CanJunp())
        {
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        }
    }
    bool CanJunp()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit,0.1f) )
        {
            return hit.collider != null;
        }
        return false;
    }





}
