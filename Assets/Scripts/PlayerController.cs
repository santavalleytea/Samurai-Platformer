using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rb;

    public GameObject idleBodyParts;
    public SpriteRenderer playerSpriteRenderer;

    private PlayerCombat playerCombat;
    //public GameObject shurikenPrefab;
    public Transform throwPoint;
  
    public float throwForce = 10f;
    private bool canThrow = true;
    public float throwCoolDown = 1.0f;

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

        idleBodyParts.SetActive(true);
        playerSpriteRenderer.enabled = false;

        // Idle body hidden during non-idle states
        if (!stateInfo.IsName("Idle")) {
            idleBodyParts.SetActive(false);
            playerSpriteRenderer.enabled = true;
        }

        if (isRunning) {
            // Running logic
            idleBodyParts.SetActive(false);
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
            idleBodyParts.SetActive(false);
            playerSpriteRenderer.enabled = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger(jumpTriggerHash);
        }

        // Attack key trigger
        if (attackKey) {
            idleBodyParts.SetActive(false);
            playerSpriteRenderer.enabled = true;
            animator.SetTrigger(attackTriggerHash);

        } 

        if (stateInfo.IsName("Attack")) {
            idleBodyParts.SetActive(false);
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

    //private void ThrowShuriken() {
    //    if (!canThrow) {
    //        return;
    //    }

    //    GameObject shuriken = Instantiate(shurikenPrefab, throwPoint.position, throwPoint.rotation);
    //    Rigidbody2D rbShuriken = shuriken.GetComponent<Rigidbody2D>();
    //    Vector2 throwDirection = transform.rotation.eulerAngles.y == 0 ? Vector2.right : Vector2.left;
    //    rbShuriken.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

    //    StartCoroutine(ThrowCooldown());
    //}

    //IEnumerator ThrowCooldown() {
    //    canThrow = false;
    //    yield return new WaitForSeconds(throwCoolDown);
    //    canThrow = true;
    //}
}
