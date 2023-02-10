using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PayingHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Table;
    [SerializeField]
    private List<GameObject> m_Girls;
    [SerializeField]
    private List<GameObject> m_DressParents;
    [SerializeField]
    private RectTransform m_Dress;
    [SerializeField]
    private Vector2 m_DressPosition;
    [SerializeField]
    private List<Animator> m_GirlAnims;
    [SerializeField]
    private Animator m_RegisterAnim;
    [SerializeField]
    private Animator m_ReceiptAnim;
    [SerializeField]
    private Animator m_OpenRegisterAnim;
    [SerializeField]
    private Animator m_MoneyAnim;
    [SerializeField]
    private GameObject m_ReceiptButton;
    [SerializeField]
    private GameObject m_MoneyButton;
    [SerializeField]
    private GameObject m_LevelCompletedPage;
    [SerializeField]
    private GameObject m_FinishButton;
    [SerializeField]
    private AudioSource m_RegisterAudio;
    [SerializeField]
    private GameObject m_RegisterOpenSound;
    [SerializeField]
    private GameObject m_RegisterCloseSound;
    [SerializeField]
    private GameObject m_EnterButton;
    [SerializeField]
    private GameObject m_OpenRegisterButton;
    [SerializeField]
    private GameObject m_RegisterButton;
    [SerializeField]
    private GameObject m_Confetti;
    [SerializeField]
    private GameObject m_Stars;
    [SerializeField]
    private List<GameObject> m_Hints;
    [SerializeField]
    private SpriteRenderer m_MoneySpriteRenderer;
    [SerializeField]
    private GameObject m_MoneyParticles;
    [SerializeField]
    private Animator m_MoneyTextAnim;
    [SerializeField]
    private GameHandler m_GameHandler;

    private int m_HintCount;

    private bool m_CanShowHint;

    private Animator m_GirlAnim;

    private int m_GirlIndex;

    private bool m_RegisterClicked;
    private bool m_EnterClicked;
    private bool m_CanOpenRegister;
    private bool m_CanClickMoney;
    private bool m_CanClickReceipt;

    private bool m_DetectMoneySwipe;
    private bool m_DetectReceiptSwipe;

    private Vector3 m_MoneyFirstSwipePos;
    private Vector3 m_MoneySecondSwipePos;

    private Vector3 m_ReceiptFirstSwipePos;
    private Vector3 m_ReceiptSecondSwipePos;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();

        m_GirlIndex = PlayerPrefs.GetInt("Girl");
        m_GirlAnim = m_GirlAnims[m_GirlIndex];
        m_Table.SetActive(true);
        m_Girls[m_GirlIndex].SetActive(true);
        m_Dress = DontDestroy.Instance.GetDressRectTransform();
        //m_Dress.transform.SetParent(m_DressParents[m_GirlIndex].transform);
        DontDestroy.Instance.SetLayerOrder(10);
        m_Dress.anchoredPosition = m_DressPosition;
        m_Dress.transform.localScale = Vector3.one * 1.117945f;
        m_CanShowHint = true;
    }

    public void ShowHint()
    {
        if (m_HintCount < m_Hints.Count && m_CanShowHint)
        {
            m_Hints[m_HintCount].SetActive(true);
        }
    }

    private void Update()
    {
       if (Input.GetMouseButtonUp(0))
        {
            //if (m_DetectMoneySwipe)
            //{
            //    m_MoneySecondSwipePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_MoneySecondSwipePos.x - m_MoneyFirstSwipePos.x) > 0)
            //    {
            //        if (m_MoneySecondSwipePos.x > m_MoneyFirstSwipePos.x)
            //        {
            //            m_MoneyAnim.speed = 1;
            //            m_GirlAnim.speed = 1;
            //            m_CanClickReceipt = true;
            //            m_MoneyButton.SetActive(false);
            //            m_ReceiptButton.SetActive(true);
            //            m_CanClickMoney = false;
            //            m_DetectMoneySwipe = false;
            //        }
            //    }
            //}

            //if (m_DetectReceiptSwipe)
            //{
            //    m_ReceiptSecondSwipePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //    if (Mathf.Abs(m_ReceiptSecondSwipePos.x - m_ReceiptFirstSwipePos.x) > 0)
            //    {
            //        if (m_ReceiptSecondSwipePos.x < m_ReceiptFirstSwipePos.x)
            //        {
            //            m_HintCount++;
            //            m_ReceiptAnim.speed = 1;
            //            m_GirlAnim.speed = 1;
            //            m_CanClickReceipt = false;
            //        }
            //    }

            //    m_DetectReceiptSwipe = false;
            //}
        }
    }

    public void ClickRegister()
    {
        if (!m_RegisterClicked)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_CanShowHint = false;
            m_RegisterAudio.Play();
            m_RegisterAnim.enabled = true;
            m_GirlAnim.enabled = true;
            m_EnterButton.SetActive(true);
            m_RegisterButton.SetActive(false);
            m_RegisterClicked = true;
            StartCoroutine(IncreaseHintCount());
        }
    }

    IEnumerator IncreaseHintCount()
    {
        yield return new WaitForSeconds(7f);
        m_HintCount++;
        m_CanShowHint = true;
    }

    public void RegisterEnter()
    {
        if (m_RegisterAnim.speed == 0 && m_RegisterClicked && !m_EnterClicked)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_CanShowHint = false;
            m_RegisterAudio.Play();
            m_RegisterAnim.speed = 1;
            m_ReceiptAnim.enabled = true;
            m_GirlAnim.speed = 1;
            m_EnterClicked = true;
            m_EnterButton.SetActive(false);
        }
    }

    public void EnableMoney()
    {
        m_MoneyAnim.gameObject.SetActive(true);
    }

    public void EnableOpeningRegister()
    {
        m_CanShowHint = true;
        m_OpenRegisterButton.SetActive(true);
        m_CanOpenRegister = true;
    }

    public void EnableClickingMoney()
    {
        m_CanShowHint = true;
        m_CanClickMoney = true;
    }

    public void ContinueReceipt()
    {
        m_ReceiptAnim.speed = 1;
    }

    public void CloseRegister()
    {
        m_OpenRegisterAnim.SetTrigger("close");
        m_RegisterCloseSound.SetActive(true);
        m_ReceiptAnim.enabled = true;
    }

    public void OpenRegister()
    {
        if (m_CanOpenRegister)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            m_CanShowHint = false;
            m_RegisterOpenSound.SetActive(true);
            m_OpenRegisterAnim.enabled = true;
            m_GirlAnim.speed = 1;
            m_CanOpenRegister = false;
            m_OpenRegisterButton.SetActive(false);
        }
    }

    public void EnableReceipt()
    {
        m_CanClickReceipt = true;
        m_MoneyButton.SetActive(false);
        m_ReceiptButton.SetActive(true);
    }

    public void PauseRegisterSound()
    {
        m_RegisterAudio.Pause();
    }

    public void ClickReceipt()
    {
        if (m_CanClickReceipt)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_ReceiptFirstSwipePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_DetectReceiptSwipe = true;
        }
    }

    public void EndReceiptDrag()
    {
        if (m_DetectReceiptSwipe)
        {
            m_ReceiptSecondSwipePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(m_ReceiptSecondSwipePos.x - m_ReceiptFirstSwipePos.x) > 0)
            {
                if (m_ReceiptSecondSwipePos.x < m_ReceiptFirstSwipePos.x)
                {
                    m_HintCount++;
                    m_ReceiptAnim.speed = 1;
                    m_GirlAnim.speed = 1;
                    m_CanClickReceipt = false;
                    m_DetectReceiptSwipe = false;
                }
            }
        }
    }

    public void GetReceipt()
    {
        m_ReceiptAnim.speed = 1;
    }

    public void ClickMoney()
    {
        if (m_CanClickMoney)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_MoneyFirstSwipePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_DetectMoneySwipe = true;
        }
    }

    public void EndMoneyDrag()
    {
        if (m_DetectMoneySwipe)
        {
            m_MoneySecondSwipePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(m_MoneySecondSwipePos.x - m_MoneyFirstSwipePos.x) > 0)
            {
                if (m_MoneySecondSwipePos.x > m_MoneyFirstSwipePos.x)
                {
                    m_MoneySpriteRenderer.sortingOrder = 20;
                    m_Hints[m_HintCount].SetActive(false);
                    m_HintCount++;
                    m_MoneyAnim.speed = 1;
                    m_GirlAnim.speed = 1;
                    m_CanClickReceipt = true;
                    m_MoneyTextAnim.SetTrigger("pop");
                    
                    if (PlayerPrefs.GetInt("StartMoney") == 0)
                    {
                        MoneyHandler.Instance.AddMoney(200);
                        PlayerPrefs.SetInt("StartMoney", 1);
                    }

                    m_GameHandler.UpdateText();
                    m_MoneyParticles.SetActive(true);
                    m_MoneyButton.SetActive(false);
                    m_ReceiptButton.SetActive(true);
                    m_CanClickMoney = false;
                    m_DetectMoneySwipe = false;
                }
            }
        }
    }

    public IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(1f);
        m_Stars.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_LevelCompletedPage.SetActive(true);
        m_FinishButton.SetActive(true);
        m_Confetti.SetActive(true);
    }

    public void Finish()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
