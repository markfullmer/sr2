using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NPC : CharacterBase {

    public bool moveable;

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
            if (npcCanMoveTo(targetCell)) {
                StartCoroutine(doMove(targetCell));
            }
        }
    }

    protected void end() {
        FindObjectOfType<DialogueManager>().exit();
        isInteracting = false;
    }

    protected IEnumerator actionWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        isInteracting = true;
    }

    protected IEnumerator actionRestart(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        isInteracting = false;
        GameControl.control.game_state = "origin";
        GameControl.control.playerInteracting = false;
        FindObjectOfType<DialogueManager>().exit();
        SceneManager.LoadScene("Intro");
    }

}
