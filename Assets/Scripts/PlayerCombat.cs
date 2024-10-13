using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    public GameObject swordHitBox;

    private void Start() {
        swordHitBox.SetActive(false); // Ensure the sword hitbox starts disabled
    }

    public void EnableSwordHitBox() {
        swordHitBox.SetActive(true);
    }

    public void DisableSwordHitBox() {
        swordHitBox.SetActive(false);
    }
}