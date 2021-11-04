using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private PauseController pauseController;

    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    public float ladderSpeed = 4f;
    [Range(0,1)]
    public float holdJumpGravityFactor = 0.5f;
    [Range(1,5)]
    public float fallGravityFactor = 2f;

    public bool isTouchingLadder;

    private float baseGravity;

    private bool isOnLadder = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        baseGravity = rigidbody.gravityScale;
        pauseController = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<PauseController>();
    }

    // Update is called once per frame
    void Update() {
        if (!pauseController.isPaused) {

            Vector2 velocity = rigidbody.velocity;
            isTouchingLadder = touchingLadder();
            if (!isOnLadder) {
                // movement
                float inputX = Input.GetAxisRaw("Horizontal");
                velocity.x = moveSpeed * inputX;

                if (Input.GetButtonDown("Jump") && isGrounded()) {
                    velocity.y = jumpSpeed;
                }

                // jump gravity mechanics
                float gravityFactor = 1f;

                if (velocity.y < 0) {
                    gravityFactor *= fallGravityFactor;
                }

                if (Input.GetButton("Jump")) {
                    gravityFactor *= holdJumpGravityFactor;
                }

                rigidbody.gravityScale = baseGravity * gravityFactor;

                if (touchingLadder() && (Input.GetAxisRaw("Vertical") != 0f)) {
                    isOnLadder = true;
                }

            } else {
                // ladder mechanics
                rigidbody.gravityScale = 0;

                bool jumpPressed = Input.GetButton("Jump");

                velocity.y = Input.GetAxisRaw("Vertical") * ladderSpeed;
                velocity.x = 0;

                if (jumpPressed) {
                    velocity.y = jumpSpeed;
                }

                if (isGrounded() || !touchingLadder() || jumpPressed) {
                    isOnLadder = false;
                }
            }

            animator.SetFloat("vx", velocity.x);
            animator.SetFloat("vy", velocity.y);

            rigidbody.velocity = velocity;
        }
    }

    private bool isGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(
            boxCollider.bounds.center,
            Vector2.down,
            boxCollider.bounds.extents.y + 0.1f,
            LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    private bool touchingLadder() {
        Vector2 playerTop = boxCollider.bounds.center;
        playerTop.y += boxCollider.bounds.extents.y;
        playerTop.x -= 0.1f;
        Vector2 playerBottom = boxCollider.bounds.center;
        playerBottom.y -= boxCollider.bounds.extents.y;
        playerBottom.x += 0.1f;
        Collider2D c2d = Physics2D.OverlapArea(playerTop, playerBottom, LayerMask.GetMask("Ladder"));
        return c2d != null;
    }
}
