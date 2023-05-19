using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePad : MonoBehaviour
{
    [SerializeField] AudioClip note;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.PlayOneShot(note);
    }

}
