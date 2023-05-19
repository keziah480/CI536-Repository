using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeMachine : MonoBehaviour
{
    [SerializeField] GameObject[] parts;
    [SerializeField] GameObject finishScreen;
    bool complete;
    public void FixTimeMachine(bool[] playerParts)
    {
        bool tmFixed = true;

        for (int i = 0; i < 5; i++)
        {
            if (!playerParts[i]) tmFixed = false;
            parts[i].SetActive(playerParts[i]);
        }

        if (tmFixed) { complete = true; finishScreen.SetActive(true); }
    }

    private void Update()
    {
        if (complete && Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(FindObjectOfType<EssentialObjects>().gameObject);
            SceneManager.LoadScene("Menu");
        }
    }
}
