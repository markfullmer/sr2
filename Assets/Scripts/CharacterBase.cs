using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{

    public Tilemap obstaclesTilemap;
    public Tilemap interactivesTilemap;
    public Tilemap floorTilemap;
    public InfoPanel panel;
    public Text text;

    protected bool canMoveTo(Vector2 targetCell) {
        if (positionHasPlayer(targetCell)) {
            return false;
        }
        if (positionHasNPC(targetCell)) {
            return false;
        }
        if (getCell(interactivesTilemap, targetCell) != null) {
            return false;
        }
        if (getCell(obstaclesTilemap, targetCell) != null) {
            return false;
        }
        //if (getCell(floorTilemap, targetCell) != null) {
        //    return true;
        //}
        return true;
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
        foreach (GameObject character in characters) {
            if (Vector2.Distance(currentCell, character.transform.position) <= 2) {
                return character;
            }
        }
        return null;
    }

    protected TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos) {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }
    protected bool hasTile(Tilemap tilemap, Vector2 cellWorldPos) {
        return tilemap.HasTile(tilemap.WorldToCell(cellWorldPos));
    }
}
