using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OffDutyGuard : NPC {

	private GameObject selected;

    void triggerResponse(string reply) {
        string state = GameControl.control.offduty_state;
        if (state == "0" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("A uniformed Sishaz -- one of the few unisexual species in the Imperium -- off duty by the look of their posture, nods. Dr. Felsane's seminal work on Sishaz social behaviors, you recall, had concluded that Sishaz appreciate the artfully delivered insult.");
            FindObjectOfType<DialogueManager>().SetReplies("Busy guarding the station from pirates?", "Your station is a mess and a bore. I'm lost");
        }
        else if (state == "0") {
            GameControl.control.offduty_state = "1";
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("Guardin'?! Ha, no. I'm maintenance and engineering. Well, maintenance, mostly, when it comes right down to it.\n\nEngineering staff think they're better than everyone else.");
                FindObjectOfType<DialogueManager>().SetReplies("Engineering?", "What kind of maintenance?");
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().SetDialogue("'You're only lost if you're looking for something,' the officer chuckles.\n\n'Stop looking and problem's solved.'");
                FindObjectOfType<DialogueManager>().SetReplies("OKay, I'm *not* lost. Can you *not* point me in the direction of Customs?", "Funny. Real funny. There's nothing to do here.");
            }
        }
        else if (state == "1") {
            GameControl.control.offduty_state = "2";
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("Engineering, Customs, & the observation deck are all accessible through Turbolift A.");
                FindObjectOfType<DialogueManager>().SetReplies("Observation deck?", "Leave");
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().SetDialogue("Well, I keep myself busy with the damn turbolift. Thing seems to break every other shift. If it isn't the anti-grav compensator it's the dilithium inverter.");
                FindObjectOfType<DialogueManager>().SetReplies("Dilithium inverter?", "Leave");
            }         
        }
        else if (state == "2") {
            GameControl.control.offduty_state = "3";
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("Just last week, 20 people got stuck on the observation deck when the dilithium inverter crapped out. They got to do lots of observing! It took my two hours to replace the coil.");
                FindObjectOfType<DialogueManager>().SetReplies("Leave");
            }
            else if (reply == "2") {
                end();
            }            
        }
        else if (state == "3") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue("Heyo again. Found what you're looking for yet, or have you taken my advice & stopped looking?");
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
