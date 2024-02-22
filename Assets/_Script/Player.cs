using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Serialized")]
    public float moveSpeed;
    public float jumpForce;
    public int curHP;
    public int MaxHP;
    public Animator anin;
    public float attackRange;
    public int damage;
    private bool isAttacking;

    public Rigidbody rb;

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Attack();
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

    public void TakeDamage(int damageToTake)
    {
        curHP -= damageToTake;

        //update the UI health bar

        if (curHP <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    void Attack()
    {
        isAttacking = true;
        anin.SetBool("IsAttacking", isAttacking);
        Invoke("TryDamage", 0.7f);
        Invoke("DisableIsAttack", 1.5f);


    }
    void TryDamage()
    {
        Ray ray = new Ray(transform.position + transform.forward, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, attackRange, 1 << 8);

        foreach (var a in hits)
        {
            a.collider.GetComponent<Enemy>()?.TakeDamage(damage);
        }

    }
    void DisableIsAttack()
    {
        isAttacking = false;
        anin.SetBool("IsAttacking", isAttacking);

    }





}
