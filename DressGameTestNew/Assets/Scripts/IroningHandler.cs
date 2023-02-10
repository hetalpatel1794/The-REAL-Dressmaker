using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IroningHandler : MonoBehaviour
{
    [SerializeField]
    private Animator m_IroningBoard;
    [SerializeField]
    private PositionsHandlerLastScene m_PositionsHandlerLastScene;
    [SerializeField]
    private GameObject m_DressSewingHandler;
    [SerializeField]
    private GameObject m_DressHandler;
    [SerializeField]
    private SpriteRenderer m_IroningBoardSprite;

    [SerializeField]
    private Animator m_Iron;

    //[SerializeField]
    //private GameObject m_Drawer;
    [SerializeField]
    private Canvas m_Canvas;

    [SerializeField]
    private GameObject m_IronCable;
    [SerializeField]
    private GameObject m_IronLoop;
    [SerializeField]
    private Animator m_Spray;
    [SerializeField]
    private float m_IroningDuration;
    [SerializeField]
    private MoveDressHandler m_MoveDressHandler;
    [SerializeField]
    private GameObject m_Dress;

    [SerializeField]
    private GameObject m_DressButton;
    [SerializeField]
    private Vector3 m_CorrectPos;

    [SerializeField]
    private GameObject m_DrawerObject;
    [SerializeField]
    private GameObject m_IronButtons;

    [SerializeField]
    private EventSystem m_EventSystem;

    [SerializeField]
    private GameObject m_IronButton;

    [SerializeField]
    private GameObject m_IronTrigger;

    [SerializeField]
    private Vector3 m_IronPos;

    [SerializeField]
    private GameObject m_FinishButton;

    [SerializeField]
    private GameObject m_SprayButton;

    [SerializeField]
    private GameObject m_DressBlend;

    [SerializeField]
    private Image m_Wrinkles;

    [SerializeField]
    private GameObject m_WrinklesParent;

    [SerializeField]
    private AudioSource m_SpraySound;

    [SerializeField]
    private Vector3 m_OldIronScale;
    [SerializeField]
    private Vector3 m_NewIronScale;
    [SerializeField]
    private float m_IronScaleSpeed;
    [SerializeField]
    private SpriteRenderer m_SpraySpriteRenderer;
    [SerializeField]
    private AudioSource m_IroningBoardSound;
    [SerializeField]
    private SpriteRenderer m_ThreadSpriteRenderer;
    [SerializeField]
    private List<Sprite> m_ThreadSprites;
    [SerializeField]
    private SpriteRenderer m_ThreadColor;
    [SerializeField]
    private List<Color> m_ThreadColors;
    [SerializeField]
    private GameObject m_Stars;

    [SerializeField]
    private List<GameObject> m_Hints;

    private int m_HintCount;

    private PointerEventData m_PointerEventData;

    private bool m_OpenIroningBoard;
    private bool m_ClickIron;
    private bool m_TurnIronOn;
    private bool m_CanIron;
    private bool m_IronSet;
    private bool m_CanUseSpray;
    private bool m_DetectIroningBoardSwipeFirst;
    private bool m_DetectIroningBoardSwipeSecond;
    private bool m_FirstSwipePartDetected;
    private bool m_SeconeSwipePartDetected;
    private bool m_FirstTurnOnSwipeDetected;
    private bool m_CanReleaseIron;

    private bool m_MoveSpray;

    private bool m_DetectTurnOnSwipe;

    private bool m_MoveDress;

    private bool m_HintIncreased;

    private bool m_ReturnSpray;

    private RaycastHit2D m_Hit;
    private Vector3 m_MousePos;
    private Vector3 m_MousePos2D;
    private Vector3 m_offset;

    private Vector3 m_FirstBoardFirstSwipeMousePosition;
    private Vector3 m_FirstBoardSecondSwipeMousePosition;

    private Vector3 m_SecondBoardFirstSwipeMousePosition;
    private Vector3 m_SecondSecondSwipeMousePosition;

    private Vector3 m_FirstTurnOnSwipeMousePosition;
    private Vector3 m_SecondTurnOnSwipeMousePosition;

    private Vector2 m_DressOffset;
    private Vector3 m_IronOffset;
    private Vector3 m_IronButtonOffset;
    private Vector3 m_SprayOffset;
    private Vector3 m_SprayButtonOffset;

    private Vector3 m_SprayStartPos;
    private Vector3 m_SprayButtonStartPos;

    private Vector3 m_CurrentSprayPos;
    private Vector3 m_CurrentSprayButtonPos;

    private bool m_MoveIron;

    private float t;
    private float s;
    private float m_WrinklesValue;
    private float i;

    private int m_IronCount;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();

        m_ThreadSpriteRenderer.sprite = m_ThreadSprites[PlayerPrefs.GetInt("Thread")];
        m_ThreadColor.color = m_ThreadColors[PlayerPrefs.GetInt("Thread")];

        m_OpenIroningBoard = true;
        m_DrawerObject.SetActive(false);

        m_SprayStartPos = m_Spray.transform.position;
        m_SprayButtonStartPos = m_SprayButton.transform.position;

        m_WrinklesValue = 1;
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
            if (m_CanIron)
            {
                m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_MousePos2D = new Vector2(m_MousePos.x, m_MousePos.y);

                m_Hit = Physics2D.Raycast(m_MousePos2D, Vector2.zero);
                if (m_Hit.collider != null && m_Hit.transform.gameObject.name == "Iron")
                {
                    m_offset = m_IronLoop.transform.localPosition - m_MousePos;

                    if (!m_IronSet)
                    {
                        m_IronLoop.SetActive(true);
                        m_IronCable.SetActive(true);
                        m_Iron.gameObject.SetActive(false);
                        m_IronSet = true;
                    }
                }

                if (m_Hit.collider != null && m_Hit.transform.gameObject.name == "IronLoop")
                {
                    m_offset = m_IronLoop.transform.localPosition - m_MousePos;
                }
            }

            if (m_DetectIroningBoardSwipeFirst)
            {
                m_FirstBoardFirstSwipeMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                m_FirstSwipePartDetected = true;
            }

            if (m_DetectIroningBoardSwipeSecond)
            {
                m_SecondBoardFirstSwipeMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                m_SeconeSwipePartDetected = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (m_CanIron)
            {
                m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_MousePos2D = new Vector2(m_MousePos.x, m_MousePos.y);

                m_Hit = Physics2D.Raycast(m_MousePos2D, Vector2.zero);
                if (m_Hit.collider != null && m_Hit.transform.gameObject.name == "IronLoop")
                {
                    //m_Iron.transform.localPosition = new Vector3(Mathf.Clamp(m_MousePos.x + m_offset.x, 6.5f, 9.4f), Mathf.Clamp(m_MousePos.y + m_offset.y, -0.775f, -0.455f), 0);
                    m_IronLoop.transform.localPosition = new Vector3(Mathf.Clamp(m_MousePos.x + m_offset.x, 6.5f, 9.4f), Mathf.Clamp(m_MousePos.y + m_offset.y, -0.775f, -0.455f), 0);

                    t += Time.deltaTime / m_IroningDuration;
                    m_WrinklesValue -= Time.deltaTime / m_IroningDuration;
                    Color m_Col = m_Wrinkles.color;
                    m_Col.a = m_WrinklesValue;
                    m_Wrinkles.color = m_Col;

                    m_Hints[m_HintCount].SetActive(false);

                    if (t >= 1)
                    {
                        if (!m_FinishButton.activeInHierarchy)
                        {
                            m_FinishButton.SetActive(true);
                        }
                    }
                }
            }

            if (m_MoveDress)
            {
                m_Dress.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_DressOffset;
            }

            if (m_MoveIron)
            {
                m_Iron.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_IronOffset;
                m_IronButton.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_IronButtonOffset;

                i += Time.deltaTime / m_IronScaleSpeed;
                m_Iron.transform.localScale = Vector3.Lerp(m_OldIronScale, m_NewIronScale, i);
            }

            if (m_MoveSpray)
            {
                if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x < 0.5f)
                {
                    m_Spray.transform.rotation = Quaternion.Euler(0, 0, m_Spray.transform.rotation.z);
                }

                else
                {
                    m_Spray.transform.rotation = Quaternion.Euler(0, -180, m_Spray.transform.rotation.z);
                }

                m_Spray.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_SprayOffset;
                m_SprayButton.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_SprayButtonOffset;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (m_DetectIroningBoardSwipeFirst && m_FirstSwipePartDetected)
            {
                m_FirstBoardSecondSwipeMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

                if (Mathf.Abs(m_FirstBoardSecondSwipeMousePosition.x - m_FirstBoardFirstSwipeMousePosition.x) > 0)
                {
                    if (m_FirstBoardSecondSwipeMousePosition.x > m_FirstBoardFirstSwipeMousePosition.x)
                    {
                        m_Hints[m_HintCount].SetActive(false);
                        m_HintCount++;
                        m_IroningBoard.speed = 1;
                        m_DetectIroningBoardSwipeSecond = true;
                        m_DetectIroningBoardSwipeFirst = false;
                        m_IroningBoardSound.Play();
                    }
                }

                m_FirstSwipePartDetected = false;
            }

            if (m_DetectIroningBoardSwipeSecond && m_SeconeSwipePartDetected)
            {
                m_SecondSecondSwipeMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (Mathf.Abs(m_SecondSecondSwipeMousePosition.y - m_SecondBoardFirstSwipeMousePosition.y) > 0)
                {
                    if (m_SecondSecondSwipeMousePosition.y < m_SecondBoardFirstSwipeMousePosition.y)
                    {
                        m_Hints[m_HintCount].SetActive(false);
                        m_HintCount++;
                        m_DressButton.SetActive(true);
                        m_DetectIroningBoardSwipeSecond = false;
                        m_IronTrigger.SetActive(true);
                        m_IroningBoard.speed = 1;
                    }
                }

                m_SeconeSwipePartDetected = false;
            }

            //m_CanIron = false;
            //if (m_TurnIronOn && m_FirstTurnOnSwipeDetected)
            //{
            //    m_SecondTurnOnSwipeMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_SecondTurnOnSwipeMousePosition.x - m_FirstTurnOnSwipeMousePosition.x) > 0)
            //    {
            //        if (m_SecondTurnOnSwipeMousePosition.x > m_FirstTurnOnSwipeMousePosition.x)
            //        {
            //            m_Iron.speed = 1;
            //            m_CanUseSpray = true;
            //            m_TurnIronOn = false;
            //        }
            //    }

            //    m_SeconeSwipePartDetected = false;
            //    m_DetectTurnOnSwipe = false;
            //}
        }

        if (m_ReturnSpray)
        {
            s += Time.deltaTime / 0.5f;

            m_Spray.transform.position = Vector3.Lerp(m_CurrentSprayPos, m_SprayStartPos, s);
            m_SprayButton.transform.position = Vector3.Lerp(m_CurrentSprayButtonPos, m_SprayButtonStartPos, s);

            if (s == 1)
            {
                m_ReturnSpray = false;
            }
        }
    }

    IEnumerator MoveToManequin()
    {
        m_Stars.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        m_PositionsHandlerLastScene.SetCameraToManequin();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //m_MoveDressHandler.enabled = true;
    }

    public void OpenIroningBoard()
    {
        if (m_OpenIroningBoard)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            //m_DressSewingHandler.SetActive(false);
            m_IroningBoard.enabled = true;
            m_DetectIroningBoardSwipeFirst = true;
            m_PositionsHandlerLastScene.SetCameraToDressIroning();
            //m_ClickIron = true;
            //m_DressButton.SetActive(true);
            //StartCoroutine(EnableDress());
            m_OpenIroningBoard = false;
        }
    }

    IEnumerator EnableDress()
    {
        yield return new WaitForSeconds(1.3f);
        m_DressHandler.SetActive(true);
        m_IroningBoardSprite.sortingOrder = 6;
        //m_Drawer.SetActive(false);
        m_Canvas.sortingOrder = 7;
    }

    public void MoveDress()
    {
        m_Hints[m_HintCount].SetActive(false);
        m_DressOffset = m_Dress.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_IroningBoardSprite.sortingOrder = 2;
        m_MoveDress = true;
    }

    public void StopMovingDress()
    {
        m_MoveDress = false;

        if (Vector3.Distance(m_Dress.transform.position, m_CorrectPos) < 1)
        {
            m_DressButton.SetActive(false);
            m_DressSewingHandler.SetActive(false);
            m_ClickIron = true;
            m_DressHandler.SetActive(true);
            m_IroningBoardSprite.sortingOrder = 6;
            //m_Drawer.SetActive(false);
            m_HintCount++;
            m_Canvas.sortingOrder = 7;
        }
    }

    public void ClickIron()
    {
        if (m_ClickIron)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_IronOffset = m_Iron.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_IronButtonOffset = m_IronButton.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_Iron.enabled = true;
            m_MoveIron = true;
            m_CanReleaseIron = true;
            //m_Iron.enabled = true;
            //m_IronButtons.SetActive(true);
            //m_ClickIron = false;
        }
    }

    public void ReleaseIron()
    {
       if (m_CanReleaseIron)
        {
            m_MoveIron = false;

            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(m_PointerEventData, results);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.name == "IronTrigger")
                {
                    m_HintCount++;
                    m_Iron.transform.localPosition = m_IronPos;
                    m_TurnIronOn = true;
                    m_ClickIron = false;
                    m_IronTrigger.SetActive(false);
                }
            }

            m_CanReleaseIron = false;
        }
    }

    public void ContinueIron(int i_Count)
    {
        if (i_Count == m_IronCount)
        {
            m_Iron.speed = 1;

            if (i_Count == 2)
            {
                m_TurnIronOn = true;
                m_IronButtons.SetActive(false);
            }

            m_IronCount++;
        }
    }

    public void TurnIronOn()
    {
        if (m_TurnIronOn)
        {
            //m_Iron.speed = 1;
            //m_CanUseSpray = true;
            //m_TurnIronOn = false;
            m_FirstTurnOnSwipeMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            m_FirstTurnOnSwipeDetected = true;
        }
    }

    public void EndTurnIronOnDrag()
    {
        if (m_TurnIronOn && m_FirstTurnOnSwipeDetected)
        {
            m_SecondTurnOnSwipeMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (Mathf.Abs(m_SecondTurnOnSwipeMousePosition.x - m_FirstTurnOnSwipeMousePosition.x) > 0)
            {
                if (m_SecondTurnOnSwipeMousePosition.x > m_FirstTurnOnSwipeMousePosition.x)
                {
                    m_Iron.speed = 1;
                    m_CanUseSpray = true;
                    m_TurnIronOn = false;
                }
            }

            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_SeconeSwipePartDetected = false;
            m_DetectTurnOnSwipe = false;
        }
    }

    public void UseSpray()
    {
        if (m_CanUseSpray)
        {
            m_Hints[m_HintCount].SetActive(false);
            if (!m_HintIncreased)
            {
                m_HintCount++;
                m_HintIncreased = true;
            }
            m_SpraySpriteRenderer.sortingOrder = 10;
            m_SprayOffset = m_Spray.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_SprayButtonOffset = m_SprayButton.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_MoveSpray = true;
            m_Spray.SetTrigger("move");
            m_ReturnSpray = false;
            m_SpraySound.Play();
            m_CanIron = true;
        }
    }

    public void ReleaseSpray()
    {
        if (m_CanUseSpray)
        {
            m_MoveSpray = false;
            s = 0;
            m_Spray.SetTrigger("back");
            m_CurrentSprayPos = m_Spray.transform.position;
            m_CurrentSprayButtonPos = m_SprayButton.transform.position;
            m_Spray.transform.rotation = Quaternion.Euler(0, 0, m_Spray.transform.rotation.z);
            m_SpraySound.Stop();
            m_ReturnSpray = true;
        }
    }

    public void FinishIroning()
    {
        m_Hints[m_HintCount].SetActive(false);
        m_HintCount++;
        m_IronCable.SetActive(false);
        m_IronLoop.SetActive(false);
        StartCoroutine(MoveToManequin());
        m_CanIron = false;
        m_FinishButton.SetActive(false);
        m_DressBlend.SetActive(true);
        m_WrinklesParent.SetActive(false);
        m_IronButtons.SetActive(false);
        m_CanUseSpray = false;
    }
}
