using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyLoginViewModel : MonoBehaviour
{
    [SerializeField]
    private Button applyNicknameButton;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private TMP_InputField nicknameField;

    public void ShowLobbyLoginScreen()
    {
        gameObject.SetActive(true);
    }

    public void HideLobbyLoginScreen()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        applyNicknameButton.onClick.AddListener(CheckNicknameValid);
    }

    private void OnDisable()
    {
        applyNicknameButton.onClick.RemoveListener(CheckNicknameValid);    
    }

    private void CheckNicknameValid()
    {
        if (nicknameField.text.Length > 0)
        {
            gameManager.SpawnLocalPlayer(nicknameField.text);
        }
    }
}
