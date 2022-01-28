using TMPro;
using System.Collections;
using UnityEngine;

public class PointsIndicator : MonoBehaviour
{
    public TMP_Text text;
    public float lifeTime = 0.5f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;
    float fraction;

    private void Start()
    {
        fraction = lifeTime / 2f;
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        float dir = Random.rotation.eulerAngles.z;
        iniPos = new Vector3(0, 3, 0);
        float dist = Random.Range(minDist, maxDist);
        targetPos = iniPos + (Quaternion.Euler(0, 0, dir)) * new Vector3(dist / 2, dist / 2, 0f);
        transform.localScale = Vector3.one;
        StartCoroutine(IndicatorAnim());
    }


    private IEnumerator IndicatorAnim()
    {
        while (timer < lifeTime)
        {
            timer += Time.deltaTime;
            if (timer > fraction)
            {
                text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifeTime - fraction));
            }

            transform.localPosition = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifeTime));
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 3, Mathf.Sin(timer / lifeTime));
            yield return new WaitForEndOfFrame();
        }
        timer = lifeTime;
        Destroy(gameObject);
    }

    public void SetPoints(float points, Genre genre)
    {
        if (genre == Genre.COMEDY)
            text.color = Color.yellow;
        else if (genre == Genre.TRAGEDY)
            text.color = Color.magenta;
        text.text = "+" + points.ToString();
    }


}
