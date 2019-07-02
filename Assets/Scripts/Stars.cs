using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 position = transform.position;
        transform.Rotate(Vector3.forward * -1f * Time.deltaTime);
    }
}
