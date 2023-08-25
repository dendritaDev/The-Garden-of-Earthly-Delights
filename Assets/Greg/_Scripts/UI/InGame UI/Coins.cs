using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] DataContainer data;
    [SerializeField] TMPro.TextMeshProUGUI coinsCountText;

    public void Add(int amount)
    {
        data.coins += amount;
        coinsCountText.text = string.Format("Coins: {0}", data.coins.ToString());
    }
}
