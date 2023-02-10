using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineBuyingHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Prices;
    //[SerializeField]
    //private Animator m_PricesForward;
    //[SerializeField]
    //private Animator m_PricesBackward;
    //[SerializeField]
    //private GameObject m_PricesForwardObject;
    //[SerializeField]
    //private GameObject m_PricesBackwardObject;

    //public void PlayPriceForward(int i_Anim)
    //{
    //    m_PricesForward.Play("F" + i_Anim, 0, 1);
    //}

    //public void CheckIfBoughtForward(int i_Index)
    //{
    //    if (PlayerPrefs.GetInt("DressBought" + i_Index) == 0)
    //    {
    //        m_PricesForwardObject.SetActive(true);
    //    }

    //    else
    //        m_PricesForwardObject.SetActive(false);
    //}

    public void ShowPrice(int i_DressIndex)
    {
        if (PlayerPrefs.GetInt("DressBought" + i_DressIndex) == 0)
        {
            switch(i_DressIndex)
            {
                case 4: m_Prices[0].SetActive(true); break;
                case 5: m_Prices[1].SetActive(true); break;
                case 6: m_Prices[2].SetActive(true); break;
                case 7: m_Prices[3].SetActive(true); break;
                case 8: m_Prices[4].SetActive(true); break;
                case 9: m_Prices[5].SetActive(true); break;
            }
        }
    }

    public void RemovePricesImmediately()
    {
        for (int i = 0; i < m_Prices.Count; i++)
        {
            m_Prices[i].SetActive(false);
        }
    }

    public void RemovePrices()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < m_Prices.Count; i++)
        {
            m_Prices[i].SetActive(false);
        }
    }

    //public void PlayPricesBackward(int i_Anim)
    //{
    //    m_PricesBackwardObject.SetActive(true);
    //    m_PricesBackward.Play("B" + i_Anim, 0, 1);
    //    m_PricesForward.Play("B" + (i_Anim-1) + "A", 0, 1);
    //}

    //public void PlayAnimationForward(int i_DressIndex)
    //{
    //    if (PlayerPrefs.GetInt("DressBought" + i_DressIndex) == 0)
    //    {
    //        switch (i_DressIndex)
    //        {
    //            case 6: m_PricesForward.gameObject.SetActive(true); m_PricesForward.Play("1", 0, 0); break;
    //            case 7: m_PricesForward.gameObject.SetActive(true); m_PricesForward.Play("2", 0, 0); break;
    //            case 8: m_PricesForward.gameObject.SetActive(true); m_PricesForward.Play("3", 0, 0); break;
    //            case 9: m_PricesForward.gameObject.SetActive(true); m_PricesForward.Play("4", 0, 0); break;
    //        }
    //    }
    //}

    //public void PlayAnimationBackward(int i_DressIndex)
    //{
    //    if (PlayerPrefs.GetInt("DressBought" + i_DressIndex) == 0)
    //    {
    //        switch (i_DressIndex)
    //        {
    //            case 6: m_PricesForward.gameObject.SetActive(true); m_PricesForward.Play("8", 0, 0); break;
    //            case 7: m_PricesForward.gameObject.SetActive(true); m_PricesForward.Play("7", 0, 0); break;
    //            case 8: m_PricesForward.gameObject.SetActive(true); m_PricesForward.Play("6", 0, 0); break;
    //            case 9: m_PricesForward.gameObject.SetActive(true); m_PricesForward.Play("5", 0, 0); break;
    //        }
    //    }
    //}

    //public void DisableAnim()
    //{
    //    m_PricesForward.gameObject.SetActive(false);
    //}
}
