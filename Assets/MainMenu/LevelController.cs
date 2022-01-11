using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Animator cloudAnim;
    public bool player1In;
    public bool player2In;
    public float timer;
    public float maxtime;

    public Image fillImage;
    public bool exitGame;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1In && player2In && (timer < maxtime))
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (timer != maxtime)
            {
                timer = 0f;
            }

        }


        if (timer > maxtime)
        {
            timer = maxtime;
            StartCoroutine(CloudsAnim());
        }
        fillImage.fillAmount = timer / maxtime;
    }

    IEnumerator CloudsAnim()
    {
        cloudAnim.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(2);
        if (exitGame)
        {
            Application.Quit(0);
        }
        else
        {
            SceneManager.LoadScene("PlayableScene");
            // Debug.Log("Level loaded");w
        }



    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Movement")
        {
            if (collider.GetComponentInParent<Player>() != null)
            {
                if (!player1In)
                    player1In = true;
                else
                    player2In = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.name == "Movement")
        {
            if (collider.GetComponentInParent<Player>() != null)
            {
                if (player1In)
                    player1In = false;
                else
                    player2In = false;
            }
        }
    }
}
