using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public bool uiActive = true;
    private bool start = true;
    private InfoPanel panel;
    public Text text;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("uiActive", true);
        text.text = "\n\nDocking Privileges at Hiathra Granted.\n\nWARNING! Due to an outbreak of Manchi Flu, this station is under quarantine.\n\nNO DOCKED SHIPS ARE PERMITTED TO LEAVE.";
    }

    // Update is called once per frame
    void Update() {
        if (start && Input.anyKey) {
            text.text = "";  
            start = false;
            animator.SetBool("uiActive", false);
        }                              
    }
}
