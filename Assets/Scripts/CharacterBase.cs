using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour {

    protected Tilemap collideable;
    protected Tilemap exteriorFloor;
    protected Tilemap floor;
    protected GameObject gui;
    protected Animator animator;
    protected bool isInteracting;

    protected void Start() {
        floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        if (GameObject.Find("ExteriorFloor") != null) {
            exteriorFloor = GameObject.Find("ExteriorFloor").GetComponent<Tilemap>();
        }
        collideable = GameObject.Find("Collideable").GetComponent<Tilemap>();
        gui = GameObject.Find("GUI");
    }

    protected bool canMoveTo(Vector2 targetCell) {
        if (isInteracting) {
            return false;
        }
        if (positionHasPlayer(targetCell)) {
            return false;
        }
        if (positionHasNPC(targetCell)) {
            return false;
        }
        if (positionHasLockedDoor(targetCell)) {
            return false;
        }
        if (getCell(collideable, targetCell) != null) {
            return false;
        }
        if (getCell(floor, targetCell) != null) {
            return true;
        }
        if (exteriorFloor != null) {
            if (getCell(exteriorFloor, targetCell) != null) {
                return true;
            }
        }
        return false;
    }

    protected bool positionIsExterior(Vector3 targetCell) {
        if (exteriorFloor != null && getCell(exteriorFloor, targetCell) != null) {
            return true;
        }
        return false;
    }

    protected bool positionHasPlayer(Vector3 targetCell) {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            if (Vector2.Distance(targetCell, player.transform.position) <= 0.5) {
                return true;
            }
        }
        return false;
    }

    protected bool positionHasNPC(Vector3 targetCell) {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject player in players) {
            if (Vector2.Distance(targetCell, player.transform.position) <= 0.5) {
                return true;
            }
        }
        return false;
    }

    protected bool positionHasLockedDoor(Vector3 targetCell) {
        GameObject[] doors;
        doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject door in doors) {
            if (Vector2.Distance(targetCell, door.transform.position) <= 0.5) {
                Door controlscript = door.GetComponent<Door>();
                if (controlscript.locked == true) {
                    return true;
                }
            }
        }
        return false;
    }

    protected IEnumerator doMove(Vector3 end) {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, 10 * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    protected GameObject npcInRange(Vector2 currentCell) {
        GameObject[] characters;
        characters = GameObject.FindGameObjectsWithTag("NPC");
        GameObject interactor = null;
        float currentPlayerDistance = 2;
        foreach (GameObject character in characters) {
            float playerDistance = Vector2.Distance(currentCell, character.transform.position);
            if (playerDistance <= 2 && playerDistance <= currentPlayerDistance) {
                // If a closer player is found, set that player to the interactor.
                currentPlayerDistance = playerDistance;
                interactor = character;
            }
        }
        return interactor;
    }

    protected GameObject interactiveInRange(Vector2 currentCell) {
        GameObject[] interactives = GameObject.FindGameObjectsWithTag("Interactive");
        GameObject interactor = null;
        foreach (GameObject interactive in interactives) {
            float distance = Vector2.Distance(currentCell, interactive.transform.position);
            if (distance <= 1) {
                interactor = interactive;
                break;
            }
        }
        return interactor;
    }

    protected TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos) {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }
    protected bool hasTile(Tilemap tilemap, Vector2 cellWorldPos) {
        return tilemap.HasTile(tilemap.WorldToCell(cellWorldPos));
    }
}
