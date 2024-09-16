using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rb;

    public float speed = 5.0f;
    public float jumpForce = 0.5f;
    public LayerMask groundLayer;
    private bool isGrounded = false;

    int isRunningHash;
    int attackTriggerHash;
    int jumpTriggerHash;

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        isRunningHash = Animator.StringToHash("isRunning");
        attackTriggerHash = Animator.StringToHash("attackTrigger");
        jumpTriggerHash = Animator.StringToHash("jumpTrigger");

    }

    void Update() {
        bool rightKey = Input.GetKey(KeyCode.D);
        bool leftKey = Input.GetKey(KeyCode.A);
        bool attackKey = Input.GetKey(KeyCode.Mouse0);
        bool jumpKey = Input.GetKeyDown(KeyCode.Space);

        bool isRunning = rightKey || leftKey;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.SetBool(isRunningHash, isRunning);

        if (rightKey) {
            // Turn right then move
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        } else if (leftKey) {
            // Turn left then move
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        // Attack key trigger
        if (attackKey) {
            animator.SetTrigger(attackTriggerHash);
        } 

        if (stateInfo.IsName("Attack1")) {
            animator.ResetTrigger(attackTriggerHash);
        }

        //isGrounded = IsGrounded();

        if (jumpKey && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger(jumpTriggerHash);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
