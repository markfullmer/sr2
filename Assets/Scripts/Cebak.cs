using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Cebak : NPC {

	private GameObject selected;

    void triggerResponse(string reply) {
        string state = GameControl.control.cebak_state;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Hiathra_main_floor") {
            mainFloor(reply, state);
        }
        else {
            observationDeck(reply, state);
        }
    }

    void observationDeck(string reply, string state) {
        if (state == "drinks" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("So Cebak hadn't been lying about meeting on the observation deck. There she is, acknowledging you with an all-too-intense gaze.\n\nSomething tells you she isn't just interested in drinks.\n\nTo confront or not to confront...");
            FindObjectOfType<DialogueManager>().SetReplies("So! Drinks...", "Out with it. Why'd you really want to meet?");
        }
        else if (state == "drinks") {
            GameControl.control.cebak_state = "4";
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("'Indeed.' Cebak saunters over to the bar and orders two Rigelian ales, then turns back to you, her face innocent and carefree.\n\n'You were saying you knew of my sister on Lagrange?'");
                FindObjectOfType<DialogueManager>().SetReplies("Make up a story about Cebak's sister", "Change the subject");
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().SetDialogue("'For Bok's sake! Sometimes a drink it just a drink! Regale me with some of your travels?'");
                FindObjectOfType<DialogueManager>().SetReplies("Tell a story about trading in Nar'see sector", "Insist Cebak has an ulterior agenda");
            }
        }
        else if (state == "4") {
            if (reply == "1") {
                GameControl.control.cebak_state = "5";
                FindObjectOfType<DialogueManager>().SetDialogue("Cebak listens, her face an indecipherable enigma. You can't tell if her mind is somewhere else or she finds your story less than riveting. After a pause, she stares into her drink.\n\n'I'd called myself a station barnacle before. It might be nice to travel elsewhere...'");
                FindObjectOfType<DialogueManager>().SetReplies("Suggest a passenger freighter for travel", "Offer to take Cebak on your ship");
            }
            else if (reply == "2") {
                GameControl.control.cebak_state = "5";
                FindObjectOfType<DialogueManager>().SetDialogue("Cebak seems unphased by your deliberate attempt to redirect the conversation. She laughs gently.\n\n'You know, I've actually been wanting to get off this station for awhile now. Places to go, things to see...'");
                FindObjectOfType<DialogueManager>().SetReplies("Discuss the quarantine on the station", "Discuss other star systems");
            }         
        }
        else if (state == "5") {
            GameControl.control.cebak_state = "6";
            FindObjectOfType<DialogueManager>().SetDialogue("A flicker of understanding crosses Cebak's face. She downs the rest of the Rigelian ale.\n\n'Perhaps it is time for me to travel. Your ship perhaps has room for a lowly station barnacle? Adventures deep into the Far Arm?'");                
            FindObjectOfType<DialogueManager>().SetReplies("Fortune and glory. If I can get my ship operational yes", "No, my ship doesn't have room");
        }
        else if (state == "6") {
            GameControl.control.cebak_state = "join";
            FindObjectOfType<DialogueManager>().SetDialogue("At your response, Cebak abruptly changes the subject. She got the answer she needed, it seems, or at least she has a read on you.\n\nCebak blathers on about Hiathra station for a moment, and the poor plight of the Manchi labourers, but her mind seems elsewhere. After downing another Rigelian ale, she sets her tumbler down.\n\n'Be seeing you around the station, then?'");                
            FindObjectOfType<DialogueManager>().SetReplies("Say goodbye pleasantly", "Walk away rudely");
        }
        else if (state == "join") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue("Cebak is eyeing the bar, clearly deciding whether or not to have another Rigelian ale.\n\n'You know, if you are planning to leave the station, I might be persuaded to come along.' \n\nCebak winks. 'Think about it.'\n\nShe turns back to the bar and orders a drink.");
                FindObjectOfType<DialogueManager>().SetReplies("Leave");
            }
            else {
                end();
            }
    
        }
    }

    void mainFloor(string reply, string state) {
        if (state == "0" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("In the middle of the luxuriously carpeted station rec room stands a woman. She appears to be pouring over a manifest of some sort on a vid screen.");
            FindObjectOfType<DialogueManager>().SetReplies("Have we met before? You look familiar", "Been on the station long?");
        }
        else if (state == "0") {
            GameControl.control.cebak_state = "1";
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("The woman takes her eyes off the vid screen and gazes up, softly.\n\n'I don't believe we're acquainted. I'm Cebak.'");
                FindObjectOfType<DialogueManager>().SetReplies("And you have a sister...named Tiwa!", "I'm a...merchant. Just hopping through Malir gates.");
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().SetDialogue("'Lived here about all my life! I'm a station barnacle. No interest -- no reason -- to travel out of sector. Plus, the Malir gates give me the creeps.'");
                FindObjectOfType<DialogueManager>().SetReplies("I hear Nar'see is a boom sector right now.", "Malir gates...the only way to travel");
            }
        }
        else if (state == "1") {
            if (reply == "1") {
                GameControl.control.cebak_state = "friendly";
                FindObjectOfType<DialogueManager>().SetDialogue("'Yes, I have a sister on Lagrange station in Nar'see,' Cebak nods. She seems a bit taken aback.\n\n'She's more the traveler than me. You know her?'");
                FindObjectOfType<DialogueManager>().SetReplies("Yes, we're business acquaintances", "No, not directly. She's...a friend of a friend.");
            }
            else if (reply == "2") {
                GameControl.control.cebak_state = "suspicious";
                FindObjectOfType<DialogueManager>().SetDialogue("Cebak's eyes narrow, and for a brief moment you have the utter conviction that this woman is more than she is letting on.\n\n'Yes, the Imperium relies on...and exploits...the Malir gates, regulating who and what can travel through near space.");
                FindObjectOfType<DialogueManager>().SetReplies("A pox on the Imperium, eh?", "Somebody's got to regulate things, what with the Manchi here");
            }         
        }
        else if (state == "suspicious") {
            GameControl.control.cebak_state = "drinks";
            FindObjectOfType<DialogueManager>().SetDialogue("The brief hint of deeper intent is gone from the woman's face. She laughs as if the conversation had been nothing but light banter.\n\n'Indeed! All the better reason to stay here on Hiathra. Have a drink with me later? The observation deck?'");                
            FindObjectOfType<DialogueManager>().SetReplies("Definitely. See you there.", "If I can make it, sure.");
        }
        else if (state == "friendly") {
            GameControl.control.cebak_state = "drinks";
            FindObjectOfType<DialogueManager>().SetDialogue("'Well, I'd love to hear more about it. It's been ages since I spoke to my sister.'For the briefest of moments you see something flicker across Cebak's face, something that suggests there are more levels to this conversation than it seems. As quickly as it appeared, the look is gone.\n\n'Have a drink with me later? The observation deck?'");
            FindObjectOfType<DialogueManager>().SetReplies("Definitely. See you there.", "If I can make it, sure.");
        }
        else if (state == "drinks") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue("Cebak looks up at your approach.\n\n'Oh! We were going to have drinks on the observation deck. Meet you there in a few?'\n\nYou're left wondering whether Cebak's offer is, or ever was, sincere.");
                FindObjectOfType<DialogueManager>().SetReplies("Leave");
            }
            else {
                end();
            }
    
        }
    }

    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
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

    public void interact() {
		selected = GameObject.Find("Button1");
		EventSystem.current.SetSelectedGameObject(selected);
        FindObjectOfType<DialogueManager>().openInputPanel();
        triggerResponse("0");
	    StartCoroutine(actionWarmUp(0.6f));
	}
}
