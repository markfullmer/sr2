using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public bool uiActive = true;
    public Text text;
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator.SetBool("uiActive", false);
    }
}
