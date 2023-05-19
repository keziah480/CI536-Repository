using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ObjectAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    public bool isPlaying = false;
    int curframe = 0;
    [SerializeField] float frameRate;
    float switchFrameTime;

    [SerializeField] GameObject subObject;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (isPlaying && switchFrameTime < Time.time)
        {
            switchFrameTime = Time.time + frameRate;
            if (curframe >= sprites.Length || curframe == 0) curframe = 1;
            spriteRenderer.sprite = sprites[curframe];
            curframe++;
        }
        else if (!isPlaying) spriteRenderer.sprite = sprites[0];
    }

    public void Activate(bool _activate)
    {
        isPlaying = _activate;
        if (subObject != null) subObject.SetActive(_activate);
    }
}
