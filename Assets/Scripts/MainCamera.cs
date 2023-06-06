using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    float speed = 3f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
       transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        Vector3 position = target.position;
        position.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime) + new Vector3(-0.35f, 0, 0);
    }
}
