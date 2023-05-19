using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Cutscene : MonoBehaviour
{
    [SerializeField] Image cutSceneImage;

    [SerializeField] Sprite[] cutsceneShots;

    [SerializeField] EssentialObjectsLoader loader;

    [SerializeField] GameObject title;
    [SerializeField] GameObject playButton;
    AudioSource audioSource;

    bool playingCutscene;

    private void Start()
    {   
        audioSource= GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !playingCutscene) PlayButton();
    }

    public void PlayButton()
    {
        playingCutscene= true;
        title.SetActive(false);
        playButton.SetActive(false);
        StartCoroutine(StartCutscene());
    }
    IEnumerator StartCutscene()
    {
        audioSource.Play();
        yield return new WaitForSeconds(3f);
        cutSceneImage.sprite = cutsceneShots[1];
        audioSource.Play();
        yield return new WaitForSeconds(3f);
        audioSource.Play();
        cutSceneImage.sprite = cutsceneShots[2];
        yield return new WaitForSeconds(3f);
        audioSource.Play();
        cutSceneImage.sprite = cutsceneShots[3];
        yield return new WaitForSeconds(3f);
        audioSource.Play();
        cutSceneImage.sprite = cutsceneShots[4];

        cutSceneImage.transform.localScale = new Vector3(2.2f,1.4f,1);

        for (int i = 0; i < 1000; i++)
        {
            
            cutSceneImage.transform.Rotate(0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene("Landing");
    }
}
