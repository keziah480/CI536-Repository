using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    CharacterAnimator characterAnimator;
    PlayerInventory inventory;
    PlayerMovement movement;
    [SerializeField] TextBox textBox;
    [SerializeField] TextBox complexTextBox;

    [SerializeField] GameObject mysteryBox;

    public bool interacting = false;
    AnimDir interactDirection;

    public bool[] interacted = { false, false, false, false, false, false, false, false, false };

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<CharacterAnimator>();
        inventory = GetComponent<PlayerInventory>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame

    IEnumerator TalkToNPC(NPC interactedNPC)
    {
        if (interactedNPC.state != NPCState.Waiting) { yield break; }

        interacting = true; 
        interactedNPC.state = NPCState.Talking;
        textBox.SetTextBoxActive(true);

        // Make the NPC face the player
        interactedNPC.GetComponent<CharacterAnimator>().characterDir = interactDirection;

        textBox.SetNameText(interactedNPC.GetName());

        foreach (string line in interactedNPC.GetDialogue())
        {
            textBox.SetDialogueText(line);
            yield return new WaitWhile(() => (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E))); 
            yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E))); 
        }

        textBox.SetTextBoxActive(false);
        interacting= false;
        interactedNPC.state = NPCState.Waiting;

        yield return null;
    }
    IEnumerator TalkToComplexNPC(ComplexNPC interactedNPC)
    {
        if (interacted[interactedNPC.npcID]) { yield break; }
        if (interactedNPC.state != NPCState.Waiting) { yield break; }

        interacting = true; 
        interactedNPC.state = NPCState.Talking;

        // Make the NPC face the player
        interactedNPC.GetComponent<CharacterAnimator>().characterDir = interactDirection;

        textBox.SetNameText(interactedNPC.GetName());
        complexTextBox.SetNameText(interactedNPC.GetName());
        complexTextBox.ChangeSelection();

        foreach (ComplexDialogue line in interactedNPC.GetDialogue())
        {
            if (line.response1 == "") { 
                textBox.SetTextBoxActive(true); 
                complexTextBox.SetTextBoxActive(false);
                textBox.SetDialogueText(line.dialogueLine);
            }
            else { 
                textBox.SetTextBoxActive(false); 
                complexTextBox.SetTextBoxActive(true);
                complexTextBox.SetDialogueText(line.dialogueLine);
                complexTextBox.SetResponse1Text(line.response1);
                complexTextBox.SetResponse2Text(line.response2);
            }
            
            yield return new WaitWhile(() => (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E))); 
            yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E)));

            // Bard 

            if ((line.specialAction1 == 1 && complexTextBox.selection)) { textBox.SetTextBoxActive(false); yield return (FindObjectOfType<Bard>().PlaySongPart1());  }
            else if ((line.specialAction1 == 1 && !complexTextBox.selection)) { break; }

            if ((line.specialAction1 == 2 && complexTextBox.selection) || (line.specialAction2 == 2 && !complexTextBox.selection)) { complexTextBox.SetTextBoxActive(false); yield return (FindObjectOfType<Bard>().PlaySongPart2()); }
            else if ((line.specialAction1 == 2 && !complexTextBox.selection) || (line.specialAction2 == 2 && complexTextBox.selection)) { complexTextBox.SetTextBoxActive(false); yield return FindObjectOfType<Bard>().PlayWrongNote(19); break; }

            if ((line.specialAction1 == 3 && complexTextBox.selection) || (line.specialAction2 == 3 && !complexTextBox.selection)) { complexTextBox.SetTextBoxActive(false); yield return (FindObjectOfType<Bard>().PlaySongPart3()); }
            else if ((line.specialAction1 == 3 && !complexTextBox.selection) || (line.specialAction2 == 3 && complexTextBox.selection)) { complexTextBox.SetTextBoxActive(false); yield return FindObjectOfType<Bard>().PlayWrongNote(20); break; }

            if ((line.specialAction2 == 4 && !complexTextBox.selection) || (line.specialAction2 == 4 && !complexTextBox.selection)) { complexTextBox.SetTextBoxActive(false); yield return (FindObjectOfType<Bard>().PlaySongPart4()); interacted[1] = true; }
            else if ((line.specialAction2 == 4 && complexTextBox.selection) || (line.specialAction2 == 4 && complexTextBox.selection)) { complexTextBox.SetTextBoxActive(false); yield return FindObjectOfType<Bard>().PlayWrongNote(21); break; }

            if (line.specialAction1 == 5) { StartCoroutine(inventory.GivePart(0)); }

            // Bartender
            if (line.specialAction1 == 6 && complexTextBox.selection) { if (inventory.gold >= 10) { movement.DrinkAle(); inventory.gold -= 10; break; } }
            else if ((line.specialAction1 == 6 && !complexTextBox.selection)) { break; }

            // Merchant
            if (line.specialAction1 == 18 && complexTextBox.selection) { }
            else if ((line.specialAction1 == 18 && !complexTextBox.selection)) { break; }
            
            if (line.specialAction1 == 7 && complexTextBox.selection) { if (inventory.gold >= 15) { inventory.rope = true; inventory.gold -= 15; if (inventory.jewels[0]) interacted[2] = true; break; } }
            else if ((line.specialAction1 == 7 && !complexTextBox.selection)) { if (inventory.gold >= 30) { StartCoroutine(inventory.GiveJewel(0)); if (inventory.rope) interacted[2] = true; inventory.gold -= 30; break; } }

            // Guard
            if (line.specialAction1 == 8 && complexTextBox.selection) { yield return FindObjectOfType<CastleGate>().RaiseGate(); interacted[3] = true; inventory.raisedGate = true; break; }
            else if ((line.specialAction1 == 8 && !complexTextBox.selection)) { break; }
            
            // Rich guy
            if (line.specialAction1 == 9 && complexTextBox.selection) { inventory.gold += 1; break; }
            else if ((line.specialAction1 == 9 && !complexTextBox.selection)) {  }
        
            if (line.specialAction1 == 10 && complexTextBox.selection) { StartCoroutine(inventory.GiveJewel(1)); interacted[4] = true; break; }
            else if ((line.specialAction1 == 10 && !complexTextBox.selection)) {  }
        
            //Witch
            if (line.specialAction1 == 11 && complexTextBox.selection) {
                if (inventory.gold >= 5 && (!inventory.jewels[2] || !inventory.mainParts[2]))
                {
                    inventory.gold -= 5;
                    Instantiate(mysteryBox, new Vector3(-1.5f, -0.5f), Quaternion.identity);
                    Instantiate(mysteryBox, new Vector3(0.5f, -0.5f), Quaternion.identity);
                    Instantiate(mysteryBox, new Vector3(2.5f, -0.5f), Quaternion.identity);
                    break;
                }
                
            }
            else if ((line.specialAction1 == 11 && !complexTextBox.selection)) { break; }


            if (line.specialAction1 == 12 && complexTextBox.selection) { StartCoroutine(inventory.GiveJewel(3)); break; }
            else if (line.specialAction1 == 12 && !complexTextBox.selection) { }

            if (line.specialAction1 == 13 && complexTextBox.selection) { StartCoroutine(inventory.GivePart(3)); interacted[7] = true; break; }
            else if (line.specialAction1 == 13 && !complexTextBox.selection) { }

            if (line.specialAction1 == 14 && complexTextBox.selection) { if (inventory.jewels[0] && inventory.jewels[1] && inventory.jewels[2] && inventory.jewels[3] && inventory.jewels[4]) { StartCoroutine(inventory.GivePart(4)); interacted[8] = true; break; } }
            else if (line.specialAction1 == 14 && !complexTextBox.selection) { break; }

            if (line.specialAction1 == 15 && !complexTextBox.selection) { inventory.dogLover = true; break; } 
            else if (line.specialAction1 == 15 && complexTextBox.selection) { inventory.dogLover = true; break; }

            if (line.specialAction1 == 16 && complexTextBox.selection) { if (inventory.dogLover) { StartCoroutine(inventory.GiveJewel(4)); interacted[6] = true; break; } }
            else if (line.specialAction1 == 16 && !complexTextBox.selection) { break; }

            if (line.specialAction1 == 17 && complexTextBox.selection) { StartCoroutine(inventory.GiveJewel(3)); Destroy(interactedNPC.gameObject); interacted[5] = true; break; }
            else if (line.specialAction1 == 17 && !complexTextBox.selection) { }
        }

        textBox.SetTextBoxActive(false);
        complexTextBox.SetTextBoxActive(false);
        interacting= false;
        interactedNPC.state = NPCState.Waiting;

        yield return null;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E)) && !interacting)
        {
            float xCheck = transform.position.x;
            float yCheck = transform.position.y - 0.5f;

            // Find which direction the player is in from the perspective of the NPC
            if (characterAnimator.characterDir == AnimDir.Up) { yCheck += 1; interactDirection = AnimDir.Down; }
            else if (characterAnimator.characterDir == AnimDir.Down) { yCheck -= 1; interactDirection = AnimDir.Up; }
            else if (characterAnimator.characterDir == AnimDir.Left) { xCheck -= 1; interactDirection = AnimDir.Right; }
            else if (characterAnimator.characterDir == AnimDir.Right) { xCheck += 1; interactDirection = AnimDir.Left; }

            foreach (Collider2D c in Physics2D.OverlapBoxAll(new Vector2(xCheck, yCheck), new Vector2(0.8f, 0.8f), 0))
            {
                // Talk to NPC
                if (c.CompareTag("NPC")) StartCoroutine(TalkToNPC(c.GetComponent<NPC>()));

                // Talk to Complex NPC
                if (c.CompareTag("ComplexNPC")) StartCoroutine(TalkToComplexNPC(c.GetComponent<ComplexNPC>()));

                // Collect coin
                if (c.CompareTag("Coin")) { Destroy(c.gameObject); inventory.gold += 1; }

                // Interact with well
                if (c.CompareTag("Well")) { c.GetComponent<Well>().InteractWell(); }

                // Pick corn
                if (c.CompareTag("Plant")) { GetComponent<PlayerInventory>().InteractPlants(); Destroy(c.gameObject); inventory.gold += 3; }
                
                // Fix TimeMachine
                if (c.CompareTag("TimeMachine")) { 
                    c.GetComponent<TimeMachine>().FixTimeMachine(inventory.mainParts);
                    for (int i = 0; i < 5; i++) inventory.fixedParts[i] = inventory.mainParts[i];
                    
                }

                // Open mystery box
                if (c.CompareTag("MysteryBox"))
                {
                    c.GetComponent<MysteryBox>().OpenBox();
                    foreach(MysteryBox m in FindObjectsOfType<MysteryBox>()) Destroy(m.gameObject);
                }
            }
        }

        else if (interacting)
        {
            if (Input.GetAxisRaw("Vertical") > 0) { complexTextBox.selection = true; complexTextBox.ChangeSelection(); }
            if (Input.GetAxisRaw("Vertical") < 0) { complexTextBox.selection = false; complexTextBox.ChangeSelection(); }
        }
    }
}
