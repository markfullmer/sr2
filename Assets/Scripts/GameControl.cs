using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    public float credits;
    public string repute;
    public bool fromTurbolift;
    public string barkeep_state;
    public string orellian_state;
    public string orellian_relationship;
    public bool playerInteracting;
    public bool isControlPanel;

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
