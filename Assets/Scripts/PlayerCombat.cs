using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    public GameObject swordHitBox;
    public int attackDamage = 1;
    public float knockbackDistance = 5.0f;

    private void Start() {
        
    }

    public void EnableSwordHitBox() {
        swordHitBox.SetActive(true);
    }

    public void DisableSwordHitBox() {
        swordHitBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        EnemyController enemy = collision.GetComponent<EnemyController>();

        if (enemy != null) {
            Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
            enemy.TakeDamage(attackDamage, knockbackDirection);
        }

    }
}
