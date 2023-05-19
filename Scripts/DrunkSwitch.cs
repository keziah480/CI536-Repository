using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] drunkObjects;
    public void SwitchActiveState()
    {
        foreach (GameObject obj in drunkObjects) {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
