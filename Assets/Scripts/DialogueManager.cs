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
	public GameObject controlPanel;
	public GameObject customInput;
	public GameObject merchPanel;
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
	public Text merchText1;
	public Text merchText2;
	public Text merchText3;
	public Text merchText4;
	public Text merchText5;
	public Text merchText6;
	public Text merchResponse;
	public Selectable merch1;
	public Selectable merch2;
	public Selectable merch3;
	public Selectable merch4;
	public Selectable merch5;
	public Selectable merch6;

    void Start()
    {
        inputPanel.gameObject.SetActive (false);
		infoPanel.gameObject.SetActive (false);
		merchPanel.gameObject.SetActive (false);
		controlPanel.gameObject.SetActive (false);
		customInput.gameObject.SetActive (false);
    }

	public void openControlPanel() {
		StartCoroutine(controlPanelWarmUp(0.3f));
	}

	public void openInputPanel() {
		StartCoroutine(inputPanelWarmUp(0.3f));
	}

	public void openMerchPanel() {
		StartCoroutine(merchPanelWarmUp(0.3f));
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

    private IEnumerator merchPanelWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		GameControl.control.isMerchPanel = true;
		merchPanel.gameObject.SetActive (true);
		controlPanel.gameObject.SetActive (true);
        inputPanel.gameObject.SetActive (false);
		infoPanel.gameObject.SetActive (false);
		merch1.Select();
    }

    private IEnumerator inputPanelWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		GameControl.control.isControlPanel = false;
		controlPanel.gameObject.SetActive (false);
		customInput.gameObject.SetActive (false);
        inputPanel.gameObject.SetActive (true);
		infoPanel.gameObject.SetActive (true);
		reply1.Select();
    }

	public void closeControlPanel() {
		GameControl.control.isControlPanel = false;
		controlPanel.gameObject.SetActive (false);
	}

	public void closeInputPanel() {
		GameControl.control.isControlPanel = false;
		inputPanel.gameObject.SetActive (false);
	}

	public void exit() {
		StartCoroutine(actionCloseDown(0.3f));
	}

	public void DisableReplies() {
		reply1.interactable = false;
		reply2.interactable = false;
	}

	public void PromptInput() {
		EventSystem.current.SetSelectedGameObject(null);
		StartCoroutine(actionWarmUpPrompt(0.5f));
	}

	public void SetControlPanel (string b1 = null, string b2 = null, string b3 = null, string b4 = null)
	{
		controlPanel.gameObject.SetActive (true);
		button1.Select();
		if (b1 != null) {
			button1.gameObject.SetActive (true);
			buttonText1.text = b1;
		}
		else {
			button1.gameObject.SetActive (false);
		}
		if (b2 != null) {
			button2.gameObject.SetActive (true);
			buttonText2.text = b2;
		}
		else {
			button2.gameObject.SetActive (false);
		}
		if (b3 != null) {
			button3.gameObject.SetActive (true);
			buttonText3.text = b3;
		}
		else {
			button3.gameObject.SetActive (false);
		}
		if (b4 != null) {
			button4.gameObject.SetActive (true);
			buttonText4.text = b4;
		}
		else {
			button4.gameObject.SetActive (false);
		}
	}

	public void setBuy (string m1 = null, string m2 = null, string m3 = null, string m4 = null, string m5 = null, string m6 = null)
	{
		if (m1 != null) {
			merch1.gameObject.SetActive (true);
			merchText1.text = m1;
		}
		else {
			merch1.gameObject.SetActive (false);
		}
		if (m2 != null) {
			merch2.gameObject.SetActive (true);
			merchText2.text = m2;
		}
		else {
			merch2.gameObject.SetActive (false);
		}
		if (m3 != null) {
			merch3.gameObject.SetActive (true);
			merchText3.text = m3;
		}
		else {
			merch3.gameObject.SetActive (false);
		}
		if (m4 != null) {
			merch4.gameObject.SetActive (true);
			merchText4.text = m4;
		}
		else {
			merch4.gameObject.SetActive (false);
		}
		if (m5 != null) {
			merch5.gameObject.SetActive (true);
			merchText5.text = m5;
		}
		else {
			merch5.gameObject.SetActive (false);
		}
		if (m6 != null) {
			merch6.gameObject.SetActive (true);
			merchText6.text = m6;
		}
		else {
			merch6.gameObject.SetActive (false);
		}
		openMerchPanel();
	}

	public void setMerchResponse(string text = null) {
		merchResponse.text = text;
	}

	public void setSell (string m1 = null, string m2 = null, string m3 = null, string m4 = null, string m5 = null, string m6 = null)
	{
		if (m1 != null) {
			merch1.gameObject.SetActive (true);
			merchText1.text = m1;
		}
		else {
			merch1.gameObject.SetActive (false);
		}
		if (m2 != null) {
			merch2.gameObject.SetActive (true);
			merchText2.text = m2;
		}
		else {
			merch2.gameObject.SetActive (false);
		}
		if (m3 != null) {
			merch3.gameObject.SetActive (true);
			merchText3.text = m3;
		}
		else {
			merch3.gameObject.SetActive (false);
		}
		if (m4 != null) {
			merch4.gameObject.SetActive (true);
			merchText4.text = m4;
		}
		else {
			merch4.gameObject.SetActive (false);
		}
		if (m5 != null) {
			merch5.gameObject.SetActive (true);
			merchText5.text = m5;
		}
		else {
			merch5.gameObject.SetActive (false);
		}
		if (m6 != null) {
			merch6.gameObject.SetActive (true);
			merchText6.text = m6;
		}
		else {
			merch6.gameObject.SetActive (false);
		}
		openMerchPanel();
	}

	public void SetReplies (string b1 = null, string b2 = null)
	{
		StartCoroutine(actionWarmUp(0.3f));
		reply1.interactable = true;
		reply2.interactable = true;
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
		customInput.gameObject.SetActive (false);
		inputPanel.gameObject.SetActive (true);
		infoPanel.gameObject.SetActive (true);
		reply1.Select();
    }

    private IEnumerator actionWarmUpPrompt(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		GameControl.control.playerInteracting = true;
		inputPanel.gameObject.SetActive (false);
		customInput.gameObject.SetActive (true);
		infoPanel.gameObject.SetActive (true);
		textInput.interactable = true;
		inputField.text = "";
		textInput.Select();
    }

	public void EndDialogue() {
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
		merchPanel.gameObject.SetActive (false);
		GameControl.control.isControlPanel = false;
		GameControl.control.isMerchPanel = false;
		GameControl.control.playerInteracting = false;
    }

}