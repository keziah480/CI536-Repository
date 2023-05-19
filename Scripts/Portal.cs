using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    PlayerMovement playerMovement;
    PlayerInventory playerInventory;
    [SerializeField] Vector2 teleportPosition;
    Fader fader;
    CameraFollow camera;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Switch scene
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        fader = FindObjectOfType<Fader>();
        camera= FindObjectOfType<CameraFollow>();

        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        DontDestroyOnLoad(gameObject);
        yield return fader.FadeIn();
        playerMovement.x = teleportPosition.x;
        playerMovement.tarX = teleportPosition.x;
        playerMovement.y = teleportPosition.y;
        playerMovement.tarY = teleportPosition.y;
        playerMovement.transform.position = new Vector3(teleportPosition.x, teleportPosition.y);
        SceneManager.LoadScene(sceneToLoad);

        yield return new WaitForSeconds(0.5f);

        switch (sceneToLoad)
        {
            case "Market":
                camera.minX = -33.5f;
                camera.maxX = 10.5f;
                camera.minY = -7f;
                camera.maxY = 13;

                FindObjectOfType<Well>().LoadWell();
                if (playerInventory.raisedGate) { FindObjectOfType<CastleGate>().SetRaised(); }
                playerInventory.SpawnPlants();
                
                FindObjectOfType<MusicBox>().nextClip = 0;
                break;

            case "Tavern":

                camera.minX = 0f;
                camera.maxX = 0f;
                camera.minY = -2f;
                camera.maxY = 1;

                if (playerMovement.drunk) {  FindObjectOfType<DrunkSwitch>().SwitchActiveState(); }
                FindObjectOfType<MusicBox>().nextClip = 1;
                break;

            case "Courtyard":
                camera.minX = -0.5f;
                camera.maxX = -0.5f;
                camera.minY = -3f;
                camera.maxY = 3;
                camera.offsetY = 0;
                FindObjectOfType<MusicBox>().nextClip = 2;
                break;

            case "KingsHall":
                camera.minX = 0.5f;
                camera.maxX = 0.5f;
                camera.minY = -3f;
                camera.maxY = 3;
                camera.offsetY = 2f;
                FindObjectOfType<MusicBox>().nextClip = 3;
                break;

            case "Landing":
                camera.minX = -4f;
                camera.maxX = 11.5f;
                camera.minY = -5f;
                camera.maxY = -1;
                FindObjectOfType<TimeMachine>().FixTimeMachine(playerInventory.fixedParts);
                break;
        }

        if (playerMovement.drunk) { FindObjectOfType<DrunkSwitch>().SwitchActiveState(); }
        yield return fader.FadeOut();
        Destroy(gameObject);
    }

}
