using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MaterialSelectionHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Materials;
    [SerializeField]
    private float m_SwipeDuration;
    [SerializeField]
    private float m_MinSwipeDistance;
    [SerializeField]
    private GameObject m_MaterialSamplerObject;
    [SerializeField]
    private List<GameObject> m_MaterialSamples;
    [SerializeField]
    private List<Image> m_MaterialPatternImages;
    [SerializeField]
    private List<Image> m_MaterialColorImages;
    [SerializeField]
    private List<Sprite> m_Patterns;
    [SerializeField]
    private GameObject m_FinishButton;
    [SerializeField]
    private GameObject m_ColorPicker;
    [SerializeField]
    private ColorPickerHandler m_ColorPickerHandler;
    [SerializeField]
    private PositionsHandler m_PositionHandler;
    //[SerializeField]
    //private GameObject m_Measures;
    [SerializeField]
    private GameObject m_Magazine;
    [SerializeField]
    private GameObject m_MeasuringTape;
    [SerializeField]
    private GameObject m_Notebook;
    [SerializeField]
    private List<GameObject> m_Dresses;
    [SerializeField]
    private GameObject m_OpenMaterialsButton;
    [SerializeField]
    private List<GameObject> m_GirlsMeasures;
    [SerializeField]
    private GameObject m_Parent;
    [SerializeField]
    private List<GameObject> m_Outlines;
    [SerializeField]
    private Image m_MagazineImage;
    [SerializeField]
    private List<Sprite> m_MagazinePages;
    [SerializeField]
    private GameObject m_Stars;
    [SerializeField]
    private List<GameObject> m_Hints;
    [SerializeField]
    private GameObject m_BuyingPopup;
    [SerializeField]
    private GameObject m_NotEnoughMoneyPopup;
    [SerializeField]
    private List<GameObject> m_Pricetags;
    [SerializeField]
    private GameHandler m_GameHandler;
    [SerializeField]
    private GameObject m_Paying;

    [SerializeField]
    private List<Image> m_PricetagImages;
    [SerializeField]
    private GameObject m_ChairtyChild;

    [SerializeField]
    private GameObject m_ComingSoon;
    [SerializeField]
    private GameObject m_CharityTicket;

    private int m_HintCount;

    private List<GameObject> m_DressSteps = new List<GameObject>();

    private Quaternion m_OldRot;
    private Quaternion m_NewRot;

    private Image m_DummyColorImage;
    private Image m_DummyPatternImage;

    private float m_NewValue;
    private float m_OldValue;

    private Vector3 m_FirstMousePos;
    private Vector3 m_SecondMousePos;

    private int m_AnimsIndex;
    private float t;
    private int m_MaterialSampleCount;
    private int m_DressCount;

    private int m_SampleIndex;
    private int m_PatternIndex;

    private int m_CurrentPrice;

    private bool m_MaterialsOpened;
    private bool m_CanSwipeMaterials;
    private bool m_Rotate;
    private bool m_ScreenPressed;
    private bool m_ColorPickerOpened;
    private bool m_CanChangePatterns;

    private bool m_SelectAgain;

    private bool m_ClickedOnAd;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();
        m_AnimsIndex = PlayerPrefs.GetInt("Dress");

        m_MagazineImage.sprite = m_MagazinePages[m_AnimsIndex];

        m_Dresses[m_AnimsIndex].SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            m_DressSteps.Add(m_Dresses[m_AnimsIndex].transform.GetChild(i).gameObject);
        }

        m_OldRot = m_Materials.transform.rotation;
        m_NewRot = m_OldRot;

        m_NewValue = 23.24f;
        m_OldValue = m_NewValue;

        EnablePricetags();
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

    public void ShowHint()
    {
        if (m_HintCount < m_Hints.Count)
        {
            m_Hints[m_HintCount].SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_CanSwipeMaterials)
            {
                m_FirstMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                m_ScreenPressed = true;
            }

            if (m_ComingSoon.activeInHierarchy)
            {
                m_ComingSoon.SetActive(false);
            }
        }

        if (Input.GetMouseButtonUp(0) && m_ScreenPressed && !m_ColorPickerHandler.IsPickingColor())
        {
            SwipeMaterials();
        }

        if (m_Rotate)
        {
            RotateMaterials();
        }
    }

    public void OpenMaterials()
    {
        if (!m_MaterialsOpened)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_Materials.SetActive(true);
            m_MaterialSamplerObject.SetActive(false);
            m_CanSwipeMaterials = true;
            m_DressSteps[0].SetActive(true);
            m_OpenMaterialsButton.SetActive(false);
            m_MaterialsOpened = true;
        }
    }

    public void ComingSoon()
    {
        m_ComingSoon.SetActive(true);
    }
    public void ShowCharityTicket()
    {
        m_CharityTicket.SetActive(true);
    }
    public void OpenCharityPage()
    {
        Application.OpenURL("https://dandelionafrica.org/");
    }
    public void SelectMaterial(int i_MaterialIndex)
    {
        if (i_MaterialIndex > 23)
        {
            if (PlayerPrefs.GetInt("MaterialBought" + i_MaterialIndex) == 1)
            {
                SelectMaterialFinish(i_MaterialIndex);
            }
        }

        else
            SelectMaterialFinish(i_MaterialIndex);
    }
    private void EnablePricetags()
    {
        for (int  i = 10; i < 30; i++)
        {
            if (PlayerPrefs.GetInt("MaterialBought" + i) == 0)
            {
                m_Pricetags[i - 10].SetActive(true);
            }
        }
    }

    public void WatchAnAdd()
    {
        m_ClickedOnAd = true;
        AdsHandler.Instance.ShowRewardVideoAd();
    }

    private void RewardVideoWatched()
    {
        if (m_ClickedOnAd)
        {
            PlayerPrefs.SetInt("MaterialBought" + m_PatternIndex, 1);
            m_Pricetags[m_PatternIndex - 10].SetActive(false);
            m_ClickedOnAd = false;
        }
    }

    private void RewardFailed()
    {
        m_ClickedOnAd = false;
    }

    public void OpenBuyPopup(int i_CurrentPrice)
    {
        m_BuyingPopup.SetActive(true);
        m_CurrentPrice = i_CurrentPrice;
        //m_PatternIndex = i_MaterialIndex;
        //switch(i_CurrentPrice)
        //{
        //    case 100: m_PatternIndex = 25; break;
        //    case 250: m_PatternIndex = 26; break;
        //    case 400: m_PatternIndex = 27; break;
        //    case 600: m_PatternIndex = 28; break;
        //}
    }

    public void SetIndex(int i_MaterialIndex)
    {
        m_PatternIndex = i_MaterialIndex;
    }

    public void Buy()
    {
        if (m_CurrentPrice == 1)
        {
            Debug.Log("Buy");
        }

        else
        {
            if (MoneyHandler.Instance.CheckIfHaveMoney(m_CurrentPrice))
            {
                m_BuyingPopup.SetActive(false);
                m_NotEnoughMoneyPopup.SetActive(false);
                MoneyHandler.Instance.RemoveMoney(m_CurrentPrice);
                m_GameHandler.UpdateText();
                PlayerPrefs.SetInt("MaterialBought" + m_PatternIndex, 1);
                m_BuyingPopup.SetActive(false);
                m_Pricetags[m_PatternIndex - 10].SetActive(false);
            }

            else
                m_NotEnoughMoneyPopup.SetActive(true);
        }
    }

    public void ContinuePlaying()
    {
        m_BuyingPopup.SetActive(false);
        m_NotEnoughMoneyPopup.SetActive(false);
    }

    public void AddMoney()
    {
        m_Paying.SetActive(true);
        m_BuyingPopup.SetActive(false);
        m_NotEnoughMoneyPopup.SetActive(false);
    }

    public void UnlockCharityMaterial()
    {
        PlayerPrefs.SetInt("MaterialBought" + 29, 1);
        m_PricetagImages[0].enabled = false;
        m_ChairtyChild.SetActive(false);
        StartCoroutine(DisablePrices(19));
    }

    public void UnlockMaterial1()
    {
        PlayerPrefs.SetInt("MaterialBought" + 28, 1);
        m_PricetagImages[1].enabled = false;
        StartCoroutine(DisablePrices(18));
    }

    public void UnlockMaterial2()
    {
        PlayerPrefs.SetInt("MaterialBought" + 27, 1);
        m_PricetagImages[2].enabled = false;
        StartCoroutine(DisablePrices(17));
    }

    public void UnlockMaterial3()
    {
        PlayerPrefs.SetInt("MaterialBought" + 26, 1);
        m_PricetagImages[3].enabled = false;
        StartCoroutine(DisablePrices(16));
    }

    IEnumerator DisablePrices(int i_PriceIndex)
    {
        yield return new WaitForSeconds(0.1f);
        m_Pricetags[i_PriceIndex].SetActive(false);
    }

    private void SelectMaterialFinish(int i_MaterialIndex)
    {
        if (!m_SelectAgain)
        {
            if (!m_ColorPickerOpened)
            {
                m_ColorPicker.SetActive(true);
                m_ColorPickerOpened = true;
            }

            m_PatternIndex = i_MaterialIndex;

            if (m_MaterialSampleCount < m_MaterialSamples.Count)
            {
                if (m_MaterialSampleCount > 0)
                {
                    m_ColorPickerHandler.ResetColor();
                }

                m_SampleIndex = m_MaterialSampleCount;
                m_MaterialSamples[m_MaterialSampleCount].SetActive(true);
                m_MaterialPatternImages[m_MaterialSampleCount].sprite = m_Patterns[i_MaterialIndex];
                PlayerPrefs.SetInt("Pattern" + m_MaterialSampleCount, i_MaterialIndex);
                //m_DummyColorImage = m_MaterialColorImages[m_MaterialSampleCount];
                m_DummyColorImage = m_MaterialPatternImages[m_MaterialSampleCount];
                m_MaterialSampleCount++;

                if (m_MaterialSampleCount == m_MaterialSamples.Count)
                {
                    if (!m_FinishButton.activeInHierarchy)
                    {
                        m_FinishButton.SetActive(true);
                    }
                }

                m_DressCount++;

                switch (m_DressCount)
                {
                    case 1:
                        m_DressSteps[0].SetActive(false);
                        m_DressSteps[1].SetActive(true);
                        break;
                    case 2:
                        m_DressSteps[1].SetActive(false);
                        m_DressSteps[2].SetActive(true);
                        break;
                    case 3:
                        m_CanChangePatterns = true;
                        break;
                }
            }
        }

        else
        {
            m_PatternIndex = i_MaterialIndex;
            m_DummyPatternImage.sprite = m_Patterns[i_MaterialIndex];
            PlayerPrefs.SetInt("Pattern" + m_SampleIndex, i_MaterialIndex);
            m_DummyColorImage.materialForRendering.SetFloat("_HsvShift", 0);
            m_DummyColorImage.materialForRendering.SetFloat("_HsvBright", 1);
        }
    }

    public void ClickMaterialSampler(int i_SamplerIndex)
    {
        if (m_CanChangePatterns)
        {
            for (int i = 0; i < m_DressSteps.Count; i++)
            {
                if (i_SamplerIndex != i)
                {
                    m_DressSteps[i].SetActive(false);
                }

                else
                    m_DressSteps[i].SetActive(true);
            }

            for (int i = 0; i < m_Outlines.Count; i++)
            {
                if (i_SamplerIndex == i)
                {
                    m_Outlines[i].SetActive(true);
                }

                else
                {
                    m_Outlines[i].SetActive(false);
                }
            }

            m_MaterialSampleCount = i_SamplerIndex;
            m_SampleIndex = m_MaterialSampleCount;
            m_DummyPatternImage = m_MaterialPatternImages[i_SamplerIndex];
            m_DummyColorImage = m_DummyPatternImage;
            m_ColorPickerHandler.ResetColor();
            m_SelectAgain = true;
        }
    }

    public void SetColor(float i_Color)
    {
        if (m_PatternIndex == 8)
        {
            float m_BrightnessValue = i_Color * 2 / 360;

            m_DummyColorImage.materialForRendering.SetFloat("_HsvBright", m_BrightnessValue);
            PlayerPrefs.SetFloat("Color" + m_SampleIndex, m_BrightnessValue);
        }

        else
        {
            m_DummyColorImage.materialForRendering.SetFloat("_HsvShift", i_Color);
            PlayerPrefs.SetFloat("Color" + m_SampleIndex, i_Color);
        }
    }

    public void EnableColor()
    {
        m_DummyColorImage.gameObject.SetActive(true);
    }

    public void FinishMaterialSelection()
    {
        m_Hints[m_HintCount].SetActive(false);
        m_HintCount++;
        m_Stars.SetActive(true);
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        //yield return new WaitForSeconds(1f);
        //m_PositionHandler.SetCameraToMeasures();
        //m_Measures.SetActive(true);
        //m_GirlsMeasures[PlayerPrefs.GetInt("Girl")].SetActive(true);
        //StartCoroutine(LoadNextScene());
        //m_Magazine.SetActive(false);
        //m_MeasuringTape.SetActive(false);
        //m_Notebook.SetActive(false);
        //m_Parent.SetActive(false);
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //gameObject.SetActive(false);
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RotateMaterials()
    {
        t += Time.deltaTime / m_SwipeDuration;

        m_Materials.transform.rotation = Quaternion.Slerp(m_OldRot, m_NewRot, t);

        if (m_Materials.transform.rotation == m_NewRot)
        {
            m_Rotate = false;
        }
    }

    private void SwipeMaterials()
    {
        m_SecondMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (Mathf.Abs(m_SecondMousePos.x - m_FirstMousePos.x) > m_MinSwipeDistance)
        {
            if (m_SecondMousePos.x > m_FirstMousePos.x)
            {
                m_Hints[m_HintCount].SetActive(false);

                m_NewValue = m_OldValue - 34.86f;

                m_OldRot = Quaternion.Euler(0, 0, m_OldValue);
                m_NewRot = Quaternion.Euler(0, 0, m_NewValue);

                m_OldValue = m_NewValue;

                t = 0;
                m_Rotate = true;
            }

            else if (m_SecondMousePos.x < m_FirstMousePos.x)
            {
                m_Hints[m_HintCount].SetActive(false);

                m_NewValue = m_OldValue + 34.86f;

                m_OldRot = Quaternion.Euler(0, 0, m_OldValue);
                m_NewRot = Quaternion.Euler(0, 0, m_NewValue);

                m_OldValue = m_NewValue;

                t = 0;
                m_Rotate = true;
            }
        }

        m_ScreenPressed = false;
    }
}
