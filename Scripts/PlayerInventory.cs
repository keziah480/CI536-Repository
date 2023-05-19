using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public bool[] mainParts = { false, false, false, false, false };
    public bool[] fixedParts = { false, false, false, false, false };
    public bool[] jewels = { false, false, false, false, false };
    public bool raisedGate;
    public bool rope;
    public bool ropedWell;
    public bool dogLover;
    public int gold = 0;

    

    Plant[,] plants = new Plant[7,3];
    [SerializeField] GameObject seedlingObject;
    [SerializeField] GameObject plantObject;

    [SerializeField] TextMeshProUGUI goldCount;
    [SerializeField] GameObject newItemUI;
    [SerializeField] Image newItemImage;
    [SerializeField] Sprite[] jewelSprites;
    [SerializeField] Sprite[] itemSprites;

    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                plants[i, j] = new Plant();
            }
        }

        StartCoroutine(FindObjectOfType<Fader>().FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        // Update UI
        goldCount.text = "x " + gold.ToString();
        if (Input.GetKeyDown(KeyCode.Z)) { InteractPlants(); }
    }

    public IEnumerator GivePart(int partID)
    {
        newItemUI.SetActive(true);
        newItemImage.sprite = itemSprites[partID];
        mainParts[partID] = true;
        yield return new WaitForSeconds(3f);
        newItemUI.SetActive(false);
        yield return null;
    }
    public IEnumerator GiveJewel(int jewelID)
    {
        newItemUI.SetActive(true);
        newItemImage.sprite = jewelSprites[jewelID];
        jewels[jewelID] = true;
        FindObjectOfType<Crown>().UpdateJewels(jewels);
        yield return new WaitForSeconds(3f);
        newItemUI.SetActive(false);
        yield return null;
    }

    public void SendJewel(int jewelID)
    {
        StartCoroutine(GiveJewel(jewelID));
    }

    public void SendPart(int partID)
    {
        StartCoroutine(GivePart(partID));
    }
    

    public void SpawnPlants()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (plants[i, j].growing)
                {
                    if (Time.time - plants[i, j].plantedTime > 20)
                    {
                        Instantiate(plantObject, new Vector3(i + 5 + 0.5f, j - 8 - 0.5f), Quaternion.identity);
                        plants[i, j].pickable = true;
                    }
                    else
                    {
                        Instantiate(seedlingObject, new Vector3(i + 5 + 0.5f, j - 8 - 0.5f), Quaternion.identity);
                    }
                }
            }
        }
    }

    public void InteractPlants()
    {
        int plantSquareX = Mathf.RoundToInt(transform.position.x - 0.5f);
        int plantSquareY = Mathf.RoundToInt(transform.position.y);

        switch (GetComponent<CharacterAnimator>().characterDir)
        {
            case (AnimDir.Right):
                plantSquareX += 1;
                break; 
            case (AnimDir.Left):
                plantSquareX -= 1;
                break;
            case (AnimDir.Up):
                plantSquareY += 1;
                break;
            case (AnimDir.Down):
                plantSquareY -= 1;
                break;
            default:
                break;
        }

        if (5 <= plantSquareX && transform.position.x <= 11 && -8 <= plantSquareY && transform.position.y <= -6)
        {
            plantSquareX -= 5;
            plantSquareY += 8;
            if (plants[plantSquareX, plantSquareY].pickable)
            {
                plants[plantSquareX, plantSquareY].growing = false;
                plants[plantSquareX, plantSquareY].pickable = false;
            }
            else if (!plants[plantSquareX, plantSquareY].growing)
            {
                plants[plantSquareX, plantSquareY].plantedTime = Time.time;
                plants[plantSquareX, plantSquareY].growing = true;
                Instantiate(seedlingObject, new Vector3(plantSquareX + 5.5f, plantSquareY - 8.5f), Quaternion.identity);
            }
        }
    }
    
    
}
