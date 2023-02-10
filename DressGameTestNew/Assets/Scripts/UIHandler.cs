using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MainMenu;
    [SerializeField]
    private GameObject m_SelectGirls;
    [SerializeField]
    private GameObject m_HiGirls;
    [SerializeField]
    private ExploreHandler m_ExplorePageHandler;
    [SerializeField]
    private PositionsHandler m_PositionHandler;
    [SerializeField]
    private Transform m_RadioParent;
    [SerializeField]
    private GameObject m_ComingSoon;

    //private IEnumerator Start()
    //{
    //    if (SceneManager.GetActiveScene().buildIndex == 0)
    //    {
    //        RadioHandler.Instance.gameObject.transform.SetParent(m_RadioParent);
    //    }
    //}

    private void Start()
    {
        PlayerPrefs.DeleteKey("StartMoney");
    }

    public void Play()
    {
        m_MainMenu.SetActive(false);
        m_SelectGirls.SetActive(true);
        m_ExplorePageHandler.ResetBackgrounds();
        m_PositionHandler.ResetCamera();
        RadioHandler.Instance.PlaySong();
        //RadioHandler.Instance.gameObject.transform.parent = null;
    }

    public void EnableGirls()
    {
        m_SelectGirls.SetActive(false);
        m_HiGirls.SetActive(true);
    }

    public void ComingSoon()
    {
        m_ComingSoon.SetActive(true);
    }

    public void VideoTutorial()
    {
        Application.OpenURL("https://www.tiktok.com/@noiesnoise/video/7197397896126270725");
    }
}
