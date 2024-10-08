using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rb;

    public SpriteRenderer playerSpriteRenderer;

    private PlayerCombat playerCombat;

    public float speed = 5.0f;
    public float jumpForce = 0.5f;
    public LayerMask groundLayer;
    private bool isGrounded = false;

    int isRunningHash;
    int attackTriggerHash;
    int jumpTriggerHash;
    int yVelocityHash;
    int isGroundedHash;

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerCombat = GetComponent<PlayerCombat>();

        isRunningHash = Animator.StringToHash("isRunning");
        attackTriggerHash = Animator.StringToHash("attackTrigger");
        jumpTriggerHash = Animator.StringToHash("jumpTrigger");
        yVelocityHash = Animator.StringToHash("yVelocity");
        isGroundedHash = Animator.StringToHash("isGrounded");
    }

    void Update() {
        bool rightKey = Input.GetKey(KeyCode.D);
        bool leftKey = Input.GetKey(KeyCode.A);
        bool attackKey = Input.GetKey(KeyCode.Mouse0);
        bool jumpKey = Input.GetKeyDown(KeyCode.Space);

        bool isRunning = rightKey || leftKey;

        animator.SetFloat(yVelocityHash, rb.velocity.y);

        animator.SetBool(isGroundedHash, isGrounded);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.SetBool(isRunningHash, isRunning);

        // Idle body hidden during non-idle states
        if (!stateInfo.IsName("Idle")) {
            playerSpriteRenderer.enabled = true;
        }

        if (isRunning) {
            // Running logic
            playerSpriteRenderer.enabled = true;

            if (rightKey) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            } else if (leftKey) {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            }
        }

        if (jumpKey && isGrounded) {
            playerSpriteRenderer.enabled = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger(jumpTriggerHash);
        }

        // Attack key trigger
        if (attackKey) {
            playerSpriteRenderer.enabled = true;
            animator.SetTrigger(attackTriggerHash);

        } 

        if (stateInfo.IsName("Attack")) {
            playerSpriteRenderer.enabled = true;
            animator.ResetTrigger(attackTriggerHash);
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
