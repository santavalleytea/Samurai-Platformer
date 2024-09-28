using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private Rigidbody2D rb;

    public float knockbackForce = 5f;

    public float speed = 2f;
    public float detectRadius = 5f;
    public LayerMask playerLayer;
    public int health = 5;

    protected bool playerDetected = false;
    protected Transform player;

   protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update() {
        DetectPlayer();
        if (playerDetected) {
            AttackPlayer();
        } else {
            Patrol();
        }
    }

    protected void DetectPlayer() {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer);
        if (playerCollider != null) {
            playerDetected = true;
            player = playerCollider.transform;
        } else {
            playerDetected = false;
        }
    }

    protected virtual void AttackPlayer() {

    }

    protected virtual void Patrol() {

    }

    public virtual void TakeDamage(int damage, Vector2 knockbackDirection) {
        health -= damage;

        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        if (health <= 0) {
            EnemyDeath();
        } else {
            StartCoroutine(StopKnockback());
        }
    }

    private IEnumerator StopKnockback() {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
    }

    protected virtual void EnemyDeath() {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
