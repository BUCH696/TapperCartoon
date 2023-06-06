using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public GameObject Beer;
    public GameObject BOOM;
    int HealthPoint;
    public int MaxHealthPoint;
    bool On = false;
    public float speed;
    public float Force;
    bool stopGuest;
    GameObject i;
    public AudioSource Audio;
    public GameObject PlayerTakeBeer;
    bool FhotBuffBottle = false;



    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();

        StartCoroutine(Fade(1f, 0f, 1f));
        HealthPoint = MaxHealthPoint;  
    }



    void FixedUpdate()
    {
        if (On)
        {
            transform.position += new Vector3(speed, 0, 0);
        }
    }

    IEnumerator Fade(float duration, float min, float max)
    {
        anim.SetBool("Run", false);
        float time = 0;

        while (time < duration)
        {
            transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(min, max, time / duration));

            time += Time.deltaTime;
            yield return null;
        }
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, max);

        yield return new WaitForSeconds(2);
        anim.SetBool("Run", true);
        On = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bottle" && stopGuest == false)
        {
            if (HealthPoint > 1)
            {
                FhotBuffBottle = false;
                StartCoroutine(StopGuest());
                i = Instantiate(BOOM, coll.transform.position, coll.transform.rotation);
                Destroy(coll.gameObject);
                Destroy(i, 1f);
                CameraShake.Shake(0.5f, 0.2f);
                transform.position += new Vector3(-Force, 0, 0);
                HealthPoint -= 1;

                if (PlayerMove.FindObjectOfType<PlayerMove>().i == null)
                {
                    PlayerMove.FindObjectOfType<PlayerMove>().TakeBeer = false;
                }
            }
            else
            {
                CameraShake.Shake(0.5f, 0.2f);
                Destroy(coll.gameObject);
                transform.position += new Vector3(-Force , 0, 0) * Time.deltaTime;
                i = Instantiate(BOOM, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
                Destroy(i, 1f);
                StopAllCoroutines();
                PlayerMove.FindObjectOfType<PlayerMove>().score++;

                if (PlayerMove.FindObjectOfType<PlayerMove>().i == null)
                {
                    PlayerMove.FindObjectOfType<PlayerMove>().TakeBeer = false;
                }
            }
        }

        if (coll.gameObject.tag == "Buff Bottle" && stopGuest == false)
        {
            if (HealthPoint > 1)
            {
                FhotBuffBottle = true;
                StartCoroutine(StopGuest());
                i = Instantiate(BOOM, coll.transform.position, coll.transform.rotation);
                Destroy(coll.gameObject);
                Destroy(i, 1f);
                CameraShake.Shake(0.7f, 0.4f);
                transform.position += new Vector3(-Force * 2f, 0, 0);
                HealthPoint -= 1;

                if (PlayerMove.FindObjectOfType<PlayerMove>().i == null)
                {
                    PlayerMove.FindObjectOfType<PlayerMove>().TakeBeer = false;
                }
            }
            else
            {
                CameraShake.Shake(0.7f, 0.4f);
                Destroy(coll.gameObject);
                transform.position += new Vector3(-Force * 2f, 0, 0) * Time.deltaTime;
                i = Instantiate(BOOM, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
                Destroy(i, 1f);
                StopAllCoroutines();
                PlayerMove.FindObjectOfType<PlayerMove>().score++;

                if (PlayerMove.FindObjectOfType<PlayerMove>().i == null)
                {
                    PlayerMove.FindObjectOfType<PlayerMove>().TakeBeer = false;
                }
            }
        }

        IEnumerator StopGuest()
        {
            stopGuest = true;
            On = false;
            anim.SetBool("Run", false);
            if(FhotBuffBottle)
            {
                yield return new WaitForSeconds(5);
            } else
            {
                yield return new WaitForSeconds(3);
            }
            
            anim.SetBool("Run", true);
            Audio.Play();
            On = true;
            stopGuest = false;

        }
    }
}
