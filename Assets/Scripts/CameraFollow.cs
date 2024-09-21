using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float followSpeed = 2f;
    public Transform player;

    void Update() {
        Vector3 newPos = new Vector3(player.position.x, Mathf.Clamp(player.position.y, 0, Mathf.Infinity), -10f);
        transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
