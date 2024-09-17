using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed = 2f;
    public float detectRadius = 5f;
    public LayerMask playerLayer;
    public int health = 5;

    protected bool playerDetected = false;
    protected Transform player;

    // Update is called once per frame
    void Update() {
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

    public virtual void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            EnemyDeath();
        }
    }

    protected virtual void EnemyDeath() {

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
