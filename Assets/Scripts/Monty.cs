using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Monty : NPC {

	private GameObject selected;

    public void interact() {
        Debug.Log("interact triggered");
		selected = GameObject.Find("Button1");
		EventSystem.current.SetSelectedGameObject(selected);
        FindObjectOfType<DialogueManager>().openInputPanel();
        if (GameControl.control.monty_state == "3") {
            FindObjectOfType<DialogueManager>().SetDialogue("I'm sorry, this is abuse. You want Room 12A, just along the corridor.\n\nStupid git.");
            FindObjectOfType<DialogueManager>().SetReplies("Leave");
        }
        else {
            triggerResponse("0");
        }
	    StartCoroutine(actionWarmUp(0.6f));
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

    void triggerResponse(string reply) {
        string state = GameControl.control.monty_state;
        if (state == "0" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("WHAT DO YOU WANT?");
            FindObjectOfType<DialogueManager>().SetReplies("Well, I was told outside that...", "Leave");
        }
        else if (state == "0") {
            if (reply == "1") {
                GameControl.control.monty_state = "1";
                FindObjectOfType<DialogueManager>().SetDialogue("Don't give me that, you snotty-faced heap of parrot droppings!");
                FindObjectOfType<DialogueManager>().SetReplies("What?", "Leave...quickly");
            }
            else if (reply == "2") {
                GameControl.control.monty_state = "0";
                end();
            }
        }
        else if (state == "1") {
            if (reply == "1") {
                GameControl.control.monty_state = "2";
                FindObjectOfType<DialogueManager>().SetDialogue("Shut your festering gob, you tit! Your type really makes me puke, you vacuous, coffee-nosed, malodorous, pervert!!!");
                FindObjectOfType<DialogueManager>().SetReplies("Look, I CAME HERE FOR AN ARGUMENT, I'm not going to just stand...!!", "Run away");                
            }
            else {
                GameControl.control.monty_state = "0";
                end();
            }            
        }
        else if (state == "2") {
            if (reply == "1") {
                GameControl.control.monty_state = "3";
                FindObjectOfType<DialogueManager>().SetDialogue("--OH!\n\n Oh, I'm sorry! This is 'Abuse'.\n\nYou want Room 12A, just along the corridor.");
                FindObjectOfType<DialogueManager>().SetReplies("Leave");
            }
            else {
                GameControl.control.monty_state = "3";
                end();
            }            
        }
        else if (state == "3") {
            end();           
        }
        else {
            GameControl.control.monty_state = "0";
            end();        
        }
    }
}
