using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Table : MonoBehaviour
{
    public bool byTable = false;
    public GameObject b;
    public Transform Player;
    public Rigidbody2D rb;
    public GameObject[] BottleBeer;
    public GameObject i;
    public bool BuffBottle;
    

    void Start()
    {
       
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            E();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Bottle")
        {
            BuffBottle = false;
            i = coll.gameObject;
            byTable = true;
        }

        if (coll.transform.tag == "Buff Bottle")
        {
            BuffBottle = true;
            i = coll.gameObject;
            byTable = true;
        }

        if (coll.transform.tag == "Guest")
        {
            CameraShake.Shake(0.2f, 0.2f);
            PlayerMove.FindObjectOfType<PlayerMove>().score = 0;
            SceneManager.LoadScene("Playing");
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Bottle")
        {
            i = null;
            byTable = false;
        }

        if (coll.gameObject.tag == "Buff Bottle")
        {
            i = null;
            byTable = false;
        }
    }

    public void E()
    {
        if (byTable)
        {
        Destroy(i);
        Destroy(PlayerMove.FindObjectOfType<PlayerMove>().i);

            if (BuffBottle == false)
            {
                b = Instantiate(BottleBeer[0], transform.position, transform.rotation);
            } else
            {
                b = Instantiate(BottleBeer[1], transform.position, transform.rotation);
            }

        PlayerMove.FindObjectOfType<PlayerMove>().TakeBeer = false;
        PlayerMove.FindObjectOfType<PlayerMove>().i = null;
        rb = b.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * -6f, ForceMode2D.Impulse);
        }
    }

    
}
