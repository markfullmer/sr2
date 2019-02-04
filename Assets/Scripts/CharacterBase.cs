using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour {

    protected Tilemap collideable;
    protected Tilemap floor;
    protected GameObject gui;
    protected Animator animator;

    protected void Start() {
        floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        collideable = GameObject.Find("Collideable").GetComponent<Tilemap>();
        gui = GameObject.Find("GUI");
        animator = gui.GetComponentsInChildren<Animator>()[0];
    }

    protected bool canMoveTo(Vector2 targetCell) {
        if (animator.GetBool("uiActive")) {
            return false;
        }
        if (positionHasPlayer(targetCell)) {
            return false;
        }
        if (positionHasNPC(targetCell)) {
            return false;
        }
        if (getCell(collideable, targetCell) != null) {
            return false;
        }
        if (getCell(floor, targetCell) != null) {
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
            if (playerDistance <= 1.5 && playerDistance < currentPlayerDistance) {
                // If a closer player is found, set that player to the interactor.
                currentPlayerDistance = playerDistance;
                interactor = character;
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
