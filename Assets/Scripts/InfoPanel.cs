using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    private int state = 1;
    private InfoPanel panel;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<InfoPanel>();
        text.text = "\n\nDocking Privileges at Hiathra Granted.\n\n WARNING! Due to an outbreak of Manchi Flu, this station is under quarantine. NO DOCKED SHIPS ARE PERMITTED TO LEAVE.";
    }

    // Update is called once per frame
    void Update() {
        if (state == 1 && Input.anyKey) {
            panel.gameObject.SetActive (false);      
            state = 2;
        }                              
    }
}
