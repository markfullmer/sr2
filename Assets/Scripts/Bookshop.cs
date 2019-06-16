using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Bookshop : NPC {

	private GameObject selected;

    public void interact() {
		selected = GameObject.Find("Button1");
		EventSystem.current.SetSelectedGameObject(selected);
        FindObjectOfType<DialogueManager>().openInputPanel();
        if (GameControl.control.bookshop_state == "7") {
            FindObjectOfType<DialogueManager>().SetDialogue("You again. Why don't you try W.H. Smith's?.");
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
        string state = GameControl.control.bookshop_state;
        if (state == "0" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("Good morning, sir. Can I help you?");
            FindObjectOfType<DialogueManager>().SetReplies("Do you have a copy of 'Thirty Days in the Samarkind Desert with the Duchess of Kent' by A. E. J. Eliott, O.B.E.?", "Leave");
        }
        else if (state == "0") {
            if (reply == "1") {
                GameControl.control.bookshop_state = "1";
                FindObjectOfType<DialogueManager>().SetDialogue("Ah, well, I don't know the book, sir....");
                FindObjectOfType<DialogueManager>().SetReplies("How about 'A Hundred and One Ways to Start a Fight'?", "Leave");
            }
            else if (reply == "2") {
                GameControl.control.bookshop_state = "0";
                end();
            }
        }
        else if (state == "1") {
            if (reply == "1") {
                GameControl.control.bookshop_state = "2";
                FindObjectOfType<DialogueManager>().SetDialogue("By?");
                FindObjectOfType<DialogueManager>().SetReplies("An Irish gentleman whose name eludes me for the moment.", "Leave");                
            }
            else {
                GameControl.control.bookshop_state = "0";
                end();
            }            
        }
        else if (state == "2") {
            if (reply == "1") {
                GameControl.control.bookshop_state = "3";
                FindObjectOfType<DialogueManager>().SetDialogue("Ah, no, well we haven't got it in stock, sir....");
                FindObjectOfType<DialogueManager>().SetReplies("Oh, well, not to worry, not to worry. Can you help me with 'David Coperfield'?","Leave");
            }
            else {
                GameControl.control.bookshop_state = "0";
                end();
            }            
        }
        else if (state == "3") {
            if (reply == "1") {
                GameControl.control.bookshop_state = "4";
                FindObjectOfType<DialogueManager>().SetDialogue("Ah, yes...Dickens...");
                FindObjectOfType<DialogueManager>().SetReplies("No, Edmund Wells.","Leave");
            }
            else {
                GameControl.control.bookshop_state = "0";
                end();
            }            
        }
        else if (state == "4") {
            if (reply == "1") {
                GameControl.control.bookshop_state = "5";
                FindObjectOfType<DialogueManager>().SetDialogue("I...think you'll find Charles Dickens wrote 'David Copperfield', sir....");
                FindObjectOfType<DialogueManager>().SetReplies("No, no, Dickens wrote 'David Copperfield' with *two* Ps. This is 'David Coperfield' with *one* P by Edmund Wells.","Leave");
            }
            else {
                GameControl.control.bookshop_state = "0";
                end();
            }            
        }
        else if (state == "5") {
            if (reply == "1") {
                GameControl.control.bookshop_state = "6";
                FindObjectOfType<DialogueManager>().SetDialogue("'David Coperfield' with one P?");
                FindObjectOfType<DialogueManager>().SetReplies("Yes, I should have said.","Leave");
            }
            else {
                GameControl.control.bookshop_state = "6";
                end();
            }            
        }
        else if (state == "6") {
            if (reply == "1") {
                GameControl.control.bookshop_state = "7";
                FindObjectOfType<DialogueManager>().SetDialogue("Yes, well in that case we don't have it.");
                FindObjectOfType<DialogueManager>().SetReplies("Funny, you've got a lot of books here...","Leave");
            }
            else {
                GameControl.control.bookshop_state = "7";
                end();
            }            
        }
        else {
            GameControl.control.bookshop_state = "7";
            end();        
        }
    }
}
