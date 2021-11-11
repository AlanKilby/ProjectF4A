using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCreation : MonoBehaviour
{
    [SerializeField] private GameObject playerNameMenu;
    [SerializeField] private TMP_InputField playerNameInput;

    [SerializeField] private Spawner spawner;

    public void ChoosePlayerName()
    {
        spawner.SetPlayerName(playerNameInput.text);
        playerNameMenu.SetActive(false);
        spawner.SpawnNewPlayer();
    }

}
