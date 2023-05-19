using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexNPC : MonoBehaviour
{
    [SerializeField] string npcName;

    [SerializeField] ComplexDialogue[] dialogue;

    public NPCState state = NPCState.Idle;

    int currentMove = 0;
    [SerializeField] Vector2[] movementPattern;

    PlayerMovement movement;
    AudioSource audioSource;

    public int npcID;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }
    IEnumerator FollowMovementPattern()
    {
        // Move to next point
        state = NPCState.Walking;

        movement.tarX = movementPattern[currentMove].x;
        movement.tarY = movementPattern[currentMove].y;

        yield return new WaitUntil(() => (movement.x == movement.tarX) && (movement.y == movement.tarY));

        // Wait for a set time (This is when the NPC can be interacted with)

        state = NPCState.Waiting;

        yield return new WaitForSeconds(5f);

        // Wait until the player is no longer talking with the NPC

        yield return new WaitUntil(() => state == NPCState.Waiting);

        // Set next way point
        currentMove += 1;
        if (currentMove >= movementPattern.Length) { currentMove = 0; }

        state = NPCState.Idle;

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        // Walk to next waypoint
        if (state == NPCState.Idle && movementPattern.Length > 0) StartCoroutine(FollowMovementPattern());
    }

    public string GetName() { return npcName; }

    public ComplexDialogue[] GetDialogue()
    {
        return dialogue;
        
    }
}
