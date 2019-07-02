using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Sign : NPC {

    public void interact(string name) {
        string dialogue = "Nothing here";
        if (name == "SignAccidents") {
            dialogue = "POSTED\n\n------\n\nDAYS WITHOUT AN ACCIDENT: 3";
        }
        else if (name == "SignEAccess") {
            dialogue = "This month's access codes:\n\nTurbolift: ex-parrot\n\nLife Support: 070886\n\nP.A.: 36742\n\n\n\nReminder: do NOT share with anyone outside Engineering.";
        }
        else if (name == "SignMain") {
            dialogue = "DOCKING BAY 3\n\n-------------\n\nWARNING: MOVING VEHICLES. PLEASE EXERCISE ALL APPROPRIATE SAFETY PRECAUTION";
        }
        else if (name == "SignMainFrame") {
            dialogue = "You're looking at an archaic T-MUX mainframe. With a few keystrokes, you're able to open a file stored in the temp filesystem:\n\nSTATION_ACCOUNTING.PHB\n\nA perusal of the file shows Hiathra Station profits increasing dramatically. Looks like it's due mostly to tariff and customs revenue, and asteroid-cheap labour wages.";
        }
        else if (name == "SignTV") {
            dialogue = "A public-access talking head fills the vidscreen. The commentator seems to be discussing something related to the Manchi flu...its transmission rates...p-values.\n\nYou can't follow the point very well...";
        }
        else if (name == "SignShipping") {
            dialogue = "A handheld tabulator lays, discarded, atop one of the cargo units. You scan it briefly.\n\nIt appears to be a record of cargo manifest transfers with dates, weights, and values and ownerships.";
        }
        else if (name == "SignCrew") {
            dialogue = "One of the drawers of this residential storage cabinet is unlocked.\n\nInside, there is a leisure garment, some accoutrements, and a standard-issue sani mask.";
        }
        else if (name == "SignObservation") {
            dialogue = "OBSERVATION DECK\n\n--------------\n\nENJOY OUR UNPARALLELED VIEW OF THE UNIVERSE";
        }
        else if (name == "SignDesert") {
            dialogue = "ARBOR\n\n-----\n\nTHIS AREA IS A BIOLOGICAL REPRODUCTION OF LIFE ON THE MANCHI HOMEWORLD.\n\nEXHIBIT RUNS TILL AUG 12";
        }
        else if (name == "SignPC") {
            dialogue = "Whomever owns this computer, they left it on. The screen shows the vintage game HIVE, paused on Level 25. You recall that HIVE was discontinued after the TRC labelled it discriminatory to the Manchi species.";
        }
        else if (name == "SignCabinet") {
            dialogue = "You try the drawers in this cabinet. One of them slides open, revealing a stash of vac-sealed candy bars. You help yourself to a caramel Red Dwarf bar.";
        }
        FindObjectOfType<DialogueManager>().closeControlPanel();
        FindObjectOfType<DialogueManager>().SetDialogue(dialogue);
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