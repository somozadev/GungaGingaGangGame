using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public UIManager uiManager;

    public Player player1;
    public Player player2;

    public List<Npc> comedy_npcs;
    public List<Npc> tragedy_npcs;
    public List<Npc> null_npcs;


    public int GetPointsToAddPlayer(Genre genre)
    {
        int sum = 0;
        if (genre.Equals(Genre.COMEDY))
            foreach (Npc n in comedy_npcs)
                sum += n.getPoints;
        else if (genre.Equals(Genre.TRAGEDY))
            foreach (Npc n in tragedy_npcs)
                sum += n.getPoints;
        else
            sum = 0;
        return sum;
    }


    public void MoveNpcTo(Npc npc, Genre genre)
    {
        if (genre.Equals(Genre.COMEDY))
        {
            if (tragedy_npcs.Contains(npc)) { tragedy_npcs.Remove(npc); tragedy_npcs.TrimExcess(); }
            if (null_npcs.Contains(npc)) { null_npcs.Remove(npc); null_npcs.TrimExcess(); }
            if (!comedy_npcs.Contains(npc)) { comedy_npcs.Add(npc); }
        }
        else if (genre.Equals(Genre.TRAGEDY))
        {
            if (comedy_npcs.Contains(npc)) { comedy_npcs.Remove(npc); comedy_npcs.TrimExcess(); }
            if (null_npcs.Contains(npc)) { null_npcs.Remove(npc); null_npcs.TrimExcess(); }
            if (!tragedy_npcs.Contains(npc)) { tragedy_npcs.Add(npc); }
        }
        else
        {
            if (comedy_npcs.Contains(npc)) { comedy_npcs.Remove(npc); comedy_npcs.TrimExcess(); }
            if (tragedy_npcs.Contains(npc)) { tragedy_npcs.Remove(npc); tragedy_npcs.TrimExcess(); }
            if (!null_npcs.Contains(npc)) { null_npcs.Add(npc); }
        }
    }
}
