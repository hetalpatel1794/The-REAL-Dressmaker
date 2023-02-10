using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private SceneLoadFunctionCallingHandler m_SceneLoadFunctionCallingHandler;
    [SerializeField]
    private bool m_LoadDelay;
    [SerializeField]
    private TextMeshProUGUI m_MoneyText;
    [SerializeField]
    private GameObject m_Menu;
    [SerializeField]
    private GameObject m_Shop;

    private bool m_Muted;

    private void Start()
    {
        if (m_MoneyText != null)
        {
            if (PlayerPrefs.HasKey("Money"))
            {
                m_MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
            }

            else
                m_MoneyText.text = MoneyHandler.Instance.GetMoney().ToString();
        }

        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            //AudioListener.volume = 1;
            RadioHandler.Instance.TurnOn();
            m_Muted = false;
        }

        else
        {
            //AudioListener.volume = 0;
            RadioHandler.Instance.TurnOff();
            m_Muted = true;
        }
    }

    public void UpdateText()
    {
        m_MoneyText.text = MoneyHandler.Instance.GetMoney().ToString();
    }

    public void LoadNextScene()
    {
        if (m_SceneLoadFunctionCallingHandler != null)
        {
            m_SceneLoadFunctionCallingHandler.CallFunction();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //if (m_LoadDelay)
        //{
        //    StartCoroutine(LoadDelay());
        //}

        //else
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void HomeButton()
    {
        m_Menu.SetActive(true);
    }

    public void Shop()
    {
        m_Shop.SetActive(true);
        m_Menu.SetActive(false);
    }

    public void Mute()
    {
        if (!m_Muted)
        {
            PlayerPrefs.SetInt("Mute", 1);
            RadioHandler.Instance.TurnOff();
            //AudioListener.volume = 0;
            m_Muted = true;
        }

        else
        {
            PlayerPrefs.SetInt("Mute", 0);
            //AudioListener.volume = 1;
            RadioHandler.Instance.TurnOn();
            m_Muted = false;
        }
    }

    public void Restart()
    {
        Application.OpenURL("https://www.tiktok.com/@noiesnoise/video/7197397896126270725");
    }

    public void Continue()
    {
        m_Menu.SetActive(false);
    }
}
