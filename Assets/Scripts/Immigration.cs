using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Immigration : NPC {

	private GameObject selected;

    void triggerResponse(string reply) {
        string state = GameControl.control.immigration_state;
        if (state == "0" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("A man in a ruffled white vac suit is pawing through a pile of papers on his desk. Who uses paper anymore?!\n\n'Cheerio,' the man says, his face wincing. This fellow is clearly overworked.\n\n'Station legal counseling and services at your...um...service. How can I help?'");
            FindObjectOfType<DialogueManager>().SetReplies("Any interesting cases?", "I want to file a patent");
        }
        else if (state == "0") {
            GameControl.control.immigration_state = "1";
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("The attorney looks surprised, as if he was expecting a complaint. 'We've got plenty of cases. Not sure about interesting. It's mostly immigration law right now. Imperium citizenship questions and the like.'");
                FindObjectOfType<DialogueManager>().SetReplies("Citizenship?", "Leave");
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().SetDialogue("'Got a million-credit idea, eh? Well, that's not really my jurisdiction. Unless your invention has something to do with fixing Hiathra Station's HVAC, that is!'");
                FindObjectOfType<DialogueManager>().SetReplies("What about the maintenance crew?", "Leave");
            }
        }
        else if (state == "1") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue("The attorney looks busy, shuffling through a stack of cases.");
                FindObjectOfType<DialogueManager>().SetReplies("What are you working on?", "Leave");
            }
            if (reply == "1") {
                GameControl.control.immigration_state = "2";
                FindObjectOfType<DialogueManager>().SetDialogue("'Well, the station's hiring plenty of workers, mostly Manchi. But people don't like it. So they try to find laws to restrict Manchi immigration.'\n\nThe attorney pauses to toss a treat to his pet boramph. The boramph's collar tag reads 'Boss.'\n\n'Anyway, Imperium civil code just doesn't restrict citizenship based on...whatever it is these people have against the Manchi.");
                FindObjectOfType<DialogueManager>().SetReplies("That's too bad for the Manchi", "The Manchi don't deserve legal representation");
            }
            else if (reply == "2") {
                end();
            }            
        }
        else if (state == "2") {
            if (reply == "1") {
                GameControl.control.immigration_state = "3";
                FindObjectOfType<DialogueManager>().SetDialogue("'Most people don't see it that way, unfortunately,' the attorney says, scratching Boss's thorax.\n\n'Then again, most people haven't seen their homeworld devastated by solar flares. The Manchi wouldn't be immigrating if they felt they had another option...'");
                FindObjectOfType<DialogueManager>().SetReplies("Leave");
            }
            else if (reply == "2") {
                end();
            }            
        }
        else if (state == "3") {
            GameControl.control.immigration_state = "1";
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
