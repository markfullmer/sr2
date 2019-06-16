using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ManchiWorker : NPC {

    public void interact() {
        FindObjectOfType<DialogueManager>().closeControlPanel();
        int choice = Random.Range(0, 5);
        List<string> iList = new List<string>();
        iList.Add("The insectoid alien, busy moving a thermo tank, pauses. It appears to make eye contact with you before moving away...");
        iList.Add("At your approach, the Manchi shuffles away, making those hideous clicking sounds.\n\nSo many Manchi workers, you think to yourself. But then, Manchi labor is cheaper than bots...");
        iList.Add("'Xhad..kclx kclx zkkkkk.'\n\nPleased to meet you, too, you mutter under your breath.");
        iList.Add("The laborer appears to be busy taking inventory. It seems to ignore your presence.");
        iList.Add("The Manchi worker moves away quickly. If you could read insectoid emotions, you'd almost guess the bugger looked afraid.");
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