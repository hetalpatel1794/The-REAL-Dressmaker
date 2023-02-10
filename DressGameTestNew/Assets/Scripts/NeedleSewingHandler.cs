using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NeedleSewingHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Thread;
    [SerializeField]
    private GameObject m_ThreadColorObject;
    [SerializeField]
    private GameObject m_ThreadTop;
    [SerializeField]
    private Animator m_Needle;
    [SerializeField]
    private GameObject m_Buttons;
    [SerializeField]
    private Animator m_ThreadAnim;
    [SerializeField]
    private Animator m_ThreadAnimColor;
    [SerializeField]
    private Animator m_ThreadTopAnim;
    [SerializeField]
    private GameObject m_GetThreadButton;
    [SerializeField]
    private GameObject m_TieThreadButton;
    [SerializeField]
    private Animator m_NeedleAnim;
    [SerializeField]
    private PositionsHandlerLastScene m_PositionsHandlerLastScene;
    [SerializeField]
    private GameObject m_NeedleThread;
    [SerializeField]
    private GameObject m_FinishButton;
    [SerializeField]
    private List<Animator> m_GirlWow;
    [SerializeField]
    private TryingHandler m_TryingHandler;
    [SerializeField]
    private List<Color> m_Colors;
    [SerializeField]
    private SpriteRenderer m_ThreadColor;
    [SerializeField]
    private GameObject m_Arrow;
    [SerializeField]
    private Vector3 m_NewThreadPosition;
    [SerializeField]
    private Vector2 m_DressPositon;
    [SerializeField]
    private GameObject m_Stars;

    [SerializeField]
    private List<GameObject> m_Hints;

    private int m_HintCount;

    private ButtonHandler[] m_AllButtons;

    private DressHandler m_DressHandler;
    private GameObject m_MagicDown;
    private GameObject m_MagicUp;
    private Image m_DressDetails;

    private List<ButtonHandler> m_AddedButtons = new List<ButtonHandler>();

    private bool m_ThreadClicked;
    private bool m_CanGetNeedle;
    private bool m_GetThread;
    private bool m_UnwrapThread;
    private bool m_CanTieThread;
    private bool m_Move;
    private bool m_CanMove;
    private bool m_SetAnim;
    private bool m_ButtonsSet;

    private Vector3 m_Offset;
    private Vector3 m_MousePos;
    private Vector2 m_MousePos2D;

    private Vector3 m_FirstSwipePos;
    private Vector3 m_SecondSwipePos;

    private Vector3 m_OldThreadPosition;

    private float t;

    private bool m_FirstSwipeClicked;
    private bool m_MoveToCenter;

    private bool m_GetThreadDragFirstClick;

    private Vector3 m_GetThreadDragFirstPos;
    private Vector3 m_GetThreadDragSecondPos;

    private bool m_TieThreadDragFirstClick;

    private Vector3 m_TieThreadDragFirstPos;
    private Vector3 m_TieThreadDragSecondPos;

    private int m_ColorIndex;

    private RaycastHit2D m_Hit;

    private IEnumerator Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();
        //DontDestroy.Instance.GetDressRectTransform().anchoredPosition = m_DressPositon;
        //DontDestroy.Instance.GetDressRectTransform().transform.localScale = Vector3.one;
        DontDestroy.Instance.SetLayerOrder(3);
        //yield return new WaitForSeconds(0.5f);
        m_DressHandler = DontDestroy.Instance.GetDressHandler();
        m_DressDetails = DontDestroy.Instance.GetDressDetails();
        m_MagicUp = DontDestroy.Instance.GetMagicUp();
        m_MagicDown = DontDestroy.Instance.GetMagicDown();
        DontDestroy.Instance.DisableMoveDressHandler();
        m_Buttons.SetActive(true);
        m_DressHandler.ResetDetails();
        AddButtons();
        yield return new WaitForSeconds(0.5f);
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
        if (Input.GetMouseButtonDown(0) && m_CanMove)
        {
            m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_MousePos2D = new Vector2(m_MousePos.x, m_MousePos.y);

            m_Hit = Physics2D.Raycast(m_MousePos2D, Vector2.zero);
            if (m_Hit.collider != null && m_Hit.collider.tag == "NeedleThread")
            {
                if (m_SetAnim)
                {
                    m_ThreadAnim.SetTrigger("loop");
                    m_ThreadAnimColor.SetTrigger("loop"); 
                    m_NeedleAnim.SetTrigger("loop");
                }

                m_Offset = m_NeedleThread.transform.localPosition - m_MousePos;
                m_DressDetails.color = m_Colors[m_ColorIndex];
                m_DressHandler.SetDetails();
                m_Move = true;
            }
        }

        if (Input.GetMouseButton(0) && m_Move)
        {
            m_NeedleThread.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;
            m_ThreadAnim.speed = 1;
            m_ThreadAnimColor.speed = 1;
            m_NeedleAnim.speed = 1;

            if (m_DressHandler.DetailsFinished() && !m_ButtonsSet)
            {
                m_FinishButton.SetActive(true);
                m_ButtonsSet = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (m_Move)
            {
                m_ThreadAnim.speed = 0;
                m_ThreadAnimColor.speed = 0;
                m_NeedleAnim.speed = 0;
                m_DressHandler.StopDetails();
                m_Move = false;
            }

            //if (m_FirstSwipeClicked)
            //{
            //    m_SecondSwipePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_SecondSwipePos.x - m_FirstSwipePos.x) > 0)
            //    {
            //        if (m_SecondSwipePos.x < m_FirstSwipePos.x)
            //        {
            //            m_Hints[m_HintCount].SetActive(false);
            //            m_HintCount++;
            //            m_ThreadAnim.enabled = true;
            //            m_ThreadAnimColor.enabled = true;
            //            m_UnwrapThread = true;
            //            m_GetThread = false;
            //        }
            //    }

            //    m_FirstSwipeClicked = false;
            //}
        }

        if (m_MoveToCenter)
        {
            t += Time.deltaTime / 0.7f;
            m_NeedleThread.transform.localPosition = Vector3.Lerp(m_OldThreadPosition, m_NewThreadPosition, t);

            if (m_NeedleThread.transform.localPosition == m_NewThreadPosition)
            {
                m_MoveToCenter = false;
            }
        }
    }

    public void ClickThread(int i_Index)
    {
        if (!m_ThreadClicked && this.enabled)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_ThreadColor.color = m_Colors[i_Index];
            m_ColorIndex = i_Index;
            m_Thread.SetActive(true);
            m_ThreadColorObject.SetActive(true);
            m_ThreadTop.SetActive(true);
            m_CanGetNeedle = true;
            m_ThreadClicked = true;
        }
    }

    public void ClickNeedle()
    {
        if (m_CanGetNeedle)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_Needle.enabled = true;
            m_GetThread = true;
            m_GetThreadButton.SetActive(true);
            StartCoroutine(EnableArrow());
            m_CanGetNeedle = false;
        }
    }

    public void AddButtons()
    {
        //m_AddedButtons.Add(i_Button);

        m_AllButtons = DontDestroy.Instance.GetButtonsParent().transform.GetComponentsInChildren<ButtonHandler>();

        for (int i = 0; i < m_AllButtons.Length; i++)
        {
            m_AllButtons[i].DisableThread();
        }
    }

    public void SetButtonsAndDetails()
    {
        for (int i = 0; i < m_AllButtons.Length; i++)
        {
            m_AllButtons[i].EnableThread(m_Colors[m_ColorIndex]);
        }

        m_DressDetails.color = m_Colors[m_ColorIndex];
        m_DressHandler.SetEndDetails();
    }

    IEnumerator EnableArrow()
    {
        yield return new WaitForSeconds(0.5f);
        m_Arrow.SetActive(true);
    }

    public void DragThread()
    {
        if (m_GetThread)
        {
            //m_ThreadAnim.enabled = true;
            //m_ThreadAnimColor.enabled = true;
            //m_UnwrapThread = true;
            //m_GetThread = false;
            m_Hints[m_HintCount].SetActive(false);
            m_FirstSwipePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_FirstSwipeClicked = true;
        }
    }

    public void EndThreadDrag()
    {
        if (m_FirstSwipeClicked)
        {
            m_SecondSwipePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(m_SecondSwipePos.x - m_FirstSwipePos.x) > 0)
            {
                if (m_SecondSwipePos.x < m_FirstSwipePos.x && m_SecondSwipePos.y > m_FirstSwipePos.y)
                {
                    m_Hints[m_HintCount].SetActive(false);
                    m_HintCount++;
                    m_ThreadAnim.enabled = true;
                    m_ThreadAnimColor.enabled = true;
                    m_UnwrapThread = true;
                    m_GetThread = false;
                }
            }

            m_FirstSwipeClicked = false;
        }
    }

    public void GetThread()
    {
        if (m_UnwrapThread)
        {
            m_GetThreadDragFirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_Hints[m_HintCount].SetActive(false);
            m_GetThreadDragFirstClick = true;
            //m_HintCount++;
            //m_ThreadAnim.SetTrigger("unwrap");
            //m_ThreadAnimColor.SetTrigger("unwrap");
            //m_ThreadTopAnim.SetTrigger("unwrap");
            //m_CanTieThread = true;
            //m_TieThreadButton.SetActive(true);
        }
    }

    public void EndGetThreadDrag()
    {
        if (m_GetThreadDragFirstClick && m_UnwrapThread)
        {
            m_GetThreadDragSecondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(m_GetThreadDragSecondPos.x - m_GetThreadDragFirstPos.x) > 0)
            {
                if (m_GetThreadDragSecondPos.x > m_GetThreadDragFirstPos.x || m_GetThreadDragSecondPos.x < m_GetThreadDragFirstPos.x)
                {
                    m_HintCount++;
                    m_ThreadAnim.SetTrigger("unwrap");
                    m_ThreadAnimColor.SetTrigger("unwrap");
                    m_ThreadTopAnim.SetTrigger("unwrap");
                    m_CanTieThread = true;
                    m_TieThreadButton.SetActive(true);
                    m_UnwrapThread = false;
                }
            }

            m_GetThreadDragFirstClick = false;
        }
    }

    public void TieThread()
    {
        if (m_CanTieThread)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_TieThreadDragFirstClick = true;
            m_TieThreadDragFirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //m_HintCount++;
            //m_ThreadAnim.SetTrigger("tie");
            //m_ThreadAnimColor.SetTrigger("tie");
            //m_ThreadTop.SetActive(false);
            //StartCoroutine(Scale());
            //m_CanTieThread = false;
        }
    }

    public void EndTieThreadDrag()
    {
        if (m_CanTieThread && m_TieThreadDragFirstClick)
        {
            m_TieThreadDragSecondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(m_TieThreadDragSecondPos.x - m_TieThreadDragFirstPos.x) > 0)
            {
                if (m_TieThreadDragSecondPos.x > m_TieThreadDragFirstPos.x || m_TieThreadDragSecondPos.x < m_TieThreadDragFirstPos.x)
                {
                    m_HintCount++;
                    m_ThreadAnim.SetTrigger("tie");
                    m_ThreadAnimColor.SetTrigger("tie");
                    m_ThreadTop.SetActive(false);
                    StartCoroutine(Scale());
                    m_CanTieThread = false;
                }
            }

            m_TieThreadDragFirstClick = false;
        }
    }

    IEnumerator Scale()
    {
        yield return new WaitForSeconds(0.8f);
        m_ThreadAnim.SetTrigger("scale");
        m_ThreadAnimColor.SetTrigger("scale");
        m_NeedleAnim.SetTrigger("scale");
        m_PositionsHandlerLastScene.SetCameraToNeedleSewing();
        m_OldThreadPosition = m_NeedleThread.transform.localPosition;
        m_MoveToCenter = true;
        yield return new WaitForSeconds(0.5f);
        m_CanMove = true;
        m_SetAnim = true;
    }

    public void Finish()
    {
        m_Hints[m_HintCount].SetActive(false);
        m_HintCount++;
        m_NeedleThread.SetActive(false);
        //for (int i = 0; i < m_AddedButtons.Count; i++) 
        //{
        //    m_AddedButtons[i].EnableThread(m_Colors[m_ColorIndex]);
        //}

        for (int i = 0; i < m_AllButtons.Length; i++)
        {
            m_AllButtons[i].EnableThread(m_Colors[m_ColorIndex]);
        }
        StartCoroutine(ShowMagic());
        m_FinishButton.SetActive(false);
        StartCoroutine(MoveCamera());
    }

    IEnumerator ShowMagic()
    {
        m_MagicDown.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        m_MagicUp.SetActive(true);
    }

    IEnumerator MoveCamera()
    {
        m_Stars.SetActive(true);
        yield return new WaitForSeconds(1f);
        int m_GirlWowIndex = PlayerPrefs.GetInt("Girl");
        m_GirlWow[m_GirlWowIndex].gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_PositionsHandlerLastScene.SetCameraToTrying();
        //yield return new WaitForSeconds(1.2f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //m_GirlWow[m_GirlWowIndex].enabled = true;
        //yield return new WaitForSeconds(3f);
        //m_TryingHandler.enabled = true;
    }
}
