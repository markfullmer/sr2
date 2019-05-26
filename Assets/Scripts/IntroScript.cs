using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public Image intro;
    public Image intro2;
    public Image sunracer;
    public Image manchi;
    public Image hero;
    public Image hiathra;
    public Text text;
    public Text text2;
    public int state;
    public bool isTransitioning;

    void Start () {
        intro.enabled = true;
        intro2.enabled = false;
        sunracer.enabled = false;
        hiathra.enabled = false;
        hero.enabled = false;
        manchi.enabled = false;
        text.enabled = false;
        text2.enabled = false;
        state = 0;
    }
 
    void OnGUI() {
        Event e = Event.current;     
        if (e.isKey && isTransitioning == false) {
            isTransitioning = true;
            StartCoroutine(next());
        }
    }
    private IEnumerator next() {
        float cooldown = 0.3f;
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
            if (state == 0) {
                intro.enabled = false;
                intro2.enabled = true;
                state = 1;
            }
            else if (state == 1) {
                intro2.enabled = false;
                sunracer.enabled = true;
                text.enabled = true;
                text.text = "On a routine training mission through the Far Arm quadrant, you find yourself returned to the Karonus system.\n\nHow long had it been...thirty years?";
                state = 2;
            }
            else if (state == 2) {
                text2.enabled = true;
                text.text = "On a routine training mission through the Far Arm quadrant, you find yourself returned to the Karonus system.\n\nHow long had it been...thirty years?\n\nAs if conjured by some strange force";
                text2.text = "of memory, a ship comes into view. Sunracer class. You piloted a Sunracer all those many years ago...";
                state = 3;
            }
            else if (state == 3) {
                sunracer.enabled = false;
                manchi.enabled = true;
                text.text = "Your thoughts travel back...\n\nYou'd discovered a plot by Admiral Koth to start a war with the insectoid Manchi...he would effect a coup of the Imperium and replace Duchess Avenstar.";
                text2.enabled = false;
                state = 4;
            }
            else if (state == 4) {
                manchi.enabled = false;
                hero.enabled = true;
                text2.enabled = true;
                text2.text = "Your journey deep into Manchi space to preserve the power balance succeeded, and you had been hailed as a hero.";
                state = 5;
            }
            else if (state == 5) {
                text2.enabled = false;
                text.text = "You forcibly shake your head, trying to lose the memory.\n\nA hero, yes, but what of the universe that had been inherited? Had it been for the better? For the Manchi?\n\nFor anyone?";
                state = 6;
            }
            else if (state == 6) {
                hiathra.enabled = true;
                hero.enabled = false;
                text.text = "Your watch the pilot approach Hiathra Station's docking port. An all too familiar sense of foreboding makes itself known in the pit of your stomach.\n\n You have no clear goal...";
                state = 7;
            }
            else if (state == 7) {
                text2.text = "...except existence.";
                text2.enabled = true;
                state = 8;
            }
            else {
                StartCoroutine("leaveIntro");                
            }
            isTransitioning = false;
    }

    private IEnumerator leaveIntro() {
        hiathra.CrossFadeColor(Color.black, 2.0f, true, true);
        text.CrossFadeColor(Color.black, 2.0f, true, true);
        text2.CrossFadeColor(Color.black, 2.0f, true, true);
        float cooldown = 3;
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
		SceneManager.LoadScene("Hiathra_main_floor");
    }
}
