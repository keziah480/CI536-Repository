using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjects : MonoBehaviour
{
    private void Awake()
    {
        // Keep this object between scenes
        DontDestroyOnLoad(gameObject);
    }
}
