﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : CharacterBase {

    public bool moveable;
    protected bool isInteracting;

    new void Start() {
        base.Start(); // Load tilemaps, etc.
        if (moveable) {
            // Try to move once every 0-2 seconds.
            InvokeRepeating("handleMove", 0, 2);
        }
    }

    void handleMove() {
        Vector2 currentCell = transform.position;
        int horizontal = (int) Random.Range(-1, 2);
        int vertical = (int) Random.Range(-1, 2);
        if (horizontal != 0 || vertical != 0) {
            Vector2 targetCell = currentCell + new Vector2(horizontal, vertical);
            if (canMoveTo(targetCell)) {
                StartCoroutine(doMove(targetCell));
            }
        }
    }

    protected IEnumerator actionWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        isInteracting = true;
    }

}
