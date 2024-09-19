using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {
    public PlayerHealth playerHealth;
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")
            && collision.gameObject.layer != LayerMask.NameToLayer("MeleeHitBox")) {
            playerHealth.TakeDamage(damage);
        }
    }
}
