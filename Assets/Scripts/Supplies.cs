using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Supplies : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().closeControlPanel();
        string dialogue = "'Oh, you again. Sorry, still working through my inventory...'";
        if (GameControl.control.supplies_state == "0") {
            GameControl.control.supplies_state = "1";
            dialogue = "On the other side of the vending counter a frazzled station employee looks up at you from an inventory tablet.\n\n'Hello friend. We're in the middle of inventory right now. Check back in a couple hours...'";
        }
        FindObjectOfType<DialogueManager>().SetDialogue(dialogue);
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