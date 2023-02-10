using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPayingHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Selections;
    [SerializeField]
    private List<GameObject> m_EndButtons;
    [SerializeField]
    private TextMeshProUGUI m_TotalText;
    [SerializeField]
    private Animator m_WalletAnim;
    [SerializeField]
    private GameHandler m_GameHandler;

    private bool m_ClickedOnAd;

    private void OnEnable()
    {
        AdsHandler.RewardVideoWatched += RewardVideoWatched;
        AdsHandler.RewardFailed += RewardFailed;
    }

    private void OnDisable()
    {
        AdsHandler.RewardVideoWatched -= RewardVideoWatched;
        AdsHandler.RewardFailed -= RewardFailed;
    }

    public void Close()
    {
        RemoveSelections();
        RemoveEndButtons();
        m_Selections[0].SetActive(true);
        m_EndButtons[0].SetActive(true);
        m_TotalText.text = "";
        gameObject.SetActive(false);
    }

    public void Buy1500()
    {
        RemoveSelections();
        RemoveEndButtons();
        m_Selections[0].SetActive(true);
        m_EndButtons[0].SetActive(true);
        m_TotalText.text = "2.99";
    }

    public void Buy300()
    {
        RemoveSelections();
        RemoveEndButtons();
        m_Selections[1].SetActive(true);
        m_EndButtons[1].SetActive(true);
        m_TotalText.text = "0.99";
    }

    public void Buy5000()
    {
        RemoveSelections();
        RemoveEndButtons();
        m_Selections[2].SetActive(true);
        m_EndButtons[2].SetActive(true);
        m_TotalText.text = "4.99";
    }

    public void BuyRemoveAds()
    {
        RemoveSelections();
        RemoveEndButtons();
        m_Selections[3].SetActive(true);
        m_EndButtons[3].SetActive(true);
        m_TotalText.text = "1.99";
        Debug.Log("PlayerPrefs.GetInt(Ads)" + PlayerPrefs.GetInt("Ads"));

        if (PlayerPrefs.GetInt("Ads") == 0) //if user already removed ads then it will return "==1", and not enter the condition for buy it again
        {
            RemoveSelections();
            RemoveEndButtons();
            m_Selections[3].SetActive(true);
            m_EndButtons[3].SetActive(true);
            m_TotalText.text = "1.99";
        }
    }

    public void WatchAnAd()
    {
        AdsHandler.Instance.ShowRewardVideoAd();
        m_ClickedOnAd = true;
    }

    private void RewardVideoWatched()
    {
        if (m_ClickedOnAd)
        {
            MoneyHandler.Instance.AddMoney(5); //50
            m_GameHandler.UpdateText();
            m_WalletAnim.SetTrigger("close");
            RemoveSelections();
            m_TotalText.gameObject.SetActive(false);
            m_TotalText.text = "";
            StartCoroutine(DisableWallet());
        }
    }

    private void RewardFailed()
    {
        m_ClickedOnAd = false;
    }

    private void RemoveSelections()
    {
        for (int i = 0; i < m_Selections.Count; i++)
        {
            m_Selections[i].SetActive(false);
        }
    }

    private void RemoveEndButtons()
    {
        for (int i = 0; i < m_EndButtons.Count; i++)
        {
            m_EndButtons[i].SetActive(false);
        }
    }

    public void Add1500Hearts()
    {
        MoneyHandler.Instance.AddMoney(150); //1500
        m_GameHandler.UpdateText();
        m_WalletAnim.SetTrigger("close");
        RemoveSelections();
        m_TotalText.gameObject.SetActive(false);
        m_TotalText.text = "";
        StartCoroutine(DisableWallet());
    }

    public void Add300Hearts()
    {
        MoneyHandler.Instance.AddMoney(30); //300
        m_GameHandler.UpdateText();
        m_WalletAnim.SetTrigger("close");
        RemoveSelections();
        m_TotalText.text = "";
        StartCoroutine(DisableWallet());
    }

    public void Add5000Hearts()
    {
        MoneyHandler.Instance.AddMoney(500); //5000
        m_GameHandler.UpdateText();
        m_WalletAnim.SetTrigger("close");
        RemoveSelections();
        m_TotalText.text = "";
        StartCoroutine(DisableWallet());
    }

    public void RemoveAds()
    {
        AdsHandler.Instance.HideBanner();
        PlayerPrefs.SetInt("Ads", 1);
        m_WalletAnim.SetTrigger("close");
        RemoveSelections();
        m_TotalText.text = "";
        StartCoroutine(DisableWallet());
    }

    IEnumerator DisableWallet()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        RemoveEndButtons();
    }
}
