using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour {

	public Text dialogueText;
	public InputField inputField;
	public GameObject inputPanel;
	public GameObject infoPanel;
	public GameObject customInput;
	public GameObject controlPanel;
	public Text buttonText1;
	public Text buttonText2;
	public Text buttonText3;
	public Text buttonText4;
	public Selectable button1;
	public Selectable button2;
	public Selectable button3;
	public Selectable button4;	
	public Selectable reply1;
	public Selectable reply2;
	public Selectable textInput;
	public Text replytext1;
	public Text replytext2;

    void Start()
    {
        inputPanel.gameObject.SetActive (false);
		infoPanel.gameObject.SetActive (false);
		controlPanel.gameObject.SetActive (false);
    }

	public void openControlPanel() {
		StartCoroutine(controlPanelWarmUp(0.3f));
	}

	public void openInputPanel() {
		StartCoroutine(inputPanelWarmUp(0.3f));
	}

    private IEnumerator controlPanelWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		GameControl.control.isControlPanel = true;
		buttonText1.text = "Talk";
		buttonText2.text = "Inspect";
		buttonText3.text = "Status";
		buttonText4.text = "Done";
		controlPanel.gameObject.SetActive (true);
        inputPanel.gameObject.SetActive (false);
		infoPanel.gameObject.SetActive (false);
		button1.Select();
    }

    private IEnumerator inputPanelWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		GameControl.control.isControlPanel = false;
		controlPanel.gameObject.SetActive (false);
        inputPanel.gameObject.SetActive (true);
		infoPanel.gameObject.SetActive (true);
		reply1.Select();
    }

	public void closeControlPanel() {
		GameControl.control.isControlPanel = false;
		controlPanel.gameObject.SetActive (false);
	}

	public void exit() {
		StartCoroutine(actionCloseDown(0.3f));
	}

    private IEnumerator actionCloseDown(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		EventSystem.current.SetSelectedGameObject(null);
		inputPanel.gameObject.SetActive (false);
		infoPanel.gameObject.SetActive (false);
		customInput.gameObject.SetActive (false);
		controlPanel.gameObject.SetActive (false);
		GameControl.control.isControlPanel = false;
		GameControl.control.playerInteracting = false;
    }

	public void DisableReplies() {
		reply1.interactable = false;
		reply2.interactable = false;
	}

	public void PromptInput() {
		EventSystem.current.SetSelectedGameObject(null);
		StartCoroutine(actionWarmUpPrompt(0.5f));
	}

	public void PromptCustomInput() {
		EventSystem.current.SetSelectedGameObject(null);
		StartCoroutine(actionWarmUpCustomPrompt(0.5f));
	}

	public void SetControlPanel (string b1 = null, string b2 = null, string b3 = null, string b4 = null)
	{
		buttonText1.text = b1;
		buttonText2.text = b2;
		buttonText3.text = b3;
		buttonText4.text = b4;
	}

	public void SetReplies (string b1 = null, string b2 = null)
	{
		StartCoroutine(actionWarmUp(0.3f));
		replytext1.text = b1;
		replytext2.text = b2;
	}

	public void SetDialogue (string newText) {
		StartCoroutine(infoWarmUp(0.4f));
		dialogueText.text = newText;
	}

    private IEnumerator infoWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		infoPanel.gameObject.SetActive (true);
    }

    private IEnumerator actionWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		GameControl.control.playerInteracting = true;
		infoPanel.gameObject.SetActive (true);
		reply1.Select();
    }

    private IEnumerator actionWarmUpPrompt(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		GameControl.control.playerInteracting = true;
		inputPanel.gameObject.SetActive (true);
		infoPanel.gameObject.SetActive (true);
		textInput.interactable = true;
		inputField.text = "";
		textInput.Select();
    }

    private IEnumerator actionWarmUpCustomPrompt(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		GameControl.control.playerInteracting = true;
		customInput.gameObject.SetActive (true);
		inputPanel.gameObject.SetActive (false);
		infoPanel.gameObject.SetActive (true);
		textInput.interactable = true;
		inputField.text = "";
		textInput.Select();
    }

    private IEnumerator actionCooldown(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		EventSystem.current.SetSelectedGameObject(null);
		inputPanel.gameObject.SetActive (false);
		infoPanel.gameObject.SetActive (false);
		controlPanel.gameObject.SetActive (false);
		GameControl.control.isControlPanel = false;
		GameControl.control.playerInteracting = false;
    }

	public void EndDialogue() {
		StartCoroutine(actionCloseDown(0.3f));
	}

}