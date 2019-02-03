using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationManager : MonoBehaviour
{
     private GameObject player; //private variable for the gameobject to reference the player object.
 
     // Use this for initialization
     void Start () {
         player = GameObject.FindGameObjectWithTag("Player"); //find the player object in the scene.
         Debug.Log(player);
         //player.transform.position = gameObject.transform.position; //set the player object position to equal the empty gameobject.
     }
}
