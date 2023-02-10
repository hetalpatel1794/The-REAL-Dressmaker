using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
public class PhotoshootHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Girls;
    [SerializeField]
    private List<Animator> m_GirlsAnims;
    [SerializeField]
    private Animator m_CameraAnim;
    [SerializeField]
    private Vector2 m_DressPosition;
    [SerializeField]
    private GameObject m_EndPage;
    [SerializeField]
    private List<GameObject> m_EndGirls;
    [SerializeField]
    private Vector2 m_EndDressPosition;
    [SerializeField]
    private GameObject m_CameraButton;
    [SerializeField]
    private CaptureAndSave m_CaptureAndSave;
    [SerializeField]
    private List<GameObject> m_EndGirlsScreenshot;
    [SerializeField]
    private GameObject m_EndPageScreenshot;
    [SerializeField]
    private GameObject m_Disable;
    [SerializeField]
    private Animator m_SuccessAnim;
    [SerializeField]
    private GameObject m_End;
    [SerializeField]
    private GameObject m_Share;
    [SerializeField]
    private GameObject m_FindUs;
    [SerializeField]
    private GameObject m_Meni;

    private bool m_SavedPhoto;

    private Animator m_GirlAnim;

    private int m_GirlIndex;

    private bool m_ClickCamera;

    private int m_CameraCount;

    private void OnEnable()
    {
        CaptureAndSaveEventListener.onSuccess += OnSuccess;
    }
    private void OnDisable()
    {
        CaptureAndSaveEventListener.onSuccess -= OnSuccess;
    }

    private void Start()
    {
        DontDestroy.Instance.GetDressCanvas().layer = 0;
        DontDestroy.Instance.GetDressRectTransform().anchoredPosition = m_DressPosition;
        m_GirlIndex = PlayerPrefs.GetInt("Girl");
        DontDestroy.Instance.SetLayerOrder(5);
        DontDestroy.Instance.GetDressRectTransform().gameObject.transform.localScale = Vector3.one * 1.244145f;
        m_Girls[m_GirlIndex].SetActive(true);
        m_GirlAnim = m_GirlsAnims[m_GirlIndex];

        m_CameraButton.SetActive(true);
        m_ClickCamera = true;
    }

    public void ClickCamera()
    {
        if (m_ClickCamera && m_CameraCount < 3)
        {
            m_CameraAnim.SetTrigger("shoot");
            m_CameraCount++;
            StartCoroutine(ContinueGirlAnimations());
            m_CameraButton.SetActive(false);

            if (m_CameraCount == 3)
            {
                StartCoroutine(End());
            }
        }
    }

    IEnumerator ContinueGirlAnimations()
    {
        yield return new WaitForSeconds(2.5f);
        if (m_GirlAnim.enabled)
        {
            m_GirlAnim.speed = 1;
        }

        else
            m_GirlAnim.enabled = true;
        yield return new WaitForSeconds(1.2f);
        if (m_CameraCount < 3)
        {
            m_ClickCamera = true;
            m_CameraButton.SetActive(true);
        }
    }

    public void ExitPhotoshoot()
    {
        m_End.SetActive(true);
        if (!m_SavedPhoto)
        {
            m_Share.SetActive(false);
            m_FindUs.SetActive(true);
        }
        m_EndPage.SetActive(false);
        //m_EndPage.SetActive(false);
        //m_EndPageScreenshot.SetActive(false);
        //m_EndGirls[m_GirlIndex].SetActive(false);
        //DontDestroy.Instance.GetDress().gameObject.SetActive(false);
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=9119390799782543305");
    }

    public void ColoringBook()
    {
        Application.OpenURL("https://dashboard.mailerlite.com/forms/83577/58803159229793691/share");
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(3f);
        SkipEnd();
    }

    public void SkipEnd()
    {
        m_Meni.SetActive(false);
        m_EndPage.SetActive(true);
        m_EndPageScreenshot.SetActive(true);
        m_Disable.SetActive(false);
        m_EndGirls[m_GirlIndex].SetActive(true);
        DontDestroy.Instance.ChangeLayer();
        DontDestroy.Instance.SetLayerOrder(20);
        DontDestroy.Instance.GetDressRectTransform().anchoredPosition = m_EndDressPosition;
        DontDestroy.Instance.GetDressRectTransform().gameObject.transform.localScale = Vector3.one * 2.35f;
    }

    public void SavePhoto()
    {
        m_CaptureAndSave.CaptureAndSaveToAlbum(Screen.width * 2, Screen.height * 2,Camera.main,ImageType.JPG);
    }

    public void OnSuccess(string msg)
    {
        m_SavedPhoto = true;
        m_SuccessAnim.SetTrigger("pop");
    }

    public void Share()
    {
        Application.OpenURL("https://www.noiesnoise.com/dressmaker_competition");
    }

    public void FindUs()
    {
        Application.OpenURL("https://www.noiesnoise.com/links");
    }

    public void Exit()
    {
        Destroy(DontDestroy.Instance.gameObject);
        Application.Quit();
    }

    public void PlayAgain()
    {
        Destroy(DontDestroy.Instance.gameObject);
        SceneManager.LoadScene(1);
    }
}
