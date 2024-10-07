using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    Animator animator;
    Rigidbody2D rb;

    int deathHash;

    public int maxHealth = 10;
    public int health;
    public bool isDead = false;

    private UIManager uiManager;
    private SpriteRenderer spriteRenderer;
    public GameOverScreen gameOver;
    public GameObject idleObject;
    public int Respawn;
    //public CameraShake cameraShake;

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        deathHash = Animator.StringToHash("isDead");
        health = maxHealth;

        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdatePlayerLives(health);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage) {

        if (isDead) {
            return;
        }
       
        health -= damage;
        StartCoroutine(FlashRed());
        
        if (health <= 0) {
            health = 0;
            gameOver.Defeat();
            
        }
        uiManager.UpdatePlayerLives(health);
    }

    public void playerDeath() {
        isDead = true;
        animator.SetBool(deathHash, true);
        rb.velocity = Vector2.zero;

        FindObjectOfType<UIManager>().LoseLife();
    }

    private IEnumerator FlashRed() {
        SpriteRenderer[] idleSprite = idleObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in idleSprite) {
            sr.color = Color.red;
        }
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        foreach (SpriteRenderer sr in idleSprite) {
            sr.color = Color.white;
        }
        spriteRenderer.color = Color.white;
    }
}
