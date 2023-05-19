using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowScreen : MonoBehaviour
{
    Image image;
    bool phasing;

    void Start()
    {
        image= GetComponent<Image>();   
    }
    private void Update()
    {
        if (!phasing) StartCoroutine(PhaseColors());
    }

    IEnumerator PhaseColors()
    {
        phasing = true;
        Vector3 currentColor = new Vector3(image.color.r, image.color.g, image.color.b);
        Vector3 newColor = new Vector3(0, 0, 0);

        for (int i = 0; i < 2; i++)
        {
            switch (Random.Range(0,4))
            {
                case 0:
                    newColor.x = 1;
                    break;
                case 1:
                    newColor.y = 1;
                    break;
                case 2:
                    newColor.z = 1;
                    break;
            }
        }

        while (currentColor != newColor)
        {
            currentColor = Vector3.MoveTowards(currentColor, newColor, Time.deltaTime);
            image.color = new Color(currentColor.x, currentColor.y,currentColor.z, 0.2f);
            yield return null;
        }

        phasing = false;
    }
}
