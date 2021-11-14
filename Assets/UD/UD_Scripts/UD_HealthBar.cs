using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UD_HealthBar : MonoBehaviour
{
    public Character ch;
    public CharacterDisplay CD;

    private Image healthBar;

    private float currentAmount;

    void Start()
    {
        healthBar = GetComponent<Image>();
        healthBar.fillAmount = 1.0f;
    }


    void Update()
    {
        CalculateAmount();
        healthBar.fillAmount = currentAmount;
    }

    private void CalculateAmount()
    {
        //currentAmount = ch.hp / ch.maxHp;
        currentAmount = CD.hp / CD.GetCharacter().maxHp;
    }
}
