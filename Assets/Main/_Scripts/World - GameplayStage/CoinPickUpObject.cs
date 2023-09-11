using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int coinsAmount;
    [SerializeField] TMPro.TextMeshProUGUI coinsCountText;
    public void OnPickUp(Character character)
    {
        character.coins.Add(coinsAmount);
    }
}
