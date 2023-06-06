using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoaderScene : MonoBehaviour
{
    public GameObject Preview;

    public void SceneMenu()
    {
        StartCoroutine(Starting1());
    }
    
    public void SceneGame()
    {
        StartCoroutine(Starting2());
    }

    IEnumerator Starting1()
    {
        Preview.GetComponent<Animator>().SetBool("PrevOn", false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
    
    IEnumerator Starting2()
    {
        Preview.GetComponent<Animator>().SetBool("PrevOn", false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
