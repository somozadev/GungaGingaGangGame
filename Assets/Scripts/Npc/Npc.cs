using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private int points = 1;
    public int getPoints { get { return points; } }
    [Header("Enums")]
    [SerializeField] Genre currentGenre;
    [SerializeField] NpcType npcType;

    [Header("Range to change current genre")]
    [Space(20)]
    [SerializeField] int wait_to_launch_time_start;
    [SerializeField] int wait_to_launch_time_end;

    [Header("References")]
    [Space(20)]
    [SerializeField] GameObject throwable_prefab;
    public TMPro.TMP_Text text;

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
        {
            currentGenre = Genre.COMEDY; text.text = "COMEDY"; text.color = Color.yellow;
        }

        else if (rand > 40 && rand <= 80)
        {
            currentGenre = Genre.TRAGEDY; text.text = "TRAGEDY"; text.color = Color.blue;
        }

        else
        {
            currentGenre = Genre.NULL; text.text = "";
        }
        GameManager.Instance.MoveNpcTo(this, currentGenre);
    }

    private void PerformAction()
    {
        switch (npcType)
        {
            case NpcType.NORMAL:
                break;
            case NpcType.SPEAR:
                ThrowSpear();
                break;
            case NpcType.LIGHTNING:
                ThrowLighting();
                break;
            case NpcType.BANANA:
                ThrowBanana();
                break;
        }
    }



    private void ThrowSpear() { }
    private void ThrowLighting() { }
    private void ThrowBanana() { }


}

