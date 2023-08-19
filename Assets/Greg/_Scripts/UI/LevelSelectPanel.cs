using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPanel : MonoBehaviour
{
    [SerializeField]
    public List<SelectStageButton> stageSelectButton;

    [SerializeField] FlagsTable flagsTable;

    [SerializeField]
    DataContainer dataContainer;

    public void UpdateButtons()
    {
        for(int i = 0; i < stageSelectButton.Count; i++)
        {
            bool unlocked = UpdateButtons(stageSelectButton[i]);
            stageSelectButton[i].gameObject.SetActive(unlocked);
        }
    }

    private bool UpdateButtons(SelectStageButton stage)
    {
        bool unlocked = true;

        if(stage.stageData.stageCompletionToUnlock == null) { return unlocked; }

        for(int i= 0; i < stage.stageData.stageCompletionToUnlock.Count; i++)
        {
            string id = stage.stageData.stageCompletionToUnlock[i];
            if (flagsTable.GetFlag(id).state == false)
            {
                unlocked = false;
            }
        }

        return unlocked;
    }

    public void UpdateButtonsOnAppearing()
    {
        UpdateButtons();
    }
}
