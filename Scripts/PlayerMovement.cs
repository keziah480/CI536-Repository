using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float x, y;
    public float tarX = 0.5f, tarY = 0f;

    [SerializeField] int speed = 1;
    public bool drunk;
    public float drunkTimer;

    CharacterAnimator characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<CharacterAnimator>();
        
    }

    bool CheckIfClear(float tileX, float tileY)
    {
        foreach (Collider2D c in Physics2D.OverlapBoxAll(new Vector2(tileX, tileY), new Vector2(0.8f, 0.8f), 0)){
            if (c.CompareTag("Collidable")) return false;
            if (c.CompareTag("NPC")) return false;
            if (c.CompareTag("ComplexNPC")) return false;
            if (c.CompareTag("Well")) return false;
            if (c.CompareTag("TimeMachine")) return false;
            if (c.CompareTag("MysteryBox")) return false;
        }
        return true;
    }

    public void MoveCharacter(int xVel, int yVel)
    {
        if (xVel != 0) yVel = 0;

        if (x == tarX && y == tarY) {
            // Set target square
            if (CheckIfClear(transform.position.x + xVel, transform.position.y + yVel - 0.5f))
            {
                tarX = x + xVel;
                tarY = y + yVel;
            }

            if (xVel > 0) characterAnimator.characterDir = AnimDir.Right;
            else if (xVel < 0) characterAnimator.characterDir = AnimDir.Left;
            else if (yVel > 0) characterAnimator.characterDir = AnimDir.Up;
            else if (yVel < 0) characterAnimator.characterDir = AnimDir.Down;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (x != tarX || y != tarY)   
        {
            characterAnimator.moving = true;

            // Move towards target and face direction 
            if (x < tarX) { 
                //x += speed * Time.deltaTime;
                characterAnimator.characterDir = AnimDir.Right; 
            }
            else if (x > tarX) { 
                //x -= speed * Time.deltaTime;
                characterAnimator.characterDir = AnimDir.Left;}
            else if (y < tarY) { 
                //y += speed * Time.deltaTime; 
                characterAnimator.characterDir = AnimDir.Up; }
            else if (y > tarY) { 
                //y -= speed * Time.deltaTime; 
                characterAnimator.characterDir = AnimDir.Down; 
            }

            // Update position
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(tarX, tarY), speed * Time.deltaTime);

            // If target position is close enough to current position then just set the current to the target
            if (Mathf.Abs(transform.position.x - tarX) < 0.05) { x = tarX; }
            if (Mathf.Abs(transform.position.y - tarY) < 0.05) { y = tarY; }
            
        }
        else { 
            characterAnimator.moving = false; 
            transform.position = new Vector3(x, y);
        }

        

        if (drunk && drunkTimer < Time.time) { drunk = false; FindObjectOfType<DrunkSwitch>().SwitchActiveState(); }
    }

    public void DrinkAle()
    {
        if (!drunk) { FindObjectOfType<DrunkSwitch>().SwitchActiveState(); }
        drunk = true;
        drunkTimer = Time.time + 30;
    }
}
