using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = (Input.mousePosition + target.position) / 2;
        transform.position = new Vector3(pos.x, pos.y, -10);
    }
}
