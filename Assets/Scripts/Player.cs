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
        
        if (GameControl.control.game_state == "origin") {
            GameControl.control.playerInteracting = true;
            FindObjectOfType<DialogueManager>().SetDialogue("Docking privileges at Hiathra granted. We've done a Level 4 check of your cargo for risks of biocontamination.\n\nThe station is observing a general quarantine due to an outbreak of Manchi flu. See the customs office for further information.\n\nWe hope you will have a pleasant visit.");
            StartCoroutine(actionWarmUp(1f));
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
                        handleNPC(startCell);
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
        else if (e.isKey && GameControl.control.game_state == "origin") {
            GameControl.control.game_state = "initial";
            GameControl.control.playerInteracting = false;
            FindObjectOfType<DialogueManager>().exit();
        }
    }

    // Check for interactives & respond.
    private void handleNPC(Vector3 startCell) {
        // Will use the first NPC it finds.
        if (interactor = npcInRange(startCell)) {
            GameControl.control.playerInteracting = true;
            if (interactor.name == "Orellian") {
                interactor.GetComponent<Orellian>().interact();  
            }
            else if (interactor.name == "Guard1") {
                interactor.GetComponent<Guard>().interact();  
            }
            else if (interactor.name == "Barkeep") {
                interactor.GetComponent<Barkeep>().interact();  
            }
            else if (interactor.name == "Monty") {
                interactor.GetComponent<Monty>().interact();  
            }
            else if (interactor.name == "Bookshop") {
                interactor.GetComponent<Bookshop>().interact();  
            }
            else if (interactor.name == "Immigration") {
                interactor.GetComponent<Immigration>().interact();  
            }
            else if (interactor.name == "MaskedFigure") {
                interactor.GetComponent<MaskedFigure>().interact();  
            }
            else if (interactor.name == "CargoManager") {
                interactor.GetComponent<CargoManager>().interact();  
            }
            else if (interactor.name.Contains("CargoWorker")) {
                interactor.GetComponent<ManchiWorker>().interact();
            }
        }
        else {
            FindObjectOfType<DialogueManager>().SetDialogue("No one here.");
        }
    }

    private void handleInteract(Vector3 startCell) {
        // Will use the first NPC it finds.
        if (interactor = interactiveInRange(startCell)) {
            GameControl.control.playerInteracting = true;
            if (interactor.name == "Turbolift") {
                interactor.GetComponent<Turbolift>().interact();  
            }
            if (interactor.name == "DoNotPress") {
                interactor.GetComponent<DoNotPress>().interact();  
            }
            else if (interactor.name == "Passageway") {
                interactor.GetComponent<Passageway>().interact();  
            }
            else if (interactor.name == "EngineeringComputer1") {
                interactor.GetComponent<EngineeringComputer>().interact();  
            }
            else if (interactor.name == "EngineeringComputer2") {
                interactor.GetComponent<EngineeringComputer>().interact();  
            }
        }
        else {
            FindObjectOfType<DialogueManager>().SetDialogue("Nothing here.");
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
                if (GameControl.control.muted != true) {
                    handleSound(targetCell);
                }

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
                StartCoroutine(OpenDoor(door, doorData));
            }
            else if (doorData.isOpen == true) {
                StartCoroutine(CloseDoor(door, doorData));
                doorData.isOpen = false;
            }
        }
    }

    private IEnumerator OpenDoor(GameObject door, Door doorData) {
        float sqrRemainingDistance = (door.transform.position - doorData.open).sqrMagnitude;
        float inverseMoveTime = 1 / 0.15f;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(door.transform.position, doorData.open, inverseMoveTime * Time.deltaTime);
            door.transform.position = newPosition;
            sqrRemainingDistance = (door.transform.position - doorData.open).sqrMagnitude;
            yield return null;
        }
        doorData.isOpen = true;
    }

    private IEnumerator CloseDoor(GameObject door, Door doorData) {
        float sqrRemainingDistance = (door.transform.position - doorData.closed).sqrMagnitude;
        float inverseMoveTime = 1 / 0.15f;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(door.transform.position, doorData.closed, inverseMoveTime * Time.deltaTime);
            door.transform.position = newPosition;
            sqrRemainingDistance = (door.transform.position - doorData.closed).sqrMagnitude;

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

    protected IEnumerator actionWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
    }

}