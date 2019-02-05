﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    [SerializeField] GameObject[] waypoints;
    private int waypointIndex = 0;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject deathVFX;
    private SceneController sceneController;

    private void Start() {
        sceneController = FindObjectOfType<SceneController>();
    }


    void Update() {
        if (!sceneController.gameIsPaused) {
            Move();
        }
    }

    private void Move() {

        var moveFrame = moveSpeed * Time.deltaTime;

        if (waypointIndex <= waypoints.Length - 1) {
            var targetPosition = waypoints[waypointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed);

            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        }

        if(waypointIndex == 3) {
            waypointIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 9) {
            var tempVFX = Instantiate(deathVFX, gameObject.transform.position, transform.rotation);
            transform.position = new Vector2(9999, 9999);
            Destroy(tempVFX, 2);
            StopAllCoroutines();
            StartCoroutine(RespawnBoxAfterSeconds());
        }
    }

    IEnumerator RespawnBoxAfterSeconds() {
        yield return new WaitForSeconds(2.5f);
        sceneController.RestartGame();
    }
}