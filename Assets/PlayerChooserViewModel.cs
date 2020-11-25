using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooserViewModel : MonoBehaviour
{
    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private NetworkCameraController cameraController;

    public void ShowPlayerChooserScreen()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        nextButton.onClick.AddListener(cameraController.NextTargetPlayer);
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveListener(cameraController.NextTargetPlayer);
    }
}
