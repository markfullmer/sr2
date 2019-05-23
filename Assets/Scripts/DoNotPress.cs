using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DoNotPress : NPC {

    private AudioSource interiorAmbience;

    public void interact() {
        FindObjectOfType<DialogueManager>().SetDialogue("POSTED:\n\nDO NOT PRESS BUTTON\n\n--Mgmt");
        FindObjectOfType<DialogueManager>().SetControlPanel("Press Button", "Done");
	    StartCoroutine(actionWarmUp(0.6f));
	}
    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            if (Event.current.Equals(Event.KeyboardEvent("return")) || Event.current.Equals(Event.KeyboardEvent("[enter]"))) {
                if (EventSystem.current.currentSelectedGameObject.name == "Talk") {
                    GameObject b = GameObject.Find("InteriorAmbience");
                    if (b != null) {
                        interiorAmbience = b.GetComponent<AudioSource>();
                        if (interiorAmbience.mute == true) {
                            interiorAmbience.mute = false;
                            GameControl.control.muted = false;
                        }
                        else {
                            interiorAmbience.mute = true;
                            GameControl.control.muted = true;
                        }
                    }
				}
                else {
                    end();
                }
            }
        }
    }
}