using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Orellian : NPC {

	private bool isInteracting;
	private GameObject selected;

    public void interact() {
		selected = GameObject.Find("Button2");
		EventSystem.current.SetSelectedGameObject(selected);
        isInteracting = true;
        FindObjectOfType<DialogueManager>().InitiateUI();
        FindObjectOfType<DialogueManager>().StartDialogue("", "", "");
        triggerResponse("0");
	}

    void Update()
    {
        if (animator.GetBool("uiActive") && isInteracting && Input.anyKey) {
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

    void triggerResponse(string reply) {
        string state = GameControl.control.orellian_state;
        if (state == "0" && reply == "0") {
            Debug.Log("State is 0, reply is 0");
            FindObjectOfType<DialogueManager>().UpdateDialogue("You see a distinguished looking gentleman with silver hair and a winning smile. 'Welcome, Citizen,' he says, 'I am Orellian.' Then he pauses, eyes widening with recognition.\n\n'It's...it's you! The savior of the Imperium!'\n\nOrellian hesitates when you don't immediately answer. 'I mean...it,..It IS you, is it not?'", "Yes, it's me.", "You're thinking of someone else.");
        }
        else if (state == "0") {
            if (reply == "1") {
                GameControl.control.orellian_state = "friends";
                FindObjectOfType<DialogueManager>().StartDialogue("'Oh, it's such an honour to have you here on Hiathra again, after these many years. I've read all about your exploits. Would you mind, though, telling me more about the journey to the Manchi Homeworld?'", "Tell Orellian a story", "Leave, quickly");
            }
            else if (reply == "2") {
                GameControl.control.orellian_state = "stranger";
                FindObjectOfType<DialogueManager>().StartDialogue("'Oh,' Orellian says, disappointed. You sure look like the posters. Ah well. Now, how may I help you, citizen?", "Pilot's License", "Leave");
            }
        }
        else if (state == "friends") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().StartDialogue(getDialogueIntro() + "'You wouldn't mind telling me one of your adventures in the Far Arm, would you?'", "Tell Orellian a story", "Leave, quickly");                
            }
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().StartDialogue("You regale the diplomat with a story of your exploits. When you finish, Orellian gleams, shakes your hand.\n\n 'Next time you're at Hiathra, do stop by and say hello. If I'm not in this office, check the Administrative level. Oh...and you'll need the turbolift code 3624.'", "0", "Leave");
                GameControl.control.orellian_state = "turbolift_access";
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().EndDialogue();
            }            
        }
        else if (state == "stranger") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().StartDialogue(getDialogueIntro() + "'Now, how may I help you, citizen?'", "Pilot's License", "Leave");
            }
            if (reply == "1") {
                FindObjectOfType<DialogueManager>().StartDialogue("Ah, a standard Pilot's license...Now let's see...", "0", "Leave");
                state = "turbolift_access";
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().EndDialogue();
                state = "2";
            }            
        }
        else if (state == "turbolift_access") {
            if (reply == "0") {
                FindObjectOfType<DialogueManager>().StartDialogue("Orellian gleams, shakes your hand.\n\n 'Next time you're at Hiathra, do stop by and say hello. If I'm not in this office, check the Administrative level. Oh...and you'll need the turbolift code 3624.'", "0", "Leave");
            }
            else if (reply == "2") {
                FindObjectOfType<DialogueManager>().EndDialogue();    
            }
  
        }
    }

}