using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    [SerializeField] GameObject particles;
    PlayerInventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
    }
    public void OpenBox()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                Instantiate(particles, transform.position, Quaternion.identity);
                break;
            case 1:
                if (!inventory.mainParts[2]) inventory.SendPart(2);
                Instantiate(particles, transform.position, Quaternion.identity);
                break;
            case 2:
                if (!inventory.jewels[2]) inventory.SendJewel(2);
                Instantiate(particles, transform.position, Quaternion.identity);
                break;
        }

    }
}
