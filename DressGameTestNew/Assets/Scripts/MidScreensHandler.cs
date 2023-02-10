using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MidScreensHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_RemoveAdsButton;
    [SerializeField]
    private GameObject m_WatchAdButton;
    [SerializeField]
    private GameObject m_NextButton;
    [SerializeField]
    private GameObject m_Loading;
    [SerializeField]
    private GameObject m_BackButton;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 1)
        {
            m_RemoveAdsButton.SetActive(false);
            m_WatchAdButton.SetActive(false);
            m_NextButton.SetActive(true);
            m_BackButton.SetActive(true);
        }
    }
    public void RemoveAds()
    {
        PlayerPrefs.SetInt("Ads", 1);
        StartCoroutine(SetButtons());
    }

    IEnumerator SetButtons()
    {
        yield return new WaitForSeconds(0.1f);
        m_RemoveAdsButton.SetActive(false);
        m_WatchAdButton.SetActive(false);
        m_NextButton.SetActive(true);
        m_BackButton.SetActive(true);
    }

    public void Next()
    {
        m_Loading.SetActive(true);
        m_NextButton.SetActive(false);
        m_BackButton.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back()
    {
        m_Loading.SetActive(true);
        m_NextButton.SetActive(false);
        m_BackButton.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
