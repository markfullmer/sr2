using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObservationDeck : MonoBehaviour
{
    public GameObject cebak;

    void Start () {
        if (GameControl.control.cebak_state != "drinks") {
            cebak.gameObject.SetActive (false);
        } 
    }
}
