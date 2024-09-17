using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class BatController : EnemyController {
    Animator animator;
    Rigidbody2D rb;

    int isDeadHash;
    int isAttackHash;

    bool isDead = false;
    public float patrolDistance = 5f;
    private bool movingRight = true;
    private Vector2 startPosition;

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isDeadHash = Animator.StringToHash("isDead");
        isAttackHash = Animator.StringToHash("isAttack");
        startPosition = transform.position;
    }

    protected override void Patrol() {
        if (isDead) {
            return;
        }

        if (movingRight) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = false ;
        } else {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Vector2.Distance(startPosition, transform.position) >= patrolDistance) {
            movingRight = !movingRight;
        }
    }

    protected override void AttackPlayer() {
        if (Vector2.Distance(transform.position, player.position) <= 1.5f) {
            animator.SetBool(isAttackHash, true);

            player.GetComponent<PlayerHealth>()?.TakeDamage(1);
        }
    }

    public override void TakeDamage(int damage) {
        if (isDead) {
            return;
        }

        health -= damage;
        if (health <= 0) {
            health = 0;
            EnemyDeath();
        }
    }

    protected override void EnemyDeath() {
        isDead = true;
        animator.SetBool(isDeadHash, true);
        GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;

        Destroy(gameObject, 2f);
        
    }


}
