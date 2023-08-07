using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] UpgradeDescriptionPanel upgradeDescriptionPanel;
    PauseManager pauseManager;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    int selectedUpgradeID;
    List<UpgradeData> upgradeData;
    Level characterLevel;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
        characterLevel = GameManager.instance.playerTransform.GetComponent<Level>();
    }

    private void Start()
    {
        HideButtons();
        selectedUpgradeID = -1;
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();

        pauseManager.PauseGame();
        panel.SetActive(true);

        this.upgradeData = upgradeDatas;

        for(int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true); //Solo mostramos aquellos que no hayamos ya utilizado
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }

    //Función que le damos a cada botón del upgrade panel
    public void Upgrade(int pressedButtonID)
    {
        if(selectedUpgradeID != pressedButtonID) //el primer click del botón para que nos dejé ver la info
        {
            selectedUpgradeID = pressedButtonID;
            ShowDescription();
        }
        else //segundo click para seleccionar
        {
            characterLevel.Upgrade(pressedButtonID);
            ClosePanel();
            HideDescriptionPanel();
        }
        
        
    }

    private void HideDescriptionPanel()
    {
        upgradeDescriptionPanel.gameObject.SetActive(false);

    }

    private void ShowDescription()
    {
        upgradeDescriptionPanel.gameObject.SetActive(true);
        upgradeDescriptionPanel.Set(upgradeData[selectedUpgradeID]);

    }

    public void ClosePanel()
    {
        selectedUpgradeID = -1;

        HideButtons();

        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}
