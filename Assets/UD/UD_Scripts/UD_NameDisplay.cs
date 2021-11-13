using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UD_NameDisplay : MonoBehaviour
{
    public CharacterDisplay CD;

    private TMP_Text playerNameText;

    private void Start()
    {
        playerNameText.text = CD.GetPlayerName();
    }
}
