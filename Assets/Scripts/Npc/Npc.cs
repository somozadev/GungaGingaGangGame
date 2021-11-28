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

    [Header("Range to change throw action")]
    [Space(20)]
    [SerializeField] int wait_to_action_time_start;
    [SerializeField] int wait_to_action_time_end;

    [Header("References")]
    [Space(20)]
    [SerializeField] Transform start_point;
    [SerializeField] GameObject spear_prefab;
    [SerializeField] GameObject banana_prefab;
    [SerializeField] GameObject lighting_prefab;
    public TMPro.TMP_Text text;

    public void InitNpc()
    {
        StartNpc();
        StartAttack();
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

    public void StartAttack() { DoCorrAttack(); }
    private void DoCorrAttack() { PerformAction(); StartCoroutine(WaitToPerformAgain()); }
    private IEnumerator WaitToPerformAgain()
    {
        float elapsed_time = 0;
        while (elapsed_time <= Random.Range(wait_to_action_time_start, wait_to_action_time_end))
        {
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        DoCorrAttack();
    }
    private void SelectGenre()
    {
        int rand = Random.Range(1, 101);
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
        PerformAction();
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



    private void ThrowSpear()
    {
        DoThrowWith(NpcType.SPEAR);
    }
    private void ThrowLighting()
    {
        DoThrowWith(NpcType.LIGHTNING);
    }
    private void ThrowBanana()
    {
        DoThrowWith(NpcType.BANANA);
    }
    private void DoThrowWith(NpcType type)
    {
        GameObject prefabcall = null;
        switch (type)
        {
            case NpcType.BANANA:
                prefabcall = banana_prefab; break;
            case NpcType.SPEAR:
                prefabcall = banana_prefab; break;
            case NpcType.LIGHTNING:
                prefabcall = banana_prefab; break;
        }
        if (prefabcall != null)
        {
            if (currentGenre.Equals(Genre.COMEDY))
            {
                GameObject bullet = GameObject.Instantiate(prefabcall, start_point.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().Launch(GetPosToShoot(Genre.TRAGEDY));
            }
            else if (currentGenre.Equals(Genre.TRAGEDY))
            {
                GameObject bullet = GameObject.Instantiate(prefabcall, start_point.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().Launch(GetPosToShoot(Genre.COMEDY));
            }
        }
    }

    private Vector3 GetPosToShoot(Genre comparableGenre)
    {
        Vector3 pos = Vector3.zero;

        if (currentGenre.Equals(comparableGenre))
        {
            if (GameManager.Instance.player1.movement.current_genre.Equals(Genre.TRAGEDY) &&
            GameManager.Instance.player2.movement.current_genre.Equals(Genre.TRAGEDY)) //random entre los 2
            {
                if (Random.Range(0, 2) == 0)
                    pos = GameManager.Instance.player1.GetComponentInChildren<Rigidbody>().transform.position;
                else
                    pos = GameManager.Instance.player2.GetComponentInChildren<Rigidbody>().transform.position;

            }
            else if (GameManager.Instance.player2.movement.current_genre.Equals(Genre.TRAGEDY)) // el 2
                pos = GameManager.Instance.player2.movement.transform.position;
            else if (GameManager.Instance.player1.movement.current_genre.Equals(Genre.TRAGEDY))//el 1
                pos = GameManager.Instance.player1.movement.transform.position;

        }
        return pos;
    }


}

