using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WatchAdsHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_LoadingScreen;

    private void Start()
    {
        AdsHandler.Instance.HideBanner();
    }
    private void OnEnable()
    {
        AdsHandler.RewardVideoWatched += RewardVideoWatched;
    }

    private void OnDisable()
    {
        AdsHandler.RewardVideoWatched -= RewardVideoWatched;
    }

    public void WatchAnAdd()
    {
        AdsHandler.Instance.ShowRewardVideoAd();
    }

    public void RemoveAds()
    {

    }

    private void RewardVideoWatched()
    {
        m_LoadingScreen.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
