using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;

public class AdsHandler : MonoBehaviour
{
    public static AdsHandler Instance;

    public delegate void RewardDelegate();
    public static event RewardDelegate RewardVideoWatched;
    public static event RewardDelegate RewardFailed;

    private string m_BannerId = "ca-app-pub-4537330105400946/4788400393";
    private BannerView m_Banner;
    private string m_RewardVideoAdId = "ca-app-pub-4537330105400946/1970665367";
    private RewardedInterstitialAd m_RewardVideoAdd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeAds();
        RequestBanner();
        RequestRewardVideoAdd();
        HideBanner();
    }

    private void InitializeAds()
    {
        MobileAds.Initialize(initStatus => { });
    }

    private void RequestBanner()
    {
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        m_Banner = new BannerView(m_BannerId, adaptiveSize, AdPosition.Bottom);
        AdRequest requestBanner = new AdRequest.Builder().Build();
        m_Banner.LoadAd(requestBanner);
    }

    public void ShowBanner()
    {
        m_Banner.Show();
    }

    public void HideBanner()
    {
        m_Banner.Hide();
    }

    private void RequestRewardVideoAdd()
    {
        AdRequest m_RequestRewardVideoAdd = new AdRequest.Builder().Build();

        RewardedInterstitialAd.LoadAd(m_RewardVideoAdId, m_RequestRewardVideoAdd, AdLoadCallback);
    }

    private void AdLoadCallback(RewardedInterstitialAd i_Add, AdFailedToLoadEventArgs i_Error)
    {
        if (i_Error == null)
        {
            m_RewardVideoAdd = i_Add;

            m_RewardVideoAdd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresent;
            m_RewardVideoAdd.OnAdDidDismissFullScreenContent += HandleAdDidDismiss;
        }
    }

    private void HandleAdFailedToPresent(object sender, AdErrorEventArgs args)
    {
        RewardFailed();
    }

    private void HandleAdDidDismiss(object sender, EventArgs args)
    {
        //RewardFailed();
    }

    public void ShowRewardVideoAd()
    {
        if (m_RewardVideoAdd != null)
        {
            m_RewardVideoAdd.Show(RewardSuccessful);
        }
    }

    private void RewardSuccessful(Reward reward)
    {
        StartCoroutine(WaitBeforeReward());
    }

    IEnumerator WaitBeforeReward()
    {
        yield return new WaitForSeconds(0.1f);
        RequestRewardVideoAdd();
        RewardVideoWatched();
    }
}
