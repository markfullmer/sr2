using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Immigration : NPC {

	private GameObject selected;

    void triggerResponse(string reply) {
        string state = GameControl.control.cargomanager_state;
        if (state == "0" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("A bored looking humanoid shifts their gaze from a console, with geologic alacrity.\n\n'Customs desk. Scan your guild licence.'");
            FindObjectOfType<DialogueManager>().SetReplies("Scan guild licence", "Leave");
        }
        else if (state == "0") {
            if (reply == "1") {
                GameControl.control.cargomanager_state = "1";
                FindObjectOfType<DialogueManager>().SetDialogue("'Hrm...no outstanding fines...', the customs official says, scanning your records.\n\n 'Ah...your ship's cargo manifest indicates three containers of methane. Your ship won't be permitted to leave the station, due to the quarantine policy.'");
                FindObjectOfType<DialogueManager>().SetReplies("How do get off the station, then?", "Leave");
            }
            else if (reply == "2") {
                end();
            }
        }
        else if (state == "1") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue("'And...it's you again. Fellow with the quarantined methane. What now?'");
                FindObjectOfType<DialogueManager>().SetReplies("How do I get off the station?", "Leave");
            }
            if (reply == "1") {
                GameControl.control.cargomanager_state = "2";
                FindObjectOfType<DialogueManager>().SetDialogue("'Well, you can't un-dock your ship because of the cargo in its hold. And you can't empty your hold because the sale of quarantined cargo is prohibited.'\n\n 'Bit of a catch-22, isn't it?' the customs official says, making no attempt to hide his amusement.");
                FindObjectOfType<DialogueManager>().SetReplies("Thanks. You've been immense help", "Leave");
            }
            else if (reply == "2") {
                end();
            }            
        }
        else if (state == "2") {
            if (reply == "1") {
                GameControl.control.cargomanager_state = "3";
                FindObjectOfType<DialogueManager>().SetDialogue("'It's a sarcastically immense pleasure to serve you, bub.'");
                FindObjectOfType<DialogueManager>().SetReplies("Leave");
            }
            else if (reply == "2") {
                GameControl.control.cargomanager_state = "0";
                end();
            }            
        }
        else if (state == "3") {
            GameControl.control.cargomanager_state = "0";
            end();       
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
