using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    Animator animator;
    Rigidbody2D rb;

    int deathHash;

    public int maxHealth = 10;
    public int health;
    public bool isDead = false;

    private UIManager uiManager; //for UI 

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        deathHash = Animator.StringToHash("isDead");
        health = maxHealth;

        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdatePlayerLives(health);
    }

    public void TakeDamage(int damage) {

        if (isDead) {
            return;
        }

        health -= damage;
        if (health <= 0) {
            health = 0;
            playerDeath();
        }
        uiManager.UpdatePlayerLives(health);
    }

    public void playerDeath() {
        isDead = true;
        animator.SetBool(deathHash, true);
        rb.velocity = Vector2.zero;

        FindObjectOfType<UIManager>().LoseLife();
    }
}
