using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] musicNotes;
    [SerializeField] GameObject lute;

    public IEnumerator PlaySongPart1()
    {
        audioSource = FindObjectOfType<MusicBox>().audioSources[FindObjectOfType<MusicBox>().currentSource];

        while (audioSource.volume > 0) { audioSource.volume -= 0.01f; yield return new WaitForSeconds(0.01f); }
        audioSource.Stop();
        audioSource.volume = 0.8f;

        // Play first note group
        lute.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(musicNotes[0]);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(musicNotes[1]);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(musicNotes[2]);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(musicNotes[3]);
        yield return new WaitForSeconds(0.75f);
        audioSource.PlayOneShot(musicNotes[4]);
        yield return new WaitForSeconds(0.25f);
        audioSource.PlayOneShot(musicNotes[5]);
        yield return new WaitForSeconds(0.5f);
        lute.SetActive(false);
    }
    public IEnumerator PlaySongPart2()
    {
        // Play second note group

        lute.SetActive(true);

        audioSource.PlayOneShot(musicNotes[6]);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(musicNotes[7]);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(musicNotes[8]);
        yield return new WaitForSeconds(0.75f);
        audioSource.PlayOneShot(musicNotes[9]);
        yield return new WaitForSeconds(0.25f);
        audioSource.PlayOneShot(musicNotes[10]);
        yield return new WaitForSeconds(0.5f);

        lute.SetActive(false);

    }
    public IEnumerator PlaySongPart3()
    {
        // Play third note group

        lute.SetActive(true);

        audioSource.PlayOneShot(musicNotes[11]);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(musicNotes[12]);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(musicNotes[13]);
        yield return new WaitForSeconds(0.75f);
        audioSource.PlayOneShot(musicNotes[14]);
        yield return new WaitForSeconds(0.25f);
        audioSource.PlayOneShot(musicNotes[15]);
        yield return new WaitForSeconds(0.5f);

        lute.SetActive(false);

    }
    public IEnumerator PlaySongPart4()
    {
        // Play fourth note group
        lute.SetActive(true);


        audioSource.PlayOneShot(musicNotes[16]);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(musicNotes[17]);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(musicNotes[18]);
        yield return new WaitForSeconds(0.75f);

        lute.SetActive(false);

        audioSource.Play();


    }
    public IEnumerator PlayWrongNote(int numNote)
    {

        audioSource.PlayOneShot(musicNotes[numNote]);
        yield return new WaitForSeconds(1f);

        audioSource.Play();
    }
}
