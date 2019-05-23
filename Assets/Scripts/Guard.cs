using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Guard : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().closeControlPanel();
        FindObjectOfType<DialogueManager>().SetDialogue("Keep your distance, civilian");
        StartCoroutine(actionWarmUp(0.6f));
	}

    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            FindObjectOfType<DialogueManager>().exit();
            isInteracting = false;
        }
    }
}