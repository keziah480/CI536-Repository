using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    [SerializeField] GameObject[] jewels;

    public void UpdateJewels(bool[] playerJewels)
    {
        for (int i = 0; i < 5; i++)
        {
            jewels[i].SetActive(playerJewels[i]);
        }
        
    }
}
