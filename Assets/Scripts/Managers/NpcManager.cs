using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public List<Transform> points;
    public GameObject simple_npc_prefab;
    public GameObject banana_npc_prefab; //25%
    public GameObject spear_npc_prefab; //10%
    public GameObject lighting_npc_prefab; //1%

    public int minNumber;
    public int maxNumber;
    public void InitNpcs()
    {
        int cuantity = Random.Range(minNumber, maxNumber);
        for (int i = 0; i < cuantity; i++)
        {
            int rand = Random.Range(1, 101);
            if (rand > 0 && rand <= 1)
            {
                Transform t = GetPoint();
                GameObject npc = GameObject.Instantiate(lighting_npc_prefab, t.position, t.rotation);
                npc.GetComponent<Npc>().InitNpc();
                points.Remove(t);
                points.TrimExcess();
            }
            else if (rand > 1 && rand <= 12)
            {
                Transform t = GetPoint();
                GameObject npc = GameObject.Instantiate(spear_npc_prefab, t.position, t.rotation);
                npc.GetComponent<Npc>().InitNpc();
                points.Remove(t);
                points.TrimExcess();
            }
            else if (rand > 12 && rand <= 38)
            {
                Transform t = GetPoint();
                GameObject npc = GameObject.Instantiate(banana_npc_prefab, t.position, t.rotation);
                npc.GetComponent<Npc>().InitNpc();
                points.Remove(t);
                points.TrimExcess();
            }
            else
            {
                Transform t = GetPoint();
                GameObject npc = GameObject.Instantiate(simple_npc_prefab, t.position, t.rotation);
                npc.GetComponent<Npc>().InitNpc();
                points.Remove(t);
                points.TrimExcess();
            }
        }
    }

    private Transform GetPoint()
    {
        Transform t = points[Random.Range(0, points.Count)];
        if(t!=null)
            return t;
        else
            return GetPoint();
    }



}
