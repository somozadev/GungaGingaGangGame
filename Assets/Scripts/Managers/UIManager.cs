using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image p1;
    public Image p2;
    [SerializeField] float dif; 
    public float ratio = 0.001f;

    void Update()
    {
        if (GameManager.Instance.player1 != null && GameManager.Instance.player2 != null)
            UpdateFillAmount();
    }
    private void UpdateFillAmount()
    {
        dif = GameManager.Instance.player1.points - GameManager.Instance.player2.points;
        if (dif > 0)//p1 wins
        {
            p1.fillAmount = (0.5f + Mathf.Abs(dif) * ratio);
            p2.fillAmount = 1-p1.fillAmount;
        }
        else
        {
            p2.fillAmount = (0.5f + Mathf.Abs(dif) * ratio);
            p1.fillAmount = 1 - p2.fillAmount;

        }
    }


}
