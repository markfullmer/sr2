using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Barkeep : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().SetDialogue("The barkeep ambles over to the counter and says cheerfully: 'You look thirsty, what can I get ya?'");
        FindObjectOfType<DialogueManager>().SetControlPanel("Green Head", "Grog", "Rigelian", "Done");
	    StartCoroutine(actionWarmUp(0.6f));
	}
    void OnGUI() {
        Event e = Event.current;
        if (isInteracting && e.isKey) {
            Debug.Log(EventSystem.current.currentSelectedGameObject);
            if (Event.current.Equals(Event.KeyboardEvent("return")) || Event.current.Equals(Event.KeyboardEvent("[enter]"))) {
                if (EventSystem.current.currentSelectedGameObject.name == "Talk") {
                    GameControl.control.credits = GameControl.control.credits - 2;
                    FindObjectOfType<DialogueManager>().SetDialogue("Here you go. Another one buddy?");
				}
                else if (EventSystem.current.currentSelectedGameObject.name == "Inspect") {
                    FindObjectOfType<DialogueManager>().SetDialogue("Here you go. That stuff's mostly water. Another one buddy?");
                    GameControl.control.credits = GameControl.control.credits - 1;
				}
                else if (EventSystem.current.currentSelectedGameObject.name == "Status") {
                    FindObjectOfType<DialogueManager>().SetDialogue(getGossip());
                    GameControl.control.credits = GameControl.control.credits - 5;
				}
                else {
                    end();
                }
            }
        }
    }

    string getGossip() {
        string state = GameControl.control.barkeep_state;
        
        if (state == "manchi") {
            return "It's not my business, but I seen you chatting with that bugger. You better not be one of them bug-lovers.";
        }
        else if (state == "escape") {
            return "Rumor has it you're working on a way off this station. If you've got room for one more, I'll pay. Anything you ask.";
        }
        else {
            int choice = Random.Range(0, 6);
            List<string> iList = new List<string>();
            iList.Add("Business just ain't what it usedta, what with these buggers everywhere. Buggers don't drink. Buggers don't gamble. For krysaa, they ain't even interested in anagathics.");
            iList.Add("Suppose you heard about Avenstar? 4,340 InterGal seats lost this election. Meaning, she gotta step down. Parliament may leave Deneb System entirely.\n\nAfter 30-odd years, it's about time, says I.");
            iList.Add("Sorry about the smell. It's cause this damn bugger flu. Station doctor ordered daily sani-sweeps.");
            iList.Add("Know what I saw the other day? A Sishaz eyeing a bugger. Told me the bugger had a shapely thorax.\n\nDisgusting.");
            iList.Add("If you're a Guild runner, worst luck, eh? Customs proscribed sale of just about anything organic.\n\nIt hasn't helped the miners, neither, cause merchers are selling the mineral dirt cheap.");
            iList.Add("You know, I've really been enjoying the poems of William Wordsworth lately.\n\n'The things which I have seen I now can see no more...'");
            return iList[choice] + "\n\nAnother one?";
        }
    }
}