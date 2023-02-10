using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LacesHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Laces;
    [SerializeField]
    private Vector3 m_LacesStartPosition;
    [SerializeField]
    private float m_LacesMoveDuration;
    [SerializeField]
    private List<Animator> m_LacesAnims;
    [SerializeField]
    private List<Sprite> m_StartLaces;
    [SerializeField]
    private List<SpriteRenderer> m_LacesSpriteRenderers;
    [SerializeField]
    private List<Animator> m_ScissorsAnims;
    [SerializeField]
    private GameObject m_StartLacesObject;
    [SerializeField]
    private GameObject m_Scissors;
    [SerializeField]
    private DressHandler m_DressHandler;
    [SerializeField]
    private ButtonsHandler m_ButtonsHandler;
    [SerializeField]
    private GameObject m_ColorPickerCircle;
    [SerializeField]
    private GameObject m_ColorPicker;
    [SerializeField]
    private RectTransform m_Circle;
    [SerializeField]
    private Image m_DressLace;
    [SerializeField]
    private PositionsHandlerLastScene m_PositionsHandlerLastScene;
    [SerializeField]
    private AudioSource m_ScissorsSound;
    [SerializeField]
    private GameObject m_Stars;
    [SerializeField]
    private List<GameObject> m_Hints;
    [SerializeField]
    private List<GameObject> m_PriceTags;
    [SerializeField]
    private GameObject m_BuyPopup;
    [SerializeField]
    private List<int> m_LacePrices;
    [SerializeField]
    private GameObject m_NotEnoughMoneyPopup;
    [SerializeField]
    private List<GameObject> m_PriceTagButtons;
    [SerializeField]
    private GameHandler m_GameHandler;
    [SerializeField]
    private GameObject m_Paying;

    private int m_HintCount;

    private float m_ColorValueCurrent;
    private float m_ColorValueEnd;
    private float m_MidValue;
    private float m_OldIndex;

    private Vector3 m_LacesOldPosition;

    private bool m_HintsIncreased;
    private bool m_LacesClicked;
    private bool m_MoveLaces;
    private bool m_CanSelectLaces;
    private bool m_CanClickLaces;

    private bool m_ColorPickerSet;
    private bool m_MoveColorPicker;

    private Vector3 m_ColorPickerOffset;
    private Vector3 m_ColorPickerStartPos;

    private int m_LaceIndex;

    private float m_Hue;

    private SpriteRenderer m_LaceColor;

    private float t;

    private bool m_FirstClick;
    private float m_DoubleClickTimer;

    private bool m_ClickedOnAd;

    private void Start()
    {
        PlayerPrefs.DeleteKey("LaceColor");
        PlayerPrefs.DeleteKey("LaceIndex");
        m_ColorPickerStartPos = m_ColorPickerCircle.transform.localPosition;
    }

    public void ShowHint()
    {
        if (m_HintCount < m_Hints.Count)
        {
            m_Hints[m_HintCount].SetActive(true);
        }
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

    private void Update()
    {
        if (m_MoveLaces)
        {
            t += Time.deltaTime / m_LacesMoveDuration;

            m_Laces.transform.localPosition = Vector3.Lerp(m_LacesOldPosition, m_LacesStartPosition, t);

            if (m_Laces.transform.localPosition == m_LacesStartPosition)
            {
                m_CanSelectLaces = true;
                m_MoveLaces = false;
            }
        }
        
        if (m_MoveColorPicker)
        {
            Vector3 m_NewPos = m_ColorPickerCircle.transform.position;
            m_NewPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_ColorPickerOffset;
            m_NewPos.y = Mathf.Clamp(m_NewPos.y, 1.1f, 2.58f);
            m_NewPos.x = m_ColorPickerCircle.transform.position.x;
            m_NewPos.z = m_ColorPickerCircle.transform.position.z;
            float m_Value = m_Circle.anchoredPosition.y + 174;
            m_Hue = m_Value * 360 / 643.8f;

            if (m_OldIndex == 0 || m_OldIndex == 1)
            {
                m_LaceColor.color = Color.HSVToRGB(m_Hue / 360, 0.5f, 1, true);
            }

            else
            {
                m_LaceColor.material.SetFloat("_HsvShift", m_Hue);
            }

            m_ColorPickerCircle.transform.position = m_NewPos;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    DetectDoubleClick();
        //}

        //if (m_FirstClick)
        //{
        //    if (Time.time - m_DoubleClickTimer > 0.3f)
        //    {
        //        m_FirstClick = false;
        //    }
        //}
    }

    private void DetectDoubleClick()
    {
        if (!m_FirstClick)
        {
            m_FirstClick = true;
            m_DoubleClickTimer = Time.time;
        }

        else
        {
            //SelectLace();
            m_FirstClick = false;
        }
    }

    public void ColorPicker()
    {
        m_ColorPickerOffset = m_ColorPickerCircle.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_MoveColorPicker = true;
    }

    public void ReleaseColorPicker()
    {
        m_MoveColorPicker = false;
    }

    private void ResetColorPicker()
    {
        m_ColorPickerCircle.transform.localPosition = m_ColorPickerStartPos;
    }

    public void ClickLaces()
    {
        if (!m_LacesClicked && m_CanClickLaces)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_StartLacesObject.SetActive(false);
            m_Laces.SetActive(true);
            m_LacesOldPosition = m_Laces.transform.localPosition;
            m_MoveLaces = true;
            m_LacesClicked = true;
        }
    }

    public void CanClickLaces()
    {
        m_CanClickLaces = true;
    }

    public void SelectLace(int i_LaceIndex)
    {
        if (m_CanSelectLaces)
        {
            SetPriceTagsOff();

            if (i_LaceIndex > 2)
            {
                HandleLacePricetags(i_LaceIndex);
            }

            if (m_LaceColor != null)
            {
                //m_LaceColor.material.SetFloat("_HsvShift", 0);
                if (m_OldIndex == 0 || m_OldIndex == 1)
                {
                    m_LaceColor.color = Color.HSVToRGB(0, 0, 1, true);
                }

                else
                    m_LaceColor.material.SetFloat("_HsvShift", 0);
            }

            if (!m_HintsIncreased)
            {
                m_Hints[m_HintCount].SetActive(false);
                m_HintCount++;
                m_HintsIncreased = true;
            }

            if (!m_ColorPickerSet)
            {
                m_ColorPicker.SetActive(true);
                m_ColorPickerSet = true;
            }

            m_LaceColor = m_LacesSpriteRenderers[i_LaceIndex];

            ResetColorPicker();

            m_LaceIndex = i_LaceIndex;
            m_FirstClick = false;

            m_LacesAnims[i_LaceIndex].Rebind();
            m_LacesAnims[i_LaceIndex].enabled = true;
            m_LacesAnims[i_LaceIndex].speed = 1;

            for (int i = 0; i < m_LacesAnims.Count; i++)
            {
                if (i != i_LaceIndex)
                {
                    m_LacesAnims[i].enabled = false;
                    m_LacesSpriteRenderers[i].sprite = m_StartLaces[i];
                }
            }
        }

        m_OldIndex = i_LaceIndex;
    }

    private void HandleLacePricetags(int i_LaceIndex)
    {
        switch (i_LaceIndex)
        {
            case 3: if (PlayerPrefs.GetInt("Lace" + 3) == 0)
                {
                    m_PriceTags[0].SetActive(true);
                    m_PriceTagButtons[0].SetActive(true);
                } 
                break;
            case 4: if (PlayerPrefs.GetInt("Lace" + 4) == 0)
                {
                    m_PriceTags[1].SetActive(true);
                    m_PriceTagButtons[1].SetActive(true);
                }
                break;
            case 5: if (PlayerPrefs.GetInt("Lace" + 5) == 0)
                {
                    m_PriceTags[2].SetActive(true);
                    m_PriceTagButtons[2].SetActive(true);
                }
                break;
            case 6: if (PlayerPrefs.GetInt("Lace" + 6) == 0)
                {
                    m_PriceTags[3].SetActive(true);
                    m_PriceTagButtons[3].SetActive(true);
                }
                break;
            case 7: if (PlayerPrefs.GetInt("Lace" + 7) == 0)
                {
                    m_PriceTags[4].SetActive(true);
                    m_PriceTagButtons[4].SetActive(true);
                }
                break;
        }
    }

    private bool IsLaceBought(int i_LaceIndex)
    {
        if (PlayerPrefs.GetInt("Lace" + i_LaceIndex) == 1)
        {
            return true;
        }

        return false;
    }

    private void SetPriceTagsOff()
    {
        for (int i = 0; i < m_PriceTags.Count; i++)
        {
            m_PriceTags[i].SetActive(false);
            m_PriceTagButtons[i].SetActive(false);
        }
    }

    public void SelectLaceFinish()
    {
        if (m_LaceIndex > 2)
        {
            if (IsLaceBought(m_LaceIndex))
            {
                SelectLaceEnd();
            }
        }

        else
        {
            SelectLaceEnd();
        }
    }

    public void ShowBuyPopup()
    {
        m_BuyPopup.SetActive(true);
    }

    public void BuyLace()
    {
        if (MoneyHandler.Instance.CheckIfHaveMoney(m_LacePrices[m_LaceIndex - 3]))
        {
            m_BuyPopup.SetActive(false);
            MoneyHandler.Instance.RemoveMoney(m_LacePrices[m_LaceIndex - 3]);
            m_GameHandler.UpdateText();
            PlayerPrefs.SetInt("Lace" + m_LaceIndex, 1);
            SetPriceTagsOff();
        }

        else
        {
            m_NotEnoughMoneyPopup.SetActive(true);
        }
    }

    public void CloseBuyPopup()
    {
        m_BuyPopup.SetActive(false);
    }

    public void ContinuePlaying()
    {
        m_NotEnoughMoneyPopup.SetActive(false);
        m_BuyPopup.SetActive(false);
    }

    public void AddMoreMoney()
    {
        m_Paying.SetActive(true);
        m_BuyPopup.SetActive(false);
        m_NotEnoughMoneyPopup.SetActive(false);
    }

    public void WatchAnAd()
    {
        m_ClickedOnAd = true;
        AdsHandler.Instance.ShowRewardVideoAd();
    }

    private void RewardVideoWatched()
    {
        if (m_ClickedOnAd)
        {
            PlayerPrefs.SetInt("Lace" + m_LaceIndex, 1);
            SetPriceTagsOff();
            m_ClickedOnAd = false;
        }
    }

    private void RewardFailed()
    {
        m_ClickedOnAd = false;
    }

    private void SelectLaceEnd()
    {
        m_Hints[m_HintCount].SetActive(false);
        m_HintCount++;
        m_ScissorsAnims[m_LaceIndex].gameObject.SetActive(true);
        m_ScissorsAnims[m_LaceIndex].speed = 1;
        m_ColorPicker.SetActive(false);
        m_Scissors.SetActive(false);

        if (m_LaceIndex == 0 || m_LaceIndex == 1)
        {
            m_DressLace.material = null;
            if (m_Hue / 360 == 0)
            {
                m_DressLace.color = Color.HSVToRGB(m_Hue / 360, 0, 1, true);
            }

            else
                m_DressLace.color = Color.HSVToRGB(m_Hue / 360, 0.5f, 1, true);

            PlayerPrefs.SetFloat("LaceColor", m_Hue / 360);
        }

        else
        {
            m_DressLace.material.SetFloat("_HsvShift", m_Hue);
            PlayerPrefs.SetFloat("LaceColor", m_Hue);
        }

        StartCoroutine(DisableLaces());
    }

    IEnumerator DisableLaces()
    {
        yield return new WaitForSeconds(0.8f);
        m_LacesAnims[m_LaceIndex].speed = 1;
        yield return new WaitForSeconds(0.5f);
        m_Laces.SetActive(false);
        m_DressHandler.SetLace(m_LaceIndex);
        //m_ButtonsHandler.enabled = true;
        m_Stars.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        m_PositionsHandlerLastScene.SetCameraToButtons();
        PlayerPrefs.SetInt("LaceIndex", m_LaceIndex);
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
