using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject[] Guest;
    public Animator anim;
    float TimeTimer;
    public float SetTimeTimer;
    int random;

    void Start()
    {
        anim = transform.GetComponent<Animator>();
        anim.SetBool("OpenDoor", true);

        random = Random.Range(0, Guest.Length);
        TimeTimer = SetTimeTimer;
        anim.SetBool("OpenDoor", false);
        Instantiate(Guest[random], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        TimeTimer -= Time.deltaTime;

        if (TimeTimer < 0)
        {
            random = Random.Range(0, Guest.Length);
            anim.SetBool("OpenDoor", true);
            Instantiate(Guest[random], transform.position, transform.rotation);
            TimeTimer = SetTimeTimer;
            anim.SetBool("OpenDoor", false);
        }
    }
}
