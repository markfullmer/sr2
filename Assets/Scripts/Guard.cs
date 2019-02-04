using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Guard : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().StartDialogue("Keep your distance, civilian", "", "");
		StartCoroutine(actionWarmUp(0.3f));
	}

    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            isInteracting = false;
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
}