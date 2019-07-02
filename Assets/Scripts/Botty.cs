using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Botty : NPC {

	private GameObject selected;

    void triggerResponse(string reply) {
        string state = GameControl.control.botty_state;
        if (state == "0" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("You approach a LUX-3 model, which appears to be gazing out at the stars through the plexiglass floor, almost philosophically.\n\nA quirk in the LUX-3 self-diagnostic programming, you muse. Ah, anthropomorphism...");
            FindObjectOfType<DialogueManager>().SetReplies("Computer, exit process", "Pondering the mind-body question?");
        }
        else if (state == "0") {
            GameControl.control.botty_state = "1";
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("The LUX-3 model rotates around, slowly refocusing its ocular unit on you.\n\n'I'm sorry. I was, you might say, lost in thought.'\n\nThe unit whirs a bit, as if bringing itself out of revery.\n\n'State your request, human.'");
                FindObjectOfType<DialogueManager>().SetReplies("I need a ship", "I need money");
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().SetDialogue("'The parable of the cave, actually. I was running 3,245 parallel simulations to determine what threshold of sentience--'\n\nThe unit stops abruptly, as if only now realizing that it was being spoken to.\n\n'Oh, hello. Pleasant stars, no?'");
                FindObjectOfType<DialogueManager>().SetReplies("I'm headed to the Zed system.", "I think I see Bassruti system.");
            }
        }
        else if (state == "1") {
            GameControl.control.botty_state = "2";
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("My ship, the tesseract class in Docking Ring 3, just returned from Zed. Problem is, they've impounded it.");
                FindObjectOfType<DialogueManager>().SetReplies("Impounded?", "Leave");
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().SetDialogue("There's always money to be found in Bassruti, one way or another. I'd go there myself, if my ship weren't impounded.");
                FindObjectOfType<DialogueManager>().SetReplies("My ship is quarantined.", "Leave");
            }         
        }
        else if (state == "2") {
            GameControl.control.botty_state = "3";
            GameControl.control.barkeep_state = "escape";
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("'Customs is finding any excuse to ground ships. The Manchi flu...archaic tariffs... I haven't got the 300CRs they say I owe.'\n\n'If my programming were a bit more...ethically elastic...I might try hacking into the Engineering subsystem and releasing the docking clamps.'\n\nThe robot, pauses, reprocessing. 'A joke, of course.'");
                FindObjectOfType<DialogueManager>().SetReplies("Leave");
            }
            else if (reply == "2") {
                end();
            }            
        }
        else if (state == "3") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue("'Best be heading back to my ship now...to ponder my ethical quandary about those docking clamps...' ");
                FindObjectOfType<DialogueManager>().SetReplies("Leave");
            }
            else {
                end();
            }
    
        }
    }

    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            if (Event.current.Equals(Event.KeyboardEvent("return")) || Event.current.Equals(Event.KeyboardEvent("[enter]"))) {
                if (Input.GetKeyDown("enter") || Input.GetKeyDown("return")) {
				    if ("Button1" == EventSystem.current.currentSelectedGameObject.name) {
                        triggerResponse("1");
				    }
				    if ("Button2" == EventSystem.current.currentSelectedGameObject.name) {
					    triggerResponse("2");
				    }
                }
            }
        }
    }

    public void interact() {
		selected = GameObject.Find("Button1");
		EventSystem.current.SetSelectedGameObject(selected);
        FindObjectOfType<DialogueManager>().openInputPanel();
        triggerResponse("0");
	    StartCoroutine(actionWarmUp(0.6f));
	}
}
