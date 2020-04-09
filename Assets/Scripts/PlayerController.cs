using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public Animator anim;

    public float acceleration = 10f;
    public float jump_force = 80f;

    private float jump_blocked = 10f;
    private Vector2 maxVelocity = new Vector2(10, 10);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0) { anim.SetBool("jump", false); }
        if (rb.velocity.y < -0.1) { anim.SetBool("float", true); } else { anim.SetBool("float", false); }

        Vector2 force = Vector2.zero;
        float input_x = Input.GetAxisRaw("Horizontal");
        float try_jump = Input.GetAxisRaw("Jump");

        bool is_grounded = isGrounded();

        if (is_grounded)
        {
            if (anim.GetBool("float") || anim.GetBool("jump")) { anim.SetBool("float", false); }
            if (try_jump > 0.1)
            {
                if (jump_blocked <= 0)
                {
                    force.y += jump_force;
                    anim.SetBool("jump", true);
                    jump_blocked = 10f;
                }
                else
                {
                    jump_blocked -= 1;
                }
            }
        }
        
        if (System.Math.Abs(input_x) > 0.1)
        {
            force.x += System.Math.Sign(input_x) * acceleration;
            anim.SetFloat("x", force.x);
        }
       
        rb.AddForce(force);
        if (System.Math.Abs(rb.velocity.x) > maxVelocity.x) { rb.velocity = new Vector2(System.Math.Sign(rb.velocity.x) * maxVelocity.x, rb.velocity.y); }
        if (System.Math.Abs(rb.velocity.y) > maxVelocity.y) { rb.velocity = new Vector2(rb.velocity.x, System.Math.Sign(rb.velocity.y) * maxVelocity.y); }

        if (rb.velocity.y == 0 && rb.velocity.x == 0)
        {
            anim.SetFloat("x", 0);
            return;
        }
    }

    private bool isGrounded()
    {
        // Left, Right, and Center
        bool l = Physics2D.Linecast(bc.bounds.center - new Vector3(bc.bounds.extents.x - 0.05f, 0, 0), new Vector2(bc.bounds.center.x - bc.bounds.extents.x + 0.05f, bc.bounds.center.y - bc.bounds.extents.y - 0.1f)); ;
        bool c = Physics2D.Linecast(bc.bounds.center, new Vector2(bc.bounds.center.x, bc.bounds.center.y - bc.bounds.extents.y - 0.1f));
        bool r = Physics2D.Linecast(bc.bounds.center + new Vector3(bc.bounds.extents.x - 0.05f, 0, 0), new Vector2(bc.bounds.center.x + bc.bounds.extents.x - 0.05f, bc.bounds.center.y - bc.bounds.extents.y - 0.1f));
        return l || c || r;
    }
}
