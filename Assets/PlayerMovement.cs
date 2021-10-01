using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private Animator animator;
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rigidbody.velocity;
        float inputX = Input.GetAxisRaw("Horizontal");
        velocity.x = moveSpeed * inputX;

        if (Input.GetButtonDown("Jump") && isGrounded()) {
            velocity.y = jumpSpeed;
        }

        animator.SetFloat("vx", velocity.x);
        animator.SetFloat("vy", velocity.y);

        rigidbody.velocity = velocity;
    }

    private bool isGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(
            boxCollider.bounds.center,
            Vector2.down,
            boxCollider.bounds.extents.y + 0.1f,
            LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}
