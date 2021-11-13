using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    private bool canOpenMainMenu;
    private bool isActive;

    [SerializeField] private GameObject mainMenu;

    [SerializeField] private KeyCode keyMainMenu;

    private void Start()
    {
        canOpenMainMenu = false;
        isActive = false;
    }

    private void Update()
    {
        if (canOpenMainMenu && Input.GetKeyDown(keyMainMenu))
        {
            ChangeMainMenuState();
        }
    }

    private void ChangeMainMenuState()
    {
        isActive = !isActive;
        mainMenu.SetActive(isActive);
    }

    public void Resume() 
    {
        ChangeMainMenuState();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitRoom() 
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(1);
    }

    public void SetCanOpenMainMenu(bool canOpen) 
    {
        this.canOpenMainMenu = canOpen;
    }
}
