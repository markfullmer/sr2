using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : CharacterBase {

    void Start() {
        // Try to move once every 0-3 seconds.
        InvokeRepeating("handleMove", 0, 3);
    }

    public void interact(string character) {
        if (character == "Cebak") {
            Debug.Log("Found Cebak!!");
        }
        else {
            Debug.Log("Other NPC here!!!!");
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

}
