using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartHandler : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("Ads", 1);

        if (PlayerPrefs.GetInt("Start") == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else
            PlayerPrefs.SetInt("Start", 1);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
