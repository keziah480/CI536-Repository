using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    Image faderImage;

    private void Awake()
    {
        faderImage = GetComponent<Image>();
    }
    public IEnumerator FadeIn()
    {
        float imageAlpha = 0;

        while (imageAlpha < 1.0f)
        {
            faderImage.color = new Color(faderImage.color.r, faderImage.color.g, faderImage.color.b, imageAlpha);
            imageAlpha += 10f * Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
        }

        imageAlpha = 1;
        faderImage.color = new Color(faderImage.color.r, faderImage.color.g, faderImage.color.b, imageAlpha);

        yield return null;
    }
    public IEnumerator FadeOut()
    {
        float imageAlpha = 1;

        while (imageAlpha > 0.0f)
        {
            faderImage.color = new Color(faderImage.color.r, faderImage.color.g, faderImage.color.b, imageAlpha);
            imageAlpha -= 10f * Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
        }

        imageAlpha = 0;
        faderImage.color = new Color(faderImage.color.r, faderImage.color.g, faderImage.color.b, imageAlpha);
        
        yield return new WaitForSeconds(0.001f);
        yield return null;
    }
}
