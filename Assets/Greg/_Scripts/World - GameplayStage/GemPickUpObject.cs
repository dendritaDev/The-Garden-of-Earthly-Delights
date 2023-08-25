using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int amountEXP;
    public void OnPickUp(Character character)
    {
        character.level.AddExperience(amountEXP);
    }
}
