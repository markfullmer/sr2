using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    // Update is called once per frame
    void Update() {
        gameObject.GetComponent<Text>().text = "8 MAR 19\nCRs: " + GameControl.control.credits;
    }
}
