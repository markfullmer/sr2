using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl control;
    public string game_state = "origin";
    public float credits = 15102;
    public string repute;
    public bool fromTurbolift;
    public string barkeep_state;
    public string orellian_state;
    public string botty_state = "0";
    public string cebak_state = "0";
    public string bookshop_state = "0";
    public string offduty_state = "0";
    public string maskedfigure_state = "0";
    public string monty_state = "0";
    public string cargomanager_state = "0";
    public string orellian_relationship;
    public bool playerInteracting;
    public bool isControlPanel;
    public bool muted;
    public bool dockingReleased = false;

    // Start is called before the first frame update
    void Awake() {
        if (control == null) {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this) {
            Destroy(gameObject);
        }
    }
}
