using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Turbolift : NPC {

	private string levelRequested;
    private InputField input;
    AudioSource audioData;

    public void interact() {
        levelRequested = "";
        FindObjectOfType<DialogueManager>().SetDialogue("Turbolift A\n\n-----------\n\n[3] Observation Deck\n[2] Administrative\n[1] Main Level\n[B1] Customs/Cargo/Checkpoint\n[B2] Engineering");
        FindObjectOfType<DialogueManager>().closeControlPanel();
        FindObjectOfType<DialogueManager>().DisableReplies();
        FindObjectOfType<DialogueManager>().PromptCustomInput();
        StartCoroutine(promptWarmUp(0.6f));
	}

    private IEnumerator promptWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        input = GameObject.Find("Textfield").GetComponent<InputField>();
        input.onEndEdit.AddListener(evaluateText);
    }

    private void evaluateText(string text) {
        if (levelRequested != "" && text != "3264") {
            FindObjectOfType<DialogueManager>().SetDialogue("Turbolift A\n\n-----------\n\nInvalid or expired access code.\n\nHave a better one.");
            StartCoroutine(leaveTurbolift());      
        }
        else if (text == "3") {
            GameControl.control.fromTurbolift = true;
            StartCoroutine(leaveTurbolift("Hiathra_observation_deck"));
        }
        else if (text == "2") {
            levelRequested = "administrative";
            FindObjectOfType<DialogueManager>().SetDialogue("Turbolift A\n\n-----------\n\nAdministrative level requires clearance.\n\nEnter access code.");
            FindObjectOfType<DialogueManager>().PromptCustomInput();
        }
        else if (text == "1") {
            GameControl.control.fromTurbolift = true;
            StartCoroutine(leaveTurbolift("Hiathra_main_floor"));           
        }
        else if (text == "B1" || text == "b1") {
            GameControl.control.fromTurbolift = true;
            levelRequested = "customs";
            StartCoroutine(leaveTurbolift("Hiathra_customs"));    
        }
        else if (text == "B2" || text == "b2") {
            levelRequested = "engineering";
            FindObjectOfType<DialogueManager>().SetDialogue("Turbolift A\n\n-----------\n\nEngineering level requires clearance.\n\nEnter access code.");
            FindObjectOfType<DialogueManager>().PromptCustomInput();          
        }
        else if (text == "3264") {
            GameControl.control.fromTurbolift = true;
            if (levelRequested == "administrative") {
                StartCoroutine(leaveTurbolift("Hiathra_administrative"));
            }
            else {
                FindObjectOfType<DialogueManager>().SetDialogue("Turbolift A\n\n-----------\n\nInvalid input.\n\nHave a better one.");
                StartCoroutine(leaveTurbolift());
            }
        }
        else {
            FindObjectOfType<DialogueManager>().SetDialogue("Turbolift A\n\n-----------\n\nInvalid input.\n\nHave a better one.");
            StartCoroutine(leaveTurbolift());
        }
    }

    private IEnumerator leaveTurbolift(string scene = null) {
        GameControl.control.playerInteracting = false;
        input.onEndEdit.RemoveListener(evaluateText);
        float cooldown = 1;
        if (scene != null) {
            cooldown = 3;
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
        }
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        if (scene != null) {
            SceneManager.LoadScene(scene);
        }
		FindObjectOfType<DialogueManager>().EndDialogue();
    }

}