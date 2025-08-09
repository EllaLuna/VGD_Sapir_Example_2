using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Button button;
    [SerializeField] InputChannel inputChannel;

    void Start()
    {
        var beacon = FindFirstObjectByType<BeaconSO>();
        inputChannel = beacon.inputChannel;
        inputChannel.PauseEvent += PauseGame;

        button.onClick.AddListener(() => PauseGame());
    }

    private void PauseGame()
    {
        if (panel.activeInHierarchy)
        {
            panel.SetActive(false);
            inputChannel.EnablePlayer();
            return;
        }
        if(!panel.activeInHierarchy)
        {
            panel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(button.gameObject);
            inputChannel.EnableUI();
        }
    }

}
