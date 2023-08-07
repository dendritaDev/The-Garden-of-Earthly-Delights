using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockStageCondition : MonoBehaviour
{
    [SerializeField] DataContainer container;
    [SerializeField] FlagsTable flagTable;
    [SerializeField] string unlockCoinsFlag = "Coins 10000";

    private void OnEnable()
    {
        if(container.coins > 10000)
        {
            Flag flag = flagTable.GetFlag(unlockCoinsFlag);
            flag.state = true;
        }

    }
}
