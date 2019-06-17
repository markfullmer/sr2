using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ManchiPatron : NPC {

    public void interact() {
        GameControl.control.barkeep_state = "manchi";
        FindObjectOfType<DialogueManager>().closeControlPanel();
        int choice = Random.Range(0, 5);
        List<string> iList = new List<string>();
        iList.Add("The insectoid alien appears to make eye contact with you before moving away...");
        iList.Add("At your approach, the Manchi shuffles away, making those hideous clicking sounds.");
        iList.Add("'Xhad..kclx kclx zkkkkk.'\n\nYou don't speak Manchi, and can't tell whether you were just insulted or greeted. You move away.");
        iList.Add("A Manchi -- with an Imperium chevron on its ident plate. Who'd've imagined the Manchi would one day be citizens of the Imperium?");
        iList.Add("Manchi living this deep in the heart of the Far Arm, you reflect.\n\nOf course, it's thirty years since you had shown the Manchi raids to be a plot by Admiral Koth. Still, you wouldn't have imagined them ever on Hiathra station...");
        FindObjectOfType<DialogueManager>().SetDialogue(iList[choice]);
        StartCoroutine(actionWarmUp(0.6f));
	}

    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            FindObjectOfType<DialogueManager>().exit();
            isInteracting = false;
        }
    }
}