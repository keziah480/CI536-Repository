using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimDir { Neutral, Up, Down, Left, Right };

public class CharacterAnimator : MonoBehaviour
{
    public AnimDir characterDir;
    public bool moving = false;

    [SerializeField] bool isAnimated;

    SpriteRenderer spriteRenderer;

    [SerializeField] Sprite[] upSprites;
    [SerializeField] Sprite[] downSprites;
    [SerializeField] Sprite[] leftSprites;
    [SerializeField] Sprite[] rightSprites;

    [SerializeField] float frameRate;
    float spriteTimer;
    int curSprite;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void UpdateSprite()
    {
        if (!isAnimated) { return; }

        // Set current sprite based on direction and frame
        switch (characterDir)
        {
            case AnimDir.Up:
                spriteRenderer.sprite = upSprites[curSprite];
                break;
            case AnimDir.Down:
                spriteRenderer.sprite = downSprites[curSprite];
                break;
            case AnimDir.Left:
                spriteRenderer.sprite = leftSprites[curSprite];
                break;
            case AnimDir.Right:
                spriteRenderer.sprite = rightSprites[curSprite];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            spriteTimer += Time.deltaTime;

            if (spriteTimer >= frameRate)
            {
                // Move to next frame
                curSprite += 1; 
                spriteTimer -= frameRate;
                if (curSprite >= upSprites.Length) { curSprite = 0; }  
            }
        }

        else { curSprite = 0; }

        UpdateSprite();
    }
}
