using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] Genre currentGenre;
    public int wait_to_launch_time_start;
    public int wait_to_launch_time_end;


    void Start()
    {
        StartNpc();
    }

    public void StartNpc() { DoCorrWait(); }

    private void DoCorrWait() { SelectGenre(); StartCoroutine(WaitToLaunchAgain()); }
    private IEnumerator WaitToLaunchAgain()
    {
        float elapsed_time = 0;
        while (elapsed_time <= Random.Range(wait_to_launch_time_start, wait_to_launch_time_end))
        {
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        DoCorrWait();
    }


    private void SelectGenre()
    {
        int rand = Random.Range(1, 100);
        if (rand > 0 && rand <= 40)
            currentGenre = Genre.COMEDY;

        else if (rand > 40 && rand <= 80)
            currentGenre = Genre.TRAGEDY;

        else
            currentGenre = Genre.NULL;
    }



}

