using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EngineeringComputer : NPC {

	private string levelRequested;
    private InputField input;
    private bool triggerExit;
    AudioSource audioData;
    private AudioSource interiorAmbience;
	private GameObject selected;

    public void interact() {
        levelRequested = "";
        FindObjectOfType<DialogueManager>().SetDialogue("ENGINEERING\n\n-----------\n\n[S] SANITATION\n[L] LIFE SUPPORT\n[D] DOCKING\n[C] COMMUNICATIONS\n[H] HVAC");
        FindObjectOfType<DialogueManager>().closeControlPanel();
        FindObjectOfType<DialogueManager>().DisableReplies();
        FindObjectOfType<DialogueManager>().PromptInput();
        StartCoroutine(promptWarmUp(0.6f));
	}

    private IEnumerator promptWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        isInteracting = true;
        input = GameObject.Find("Textfield").GetComponent<InputField>();
        input.onEndEdit.AddListener(evaluateText);
    }

    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            if (triggerExit == true) {
                StartCoroutine(actionRestart(1f));
            }
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
        if (reply == "1") {
            GameControl.control.dockingReleased = true;
            FindObjectOfType<DialogueManager>().SetDialogue("ENGINEERING\n\n-----------\n\nMaintenance beginning. Some systems may experience interruption of service during maintenance cycle.");
            FindObjectOfType<DialogueManager>().closeControlPanel();
            StartCoroutine(triggerMaintenance());
        }
        else {
            interact();
        }
    }

    private void evaluateText(string text) {
        if (text == "S") {
            levelRequested = "sanitation";
            input.onEndEdit.RemoveListener(evaluateText);
            allowchoice();
            FindObjectOfType<DialogueManager>().SetDialogue("ENGINEERING\n\n-----------\n\nPreparing to run maintenance on " + levelRequested + " systems. Confirm..."); 
            FindObjectOfType<DialogueManager>().SetReplies("CONFIRM", "EXIT"); 
        }
        else if (text == "L" || text == "l") {
            levelRequested = "life support";
            FindObjectOfType<DialogueManager>().SetDialogue("ENGINEERING\n\n-----------\n\nRestarting Life Support requires clearance\n\nEnter access code.");
            FindObjectOfType<DialogueManager>().PromptInput();
        }
        else if (text == "D" || text == "d") {
            input.onEndEdit.RemoveListener(evaluateText);
            levelRequested = "docking";
            allowchoice();
            FindObjectOfType<DialogueManager>().SetDialogue("ENGINEERING\n\n-----------\n\nPreparing to run maintenance on " + levelRequested + " systems. Confirm..."); 
            FindObjectOfType<DialogueManager>().SetReplies("CONFIRM", "EXIT");  
       }
        else if (text == "C" || text == "c") {
            input.onEndEdit.RemoveListener(evaluateText);
            allowchoice();
            levelRequested = "communications";
            FindObjectOfType<DialogueManager>().SetDialogue("ENGINEERING\n\n-----------\n\nPreparing to run maintenance on " + levelRequested + " systems. Confirm..."); 
            FindObjectOfType<DialogueManager>().SetReplies("CONFIRM", "EXIT"); 
        }
        else if (text == "H" || text == "h") {
            input.onEndEdit.RemoveListener(evaluateText);
            allowchoice();
            levelRequested = "hvac";
            FindObjectOfType<DialogueManager>().SetDialogue("ENGINEERING\n\n-----------\n\nPreparing to run maintenance on " + levelRequested + " systems. Confirm...");    
            FindObjectOfType<DialogueManager>().SetReplies("CONFIRM", "EXIT"); 
        }
        else if (levelRequested == "life support") {
            if (text != "070886") {
                audioData = GetComponent<AudioSource>();
                audioData.Play(0);
                FindObjectOfType<DialogueManager>().closeControlPanel();
                FindObjectOfType<DialogueManager>().SetDialogue("You triggered an alarm. A uniformed engineer rushes toward you and locks an iron grip around your neck.\n\n'You're not authorized to be here!' he shouts, a presses a comm link for the station sentry.\n\nAfter a moment, you find yourself being marched toward the station detention cell...");
                input.onEndEdit.RemoveListener(evaluateText);
                StartCoroutine(triggerEnd(0.8f));
            }
            else {
                audioData = GetComponent<AudioSource>();
                audioData.Play(0);
                FindObjectOfType<DialogueManager>().closeControlPanel();
                FindObjectOfType<DialogueManager>().SetDialogue("You hear a deafening rush air around you, then the silence of a vacuum. Then, the utter cold of empty space.\n\nAs your consciousness seeps into oblivion, you ponder the meaning of 'life support'...");
                input.onEndEdit.RemoveListener(evaluateText);
                StartCoroutine(triggerEnd(0.8f));
            }
        }
        else {
            FindObjectOfType<DialogueManager>().SetDialogue("ENGINEERING\n\n-----------\n\nInvalid input.\n\nHave a better one.");
            StartCoroutine(leaveComputer());
        }
    }

    protected IEnumerator triggerEnd(float cooldown) {
        GameObject b = GameObject.Find("InteriorAmbience");
        if (b != null) {
            interiorAmbience = b.GetComponent<AudioSource>();
        }
        interiorAmbience.mute = true;
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        triggerExit = true;
    }

    void allowchoice() {
        selected = GameObject.Find("Button1");
        FindObjectOfType<DialogueManager>().closeControlPanel();
		EventSystem.current.SetSelectedGameObject(selected);
        FindObjectOfType<DialogueManager>().openInputPanel();
	    StartCoroutine(actionWarmUp(0.6f));
    }

    private IEnumerator leaveComputer(string action = null) {
        FindObjectOfType<DialogueManager>().closeInputPanel();
        input.onEndEdit.RemoveListener(evaluateText);
        float cooldown = 1;
        if (action != null) {

        }
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        GameControl.control.playerInteracting = false;
		FindObjectOfType<DialogueManager>().EndDialogue();
    }

    private IEnumerator triggerMaintenance(string action = null) {
        FindObjectOfType<DialogueManager>().closeInputPanel();
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        float cooldown = 3;
        if (action != null) {

        }
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        GameControl.control.playerInteracting = false;
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
		FindObjectOfType<DialogueManager>().EndDialogue();
    }

}