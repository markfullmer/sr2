using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.UI;	
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class Player : CharacterBase {

    public float speed;
    public int keyCount = 0; 
    private bool isMoving = false;
    private Vector2 pos; // position
    private float moveTime = 0.1f; // Speed
    private GameObject interactor;
    private GameObject[] doors;
    private AudioSource exteriorAmbience;
    private AudioSource interiorAmbience;

    new void Start() {
        base.Start(); // Load tilemaps, etc.
        GameObject a = GameObject.Find("ExteriorAmbience");
        if (a != null) {
            exteriorAmbience = a.GetComponent<AudioSource>();
        }
        GameObject b = GameObject.Find("InteriorAmbience");
        if (b != null) {
            interiorAmbience = b.GetComponent<AudioSource>();
        }
        
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
        if (e.isKey && !GameControl.control.playerInteracting) {
            if (!GameControl.control.isControlPanel) {
                if (Event.current.Equals(Event.KeyboardEvent("return")) || Event.current.Equals(Event.KeyboardEvent("[enter]"))) {
                    FindObjectOfType<DialogueManager>().openControlPanel();
                }
                else {
                    Vector2 startCell = transform.position;
                    handleMove(startCell);
                }
            }
            else if (GameControl.control.isControlPanel) {
                if (Event.current.Equals(Event.KeyboardEvent("return")) || Event.current.Equals(Event.KeyboardEvent("[enter]"))) {
                    if (EventSystem.current.currentSelectedGameObject.name == "Talk") {
                        Vector2 startCell = transform.position;
                        handleInteract(startCell);
				    }
                    else if (EventSystem.current.currentSelectedGameObject.name == "Inspect") {
                        Vector2 startCell = transform.position;
                        handleInteract(startCell);
				    }
                    else if (EventSystem.current.currentSelectedGameObject.name == "Status") {

				    }
                    else {
                        FindObjectOfType<DialogueManager>().exit();
                    }
                }
            }
        }
    }

    // Check for interactives & respond.
    private void handleInteract(Vector3 startCell) {
        // Will use the first NPC it finds.
        if (interactor = npcInRange(startCell)) {
            GameControl.control.playerInteracting = true;
            if (interactor.name == "Turbolift") {
                interactor.GetComponent<Turbolift>().interact();  
            }
            else if (interactor.name == "Orellian") {
                interactor.GetComponent<Orellian>().interact();  
            }
            else if (interactor.name == "Guard1") {
                interactor.GetComponent<Guard>().interact();  
            }
            else if (interactor.name == "Barkeep") {
                interactor.GetComponent<Barkeep>().interact();  
            }
        }
        else {
            FindObjectOfType<DialogueManager>().SetDialogue("No one here.");
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
                handleDoors(startCell, targetCell);
                handleSound(targetCell);
            }
        }
    }

    private void handleSound(Vector2 targetCell) {
        if (positionIsExterior(targetCell)) {
            if (exteriorAmbience != null) {
                exteriorAmbience.mute = false;
            } 
            if (interiorAmbience != null) {
                interiorAmbience.mute = true;
            }
        }
        else {
            if (exteriorAmbience != null) {
                exteriorAmbience.mute = true;
            }
            if (interiorAmbience != null) {
                interiorAmbience.mute = false;
            }
        }
    }

    private void handleDoors(Vector2 startcell, Vector2 targetCell) {
        GameObject[] doors;
        doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject door in doors) {
            var doorData = door.GetComponent<Door>();
            if (Vector2.Distance(targetCell, doorData.closed) <= 0.2) {
                StartCoroutine(MoveDoor(door, doorData.open));
                break;
            }
            else if (Vector2.Distance(startcell, doorData.closed) <= 0.5) {
                StartCoroutine(MoveDoor(door, doorData.closed));
                break;
            }
        }
    }

    private IEnumerator MoveDoor(GameObject door, Vector3 to) {

        float sqrRemainingDistance = (door.transform.position - to).sqrMagnitude;
        float inverseMoveTime = 1 / 0.15f;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(door.transform.position, to, inverseMoveTime * Time.deltaTime);
            door.transform.position = newPosition;
            sqrRemainingDistance = (door.transform.position - to).sqrMagnitude;

            yield return null;
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