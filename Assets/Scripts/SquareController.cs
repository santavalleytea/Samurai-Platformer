using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : EnemyController {

    public float moveSpeed = 2.0f;
    public float patrolDistance = 3.0f;
    public float chaseDuration = 3.0f;
    public float returnSpeed = 2.0f;

    private Vector3 startPosition;
    private float chaseTimer = 0f;
    private bool returningToPatrol = false;

    protected override void Start() {
        base.Start();
        startPosition = transform.position;
    }

    protected override void Update() {
        DetectPlayer();

        if (playerDetected) {
            chaseTimer = chaseDuration;
            AttackPlayer();
        } else if (chaseTimer > 0) {
            chaseTimer -= Time.deltaTime;
            AttackPlayer();
        } else {
            Patrol();
        }
    }
    protected override void Patrol() {
        float newX = startPosition.x + Mathf.PingPong(Time.time * moveSpeed, patrolDistance);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    protected override void AttackPlayer() {
        if (player != null) {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }

    private void ReturnToPatrol() {
        Vector3 directionToStart = (startPosition - transform.position).normalized;
        transform.position += directionToStart * returnSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, startPosition) < 0.1f) {
            returningToPatrol = false;
            Patrol();
        }
    }
}