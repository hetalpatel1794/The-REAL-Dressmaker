using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MagazineHandler : MonoBehaviour
{
    [SerializeField]
    private GirlsHandler m_GirlsHandler;
    [SerializeField]
    private PositionsHandler m_PositionHandler;
    [SerializeField]
    private SpriteRenderer m_MagazineSpriteRenderer;
    [SerializeField]
    private GameObject m_OpenMagazine;
    [SerializeField]
    private float m_MinSwipeDistance;
    [SerializeField]
    private Vector2 m_NewMagazinePosition;
    [SerializeField]
    private float m_MagazineMoveTime;
    [SerializeField]
    private RectTransform m_MagazineRectTransform;
    [SerializeField]
    private GameObject m_Materials;
    [SerializeField]
    private RectTransform m_MagazineChildRectTransform;
    [SerializeField]
    private Animator m_Anim;
    [SerializeField]
    private AudioSource m_MagazineSound;
    [SerializeField]
    private GameObject m_Stars;
    [SerializeField]
    private List<GameObject> m_Hints;
    [SerializeField]
    private GameObject m_BuyingPopup;
    [SerializeField]
    private GameObject m_NotEnoughMoneyPopup;
    [SerializeField]
    private MagazineBuyingHandler m_MagazineBuyingHandler;
    [SerializeField]
    private GameHandler m_GameHandler;
    [SerializeField]
    private GameObject m_Paying;
    [SerializeField]
    private GameObject m_BuyDressButton;

    private int m_HintCount;

    private bool m_CanOpenMagazine;
    private bool m_CanSwipeMagazine;
    private bool m_ScreenPressed;
    private bool m_CanSelectDress;
    private bool m_MoveMagazine;

    private Vector3 m_MousePos;
    private Vector2 m_MousePos2D;
    private RaycastHit2D m_RaycastHit;

    private Vector3 m_FirstMousePos;
    private Vector3 m_SecondMousePos;

    private Vector2 m_OldMagazinePosition;

    private bool m_FirstClick;
    private float m_DoubleClickTimer;

    private float m_OldMagazineScale;
    private float m_NewMagazineScale;

    private int m_ForwardCount = 1;
    private int m_BackwardCount = 10;

    private bool m_MagazineClicked;

    private Vector3 m_MagazineClickedPosition;

    private float t;

    private void Start()
    {
        StartCoroutine(EnableMagazineClick());
    }

    IEnumerator EnableMagazineClick()
    {
        yield return new WaitForSeconds(4f);
        m_CanOpenMagazine = true;
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
            if (m_CanOpenMagazine && MagazineClicked())
            {
                OpenMagazine();
                m_Hints[m_HintCount].SetActive(false);
                m_HintCount++;
                m_CanOpenMagazine = false;
            }

            if (m_CanSwipeMagazine)
            {
                m_FirstMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                m_ScreenPressed = true;
            }

            if (m_CanSelectDress)
            {
                //DetectDoubleClick();
            }
        }

        if (m_FirstClick)
        {
            if (Time.time - m_DoubleClickTimer > 0.3f)
            {
                m_FirstClick = false;
            }
        }

        if (Input.GetMouseButtonUp(0) && m_ScreenPressed && m_CanSwipeMagazine)
        {
            DetectSwipeDirection();
            m_ScreenPressed = false;
        }

        if (m_MoveMagazine)
        {
            MoveMagazine();
        }
    }

    private bool MagazineClicked()
    {
        bool m_Clicked = false;

        m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_MousePos2D = new Vector2(m_MousePos.x, m_MousePos.y);

        m_RaycastHit = Physics2D.Raycast(m_MousePos2D, Vector2.zero);

        if (m_RaycastHit.collider != null)
        {
            if (m_RaycastHit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                m_Clicked = true;
            }
        }

        return m_Clicked;
    }

    private void OpenMagazine()
    {
        m_PositionHandler.SetCameraToMagazine();
        m_MagazineSpriteRenderer.enabled = false;
        m_OpenMagazine.SetActive(true);
        StartCoroutine(EnableMagazineSwipe());
    }

    IEnumerator EnableMagazineSwipe()
    {
        yield return new WaitForSeconds(0.5f);
        m_CanSwipeMagazine = true;
    }

    private void MoveMagazine()
    {
        t += Time.deltaTime / m_MagazineMoveTime;

        m_MagazineRectTransform.anchoredPosition = Vector3.Lerp(m_OldMagazinePosition, m_NewMagazinePosition, t);
        m_MagazineRectTransform.sizeDelta = Vector2.Lerp(new Vector2(m_OldMagazineScale, m_OldMagazineScale), new Vector2(m_NewMagazineScale, m_NewMagazineScale), t);
        m_MagazineChildRectTransform.sizeDelta = Vector2.Lerp(new Vector2(m_OldMagazineScale, m_OldMagazineScale), new Vector2(m_NewMagazineScale + 10, m_NewMagazineScale + 10), t);

        if (m_MagazineRectTransform.anchoredPosition == m_NewMagazinePosition)
        {
            //m_Materials.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //gameObject.SetActive(false);

            m_MoveMagazine = false;
        }
    }

    private void DetectSwipeDirection()
    {
        m_SecondMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (Mathf.Abs(m_SecondMousePos.x - m_FirstMousePos.x) > m_MinSwipeDistance)
        {
            if (m_SecondMousePos.x > m_FirstMousePos.x)
            {
                m_Hints[m_HintCount].SetActive(false);
                OpenCatalogueBackward2();
            }

            else if (m_SecondMousePos.x < m_FirstMousePos.x)
            {
                m_Hints[m_HintCount].SetActive(false);
                OpenCatalogueForward2();

                if (!m_CanSelectDress)
                {
                    m_CanSelectDress = true;
                }
            }
        }

        m_ScreenPressed = false;
    }

    public void ClickDown()
    {
        m_MagazineClicked = true;
        m_MagazineClickedPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    public void ClickUp()
    {
        if (m_MagazineClicked && m_CanSelectDress)
        {
            if (Vector3.Distance(Camera.main.ScreenToViewportPoint(Input.mousePosition), m_MagazineClickedPosition) < 0.2f)
            {
                if (m_ForwardCount - 2 > 3)
                {
                    if (PlayerPrefs.GetInt("DressBought" + (m_ForwardCount - 2)) == 0)
                    {
                        m_BuyingPopup.SetActive(true);
                    }

                    else
                    {
                        StartCoroutine(SelectDress());
                        m_CanSwipeMagazine = false;
                        m_CanSelectDress = false;
                    }
                }

                else
                {
                    StartCoroutine(SelectDress());
                    m_CanSwipeMagazine = false;
                    m_CanSelectDress = false;
                }
            }

            m_MagazineClicked = false;
        }
    }

    public void BuyDress()
    {
        int m_CurrentPrice = 0;

        switch(m_ForwardCount - 2)
        {
            case 4: m_CurrentPrice = 65; break; // m_CurrentPrice = 650; break; 
            case 5: m_CurrentPrice = 65; break; // m_CurrentPrice = 650; break;
            case 6: m_CurrentPrice = 80; break; // m_CurrentPrice = 800; break
            case 7: m_CurrentPrice = 80; break; // m_CurrentPrice = 800; break
            case 8: m_CurrentPrice = 110; break; // m_CurrentPrice = 1100; break
        }

        if (MoneyHandler.Instance.CheckIfHaveMoney(m_CurrentPrice))
        {
            MoneyHandler.Instance.RemoveMoney(m_CurrentPrice);
            m_GameHandler.UpdateText();
            m_MagazineBuyingHandler.RemovePricesImmediately();
            PlayerPrefs.SetInt("DressBought" + (m_ForwardCount - 2), 1);
            m_BuyingPopup.SetActive(false);
            m_NotEnoughMoneyPopup.SetActive(false);
        }

        else
            m_NotEnoughMoneyPopup.SetActive(true);
    }

    public void RecieveDress()
    {
        m_MagazineBuyingHandler.RemovePricesImmediately();
        PlayerPrefs.SetInt("DressBought" + (m_ForwardCount - 2), 1);
        StartCoroutine(DisableBuyingButton());
    }

    IEnumerator DisableBuyingButton()
    {
        yield return new WaitForSeconds(0.1f);
        m_BuyDressButton.SetActive(false);
    }

    public void ContinueGame()
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

    private void OpenCatalogueForward2()
    {
        if (m_ForwardCount < 11)
        {
            m_Anim.Play(m_ForwardCount.ToString(), 0, 0);
            m_ForwardCount++;
            m_BackwardCount++;

            if (PlayerPrefs.GetInt("DressBought" + 9) == 0 && m_ForwardCount == 11)
            {
                m_BuyDressButton.SetActive(true);
            }

            m_MagazineSound.Play();
        }
    }

    private void OpenCatalogueBackward2()
    {
        if (m_BackwardCount > 10)
        {
            m_Anim.Play(m_BackwardCount.ToString(), 0, 0);
            m_ForwardCount--;
            m_BackwardCount--;
            m_BuyDressButton.SetActive(false);
            Debug.Log("Backward" + m_BackwardCount);
            m_MagazineSound.Play();
        }
    }
    //11 i 19

    //private void DetectDoubleClick()
    //{
    //    if (!m_FirstClick)
    //    {
    //        m_FirstClick = true;
    //        m_DoubleClickTimer = Time.time;
    //    }

    //    else
    //    {
    //        StartCoroutine(SelectDress());
    //        m_CanSwipeMagazine = false;
    //        m_FirstClick = false;
    //        m_CanSelectDress = false;
    //    }
    //}

    IEnumerator SelectDress()
    {
        m_HintCount++;
        m_Stars.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_GirlsHandler.SelectDress(m_ForwardCount -2);
        m_OldMagazinePosition = m_MagazineRectTransform.anchoredPosition;
        m_OldMagazineScale = 800;
        m_NewMagazineScale = 650;
        yield return new WaitForSeconds(1f);
        m_PositionHandler.SetCameraToMaterials();
        m_MoveMagazine = true;
    }
}
