using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.UI;	
using UnityEngine.SceneManagement;
using TMPro;

public class Player : CharacterBase {

    public float speed;             //Floating point variable to store the player's movement speed.
    public int keyCount = 0; 
    private bool isMoving = false;
    private bool uiActive = false;
    private Vector2 pos; // position
    private float moveTime = 0.1f; // Speed
    private BoundsInt area;
    public InfoPanel panel;
    public bool onExit = false;
    public GameObject interactor;

    void Start() {}

    // FixedUpdate is called at a fixed interval and is independent of frame rate.
    void FixedUpdate()
    {
        if (Input.anyKey) {
            Vector2 startCell = transform.position;
            if (Input.GetKeyDown("enter")) {
                //uiActive = true;
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
            Debug.Log(interactor.name);
            interactor.GetComponent<NPC>().interact(interactor.name);
        }

        // @todo handle interactive Tiles.
/*         int x = (int) startCell.x;
        int y = (int) startCell.y;
        area = new BoundsInt(new Vector3Int(x, y, 1), size: new Vector3Int(3, 3, 5));
        //Tilemap tilemap = GetComponent<Tilemap>();
        TileBase[] tileArray = interactivesTilemap.GetTilesBlock(area);
        for (int index = 0; index < tileArray.Length; index++)
        {
            if (tileArray[index] != null) {
                print(tileArray[index]);

            }
        } */
    }

    private void handleMove(Vector2 startCell) {
        // We do nothing if the player is still moving, or the UI is active.
        if (isMoving || onExit || uiActive) return;
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