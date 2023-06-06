using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    public float speed;
    public Transform PlayerHeand;
    public GameObject[] Beer;
    public bool TakeBeer = false;
    public bool byBarrel = false;
    public GameObject i;
    public Joystick joystick;
    [SerializeField] AudioSource AudioBuff;
    [SerializeField] Text ScoreText;
    public int score;
    public bool Buff = false;
    bool TimerOn;
    public float SecondBuffSet;
    float SecondBuff;



    void Start()
    {
        SecondBuff = SecondBuffSet;
        rb = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        ScoreText.text = "Î×ÊÈ: " + score.ToString();

        if (Input.GetKeyDown(KeyCode.E))
        {
            E();
        }

        if (TakeBeer && i != null)
        {
            i.transform.position = PlayerHeand.transform.position;
        }

        if(score == 20)
        {
            SceneManager.LoadSceneAsync(0);
        }

        if (TimerOn == true)
        {
            SecondBuff -= Time.deltaTime;
            if (SecondBuff < 0)
            {
                TimerOn = false;
                SecondBuff = SecondBuffSet;
                Buff = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.B) && SecondBuff == SecondBuffSet)
        {
            AudioBuff.Play();
            Buff = true;
            TimerOn = true;
        }

    }

    void FixedUpdate()
    {
        transform.position += new Vector3(speed, 0, 0) * joystick.Horizontal;
        transform.position += new Vector3(0, speed, 0) * joystick.Vertical;
        
        
        transform.position += new Vector3(speed, 0, 0) * Input.GetAxis("Horizontal");
        transform.position += new Vector3(0, speed, 0) * Input.GetAxis("Vertical");

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (joystick.Horizontal < 0 || Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (joystick.Horizontal > 0 || Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (joystick.Horizontal != 0 || joystick.Vertical != 0 || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            byBarrel = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            byBarrel = false;
        }

    }

    public void E()
    {
        if (TakeBeer == false && byBarrel)
        {
            if (Buff == false)
            {
                i = Instantiate(Beer[0], PlayerHeand.position, transform.rotation);
            } else
            {
                i = Instantiate(Beer[1], PlayerHeand.position, transform.rotation);
            }
            i.transform.parent = PlayerHeand.transform;
            TakeBeer = true;
        }
    }
}


