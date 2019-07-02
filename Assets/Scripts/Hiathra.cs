using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiathra : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 position = transform.position;
        position[2] = position[2] + 0.1f * Time.deltaTime;
        position[1] = position[1] + 0.05f * Time.deltaTime;
        position[0] = position[0] + 0.01f * Time.deltaTime;
        transform.position = position;
        transform.Rotate(Vector3.forward * 1f * Time.deltaTime);
        //transform.position = transform.position(Vector3.forward * 10 * Time.deltaTime);
    }
}
