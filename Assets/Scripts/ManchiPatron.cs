﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ManchiPatron : NPC {

    public void interact() {
        GameControl.control.barkeep_state = "manchi";
        int state = GameControl.control.patron_convo;
        FindObjectOfType<DialogueManager>().closeControlPanel();
        List<string> iList = new List<string>();
        iList.Add("A Manchi...with an Imperium chevron ident! Most of the Imperium still consider Manchi too... emotionless ... to be citizens. Or is the opposition something else?\n\nFear? And if fear...what kind of fear?");
        iList.Add("Manchi living this deep in the heart of the Far Arm.\n\nOf course, it's thirty years since the end of the Manchi wars. Still, who'd've imagined them on Hiathra Station...");
        iList.Add("The insectoid seems briefly to make eye contact before click-clicking away.");
        iList.Add("At your approach, the Manchi shuffles away, making those hideous clicking sounds.");
        iList.Add("'Xhad..kclx kclx zkkkkk.'\n\nWas that a greeting...or an insult? Eh, let buggers be buggers.");
        FindObjectOfType<DialogueManager>().SetDialogue(iList[state]);
        if (state >= 4) {
            state = 0;
        }
        else {
            state = state + 1;
        }
        GameControl.control.patron_convo = state;
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