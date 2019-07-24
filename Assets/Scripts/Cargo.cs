using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cargo : MonoBehaviour
{
    protected bool isInteracting;
    protected bool isBuying;
    protected bool isSelling;
    protected bool bought = false;
    protected bool sold = false;

    public void interact() {
        FindObjectOfType<DialogueManager>().SetDialogue("The cargo merchant says cheerfully,\n\n'Welcome, freetrader! If you're looking to trade cargo, you've come to the right place. What will it be?'");
        FindObjectOfType<DialogueManager>().SetControlPanel("BUY", "SELL", "DONE");
	    StartCoroutine(actionWarmUp(0.6f));
	}
    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            if (Event.current.Equals(Event.KeyboardEvent("return")) || Event.current.Equals(Event.KeyboardEvent("[enter]"))) {
                FindObjectOfType<DialogueManager>().closeControlPanel();
                if (GameControl.control.isMerchPanel == true) {
                    if (isBuying) {
                        handleBuy();
                    }
                    else if (isSelling) {
                        handleSell();
                    }
                    else {
                        end();
                    }
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Talk") {
                    isBuying = true;
                    FindObjectOfType<DialogueManager>().setMerchResponse(">");
                    FindObjectOfType<DialogueManager>().setBuy(
                        "titanium          30",
                        "exotic pets       48",
                        "dilithium         65",
                        "solvent grey      36",
                        "supercomputer     80",
                        "DONE");
				}
                else if (EventSystem.current.currentSelectedGameObject.name == "Inspect") {
                    string[] cargo = GameControl.control.cargo;
                    isSelling = true;
                    FindObjectOfType<DialogueManager>().setSell(cargo[0], cargo[1], cargo[2], cargo[3], "DONE");
				}
                else {
                    end();
                }
            }
        }
    }

    protected void handleBuy() {
        Selectable choice = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        string response = choice.GetComponentInChildren<Text>().text;
        if (response == "DONE") {
            isSelling = false;
            isBuying = false;
            end();
        }
        else {
            string[] cargo = GameControl.control.cargo;
            for (int i = 0; i < cargo.Length; i += 1) {
                if (cargo[i] == "<empty>") {
                    GameControl.control.cargo[i] = response;
                    string cost = response.Substring(response.Length - 2);
                    int intCost = Convert.ToInt32(cost);
                    GameControl.control.credits += -intCost;
                    bought = true;
                    break;
                }
            }
            if (bought) {
                FindObjectOfType<DialogueManager>().setMerchResponse("> Excellent choice");
                bought = false;
            }
            else {
                FindObjectOfType<DialogueManager>().setMerchResponse("> Cargo hold full");           
            }
        }
    }

    protected void handleSell() {
        Selectable choice = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        string response = choice.GetComponentInChildren<Text>().text;
        if (response == "DONE") {
            isSelling = false;
            isBuying = false;
            end();
        }
        else if (response == "<empty>") {
            FindObjectOfType<DialogueManager>().setMerchResponse("> Cargo pod empty");
        }
        else if (response.Contains("hybrid grain")) {
            FindObjectOfType<DialogueManager>().setMerchResponse("> Sale of hybrid grain is prohibited per station quarantine");
        }
        else {
            string[] cargo = GameControl.control.cargo;
            for (int i = 0; i < cargo.Length; i += 1) {
                if (cargo[i] == response) {
                    GameControl.control.cargo[i] = "<empty>";
                    string cost = response.Substring(response.Length - 2);
                    int intCost = Convert.ToInt32(cost);
                    GameControl.control.credits += intCost;
                    sold = true;
                    break;
                }
            }
            if (sold) {
                cargo = GameControl.control.cargo;
                FindObjectOfType<DialogueManager>().setSell(cargo[0], cargo[1], cargo[2], cargo[3], "DONE");
                FindObjectOfType<DialogueManager>().setMerchResponse("> I'll buy that");
                sold = false;
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

}
