using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    bool raised;
    bool raising;
    bool roped;

    [SerializeField] Transform bucket;
    [SerializeField] float maxBucketHeight;
    [SerializeField] float raiseSpeed;
    [SerializeField] Sprite[] wellSprites;
    [SerializeField] Sprite[] bucketSprites;
    [SerializeField] SpriteRenderer wellRenderer;
    [SerializeField] SpriteRenderer bucketRenderer;
    PlayerInventory inventory;

    private void Start()
    {
        inventory= FindObjectOfType<PlayerInventory>();
    }

    public void InteractWell()
    {
        if (!roped && inventory.rope)
        {
            wellRenderer.sprite = wellSprites[1];
            roped = true;
            inventory.ropedWell= true;
        }
        else if (roped && !raised && !raising)
        {
            StartCoroutine(RaiseBucket());
        }
        else if (raised && !inventory.mainParts[1]) { bucketRenderer.sprite = bucketSprites[0]; StartCoroutine(inventory.GivePart(1)); }
    }

    IEnumerator RaiseBucket()
    {
        raising = true;

        while (bucket.position.y < maxBucketHeight)
        {
            yield return new WaitForSeconds(0.01f);
            bucket.position = new Vector3(bucket.position.x, bucket.position.y + raiseSpeed, bucket.position.z);
        }

        bucket.position = new Vector3(bucket.position.x, maxBucketHeight, bucket.position.z);

        raised = true;
    }

    public void LoadWell()
    {
        if (inventory.mainParts[1]) { bucketRenderer.sprite = bucketSprites[0]; if (inventory.ropedWell) InteractWell(); StartCoroutine(RaiseBucket()); }
    }
}
