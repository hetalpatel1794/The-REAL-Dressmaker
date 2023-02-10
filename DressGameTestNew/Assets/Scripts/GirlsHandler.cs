using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GirlsHandler : MonoBehaviour
{
    [SerializeField]
    private MagazineHandler m_MagazineHandler;
    [SerializeField]
    private List<GameObject> m_Girls;
    [SerializeField]
    private List<Animator> m_Anims;
    [SerializeField]
    private List<string> m_AnimNames;
    [SerializeField]
    private List<AudioSource> m_GreatSounds;
    [SerializeField]
    private GameObject m_Bubble;
    [SerializeField]
    private PositionsHandler m_PositionHandler;
    [SerializeField]
    private List<AudioSource> m_HiSounds;

    private int m_GirlIndex;

    private IEnumerator Start()
    {
        //m_PositionHandler.SetCameraToGirlHi();
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();
        m_GirlIndex = PlayerPrefs.GetInt("Girl");
        m_Girls[m_GirlIndex].SetActive(true);
        //yield return new WaitForSeconds(0.95f);
        yield return new WaitForSeconds(0.25f);
        m_Anims[m_GirlIndex].enabled = true;
        yield return new WaitForSeconds(0.7f);
        m_HiSounds[m_GirlIndex].Play();
        m_Bubble.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        m_MagazineHandler.enabled = true;
    }

    public void SelectDress(int i_DressIndex)
    {
        PlayerPrefs.SetInt("Dress", i_DressIndex);
        m_Anims[m_GirlIndex].Play(m_AnimNames[m_GirlIndex], 0, 0);
        m_GreatSounds[m_GirlIndex].Play();
        StartCoroutine(DisableAnimator());
    }

    IEnumerator DisableAnimator()
    {
        yield return new WaitForSeconds(1.5f);
        m_Anims[m_GirlIndex].enabled = false;
    }
}
