using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cargo : MonoBehaviour
{
    protected bool isInteracting;
    protected bool isBuying;
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
                    else {
                        handleBuy();
                    }
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Talk") {
                    isBuying = true;
                    FindObjectOfType<DialogueManager>().setBuy(
                        "titanium          30",
                        "exotic pets       48",
                        "dilithium         65",
                        "solvent grey      36",
                        "supercomputer     80",
                        "DONE");
				}
                else if (EventSystem.current.currentSelectedGameObject.name == "Inspect") {
                    FindObjectOfType<DialogueManager>().setSell();
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
            end();
        }
        else {
            string[] cargo = GameControl.control.cargo;
            for (int i = 0; i < cargo.Length; i += 1) {
                if (cargo[i] == "<empty>") {
                    GameControl.control.cargo[i] = response;
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
 
        //FindObjectOfType<DialogueManager>().exit();
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
