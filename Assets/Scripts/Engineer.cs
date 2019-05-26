using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Engineer : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().closeControlPanel();
        FindObjectOfType<DialogueManager>().SetDialogue("A uniformed engineer, catching sight of you a few feet a way, rushes toward you and locks an iron grip around your neck.\n\n'You're not authorized to be here!' he shouts, a presses a comm link for the station sentry.\n\nAfter a moment, you find yourself being marched toward the station detention cell...");
        StartCoroutine(actionRestart(2f));
	}

    void OnGUI() {
        Event e = Event.current;
        if (playerInRange(transform.position)) {
            isInteracting = true;
            interact();
        }
    }
}