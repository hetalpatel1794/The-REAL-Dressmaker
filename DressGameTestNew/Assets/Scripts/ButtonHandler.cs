using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Image m_ButtonThread;
    [SerializeField]
    private List<Sprite> m_ButtonThreads;
    [SerializeField]
    private Image m_ThisButton;
    [SerializeField]
    private List<int> m_ThreadButtonIndexes;
    [SerializeField]
    private Image m_Mask;
    [SerializeField]
    private List<GameObject> m_ButtonPricetags;
    [SerializeField]
    private List<GameObject> m_ButtonPricetagButtons;
    [SerializeField]
    private GameObject m_ArrowButton;

    private ButtonsHandler m_ButtonsHandler;

    private int m_ButtonIndex;

    private int m_Index;

    private bool m_ClickedOnAd;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        m_Mask.sprite = m_ThisButton.sprite;
    }

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

    private void RewardVideoWatched()
    {
        if (m_ClickedOnAd)
        {
            transform.tag = "Button";
            PlayerPrefs.SetInt("Button" + m_ButtonIndex, 1);
            RemovePricetags();
            m_ClickedOnAd = false;
        }
    }

    private void RewardFailed()
    {
        m_ClickedOnAd = false;
    }

    public void EnableThread(Color i_Color)
    {
        m_ButtonThread.color = i_Color;
        m_Index = int.Parse(m_ThisButton.sprite.name);

        for (int i = 0; i < m_ThreadButtonIndexes.Count; i++)
        {
            if (m_Index == m_ThreadButtonIndexes[i])
            {
                SetButtonThread();
                m_ButtonThread.gameObject.SetActive(true);
            }
        }
    }

    public void SetButtonsHandler(ButtonsHandler i_ButtonsHandler)
    {
        m_ButtonsHandler = i_ButtonsHandler;
    }

    public void RemoveButton()
    {
        m_ArrowButton.SetActive(false);
    }

    public void DisableThread()
    {
        m_Index = int.Parse(m_ThisButton.sprite.name);

        for (int i = 0; i < m_ThreadButtonIndexes.Count; i++)
        {
            if (m_Index == m_ThreadButtonIndexes[i])
            {
                SetButtonThread();
                m_ButtonThread.gameObject.SetActive(false);
            }
        }
    }

    private void SetButtonThread()
    {
        switch(m_Index)
        {
            case 1: m_ButtonThread.sprite = m_ButtonThreads[0]; break;
            case 2: m_ButtonThread.sprite = m_ButtonThreads[1]; break;
            case 3: m_ButtonThread.sprite = m_ButtonThreads[2]; break;
            case 4: m_ButtonThread.sprite = m_ButtonThreads[3]; break;
            case 5: m_ButtonThread.sprite = m_ButtonThreads[4]; break;
            case 6: m_ButtonThread.sprite = m_ButtonThreads[5]; break;
            case 8: m_ButtonThread.sprite = m_ButtonThreads[6]; break;
            case 11: m_ButtonThread.sprite = m_ButtonThreads[7]; break;
        }
    }

    public void SetPricetag(int i_ButtonIndex)
    {
        m_ButtonIndex = i_ButtonIndex;

        switch(i_ButtonIndex)
        {
            case 4: if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[0].SetActive(true);
                    m_ButtonPricetagButtons[0].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(2); //20
                    transform.tag = "Untagged";
                }
                break;
            case 5:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[1].SetActive(true);
                    m_ButtonPricetagButtons[1].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(2); //20
                    transform.tag = "Untagged";
                }
                break;
            case 6:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[2].SetActive(true);
                    m_ButtonPricetagButtons[2].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(4); //40
                    transform.tag = "Untagged";
                }
                break;
            case 7:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[3].SetActive(true);
                    m_ButtonPricetagButtons[3].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(4); //40
                    transform.tag = "Untagged";
                }
                break;
            case 8:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[4].SetActive(true);
                    m_ButtonPricetagButtons[4].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(4); //40
                    transform.tag = "Untagged";
                }
                break;
            case 9:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[5].SetActive(true);
                    m_ButtonPricetagButtons[5].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(7); //70
                    transform.tag = "Untagged";
                }
                break;
            case 10:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[6].SetActive(true);
                    m_ButtonPricetagButtons[6].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(7); //70
                    transform.tag = "Untagged";
                }
                break;
            case 11:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[7].SetActive(true);
                    m_ButtonPricetagButtons[7].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(7); //70
                    transform.tag = "Untagged";
                }
                break;
            case 12:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[8].SetActive(true);
                    m_ButtonPricetagButtons[8].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(9); //90
                    transform.tag = "Untagged";
                }
                break;
            case 13:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[9].SetActive(true);
                    m_ButtonPricetagButtons[9].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(9); //90
                    transform.tag = "Untagged";
                }
                break;
            case 14:
                if (PlayerPrefs.GetInt("Button" + i_ButtonIndex) == 0)
                {
                    m_ButtonPricetags[10].SetActive(true);
                    m_ButtonPricetagButtons[10].SetActive(true);
                    m_ButtonsHandler.SetCurrentPrice(9); //90
                    transform.tag = "Untagged";
                }
                break;
        }
    }

    public void RemovePricetags()
    {
        for (int i = 0; i < m_ButtonPricetags.Count; i++)
        {
            m_ButtonPricetags[i].SetActive(false);
            m_ButtonPricetagButtons[i].SetActive(false);
        }
    }

    public void BuyButton()
    {
        m_ButtonsHandler.SetBuyingPopup();
    }

    public void WatchAnAdd()
    {
        m_ClickedOnAd = true;
        AdsHandler.Instance.ShowRewardVideoAd();
    }

    public void Close()
    {
        m_ButtonsHandler.RemoveButton();
    }
}
