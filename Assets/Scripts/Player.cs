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
    public Sprite[] sprites;
    private string controlPanelState;
    private SpriteRenderer spriteRenderer;

    new void Start() {
        base.Start(); // Load tilemaps, etc.
        spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
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
            FindObjectOfType<DialogueManager>().SetDialogue("Docking privileges at Hiathra granted. We've done a Level 4 check of your cargo for risks of biocontamination.\n\nNOTE: The station is observing general quarantine due to an outbreak of Manchi flu. See Customs for further information.\n\nHave a pleasant visit.");
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
                    if (Event.current.Equals(Event.KeyboardEvent("left")) || Event.current.Equals(Event.KeyboardEvent("left"))) {
                        StartCoroutine(actionMove(0, 1));
                    }
                    else if (Event.current.Equals(Event.KeyboardEvent("right")) || Event.current.Equals(Event.KeyboardEvent("right"))) {
                        StartCoroutine(actionMove(2, 3));
                    }
                    else if (Event.current.Equals(Event.KeyboardEvent("up")) || Event.current.Equals(Event.KeyboardEvent("up"))) {
                        StartCoroutine(actionMove(4, 5));
                    }
                    else if (Event.current.Equals(Event.KeyboardEvent("down")) || Event.current.Equals(Event.KeyboardEvent("down"))) {
                        StartCoroutine(actionMove(6, 7));
                    }
                    Vector2 startCell = transform.position;
                    handleMove(startCell);
                }
            }
            else if (GameControl.control.isControlPanel) {
                if (Event.current.Equals(Event.KeyboardEvent("return")) || Event.current.Equals(Event.KeyboardEvent("[enter]"))) {
                    if (controlPanelState == "status") {
                        handleStatus();
                    }
                    else if (EventSystem.current.currentSelectedGameObject.name == "Talk") {
                        Vector2 startCell = transform.position;
                        handleNPC(startCell);
				    }
                    else if (EventSystem.current.currentSelectedGameObject.name == "Inspect") {
                        Vector2 startCell = transform.position;
                        handleInteract(startCell);
				    }
                    else if (EventSystem.current.currentSelectedGameObject.name == "Status") {
                        controlPanelState = "status";
                        FindObjectOfType<DialogueManager>().SetControlPanel("Ship", "Onboard", "Person", "Done");
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
    
    private void handleStatus() {
        if (EventSystem.current.currentSelectedGameObject.name == "Talk") {
            FindObjectOfType<DialogueManager>().SetDialogue("DAMAGE CONTROL\n\narmor: 750/750\n\nlaser...........<ok>\n\nlauncher........<ok>\n\nengine..........<ok>\n\nfore shield.....<ok>\n\naft shield......<ok>\n\nFTL drive.......<ok>\n\n");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Inspect") {
            string[] cargo = GameControl.control.cargo;
            string cargo_manifest = "";
            foreach (string i in cargo)
            {
                cargo_manifest = cargo_manifest + i + "\n\n";
            }
            FindObjectOfType<DialogueManager>().SetDialogue("Cargo\n\n_____\n\n" + cargo_manifest);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Status") {
            FindObjectOfType<DialogueManager>().SetDialogue("_____________________\n\nRACE......homo sapien\n\nCREDITS.........1500\n\nBOUNTY.............0\n\nREPUTE........legend\n\n\n\nImperium......adored\n\nGuild..........liked\n\nPirates......neutral\n\nManchi.......neutral\n\n");
        }
        else {
            FindObjectOfType<DialogueManager>().exit();
            controlPanelState = "0";
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
            else if (interactor.name == "Cargo") {
                interactor.GetComponent<Cargo>().interact();
            }
            else if (interactor.name == "ManchiPatron") {
                interactor.GetComponent<ManchiPatron>().interact();
            }
            else if (interactor.name == "OffDutyGuard") {
                interactor.GetComponent<OffDutyGuard>().interact();
            }
            else if (interactor.name == "Botty") {
                interactor.GetComponent<Botty>().interact();
            }
            else if (interactor.name == "Cebak") {
                interactor.GetComponent<Cebak>().interact();
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
            else if (interactor.name.Contains("EngineeringComputer")) {
                interactor.GetComponent<EngineeringComputer>().interact();  
            }
            else if (interactor.name.Contains("Door")) {
                interactor.GetComponent<Door>().interact();  
            }
            else if (interactor.name.Contains("Sign")) {
                interactor.GetComponent<Sign>().interact(interactor.name);  
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
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play(0);
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

    protected IEnumerator actionMove(int first, int second) {
        var cooldown = 0.1f;
        spriteRenderer.sprite = sprites[first];
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        spriteRenderer.sprite = sprites[second];
    }

    protected IEnumerator actionWarmUp(float cooldown) {
        while ( cooldown > 0f ) {
            cooldown -= Time.deltaTime;
            yield return null;
        }
    }

}