using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjectsLoader : MonoBehaviour
{
    [SerializeField] GameObject essentialObjectsPrefab;

    private void Awake()
    {
        EssentialObjects[] existingObjects = FindObjectsOfType<EssentialObjects>();
        if (existingObjects.Length == 0)
            Instantiate(essentialObjectsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void LoadObjects()
    {
        EssentialObjects[] existingObjects = FindObjectsOfType<EssentialObjects>();
        if (existingObjects.Length == 0)
            Instantiate(essentialObjectsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
