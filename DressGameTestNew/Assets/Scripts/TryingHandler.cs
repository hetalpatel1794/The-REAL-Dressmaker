using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TryingHandler : MonoBehaviour
{
    [SerializeField]
    private Animator m_ParavanAnim;
    [SerializeField]
    private GameObject m_MoveParavanButton;
    [SerializeField]
    private GameObject m_OpenParavanLeft;
    [SerializeField]
    private GameObject m_OpenParavanRight;
    [SerializeField]
    private List<GameObject> m_Clothes;
    [SerializeField]
    private List<GameObject> m_DressParent;
    [SerializeField]
    private Vector2 m_DressPosition;
    [SerializeField]
    private RectTransform m_Dress;
    [SerializeField]
    private List<GameObject> m_GirlWow;
    [SerializeField]
    private List<GameObject> m_DressedGirl;
    [SerializeField]
    private PositionsHandlerLastScene m_PositionsHandlerLastScene;
    [SerializeField]
    private GameObject m_MoveMirrorButton;
    [SerializeField]
    private GameObject m_RotateMirrorButton;
    [SerializeField]
    private Animator m_MirrorAnim;
    [SerializeField]
    private List<Animator> m_GirlAnim;
    [SerializeField]
    private GameObject m_LevelCompletedPage;
    [SerializeField]
    private GameObject m_FinishButton;
    [SerializeField]
    private GameObject m_ObjectsToDisable;
    [SerializeField]
    private PayingHandler m_PayingHandler;
    [SerializeField]
    private List<GameObject> m_GirlsTrying;
    [SerializeField]
    private GameObject m_FirstFinishButton;
    [SerializeField]
    private Animator m_OpenParavanLeftAnim;
    [SerializeField]
    private Animator m_OpenParavanRightAnim;
    [SerializeField]
    private EventSystem m_EventSystem;
    [SerializeField]
    private GameObject m_ParavanTrigger;
    [SerializeField]
    private GameObject m_Confetti;
    [SerializeField]
    private Vector2 m_DressStartPosition;
    [SerializeField]
    private GameObject m_Stars;
    [SerializeField]
    private List<GameObject> m_Hints;

    private int m_HintCount;

    private GameObject m_MoveDressButton;

    private bool m_CanMoveParavan;
    private bool m_CanOpenParavanLeft;
    private bool m_CanOpenParavanRight;
    private bool m_CanMoveMirror;
    private bool m_RotateMirror;

    private PointerEventData m_PointerEventData;

    private bool m_DetectParavanSwipe;

    private bool m_DetectLeftParavanSwipe;
    private bool m_DetectRightParavanSwipe;

    private bool m_ParavanLeftOpened;
    private bool m_ParavanRightOpened;

    private Vector3 m_ParavanLeftFirstMousePos;
    private Vector3 m_ParavanLeftSecondMousePos;

    private Vector3 m_ParavanRightFirstMousePos;
    private Vector3 m_ParavanRightSecondMousePos;

    private Vector3 m_Offset;

    private bool m_CanShowHints;

    private int m_GirlIndex;

    private Vector3 m_FirstMouseParavanPosition;
    private Vector3 m_SecondMouseParavanPosition;

    private bool m_MoveDress;
    private bool m_CanMoveDress;

    private bool m_DetectMirrorSwipe;

    private Vector3 m_MirrorFirstMousePos;
    private Vector3 m_MirrorSecondMousePos;

    private bool m_DetectMirrorRotationSwipe;

    private Vector3 m_MirrorRotationFirstMousePos;
    private Vector3 m_MirrorRotationSecondMousePos;

    private IEnumerator Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();

        DontDestroy.Instance.GetDressRectTransform().anchoredPosition = m_DressStartPosition;
        DontDestroy.Instance.GetDressRectTransform().localScale = Vector3.one * 1.052465f;
        DontDestroy.Instance.SetLayerOrder(17);
        m_MoveDressButton = DontDestroy.Instance.GetMoveDressToParavanButton();
        m_Dress = DontDestroy.Instance.GetDress();
        DontDestroy.Instance.SetTryingHandler(this);
        m_GirlIndex = PlayerPrefs.GetInt("Girl");
        m_GirlWow[m_GirlIndex].SetActive(true);
        yield return new WaitForSeconds(11f);
        m_CanMoveParavan = true;
        m_CanShowHints = true;
    }

    public void ShowHints()
    {
        if (m_HintCount < m_Hints.Count && m_CanShowHints)
        {
            m_Hints[m_HintCount].SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //if (m_DetectParavanSwipe)
            //{
            //    m_SecondMouseParavanPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_SecondMouseParavanPosition.x - m_FirstMouseParavanPosition.x) > 0)
            //    {
            //        if (m_SecondMouseParavanPosition.x < m_FirstMouseParavanPosition.x)
            //        {
            //            m_Hints[m_HintCount].SetActive(false);
            //            m_HintCount++;
            //            m_ParavanAnim.enabled = true;
            //            m_MoveParavanButton.SetActive(false);
            //            m_OpenParavanLeft.SetActive(true);
            //            m_OpenParavanRight.SetActive(true);
            //            m_CanOpenParavanLeft = true;
            //            m_CanOpenParavanRight = true;
            //            m_CanMoveParavan = false;
            //            m_DetectParavanSwipe = false;
            //        }
            //    }
            //}

            //if (m_DetectLeftParavanSwipe)
            //{
            //    m_ParavanLeftSecondMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_ParavanLeftSecondMousePos.x - m_ParavanLeftFirstMousePos.x) > 0)
            //    {
            //        if (m_ParavanLeftSecondMousePos.x < m_ParavanLeftFirstMousePos.x)
            //        {
            //            m_OpenParavanLeftAnim.enabled = true;
            //            m_CanOpenParavanLeft = false;
            //            m_ParavanLeftOpened = true;
            //            m_OpenParavanLeft.SetActive(false);
            //            m_Hints[m_HintCount].SetActive(false);
            //            if (m_ParavanRightOpened)
            //            {
            //                m_Hints[m_HintCount].SetActive(false);
            //                m_HintCount++;
            //                m_CanShowHints = false;
            //                StartCoroutine(EnableClothes());
            //            }
            //            m_DetectLeftParavanSwipe = false;
            //        }
            //    }
            //}

            //if (m_DetectRightParavanSwipe)
            //{
            //    m_ParavanRightFirstMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_ParavanRightSecondMousePos.x - m_ParavanRightFirstMousePos.x) > 0)
            //    {
            //        if (m_ParavanRightSecondMousePos.x < m_ParavanRightFirstMousePos.x)
            //        {
            //            m_OpenParavanRightAnim.enabled = true;
            //            m_CanOpenParavanRight = false;
            //            m_ParavanRightOpened = true;
            //            m_OpenParavanRight.SetActive(false);
            //            m_Hints[m_HintCount].SetActive(false);
            //            if (m_ParavanLeftOpened)
            //            {
            //                m_Hints[m_HintCount].SetActive(false);
            //                m_HintCount++;
            //                m_CanShowHints = false;
            //                StartCoroutine(EnableClothes());
            //            }
            //        }
            //    }

            //    m_DetectRightParavanSwipe = false;
            //}

            //if (m_DetectMirrorSwipe)
            //{
            //    m_MirrorSecondMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_MirrorSecondMousePos.y - m_MirrorFirstMousePos.y) > 0)
            //    {
            //        if (m_MirrorSecondMousePos.y < m_MirrorFirstMousePos.y)
            //        {
            //            m_Hints[m_HintCount].SetActive(false);
            //            m_HintCount++;
            //            m_MirrorAnim.enabled = true;
            //            m_MoveMirrorButton.SetActive(false);
            //            m_RotateMirrorButton.SetActive(true);
            //            m_RotateMirror = true;
            //            m_CanMoveMirror = false;
            //            m_DetectMirrorSwipe = false;
            //        }
            //    }
            //}

            //if (m_DetectMirrorRotationSwipe)
            //{
            //    m_MirrorRotationSecondMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_MirrorRotationSecondMousePos.y - m_MirrorRotationFirstMousePos.x) > 0)
            //    {
            //        if (m_MirrorRotationSecondMousePos.x > m_MirrorRotationFirstMousePos.x)
            //        {
            //            m_Hints[m_HintCount].SetActive(false);
            //            m_HintCount++;
            //            m_RotateMirrorButton.SetActive(false);
            //            m_MirrorAnim.SetTrigger("rotate");
            //            StartCoroutine(PlayGirlAnimation());
            //            m_RotateMirror = false;
            //            m_DetectMirrorRotationSwipe = false;
            //        }
            //    }
            //}
        }

        if (Input.GetMouseButton(0))
        {
            if (m_MoveDress)
            {
                m_Dress.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;
            }
        }
    }

    public void MoveParavan()
    {
        if (m_CanMoveParavan && this.enabled)
        {
            //m_ParavanAnim.enabled = true;
            //m_MoveParavanButton.SetActive(false);
            //m_OpenParavanButton.SetActive(true);
            //m_CanOpenParavan = true;
            m_FirstMouseParavanPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            m_DetectParavanSwipe = true;
            //m_CanMoveParavan = false;
        }
    }

    public void EndParavanDrag()
    {
        if (m_DetectParavanSwipe)
        {
            m_SecondMouseParavanPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (Mathf.Abs(m_SecondMouseParavanPosition.x - m_FirstMouseParavanPosition.x) > 0)
            {
                if (m_SecondMouseParavanPosition.x < m_FirstMouseParavanPosition.x)
                {
                    m_Hints[m_HintCount].SetActive(false);
                    m_HintCount++;
                    m_ParavanAnim.enabled = true;
                    m_MoveParavanButton.SetActive(false);
                    m_OpenParavanLeft.SetActive(true);
                    m_OpenParavanRight.SetActive(true);
                    m_CanOpenParavanLeft = true;
                    m_CanOpenParavanRight = true;
                    m_CanMoveParavan = false;
                    m_DetectParavanSwipe = false;
                }
            }
        }
    }

    public void MoveDress()
    {
        if (m_CanMoveDress)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_Offset = m_Dress.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_MoveDress = true;
        }
    }

    public void ReleaseDress()
    {
        if (m_CanMoveDress)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(m_PointerEventData, results);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.name == "ParavanTrigger")
                {
                    m_Hints[m_HintCount].SetActive(false);
                    m_HintCount++;
                    m_CanShowHints = false;
                    StartCoroutine(FinishDressing());
                    m_ParavanTrigger.SetActive(false);
                    m_MoveDressButton.SetActive(false);
                    m_CanMoveDress = false;
                }
            }
            m_MoveDress = false;
        }
    }

    public void OpenParavanLeft()
    {
        if (m_CanOpenParavanLeft)
        {
            //m_ParavanAnim.SetTrigger("open");
            //StartCoroutine(EnableClothes());
            //m_OpenParavanButton.SetActive(false);
            //m_CanOpenParavan = false;

            m_ParavanLeftFirstMousePos= Camera.main.ScreenToViewportPoint(Input.mousePosition);
            m_DetectLeftParavanSwipe = true;
        }
    }

    public void EndParavanLeftDrag()
    {
        if (m_DetectLeftParavanSwipe)
        {
            m_ParavanLeftSecondMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (Mathf.Abs(m_ParavanLeftSecondMousePos.x - m_ParavanLeftFirstMousePos.x) > 0)
            {
                if (m_ParavanLeftSecondMousePos.x < m_ParavanLeftFirstMousePos.x)
                {
                    m_OpenParavanLeftAnim.enabled = true;
                    m_CanOpenParavanLeft = false;
                    m_ParavanLeftOpened = true;
                    m_OpenParavanLeft.SetActive(false);
                    m_Hints[m_HintCount].SetActive(false);
                    if (m_ParavanRightOpened)
                    {
                        m_Hints[m_HintCount].SetActive(false);
                        m_HintCount++;
                        m_CanShowHints = false;
                        StartCoroutine(EnableClothes());
                    }
                    m_DetectLeftParavanSwipe = false;
                }
            }
        }
    }

    public void OpenParavanRight()
    {
        if (m_CanOpenParavanRight)
        {
            m_ParavanRightFirstMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            m_DetectRightParavanSwipe = true;
        }
    }

    public void EndParavanRightDrag()
    {
        if (m_DetectRightParavanSwipe)
        {
            m_ParavanRightFirstMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (Mathf.Abs(m_ParavanRightSecondMousePos.x - m_ParavanRightFirstMousePos.x) > 0)
            {
                if (m_ParavanRightSecondMousePos.x < m_ParavanRightFirstMousePos.x)
                {
                    m_OpenParavanRightAnim.enabled = true;
                    m_CanOpenParavanRight = false;
                    m_ParavanRightOpened = true;
                    m_OpenParavanRight.SetActive(false);
                    m_Hints[m_HintCount].SetActive(false);
                    if (m_ParavanLeftOpened)
                    {
                        m_Hints[m_HintCount].SetActive(false);
                        m_HintCount++;
                        m_CanShowHints = false;
                        StartCoroutine(EnableClothes());
                    }
                }
            }

            m_DetectRightParavanSwipe = false;
        }
    }

    IEnumerator EnableClothes()
    {
        yield return new WaitForSeconds(1);
        m_GirlsTrying[m_GirlIndex].SetActive(true);
        m_GirlWow[m_GirlIndex].SetActive(false);
        yield return new WaitForSeconds(2.5f);
        m_Clothes[m_GirlIndex].SetActive(true);
        m_MoveDressButton.SetActive(true);
        m_ParavanTrigger.SetActive(true);
        m_CanMoveDress = true;
        m_CanShowHints = true;
        //yield return new WaitForSeconds(0.6f);
        //m_Dress.SetParent(m_DressParent[m_GirlIndex].transform);
        //m_Dress.anchoredPosition = m_DressPosition;
        //m_Dress.transform.localScale = Vector3.one * 0.97f;
        //m_Clothes[m_GirlIndex].transform.SetParent(m_ParavanAnim.transform);
        //yield return new WaitForSeconds(3.2f);
        //m_ParavanAnim.SetTrigger("end");
        //m_GirlsTrying[m_GirlIndex].SetActive(false);
        //m_DressedGirl[m_GirlIndex].SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        //m_PositionsHandlerLastScene.SetCameraToMirror();
        //m_MoveMirrorButton.SetActive(true);
        //m_CanMoveMirror = true;
    }

    IEnumerator FinishDressing()
    {
        m_GirlsTrying[m_GirlIndex].GetComponent<Animator>().speed = 1;
        //m_Dress.SetParent(m_DressParent[m_GirlIndex].transform);
        m_Dress.anchoredPosition = m_DressPosition;
        //m_Dress.transform.localScale = Vector3.one * 0.97f;
        m_Dress.transform.localScale = Vector3.one * 0.8677106f;
        DontDestroy.Instance.SetLayerOrder(25);
        m_Clothes[m_GirlIndex].transform.SetParent(m_ParavanAnim.transform);
        yield return new WaitForSeconds(3.2f);
        m_ParavanAnim.SetTrigger("end");
        m_GirlsTrying[m_GirlIndex].SetActive(false);
        m_DressedGirl[m_GirlIndex].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        m_PositionsHandlerLastScene.SetCameraToMirror();
        m_MoveMirrorButton.SetActive(true);
        m_CanMoveMirror = true;
        m_CanShowHints = true;
    }

    public void MoveMirror()
    {
        if (m_CanMoveMirror)
        {
            m_MirrorFirstMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_DetectMirrorSwipe = true;
            //m_MirrorAnim.enabled = true;
            //m_MoveMirrorButton.SetActive(false);
            //m_RotateMirrorButton.SetActive(true);
            //m_RotateMirror = true;
            //m_CanMoveMirror = false;
        }
    }

    public void EndMoveMirrorDrag()
    {
        if (m_DetectMirrorSwipe)
        {
            m_MirrorSecondMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(m_MirrorSecondMousePos.y - m_MirrorFirstMousePos.y) > 0)
            {
                if (m_MirrorSecondMousePos.y < m_MirrorFirstMousePos.y)
                {
                    m_Hints[m_HintCount].SetActive(false);
                    m_HintCount++;
                    m_MirrorAnim.enabled = true;
                    m_MoveMirrorButton.SetActive(false);
                    m_RotateMirrorButton.SetActive(true);
                    m_RotateMirror = true;
                    m_CanMoveMirror = false;
                    m_DetectMirrorSwipe = false;
                }
            }
        }
    }

    public void RotateMirror()
    {
        if (m_RotateMirror)
        {
            m_MirrorRotationFirstMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_DetectMirrorRotationSwipe = true;
            //m_RotateMirrorButton.SetActive(false);
            //m_MirrorAnim.SetTrigger("rotate");
            //StartCoroutine(PlayGirlAnimation());
            //m_RotateMirror = false;
        }
    }

    public void EndMirrorRotateDrag()
    {
        if (m_DetectMirrorRotationSwipe)
        {
            m_MirrorRotationSecondMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(m_MirrorRotationSecondMousePos.y - m_MirrorRotationFirstMousePos.x) > 0)
            {
                if (m_MirrorRotationSecondMousePos.x > m_MirrorRotationFirstMousePos.x)
                {
                    m_Hints[m_HintCount].SetActive(false);
                    m_HintCount++;
                    m_RotateMirrorButton.SetActive(false);
                    m_MirrorAnim.SetTrigger("rotate");
                    StartCoroutine(PlayGirlAnimation());
                    m_RotateMirror = false;
                    m_DetectMirrorRotationSwipe = false;
                }
            }
        }
    }

    IEnumerator PlayGirlAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        m_GirlAnim[m_GirlIndex].enabled = true;
        yield return new WaitForSeconds(8f);
        m_PositionsHandlerLastScene.SetCameraToGirlZoom();
        yield return new WaitForSeconds(2.2f);
        m_MirrorAnim.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        m_FirstFinishButton.SetActive(true);
    }

    public void FinishButton()
    {
        m_FirstFinishButton.SetActive(false);
        m_Stars.SetActive(true);
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(1f);
        m_LevelCompletedPage.SetActive(true);
        m_Confetti.SetActive(true);
        m_FinishButton.SetActive(true);
    }

    public void Finish()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //m_ObjectsToDisable.SetActive(false);
        //m_PositionsHandlerLastScene.SetCameraToPaying();
        //m_LevelCompletedPage.SetActive(false);
        //m_PayingHandler.enabled = true;
    }
}
