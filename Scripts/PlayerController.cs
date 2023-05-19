using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement movement;
    PlayerInteraction playerInteraction;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        if (!playerInteraction.interacting)
        {
            movement.MoveCharacter(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));

        }
    }
}
