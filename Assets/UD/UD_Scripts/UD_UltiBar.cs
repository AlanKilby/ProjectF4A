using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UD_UltiBar : MonoBehaviour
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
        currentAmount = CD.GetCharacter().hp / CD.GetCharacter().maxHp;
    }
}
