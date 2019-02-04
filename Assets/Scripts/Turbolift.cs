using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Turbolift : NPC {

	private string levelRequested;
    private InputField input;

    public void interact() {
        levelRequested = "";
        input = GameObject.Find("Textfield").GetComponent<InputField>();
        input.onEndEdit.AddListener(evaluateText);
        FindObjectOfType<DialogueManager>().PromptInput("Turbolift A\n\n-----------\n\n[3] Observation Deck\n[2] Administrative\n[1] Main Level\n[B1] Customs\n[B2] Engineering");
	}

    private void evaluateText(string text) {
        if (levelRequested != "" && text != "3264") {
            FindObjectOfType<DialogueManager>().PromptInput("Turbolift A\n\n-----------\n\nInvalid or expired access code.\n\nHave a better one.");
            StartCoroutine(leaveTurbolift());      
        }
        else if (text == "3") {
            GameControl.control.fromTurbolift = true;
            SceneManager.LoadScene("Hiathra_observation_deck");
            StartCoroutine(leaveTurbolift());
        }
        else if (text == "2") {
            levelRequested = "administrative";
            FindObjectOfType<DialogueManager>().PromptInput("Turbolift A\n\n-----------\n\nAccess to Administrative level requires clearance.\n\nPlease enter access code.");
        }
        else if (text == "1") {
            GameControl.control.fromTurbolift = true;
            SceneManager.LoadScene("Hiathra_main_floor");
            StartCoroutine(leaveTurbolift());           
        }
        else if (text == "B1" || text == "b1") {
            levelRequested = "customs";
            FindObjectOfType<DialogueManager>().PromptInput("Turbolift A\n\n-----------\n\nAccess to Customs requires clearance.\n\nPlease enter access code.");            
        }
        else if (text == "B2" || text == "b2") {
            levelRequested = "engineering";
            FindObjectOfType<DialogueManager>().PromptInput("Turbolift A\n\n-----------\n\nAccess to Engineering requires clearance.\n\nPlease enter access code.");            
        }
        else if (text == "3264") {
            // @todo
            if (levelRequested == "engineering") {
                FindObjectOfType<DialogueManager>().EndDialogue();
            }
            else {
                FindObjectOfType<DialogueManager>().EndDialogue();
            }
        }
        else {
            FindObjectOfType<DialogueManager>().PromptInput("Turbolift A\n\n-----------\n\nInvalid input.\n\nHave a better one.");
            StartCoroutine(leaveTurbolift());
        }
    }

    private IEnumerator leaveTurbolift() {
        input.onEndEdit.RemoveListener(evaluateText);
        float cooldown = 1;
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		FindObjectOfType<DialogueManager>().EndDialogue();
    }

}