using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SewingHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_LampOff;
    [SerializeField]
    private GameObject m_LampOn;

    [SerializeField]
    private GameObject m_MachineOff;
    [SerializeField]
    private GameObject m_MachineOn;

    [SerializeField]
    private Animator m_DrawerAnim;

    [SerializeField]
    private List<GameObject> m_Threads;
    [SerializeField]
    private float m_ThreadMoveDuration;
    [SerializeField]
    private Vector3 m_ThreadPosition;

    [SerializeField]
    private GameObject m_FirstArrows;

    [SerializeField]
    private List<Color> m_ThreadColors;
    [SerializeField]
    private SpriteRenderer m_Thread;
    [SerializeField]
    private SpriteRenderer m_ThreadLoop;
    [SerializeField]
    private GameObject m_ThreadsButton;

    [SerializeField]
    private Animator m_PapucicaAnim;
    [SerializeField]
    private Animator m_ThreadLoopAnim;
    [SerializeField]
    private Animator m_NeedleAnim;

    [SerializeField]
    private RectTransform m_Dress;
    [SerializeField]
    private Vector2 m_DressPosition;
    [SerializeField]
    private float m_DressMoveDuration;
    [SerializeField]
    private List<Animator> m_DressAnims;
    [SerializeField]
    private GameObject m_DressButton;
    [SerializeField]
    private Animator m_TopAnim;
    [SerializeField]
    private Animator m_RotatorAnim;
    [SerializeField]
    private SewingDressHandler m_SewingDressHandler;

    [SerializeField]
    private List<GameObject> m_ThreadButtons;

    [SerializeField]
    private PositionsHandlerLastScene m_PositionsHandlerLastScene;

    [SerializeField]
    private IroningHandler m_IroningHandler;

    [SerializeField]
    private Animator m_ThreadAnim;

    [SerializeField]
    private EventSystem m_EventSystem;

    [SerializeField]
    private AudioSource m_MachineSound;

    [SerializeField]
    private GameObject m_ThreadAnimationButtons;

    [SerializeField]
    private GameObject m_DrawerButton;

    [SerializeField]
    private GameObject m_RotatorButton1;
    [SerializeField]
    private GameObject m_RotatorButton2;

    [SerializeField]
    private GameObject m_Stars;

    [SerializeField]
    private List<GameObject> m_Hints;

    private int m_HintCount;

    private bool m_CanShowHint;

    private PointerEventData m_PointerEventData;

    private bool m_LampTurnedOn;
    private bool m_CanTurnMachineOn;
    private bool m_CanOpenDrawer;
    private bool m_CanChooseThread;
    private bool m_MoveThread;
    private bool m_CanClickThread;
    private bool m_CanClickPapucicaUp;
    private bool m_CanClickPapucicaDown;
    private bool m_MoveDress;
    private bool m_CanClickDress;
    private bool m_CanClickRotator;
    private bool m_SetTriggers;
    private bool m_SewingFinished;
    private bool m_ThreadClicked;
    private bool m_ThreadFinished;
    private bool m_FinishPapucica;

    private bool m_StartRotatorClicked;

    private Vector3 m_ThreadOldPosition;
    private Vector2 m_OldDressPosition;
    private GameObject m_SelectedThreadButton;

    private Vector3 m_DressFirstSwipePosition;
    private Vector3 m_DressSecondSwipePosition;
    private bool m_DressFirstSwipeClicked;

    private bool m_RotatorFinished;

    private Vector3 m_Offset;

    private float t;
    private int m_ThreadCount;

    private GameObject m_SelectedThread;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();

        m_CanShowHint = true;
    }

    private void Update()
    {
        if (m_MoveThread)
        {
            //t += Time.deltaTime / m_ThreadMoveDuration;
            //m_SelectedThread.transform.localPosition = Vector3.Lerp(m_ThreadOldPosition, m_ThreadPosition, t);

            //if (m_SelectedThread.transform.localPosition == m_ThreadPosition)
            //{
            //    m_FirstArrows.SetActive(true);
            //    m_CanClickThread = true;
            //    m_MoveThread = false;
            //}

            m_SelectedThread.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;
            m_SelectedThreadButton.transform.position = m_SelectedThread.transform.position;
        }

        if (m_MoveDress)
        {
            t += Time.deltaTime / m_DressMoveDuration;

            m_Dress.anchoredPosition = Vector2.Lerp(m_OldDressPosition, m_DressPosition, t);

            if (m_Dress.anchoredPosition == m_DressPosition)
            {
                m_CanClickDress = true;
                m_MoveDress = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //if (m_DressFirstSwipeClicked)
            //{
            //    m_DressSecondSwipePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_DressSecondSwipePosition.y - m_DressFirstSwipePosition.y) > 0)
            //    {
            //        if (m_DressSecondSwipePosition.y > m_DressFirstSwipePosition.x)
            //        {
            //            for (int i = 0; i < m_DressAnims.Count; i++)
            //            {
            //                m_DressAnims[i].SetTrigger("move");
            //            }

            //            m_HintCount++;
            //            m_CanClickPapucicaDown = true;
            //            m_SetTriggers = true;
            //            m_CanClickDress = false;
            //            m_SewingDressHandler.MovePatterns();
            //            StartCoroutine(StopPatterns());
            //            m_DressFirstSwipeClicked = false;
            //        }
            //    }
            //}
        }
    }

    public void ShowHint()
    {
        if (m_HintCount < m_Hints.Count && m_CanShowHint)
        {
            m_Hints[m_HintCount].SetActive(true);
        }
    }

    public void TurnLampOn()
    {
        if (!m_LampTurnedOn)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_LampOff.SetActive(false);
            m_LampOn.SetActive(true);
            m_CanTurnMachineOn = true;
            m_LampTurnedOn = true;
        }
    }

    public void TurnMachineOn()
    {
        if (m_CanTurnMachineOn)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_MachineOff.SetActive(false);
            m_MachineOn.SetActive(true);
            m_CanOpenDrawer = true;
            m_CanTurnMachineOn = false;
        }
    }

    public void OpenDrawer()
    {
        if (m_CanOpenDrawer)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_DrawerAnim.enabled = true;
            m_CanChooseThread = true;
            m_ThreadsButton.SetActive(true);
            m_DrawerButton.SetActive(false);
            m_CanOpenDrawer = false;
        }
    }

    public void ChooseThread(int i_ThreadIndex)
    {
        if (m_CanChooseThread)
        {
            m_Hints[m_HintCount].SetActive(false);
            PlayerPrefs.SetInt("Thread", i_ThreadIndex);
            m_DrawerAnim.SetTrigger("close");
            m_SelectedThread = m_Threads[i_ThreadIndex];
            m_SelectedThreadButton = m_ThreadButtons[i_ThreadIndex];
            m_Thread.color = m_ThreadColors[i_ThreadIndex];
            m_ThreadLoop.color = m_ThreadColors[i_ThreadIndex];
            m_SelectedThread.SetActive(true);
            m_ThreadOldPosition = m_SelectedThread.transform.localPosition;
            m_CanChooseThread = false;
        }

        if (!m_ThreadFinished)
        {
            m_Offset = m_SelectedThread.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_MoveThread = true;
        }
    }

    public void ReleaseThread()
    {
        m_MoveThread = false;

        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(m_PointerEventData, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.name == "ThreadTrigger")
            {
                m_CanShowHint = false;
                m_SelectedThread.transform.localPosition = m_ThreadPosition;
                m_CanClickThread = true;
                m_FirstArrows.SetActive(true);
                m_ThreadAnimationButtons.SetActive(true);
                m_ThreadFinished = true;
                m_PositionsHandlerLastScene.SetCameraToSewingZoomIn();
                m_MoveThread = false;
            }
        }
    }

    public void ClickThread(int i_Index)
    {
        if (m_CanClickThread)
        {
            m_ThreadClicked = true;
            m_CanClickThread = false;
            m_ThreadCount++;
        }

        else if (m_ThreadClicked && i_Index == m_ThreadCount)
        {
            if (!m_Thread.gameObject.activeInHierarchy)
            {
                m_Thread.gameObject.SetActive(true);
            }

            else
            {
                if (m_ThreadAnim.speed == 0)
                {
                    m_ThreadAnim.speed = 1f;
                }
            }

            if (m_ThreadCount == 8)
            {
                m_ThreadAnimationButtons.SetActive(false);
                m_CanShowHint = true;
                m_HintCount++;
                m_CanClickPapucicaUp = true;
            }

            StartCoroutine(IncreaseThreadCount());
        }
    }

    IEnumerator IncreaseThreadCount()
    {
        yield return new WaitForSeconds(0.15f);
        m_ThreadCount++;
    }

    public void ClickPapucica()
    {
        if (m_CanClickPapucicaUp)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_NeedleAnim.SetTrigger("up");
            m_PapucicaAnim.SetTrigger("up");
            m_DressButton.SetActive(true);
            m_ThreadLoopAnim.enabled = true;
            m_Dress.gameObject.SetActive(true);
            m_OldDressPosition = m_Dress.anchoredPosition;
            m_MoveDress = true;
            t = 0;
            m_CanClickPapucicaUp = false;
        }

        else if (m_CanClickPapucicaDown)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_NeedleAnim.SetTrigger("down");
            m_PapucicaAnim.SetTrigger("down");
            m_ThreadLoopAnim.SetTrigger("down");
            m_PositionsHandlerLastScene.SetCameraToSewingZoomOut();
            m_RotatorButton1.SetActive(true);
            m_RotatorButton2.SetActive(true);
            m_CanClickRotator = true;
            m_CanClickPapucicaDown = false;
        }

        else if (m_FinishPapucica)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_NeedleAnim.SetTrigger("finish");
            m_PapucicaAnim.SetTrigger("finish");
            m_ThreadLoopAnim.SetTrigger("finish");
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        m_Stars.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        m_PositionsHandlerLastScene.SetCameraToIroning();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickDress()
    {
        if (m_CanClickDress)
        {
            m_Hints[m_HintCount].SetActive(false);
            //for (int i = 0; i < m_DressAnims.Count; i++)
            //{
            //    m_DressAnims[i].SetTrigger("move");
            //}

            //m_CanClickPapucicaDown = true;
            //m_SetTriggers = true;
            //m_CanClickDress = false;
            //m_SewingDressHandler.MovePatterns();
            //StartCoroutine(StopPatterns());

            m_DressFirstSwipePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_DressFirstSwipeClicked = true;
        }
    }

    public void EndDressDrag()
    {
        if (m_DressFirstSwipeClicked)
        {
            m_DressSecondSwipePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(m_DressSecondSwipePosition.y - m_DressFirstSwipePosition.y) > 0)
            {
                if (m_DressSecondSwipePosition.y > m_DressFirstSwipePosition.x)
                {
                    for (int i = 0; i < m_DressAnims.Count; i++)
                    {
                        m_DressAnims[i].SetTrigger("move");
                    }

                    m_HintCount++;
                    m_CanClickPapucicaDown = true;
                    m_SetTriggers = true;
                    m_CanClickDress = false;
                    m_SewingDressHandler.MovePatterns();
                    StartCoroutine(StopPatterns());
                    m_DressFirstSwipeClicked = false;
                }
            }
        }
    }

    IEnumerator StopPatterns()
    {
        yield return new WaitForSeconds(0.1f);
        m_SewingDressHandler.StopPatterns();
    }

    public void ClickRotatorStart()
    {
        if (m_CanClickRotator)
        {
            m_StartRotatorClicked = true;
        }
    }

    public void ClickRotator()
    {
        if (m_CanClickRotator && m_StartRotatorClicked)
        {
            if (!m_RotatorFinished)
            {
                if (m_SetTriggers)
                {
                    m_NeedleAnim.SetTrigger("loop");
                    m_ThreadLoopAnim.SetTrigger("loop");
                    m_TopAnim.enabled = true;
                    m_RotatorAnim.enabled = true;
                    m_SetTriggers = false;
                }

                for (int i = 0; i < m_DressAnims.Count; i++)
                {
                    m_DressAnims[i].speed = 1;
                }

                m_Hints[m_HintCount].SetActive(false);
                m_MachineSound.enabled = true;
                m_NeedleAnim.speed = 1;
                m_ThreadLoopAnim.speed = 1;
                m_TopAnim.speed = 1;
                m_RotatorAnim.speed = 1;

                m_SewingDressHandler.MovePatterns();
                StartCoroutine(StopClickingRotator());
                m_StartRotatorClicked = false;
            }

            else
            {
                m_MachineSound.enabled = true;
                m_NeedleAnim.Play("Needle 3", 0, 1);
                m_NeedleAnim.speed = 1;
                m_ThreadLoopAnim.Play("ThreadLoop 3", 0, 1);
                m_ThreadLoopAnim.speed = 1;
                m_TopAnim.Play("Top", 0, 1);
                m_TopAnim.speed = 1;
                m_RotatorAnim.Play("Rotator", 0, 1);
                m_RotatorAnim.speed = 1;
                StartCoroutine(StopLoop());
                m_StartRotatorClicked = false;
            }
        }
    }

    IEnumerator StopLoop()
    {
        yield return new WaitForSeconds(1.25f);
        m_NeedleAnim.speed = 0;
        m_ThreadLoopAnim.speed = 0;
        m_TopAnim.speed = 0;
        m_RotatorAnim.speed = 0;
        m_MachineSound.enabled = false;
    }

    IEnumerator StopClickingRotator()
    {
        //0.9
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < m_DressAnims.Count; i++)
        {
            m_DressAnims[i].speed = 0;
        }

        m_NeedleAnim.speed = 0;
        m_ThreadLoopAnim.speed = 0;
        m_TopAnim.speed = 0;
        m_RotatorAnim.speed = 0;

        m_MachineSound.enabled = false;

        m_SewingDressHandler.StopPatterns();
    }

    public void SewingFinished()
    {
        if (!m_SewingFinished)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_MachineSound.enabled = false;
            m_NeedleAnim.speed = 0;
            m_ThreadLoopAnim.speed = 0;
            m_TopAnim.speed = 0;
            m_RotatorAnim.speed = 0;
            //m_CanClickRotator = false;
            m_SewingFinished = true;
            m_SewingDressHandler.StopPatterns();
            StartCoroutine(FinishSewing());
            //m_IroningHandler.enabled = true;
            m_RotatorFinished = true;
        }
    }

    IEnumerator FinishSewing()
    {
        yield return new WaitForSeconds(1f);
        m_FinishPapucica = true;
    }
}
