using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Tesseract : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().closeControlPanel();
        bool docking = GameControl.control.dockingReleased;
        string botty = GameControl.control.botty_state;
        string dialogue = "";
        StartCoroutine(actionWarmUp(0.6f));
        if ((docking == true && botty == "3")) {
            FindObjectOfType<DialogueManager>().SetDialogue("You try the hatch of the Tesseract class ship. To your surprise it opens. You step inside.\n\nThe main cabin is empty and dark. You move forward, squeezing into the cramped cockpit, to find the LUX-3 model at the controls.\n\n'Imagine...an unplanned docking systems maintenance!' LUX-3 looks at you knowingly.\n\n'Ready to shove off?'");
            FindObjectOfType<DialogueManager>().SetControlPanel("Join the trip", "Exit the ship");
        }
        else {
            dialogue = "A glowing red eyeball scans your retina.\n\nUNKNOWN IDENTITY. ACCESS DENIED\n\nYou kick the docking clamp on the tesseract class vessel and move away.";
            FindObjectOfType<DialogueManager>().SetDialogue(dialogue);
        }
	}

    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            bool docking = GameControl.control.dockingReleased;
            string botty = GameControl.control.botty_state;
            if (docking == true && botty == "3") {
                if (Event.current.Equals(Event.KeyboardEvent("return")) || Event.current.Equals(Event.KeyboardEvent("[enter]"))) {
                    if (EventSystem.current.currentSelectedGameObject.name == "Talk") {
                        SceneManager.LoadScene("zed");
			    	}
                    else {
                        end();
                    }
                }
            }
            else {
                end();
            }
        }
    }
}