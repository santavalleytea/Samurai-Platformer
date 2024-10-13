using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rb;

    public SpriteRenderer playerSpriteRenderer;

    public TrailRenderer dashTrail;

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
        bool dashKey = Input.GetKeyDown(KeyCode.Mouse1);

        bool isRunning = rightKey || leftKey;

        animator.SetFloat(yVelocityHash, rb.velocity.y);

        animator.SetBool(isGroundedHash, isGrounded);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.SetBool(isRunningHash, isRunning);

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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger(jumpTriggerHash);
        }

        // Attack key trigger
        if (attackKey) {
            animator.SetTrigger(attackTriggerHash);

        } 

        if (stateInfo.IsName("Attack")) {
            animator.ResetTrigger(attackTriggerHash);
        }

        if (dashKey) {
            animator.SetTrigger("dashTrigger");
            StartCoroutine(Dash());
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

    IEnumerator Dash() {
        Physics2D.IgnoreLayerCollision(11, 13, true);
        float dashDuration = 0.2f;
        float dashSpeed = 20.0f;
        float startTime = Time.time;

        dashTrail.emitting = true;

        Vector3 dashDirection = transform.rotation.y == 0 ? Vector3.right : Vector3.left;

        while (Time.time < startTime + dashDuration) {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        Physics2D.IgnoreLayerCollision(11, 13, false);

        dashTrail.emitting = false;
    }
}
