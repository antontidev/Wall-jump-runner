using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private PlayerChooserViewModel viewModelChooser;

    [SerializeField]
    private LobbyLoginViewModel lobbyViewModel;

    [SerializeField]
    private GameObject joystickCanvas;

    [SerializeField]
    private GameObject lobbyLogicCanvas;

    public void ShowDeadUI()
    {
        viewModelChooser.ShowPlayerChooserScreen();

        joystickCanvas.SetActive(false);
    }

    public void ShowLoginUI()
    {
        joystickCanvas.SetActive(false);

        lobbyViewModel.ShowLobbyLoginScreen();
    }

    public void ShowGameUI()
    {
        lobbyViewModel.HideLobbyLoginScreen();

        joystickCanvas.SetActive(true);
    }
}
