using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScrollWheel : MonoBehaviour
{
    public float scrollSpeed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position += transform.forward * scroll * scrollSpeed, 0);
    }
}
