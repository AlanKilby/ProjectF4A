using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCreation : MonoBehaviour
{
    [SerializeField] private GameObject playerNameMenu;
    [SerializeField] private TMP_InputField playerNameInput;

    [SerializeField] private GameObject characterSelectionMenu;

    [SerializeField] private Spawner spawner;

    public void ChoosePlayerName()
    {
        spawner.SetPlayerName(playerNameInput.text);
        playerNameMenu.SetActive(false);
        characterSelectionMenu.SetActive(true);
    }

    public void ChooseCamelia() 
    {
        spawner.SetChosenCharacter(0);
        CloseCharacterSelection();
        spawner.SpawnNewPlayer();
    }

    public void ChooseSnipy() 
    {
        spawner.SetChosenCharacter(1);
        CloseCharacterSelection();
        spawner.SpawnNewPlayer();
    }

    public void ChooseJacked() 
    {
        spawner.SetChosenCharacter(2);
        CloseCharacterSelection();
        spawner.SpawnNewPlayer();
    }

    private void CloseCharacterSelection() 
    {
        characterSelectionMenu.SetActive(false);
    }

}
