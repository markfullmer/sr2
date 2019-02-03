using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
     public void OnPointerEnter(PointerEventData eventData) {
        gameObject.GetComponent<Text>().color = Color.red;
     }
 
     public void OnPointerExit(PointerEventData eventData) {
        gameObject.GetComponent<Text>().color = Color.red;
     }
}
