using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ManchiWorker : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().closeControlPanel();
        FindObjectOfType<DialogueManager>().SetDialogue("The insectoid alien looks up briefly from moving a crate. It appears to make eye contact with you, but you can't be sure. Then it shuffles away, making those hideous clicking sounds.");
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