using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UD_NameDisplay : MonoBehaviour
{
    public TMP_Text playerNameText;

    public void DisplayName(string playerNameData)
    {
        playerNameText.text = playerNameData;
    }
}
