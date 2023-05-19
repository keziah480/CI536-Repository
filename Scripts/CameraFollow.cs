using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform followTransform;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float offsetX;
    public float offsetY;

    void Update()
    {
        // Fix the camera to the player's position
        transform.position = new Vector3(Mathf.Clamp(followTransform.position.x+offsetX, minX, maxX), Mathf.Clamp(followTransform.position.y+offsetY, minY, maxY), -10);
    }
}
