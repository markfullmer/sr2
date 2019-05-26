using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Passageway : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().closeControlPanel();
        FindObjectOfType<DialogueManager>().SetDialogue("You step down into what looks like a maintenance corridor.\n\nReaching forward into the darkness, your hands grasp the rungs of a ladder. Blindly, you lower yourself down until your feet find purchase.");
        StartCoroutine(actionWarmUp(0.6f));
	}

    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            FindObjectOfType<DialogueManager>().exit();
            isInteracting = false;
            SceneManager.LoadScene("Hiathra_engineering");
        }
    }
}