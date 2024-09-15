using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;

    float speed = 5.0f;

    int isRunningHash;
    int isAttackingHash;

    void Start() {
        animator = GetComponent<Animator>();

        isRunningHash = Animator.StringToHash("isRunning");
        isAttackingHash = Animator.StringToHash("IsAttacking");

    }

    void Update() {
        bool rightKey = Input.GetKey(KeyCode.D);
        bool leftKey = Input.GetKey(KeyCode.A);
        bool attackKey = Input.GetKey(KeyCode.Mouse0);

        bool isRunning = rightKey || leftKey;
        
        animator.SetBool(isRunningHash, isRunning);

        if (rightKey) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        } else if (leftKey) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        if (attackKey) {
            animator.SetBool(isAttackingHash, true);
        } 

        if (!attackKey) {
            animator.SetBool(isAttackingHash, false);
        }
    }
}
