using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AllIn1SpriteShader;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonsHandler : MonoBehaviour
{
    [SerializeField]
    private Animator m_ButtonsAnim;
    [SerializeField]
    private List<Sprite> m_Buttons;
    [SerializeField]
    private Image m_ButtonPrefab;
    [SerializeField]
    private GameObject m_ButtonsButtons;
    [SerializeField]
    private GameObject m_Parent;
    [SerializeField]
    private GameObject m_Canvas;
    [SerializeField]
    private DressHandler m_DressHandler;
    [SerializeField]
    private GameObject m_CircleIndicator;
    [SerializeField]
    private GameObject m_ColorPicker;
    [SerializeField]
    private RectTransform m_CircleIndicatorRect;
    [SerializeField]
    private GameObject m_FinishButton;
    //[SerializeField]
    //private NeedleSewingHandler m_NeedleSewingHandler;
    [SerializeField]
    private GameObject m_NewButtonsParent;
    [SerializeField]
    private PositionsHandlerLastScene m_PositionHandlerLastScene;
    [SerializeField]
    private Image m_Lace;
    [SerializeField]
    private List<Sprite> m_LaceSprites1;
    [SerializeField]
    private List<Sprite> m_LaceSprites2;
    [SerializeField]
    private List<Sprite> m_LaceSprites3;
    [SerializeField]
    private List<Sprite> m_LaceSprites4;
    [SerializeField]
    private List<Sprite> m_LaceSprites5;
    [SerializeField]
    private List<Sprite> m_LaceSprites6;
    [SerializeField]
    private List<Sprite> m_LaceSprites7;
    [SerializeField]
    private List<Sprite> m_LaceSprites8;
    [SerializeField]
    private List<Sprite> m_LaceSprites9;
    [SerializeField]
    private List<Sprite> m_LaceSprites10;
    [SerializeField]
    private GameObject m_Dress;
    [SerializeField]
    private GameObject m_NewDressCanvas;
    [SerializeField]
    private Image m_DressDetails;
    [SerializeField]
    private MoveDressHandler m_MoveDressHandler;
    [SerializeField]
    private GameObject m_MagicUp;
    [SerializeField]
    private GameObject m_MagicDown;
    [SerializeField]
    private GameObject m_MoveDressToParavanButton;
    [SerializeField]
    private GameObject m_Stars;
    [SerializeField]
    private List<GameObject> m_Hints;
    [SerializeField]
    private GameObject m_BuyingPopup;
    [SerializeField]
    private GameObject m_NotEnoughMoneyPopup;
    [SerializeField]
    private Canvas m_GameCanvas;
    [SerializeField]
    private GameHandler m_GameHandler;
    [SerializeField]
    private GameObject m_Paying;

    private int m_CurrentPrice;

    private int m_HintCount;

    private List<ButtonHandler> m_AddedButtons = new List<ButtonHandler>();

    private bool m_CanDetectOpenSwipe;
    private bool m_OpenSwipeFirstClicked;

    private Vector3 m_OpenSwipeFirstMousePosition;
    private Vector3 m_OpenSwipeSecondMousePosition;

    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    private GameObject m_SelectedButton;

    private bool m_ButtonsOpened;
    private bool m_CanSelectButton;
    private bool m_HoldingButton;
    private bool m_ColorPickerClicked;
    private bool m_OpenBox;

    private Image m_ButtonColor;

    private ButtonHandler m_SelectedButtonHandler;

    private GameObject m_ButtonToDestroy;

    private float m_Value;

    private int m_ButtonIndex;

    private Vector3 m_Offset;
    private Vector3 m_ColorPickerOffset;
    private Vector3 m_StartCircleIndicatorPosition;

    private bool m_RemoveArrowButton;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();

        SetUpSprite();

        DontDestroy[] m_DontDestroyList = FindObjectsOfType<DontDestroy>();
        if (m_DontDestroyList.Length > 1)
        {
            Destroy(m_DontDestroyList[1].gameObject);
        }

        DontDestroy.Instance.Setup(m_DressHandler, m_DressDetails, m_MagicUp, m_MagicDown, m_MoveDressHandler, m_MoveDressToParavanButton, m_Parent);

        m_Raycaster = m_Canvas.GetComponent<GraphicRaycaster>();
        m_EventSystem = m_Canvas.GetComponent<EventSystem>();
        m_StartCircleIndicatorPosition = m_CircleIndicatorRect.anchoredPosition;

        m_NewDressCanvas = DontDestroy.Instance.GetDressCanvas();
    }

    public void ShowHint()
    {
        if (m_HintCount < m_Hints.Count)
        {
            m_Hints[m_HintCount].SetActive(true);
        }
    }

    public void SetParent()
    {
        m_Dress.transform.SetParent(m_NewDressCanvas.transform);
        m_DressHandler.RemoveButtonDetails();
        m_Parent.transform.SetParent(m_NewButtonsParent.transform);
        Destroy(m_ButtonToDestroy);
    }

    private void SetUpSprite()
    {
        int m_DressIndex = PlayerPrefs.GetInt("Dress");
        int m_LaceIndex = PlayerPrefs.GetInt("LaceIndex");

        switch (m_DressIndex)
        {
            case 0: m_Lace.sprite = m_LaceSprites1[m_LaceIndex]; break;
            case 1: m_Lace.sprite = m_LaceSprites2[m_LaceIndex]; break;
            case 2: m_Lace.sprite = m_LaceSprites3[m_LaceIndex]; break;
            case 3: m_Lace.sprite = m_LaceSprites4[m_LaceIndex]; break;
            case 4: m_Lace.sprite = m_LaceSprites5[m_LaceIndex]; break;
            case 5: m_Lace.sprite = m_LaceSprites6[m_LaceIndex]; break;
            case 6: m_Lace.sprite = m_LaceSprites7[m_LaceIndex]; break;
            case 7: m_Lace.sprite = m_LaceSprites8[m_LaceIndex]; break;
            case 8: m_Lace.sprite = m_LaceSprites9[m_LaceIndex]; break;
            case 9: m_Lace.sprite = m_LaceSprites10[m_LaceIndex]; break;
        }

        float m_LaceColor = PlayerPrefs.GetFloat("LaceColor");
        float m_LaceColorIndex = PlayerPrefs.GetFloat("LaceColor");

        if (m_LaceIndex == 0 || m_LaceIndex == 1)
        {
            m_Lace.material = null;

            if (m_LaceColorIndex / 360 == 0)
            {
                m_Lace.color = Color.HSVToRGB(m_LaceColorIndex, 0, 1, true);
            }

            else
                m_Lace.color = Color.HSVToRGB(m_LaceColorIndex, 0.5f, 1, true);
        }

        else
        {
            m_Lace.material.SetFloat("_HsvShift", m_LaceColor);
        }

        if (PlayerPrefs.HasKey("LaceIndex"))
        {
            m_Lace.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "Button" && !m_HoldingButton)
                {
                    if (m_RemoveArrowButton)
                    {
                        ButtonHandler m_CurrentButton = result.gameObject.GetComponent<ButtonHandler>();
                        m_CurrentButton.RemoveButton();
                    }
                    m_SelectedButton = result.gameObject;
                    //m_ButtonColor = m_SelectedButton.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
                    m_SelectedButton.transform.localScale = Vector3.one * 0.15f;
                    m_Offset = m_SelectedButton.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    m_HoldingButton = true;
                }
            }

            if (m_CanDetectOpenSwipe)
            {
                m_OpenSwipeFirstClicked = true;
                m_OpenSwipeFirstMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (m_SelectedButton != null && m_HoldingButton)
            {
                m_SelectedButton.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;
            }

            if (m_ColorPickerClicked)
            {
                m_CircleIndicator.transform.position = new Vector3(m_CircleIndicator.transform.position.x, Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + m_ColorPickerOffset.y, -0.05f, 1.25f), m_CircleIndicator.transform.position.z);
                m_Value = m_CircleIndicatorRect.anchoredPosition.y + 674.25f;
                float m_Hue = m_Value * 360 / 565.5f;
                //m_ButtonColor.color = Color.HSVToRGB(m_Hue, 1, 1, true);
                m_ButtonColor.material.SetFloat("_HsvShift", m_Hue);
            }

            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "ButtonPosition" && m_SelectedButton != null)
                {
                    m_AddedButtons.Add(m_SelectedButton.GetComponent<ButtonHandler>());
                    m_SelectedButton.transform.position = result.gameObject.transform.position;
                    m_SelectedButton.tag = "Untagged";
                    m_ButtonToDestroy = null;
                    result.gameObject.tag = "Untagged";
                    m_SelectedButton = null;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_HoldingButton = false;
            m_ColorPickerClicked = false;

            if (m_CanDetectOpenSwipe && m_OpenSwipeFirstClicked)
            {
                m_OpenSwipeSecondMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

                if (Mathf.Abs(m_OpenSwipeSecondMousePosition.x - m_OpenSwipeFirstMousePosition.x) > 0)
                {
                    if (m_OpenSwipeSecondMousePosition.x > m_OpenSwipeFirstMousePosition.x)
                    {
                        m_Hints[m_HintCount].SetActive(false);
                        m_HintCount++;
                        m_ButtonsAnim.speed = 1;
                        m_CanSelectButton = true;
                        m_ButtonsButtons.SetActive(true);
                        StartCoroutine(ChangeCanvasLayer());
                        m_OpenBox = false;
                        m_CanDetectOpenSwipe = false;
                    }
                }

                m_OpenSwipeFirstClicked = false;
            }
        }
    }

    IEnumerator ChangeCanvasLayer()
    {
        yield return new WaitForSeconds(0.5f);
        m_GameCanvas.sortingOrder = 10;
    }

    public void OpenButtons()
    {
        if (!m_ButtonsOpened && this.enabled)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_ButtonsAnim.enabled = true;
            m_OpenBox = true;
            m_DressHandler.SetButtonsDetails();
            m_FinishButton.SetActive(true);
            m_ButtonsOpened = true;
            m_CanDetectOpenSwipe = true;
        }
    }

    public void OpenBox()
    {
        if (m_OpenBox)
        {
            m_ButtonsAnim.speed = 1;
            m_CanSelectButton = true;
            m_ButtonsButtons.SetActive(true);
            m_OpenBox = false;
        }
    }

    public void SelectButton(int i_ButtonIndex)
    {
        if (m_CanSelectButton)
        {
            m_ButtonIndex = i_ButtonIndex;
            //m_Hints[m_HintCount].SetActive(false);
            if (m_ButtonToDestroy != null)
            {
                Destroy(m_ButtonToDestroy);
            }

            if (!m_ColorPicker.activeInHierarchy)
            {
                m_ColorPicker.SetActive(true);
            }

            m_RemoveArrowButton = true;

            ResetColor();
            Image m_NewButton = Instantiate(m_ButtonPrefab);
            ButtonHandler m_NewButtonHandler = m_NewButton.GetComponent<ButtonHandler>();
            m_SelectedButtonHandler = m_NewButtonHandler;
            m_NewButtonHandler.SetButtonsHandler(this);
            if (m_ButtonIndex > 3)
            {
                m_NewButtonHandler.RemovePricetags();
                m_NewButtonHandler.SetPricetag(i_ButtonIndex);
            }
            m_NewButton.transform.SetParent(m_Parent.transform);
            m_NewButton.transform.localPosition = new Vector2(1420, -495);
            m_NewButton.transform.localScale = Vector3.one;
            m_NewButton.sprite = m_Buttons[i_ButtonIndex];
            //m_ButtonColor = m_NewButton.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
            m_ButtonColor = m_NewButton.GetComponent<Image>();
            m_ButtonColor.material = new Material(m_ButtonColor.material);
            //m_SelectedButton = m_NewButton.gameObject;
            m_ButtonToDestroy = m_NewButton.gameObject;
        }
    }

    private void ResetColor()
    {
        m_CircleIndicatorRect.anchoredPosition = m_StartCircleIndicatorPosition;
    }

    public void RemoveButton()
    {
        if (m_ButtonToDestroy != null)
        {
            Destroy(m_ButtonToDestroy);
            ResetColor();
        }
    }

    public void ColorPickerClicked()
    {
        m_Offset = m_CircleIndicator.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_ButtonColor.gameObject.SetActive(true);
        m_ColorPickerClicked = true;
    }

    public void Finish()
    {
        m_HintCount++;
        Destroy(m_ButtonToDestroy);
        m_ButtonsAnim.gameObject.SetActive(false);
        //m_NeedleSewingHandler.enabled = true;

        //for (int i = 0; i < m_AddedButtons.Count; i++)
        //{
        //    m_NeedleSewingHandler.AddToList(m_AddedButtons[i]);
        //}

        m_ColorPicker.SetActive(false);
        m_FinishButton.SetActive(false);
        m_DressHandler.RemoveButtonDetails();
        m_Parent.transform.SetParent(m_NewButtonsParent.transform);
        m_Stars.SetActive(true);
        m_Dress.transform.SetParent(m_NewDressCanvas.transform);
        m_DressHandler.RemoveButtonDetails();
        m_Parent.transform.SetParent(m_NewButtonsParent.transform);
        StartCoroutine(EndSequence());
    }

    public void SetBuyingPopup()
    {
        m_BuyingPopup.SetActive(true);
    }

    public void BuyButton()
    {
        if (MoneyHandler.Instance.CheckIfHaveMoney(m_CurrentPrice))
        {
            m_BuyingPopup.SetActive(false);
            MoneyHandler.Instance.RemoveMoney(m_CurrentPrice);
            m_GameHandler.UpdateText();
            PlayerPrefs.SetInt("Button" + m_ButtonIndex, 1);
            m_SelectedButtonHandler.RemovePricetags();
            //m_SelectedButton.tag = "Button";
            m_ButtonToDestroy.tag = "Button";
        }

        else
        {
            m_NotEnoughMoneyPopup.SetActive(true);
        }
    }

    public void SetCurrentPrice(int i_CurrentPrice)
    {
        m_CurrentPrice = i_CurrentPrice;
    }

    public void DisableBuyingPopup()
    {
        m_BuyingPopup.SetActive(false);
    }

    public void AddMoreMoney()
    {
        m_Paying.SetActive(true);
        m_BuyingPopup.SetActive(false);
        m_NotEnoughMoneyPopup.SetActive(false);
    }

    public void ContinueGame()
    {
        m_BuyingPopup.SetActive(false);
        m_NotEnoughMoneyPopup.SetActive(false);
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(1f);
        m_PositionHandlerLastScene.SetCameraToSewingFirstPart();
        //m_Dress.transform.SetParent(m_NewDressCanvas.transform);
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
