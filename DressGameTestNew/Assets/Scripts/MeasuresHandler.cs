using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MeasuresHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Girls;
    [SerializeField]
    private GameObject m_LevelFinishedPage;
    [SerializeField]
    private GameObject m_NotebookPencilButton;
    [SerializeField]
    private GameObject m_NextPartButton;
    [SerializeField]
    private GameObject m_Effects;
    [SerializeField]
    private List<Animator> m_GirlAnims;
    [SerializeField]
    private List<AudioSource> m_AudioSources;
    [SerializeField]
    private Animator m_NotebookAnim;
    [SerializeField]
    private Animator m_PencilAnim;
    [SerializeField]
    private Animator m_MeasuringTapeAnim;
    [SerializeField]
    private Animator m_LettersAnim;
    [SerializeField]
    private GraphicRaycaster m_Raycaster;
    [SerializeField]
    private EventSystem m_EventSystem;
    [SerializeField]
    private GameObject m_Pencil;
    [SerializeField]
    private GameObject m_Stars;
    [SerializeField]
    private List<GameObject> m_Hints;

    private int m_HintCount;

    private PointerEventData m_PointerEventData;

    private Vector3 m_Offset;

    private string m_MeasurePlace;

    private bool m_CanMoveTape;
    private bool m_MoveTape;

    private int m_GirlAnimCount;
    private int m_PencilAnimCount;
    private int m_MeasuringTapeCount;
    private int m_LetterAnimCount;

    private int m_Index;
    private bool m_NotepadOpened;
    private bool m_CanClickPencil;

    private bool m_CanIncreaseHintCount;

    private string m_ObjectToClick;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();
        m_Index = PlayerPrefs.GetInt("Girl");

        m_Girls[m_Index].SetActive(true);
        m_CanIncreaseHintCount = true;
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
        if (m_MoveTape)
        {
            m_MeasuringTapeAnim.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;
        }
    }

    public void ObjectClicked(int i_ObjectIndex)
    {
        switch(i_ObjectIndex)
        {
            case 0: NotepadClicked(); break;
            case 1: PencilClicked(); break;
            case 2: MeasuringTapeClicked(); break;
            case 3: PencilNotepadClicked(); break;
        }
    }

    public void MoveTape()
    {
        if (m_CanMoveTape)
        {
            m_Hints[m_HintCount].SetActive(false);
            if (m_CanIncreaseHintCount)
            {
                m_HintCount++;
                m_CanIncreaseHintCount = false;
            }
            m_Offset = m_MeasuringTapeAnim.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_MoveTape = true;
        }
    }

    public void StopMovingTape()
    {
        m_MoveTape = false;

        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(m_PointerEventData, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.name == m_MeasurePlace)
            {
                m_CanIncreaseHintCount = true;
                MeasuringTapeClicked();
                m_MoveTape = false;
                m_CanMoveTape = false;
            }
        }
    }

    public void PauseAnimation()
    {
        //m_GirlAnims[m_Index].speed = 0;

        switch(m_GirlAnimCount)
        {
            case 0:
                m_CanClickPencil = true;
                break;
            case 1:
                m_ObjectToClick = "MeasuringTape";
                m_MeasurePlace = "Shoulders";
                m_CanMoveTape = true;
                break;
            case 2:
                m_ObjectToClick = "PencilNotebook";
                PlayerPrefs.SetInt("MeasuringMoney", 5);
                break;
            case 3:
                m_ObjectToClick = "MeasuringTape";
                m_MeasurePlace = "Chest";
                m_CanMoveTape = true;
                break;
            case 4:
                m_ObjectToClick = "PencilNotebook";
                PlayerPrefs.SetInt("MeasuringMoney", 10);
                break;
            case 5:
                m_ObjectToClick = "MeasuringTape";
                m_MeasurePlace = "Waist";
                m_CanMoveTape = true;
                break;
            case 6:
                m_ObjectToClick = "PencilNotebook";
                PlayerPrefs.SetInt("MeasuringMoney", 15);
                break;
            case 7:
                m_ObjectToClick = "MeasuringTape";
                m_MeasurePlace = "Hips";
                m_CanMoveTape = true;
                break;
            case 8:
                m_ObjectToClick = "PencilNotebook";
                PlayerPrefs.SetInt("MeasuringMoney", 20);
                break;
            case 9:
                m_ObjectToClick = "MeasuringTape";
                m_MeasurePlace = "Arm";
                m_CanMoveTape = true;
                break;
            case 10:
                m_ObjectToClick = "PencilNotebook";
                PlayerPrefs.SetInt("MeasuringMoney", 25);
                break;
            case 11:
                m_ObjectToClick = "MeasuringTape";
                m_MeasurePlace = "Height";
                m_CanMoveTape = true;
                break;
            case 12:
                m_ObjectToClick = "PencilNotebook";
                PlayerPrefs.SetInt("MeasuringMoney", 30);
                break;
            case 13:
                m_GirlAnims[m_Index].SetTrigger(m_GirlAnimCount.ToString());
                StartCoroutine(MeasuresFinished());
                break;
        }
    }

    private void NotepadClicked()
    {
        if (!m_NotepadOpened)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            //m_AudioSources[m_Index].Play();
            m_GirlAnims[m_Index].enabled = true;
            m_NotebookAnim.enabled = true;
            m_NotebookPencilButton.SetActive(true);
            m_NotepadOpened = true;
        }
    }

    private void PencilClicked()
    {
        if (m_CanClickPencil)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            //m_AudioSources[m_Index].Play();
            m_GirlAnims[m_Index].SetTrigger(m_GirlAnimCount.ToString());
            m_PencilAnim.enabled = true;
            m_GirlAnimCount++;
            m_CanClickPencil = false;
            m_Pencil.SetActive(false);
        }
    }

    private void MeasuringTapeClicked()
    {
        //if (m_ObjectToClick == "MeasuringTape")
        //{
        //    //m_AudioSources[m_Index].Play();
        //    //m_GirlAnims[m_Index].speed = 1f;
        //    m_GirlAnims[m_Index].SetTrigger(m_GirlAnimCount.ToString());
        //    m_MeasuringTapeAnim.SetTrigger(m_MeasuringTapeCount.ToString());
        //    m_ObjectToClick = "";
        //    m_GirlAnimCount++;
        //    m_MeasuringTapeCount++;
        //}

        m_GirlAnims[m_Index].SetTrigger(m_GirlAnimCount.ToString());
        m_MeasuringTapeAnim.enabled = true;
        m_MeasuringTapeAnim.SetTrigger(m_MeasuringTapeCount.ToString());
        m_ObjectToClick = "";
        m_GirlAnimCount++;
        m_MeasuringTapeCount++;
    }

    private void PencilNotepadClicked()
    {
        if (m_ObjectToClick == "PencilNotebook")
        {
            m_Hints[m_HintCount].SetActive(false);
            m_HintCount++;
            //m_AudioSources[m_Index].Play();
            m_GirlAnims[m_Index].SetTrigger(m_GirlAnimCount.ToString());
            m_PencilAnim.SetTrigger(m_PencilAnimCount.ToString());
            m_LettersAnim.SetTrigger(m_LetterAnimCount.ToString());
            m_ObjectToClick = "";
            m_GirlAnimCount++;
            m_PencilAnimCount++;
            m_LetterAnimCount++;
        }
    }

    public IEnumerator MeasuresFinished()
    {
        yield return new WaitForSeconds(1.5f);
        m_Stars.SetActive(true);
        yield return new WaitForSeconds(1);
        m_Effects.SetActive(true);
        m_LevelFinishedPage.SetActive(true);
        m_NextPartButton.SetActive(true);
    }

    public void PlayNextPart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
