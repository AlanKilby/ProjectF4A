using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UD_UltiBar : MonoBehaviour
{
    public Character ch;
    public CharacterDisplay CD;

    private Image ultiBar;

    private float currentAmount;

    void Start()
    {
        ultiBar = GetComponent<Image>();
        ultiBar.fillAmount = 1.0f;
    }


    void Update()
    {
        CalculateAmount();
        ultiBar.fillAmount = currentAmount;
    }

    private void CalculateAmount()
    {
        //currentAmount = ch.hp / ch.maxHp;
        currentAmount = CD.ultimate / CD.GetCharacter().ultimateMaxValue;
    }
}
