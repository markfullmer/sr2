using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public Text button1;
	public Text button2;
    public Animator animator;
	private InputField input;

	public void PromptInput(string text) {
		EventSystem.current.SetSelectedGameObject(null);
		dialogueText.text = text;
		StartCoroutine(actionWarmUpPrompt(0.5f));
        input = GameObject.Find("Textfield").GetComponent<InputField>();
		input.text = "";
	}

	public void StartDialogue (string startText, string b1, string b2)
	{
		StartCoroutine(actionWarmUp(0.3f));
		dialogueText.text = startText;
		button1.text = b1;
		button2.text = b2;
	}

	public void UpdateDialogue (string newText, string b1, string b2)
	{
		StartCoroutine(actionWarmUp(0.3f));
		dialogueText.text = newText;
		button1.text = b1;
		button2.text = b2;
	}

    private IEnumerator actionWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		animator.SetBool("uiActive", true);
    }

    private IEnumerator actionWarmUpPrompt(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		animator.SetBool("uiActive", true);
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Textfield"));
    }

    private IEnumerator actionCooldown(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		EventSystem.current.SetSelectedGameObject(null);
		animator.SetBool("uiActive", false);
    }

	public void EndDialogue() {
		StartCoroutine(actionCooldown(0.3f));
	}

}