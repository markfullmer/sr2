using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Orellian : NPC {

	private GameObject selected;
    public GameObject guard;

    new void Start() {
        base.Start(); // Load tilemaps, etc.
        if (GameControl.control.orellian_state == "turbolift_access") {
            guard.gameObject.SetActive (false);
        }  
    }

    public void interact() {
		selected = GameObject.Find("Button1");
		EventSystem.current.SetSelectedGameObject(selected);
        FindObjectOfType<DialogueManager>().openInputPanel();
        triggerResponse("0");
	    StartCoroutine(actionWarmUp(0.6f));
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

    void triggerResponse(string reply) {
        string state = GameControl.control.orellian_state;
        if (state == "0" && reply == "0") {
            FindObjectOfType<DialogueManager>().SetDialogue("You see a distinguished looking gentleman with silver hair and a winning smile. 'Welcome, Citizen,' he says, 'I am Orellian.' Then he pauses, eyes widening with recognition.\n\n'It's...it's you! The savior of the Imperium!'\n\nOrellian hesitates when you don't immediately answer. 'I mean...it,..It IS you, is it not?'");
            FindObjectOfType<DialogueManager>().SetReplies("You're thinking of someone else.", "Yes, it's me.");
        }
        else if (state == "0") {
            if (reply == "2") {
                GameControl.control.orellian_state = "friends";
                FindObjectOfType<DialogueManager>().SetDialogue("'Oh, it's such an honour to have you here on Hiathra again, after these many years. I've read all about your exploits. Would you mind, though, telling me more about the journey to the Manchi Homeworld?'");
                FindObjectOfType<DialogueManager>().SetReplies("Tell Orellian a story.", "Leave...quickly");
            }
            else if (reply == "1") {
                GameControl.control.orellian_state = "stranger";
                FindObjectOfType<DialogueManager>().SetDialogue("'Oh,' Orellian says, disappointed. You sure look like the posters. Ah well. Now, how may I help you, citizen?");
                FindObjectOfType<DialogueManager>().SetReplies("Pilot's License", "Leave");
            }
        }
        else if (state == "friends") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue(getDialogueIntro() + "'You wouldn't mind telling me one of your adventures in the Far Arm, would you?'");
                FindObjectOfType<DialogueManager>().SetReplies("Tell Orellian a story", "Leave, quickly");                
            }
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().SetDialogue("You regale the diplomat with a story of your exploits, only half-heartedly trying to conceal your boredom. Or is it some feeling other than boredom...?\n\nWhen you finish, Orellian gleams and shakes your hand, almost violently.\n\n 'Next time you're at Hiathra, do stop by and say hello. If I'm not in this office, check the Administrative level. Oh...and you'll need the turbolift code 3264.'");
                FindObjectOfType<DialogueManager>().SetReplies("Thank Orellian", "Leave");
                GameControl.control.orellian_state = "turbolift_access";
                guard.gameObject.SetActive (false);
            }
            else if (reply == "2") {
                end();
            }            
        }
        else if (state == "stranger") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue(getDialogueIntro() + "'Now, how may I help you, citizen?'");
                FindObjectOfType<DialogueManager>().SetReplies("Pilot's License", "Leave");
            }
            else if (reply == "1") {
                GameControl.control.orellian_state = "pilots license";
                FindObjectOfType<DialogueManager>().SetDialogue("Ah, a standard Pilot's license...If you'll just step over here for a DNA read, I can get the filing started.");
                FindObjectOfType<DialogueManager>().SetReplies("Consent to DNA test", "Leave");
            }
            else {
                GameControl.control.orellian_state = "stranger";
                end();
            }            
        }
        else if (state == "pilots license") {
            if (reply == "1") {
                GameControl.control.orellian_state = "friends";
                FindObjectOfType<DialogueManager>().SetDialogue("Orellian idly taps his finger against the side of his trasella as the DNA scan completes. After a moment, you here a beep from the vidscreen, and then a gasp from the diplomat.\n\n 'See! I knew it was you! Oh, it's such an honour to have you here on Hiathra again, after these many years. I've read all about your exploits. Would you mind, though, telling me more about the journey to the Manchi Homeworld?'");
                FindObjectOfType<DialogueManager>().SetReplies("Tell Orellian a story.", "Leave...quickly");
            }
            else {
                GameControl.control.orellian_state = "stranger";
                end();      
            }
        }
        else if (state == "turbolift_access") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().SetDialogue("Orellian gleams, shakes your hand.\n\n 'Next time you're at Hiathra, do stop by and say hello. If I'm not in this office, check the Administrative level. Oh...and you'll need the turbolift code 3264.'");
                FindObjectOfType<DialogueManager>().SetReplies("Thank Orellian", "Leave");
            }
            else  {
                end();
            }
  
        }
    }

    string getDialogueIntro() {
        string state = GameControl.control.orellian_state;
        int choice = Random.Range(0, 2);
        if (state == "friends" || state == "turbolift_access") {
            List<string> iList = new List<string>();
            iList.Add("Orellian glances up from a navigational star chart. As his eyes settle on your face, he visibly brightens.\n\n");
            iList.Add("As the diplomat hears you approach he quickly powers down an infopad. He looks up, his face radiating an innocent smile.\n\n ");
            iList.Add("'Now, citizen, what may I do for...' Orellian pauses in mid-sentence. 'Ah, returning so soon!'\n\n");
            return iList[choice];
        }
        if (state == "stranger") {
            List<string> iList = new List<string>();
            iList.Add("Orellian momentarily brightens at seeing your face, but his smile quickly fades as he remembers you are not who he thought.\n\n");
            iList.Add("'Every time I see you, I swear you look just like the hero of the Imperium,' Orellian muses.\n\n ");
            iList.Add("At the sound of you approaching, Orellian quickly shoves something into his desk drawer.\n\n");
            return iList[choice];
        }
        return "";
    }

}