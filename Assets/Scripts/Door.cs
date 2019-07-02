using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour
{
    public bool locked;
    public bool isOpen;
    protected bool isInteracting;
    public GameObject door;
    public Vector3 open;
    public Vector3 closed;

    public void interact() {
        FindObjectOfType<DialogueManager>().SetDialogue("The door before you is locked. A bold sign reads: KEY CARD ENTRY - AUTHORIZED PERSONNEL ONLY. What do you do?");
        FindObjectOfType<DialogueManager>().SetControlPanel("USE CARD", "PICK LOCK", "DONE");
	    StartCoroutine(actionWarmUp(0.6f));
	}
    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            if (locked == false) {
                end();
            }
            if (Event.current.Equals(Event.KeyboardEvent("return")) || Event.current.Equals(Event.KeyboardEvent("[enter]"))) {
                if (EventSystem.current.currentSelectedGameObject.name == "Talk") {
                    FindObjectOfType<DialogueManager>().SetDialogue("The door before you is locked. A bold sign reads: KEY CARD ENTRY - AUTHORIZED PERSONNEL ONLY. What do you do?\n\nYou don't own a key card!");
                    FindObjectOfType<DialogueManager>().SetControlPanel("USE CARD", "PICK LOCK", "DONE");
				}
                else if (EventSystem.current.currentSelectedGameObject.name == "Inspect") {
                    int choice = Random.Range(0, 10);
                    if (choice < 5) {
                        FindObjectOfType<DialogueManager>().SetDialogue("The door before you is locked. A bold sign reads: KEY CARD ENTRY - AUTHORIZED PERSONNEL ONLY. What do you do?\n\nYou fiddle with the lock for awhile but it stays shut.");
                        FindObjectOfType<DialogueManager>().SetControlPanel("USE CARD", "PICK LOCK", "DONE");
                    }
                    else if (choice < 6) {
                        FindObjectOfType<DialogueManager>().SetDialogue("You are spotted trying to pick the lock.\n\nA burly guard grabs you by the shoulder and marches you to a detention cell.\n\nHave a better one!");
                        FindObjectOfType<DialogueManager>().closeControlPanel();
                        StartCoroutine(actionRestart(3f));
                    }
                    else {
                        FindObjectOfType<DialogueManager>().SetDialogue("You locate the optomagnetic mechanism, give a tug, and hear a click as the lock opens.");
                        StartCoroutine(unlock(0.8f));                   
                        FindObjectOfType<DialogueManager>().closeControlPanel();                    
                    }
				}
                else {
                    end();
                }
            }
        }
    }

    protected void end() {
        FindObjectOfType<DialogueManager>().exit();
        isInteracting = false;
    }

    protected IEnumerator actionWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        isInteracting = true;
    }

    protected IEnumerator unlock(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        locked = false;
    }

    protected IEnumerator actionRestart(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        isInteracting = false;
        GameControl.control.game_state = "origin";
        GameControl.control.playerInteracting = false;
        FindObjectOfType<DialogueManager>().exit();
        SceneManager.LoadScene("Intro");
    }
}
