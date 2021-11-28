using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] Player current_player;
    [SerializeField] float points_factor; 
    [SerializeField] float radius;
    [SerializeField] float max_time;
    [SerializeField] float speed;
    [SerializeField] Vector3 center;

    [Header("Debugs")]
    [SerializeField] Vector3 initial_pos;
    [SerializeField] Vector3 final_pos;
    void Start()
    {
        MoveLight();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Interactuable"))
        {
            if (current_player == null)
                current_player = other.GetComponentInParent<Player>();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Interactuable"))
        {
            if (current_player == null)
                current_player = other.GetComponentInParent<Player>();
            else
                current_player.points_factor = points_factor;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Player>() == current_player)
            if (current_player != null){
                current_player.points_factor = 1f;
                current_player = null;}

    }
    public void MoveLight() { StartCoroutine(StartMoving()); }
    private IEnumerator StartMoving()
    {
        initial_pos = transform.position;
        final_pos = GetRandomPoint();
        float elapsed_time = 0f;

        //v 14. v = e/t.
        float space = Mathf.Abs(Vector3.Distance(initial_pos, final_pos));
        max_time = space / speed;


        while (elapsed_time <= max_time)
        {
            transform.position = Vector3.Lerp(initial_pos, final_pos, elapsed_time / max_time);
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        MoveLight();
    }

    private Vector3 GetRandomPoint()
    {
        float rnd = Random.Range(0f, 1f);
        float r = radius * Mathf.Sqrt(rnd);
        float tetha = rnd * 2 * Mathf.PI;

        float x = center.x + r * Mathf.Cos(tetha);
        float y = Mathf.Abs(center.y + r * Mathf.Sin(tetha));
        Vector3 ret_ver = new Vector3(x, 0, y);
        // UnityEngine.Debug.Log(ret_ver.x + "," + ret_ver.y + "," + ret_ver.z);
        return ret_ver;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, radius);
    }
}
