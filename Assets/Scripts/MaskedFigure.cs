using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MaskedFigure : NPC {

	private GameObject selected;

    void triggerResponse(string reply) {
        string state = GameControl.control.maskedfigure_state;
        if (state == "0" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("As your footsteps, barely audible, approach a cloaked figure, the personage whips around to face you. You see -- or perhaps you only imagine -- two eyes in the darkness, almost glowing.");
            FindObjectOfType<DialogueManager>().SetReplies("I...come in...peace", "Sorry! Wrong mysterious cloaked figure. Toodles!");
        }
        else if (state == "0") {
            GameControl.control.maskedfigure_state = "1";
            FindObjectOfType<DialogueManager>().SetDialogue("A voice -- if you can call it that -- murmurs from within the cloak.\n\n'Perhaps I am what you are seeking. I know you. We know you. For thirty of your Earth years, you are and have been what some would call...a prophet. For others, a seer.'");
            FindObjectOfType<DialogueManager>().SetReplies("Who are 'we'??", "Leave...quickly");
        }
        else if (state == "1") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue("'Yes, seer, you have returned. Perhaps the time moment...what you would call destiny...is indeed at hand.'");
                FindObjectOfType<DialogueManager>().SetReplies("I don't believe in destiny.", "Leave");
            }
            if (reply == "1") {
                GameControl.control.maskedfigure_state = "2";
                FindObjectOfType<DialogueManager>().SetDialogue("Let us put that aside for the present moment. It is important that you find a way to visit the colony on Zed N-28.");
                FindObjectOfType<DialogueManager>().SetReplies("There isn't any Zed N-28 on any star charts! Zed N-27 is the furthest sector in the Far Arm.", "Leave");
            }
            else if (reply == "2") {
                end();
            }            
        }
        else if (state == "2") {
            if (reply == "1") {
                GameControl.control.maskedfigure_state = "3";
                FindObjectOfType<DialogueManager>().SetDialogue("Things are not always as they seem, seer. You must find your way to Zed N-28.");
                FindObjectOfType<DialogueManager>().SetReplies("Leave");
            }
            else if (reply == "2") {
                GameControl.control.maskedfigure_state = "0";
                end();
            }            
        }
        else if (state == "3") {
            FindObjectOfType<DialogueManager>().SetDialogue("Time not to waste. You must find your way to Zed N-28. By freighter, perhaps?");
            FindObjectOfType<DialogueManager>().SetReplies("Leave");
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
