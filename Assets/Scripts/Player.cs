using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.UI;	
using UnityEngine.SceneManagement;
using TMPro;

public class Player : CharacterBase {

    public float speed;
    public int keyCount = 0; 
    private bool isMoving = false;
    private Vector2 pos; // position
    private float moveTime = 0.1f; // Speed
    private GameObject interactor;

    new void Start() {
        base.Start(); // Load tilemaps, etc.

        // Reposition if coming from the turbolift in any scene.
        if (GameControl.control.fromTurbolift == true) {
            GameControl.control.fromTurbolift = false;
            GameObject turbolift = GameObject.Find("Turbolift");
            Vector3 newPosition = new Vector3(turbolift.transform.position.x, turbolift.transform.position.y - 1.0f, turbolift.transform.position.z);
            transform.position = newPosition;
        }
    }

    void OnGUI() {
        Event e = Event.current;
        if (!animator.GetBool("uiActive") && e.isKey) {
            Vector2 startCell = transform.position;
            if (Event.current.Equals(Event.KeyboardEvent("return")) ||
            Event.current.Equals(Event.KeyboardEvent("[enter]"))
            ) {
                handleInteract(startCell);
            }
            else {
                handleMove(startCell);
            }
        }
    }

    // Check for interactives & respond.
    private void handleInteract(Vector3 startCell) {
        // @todo set ControlPanel active...
        //panel.gameObject.SetActive(true);
        //panel.gameObject.SetActive (true);

        // Will use the first NPC it finds.
        if (interactor = npcInRange(startCell)) {
            if (interactor.name == "Turbolift") {
                interactor.GetComponent<Turbolift>().interact();  
            }
            if (interactor.name == "Orellian") {
                interactor.GetComponent<Orellian>().interact();  
            }
            if (interactor.name == "Guard1") {
                interactor.GetComponent<Guard>().interact();  
            }
        }
    }

    private void handleMove(Vector2 startCell) {
        // We do nothing if the player is still moving, or the UI is active.
        if (isMoving) return;
        // Store the current horizontal input in the float moveHorizontal.
        int horizontal = (int) Input.GetAxisRaw ("Horizontal");
        // Store the current vertical input in the float moveVertical.
        int vertical = (int) Input.GetAxisRaw ("Vertical");
        //If there's a direction, we are trying to move.
        if (horizontal != 0 || vertical != 0) {
            Vector2 targetCell = startCell + new Vector2(horizontal, vertical);
            if (canMoveTo(targetCell)) {
                StartCoroutine(Movement(targetCell));
            }
        }
    }


    private IEnumerator Movement(Vector3 end) {

        isMoving = true;

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        isMoving = false;
    }

}