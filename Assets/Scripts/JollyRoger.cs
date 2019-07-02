using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class JollyRoger : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().closeControlPanel();
        string state = GameControl.control.cargomanager_state;
        string dialogue = "";
        if (state != "0") {
            dialogue = "POSTED: THIS VESSEL IS TEMPORARYILY IMPOUNDED DUE TO QUARANTINED MATERIAL IN CARGO HOLD\n\nFOR RESOLUTION, SEE A CUSTOMS OFFICIAL";
        }
        else {
            dialogue = "As you try the hatch on your ship, the computer emits a depressing bleep sound.\n\nACCESS OVERRIDE IN EFFECT\n\nQUARANTINE PROTOCOL\n\nPLEASE VISIT STATION CUSTOMS FOR TAKEOFF CLEARANCE WAIVER"; 
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