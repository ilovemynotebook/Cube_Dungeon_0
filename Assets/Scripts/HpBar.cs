using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image FillImg;

    public void Start()
    {
        FillImg = transform.GetChild(0).GetComponent<Image>();
    }

    public void UpdateHealth(int health, int maxHealth)
    {
        FillImg.fillAmount = (float)health / maxHealth;
    }
}
