using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CutDrawSequenceHandler : MonoBehaviour
{
    [SerializeField]
    private Animator m_Metar;
    [SerializeField]
    private Animator m_Arrows;
    [SerializeField]
    private Animator m_Chalk;
    [SerializeField]
    private Animator m_Scissors;
    [SerializeField]
    private GameObject m_MetarObject;
    [SerializeField]
    private GameObject m_ChalkObject;
    [SerializeField]
    private GameObject m_ScissorsObject;
    [SerializeField]
    private int m_MaterialIndex;
    [SerializeField]
    private List<CutDrawPartHandler> m_CutDrawPartsHandler;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private MaterialDrawHandler m_MaterialDrawHandler;
    [SerializeField]
    private EventSystem m_EventSystem;
    [SerializeField]
    private int m_MatIndex;
    [SerializeField]
    private Image m_ChalkImage;
    [SerializeField]
    private Color m_ChalkColor;
    [SerializeField]
    private List<GameObject> m_Hints;
    [SerializeField]
    private Color m_NumbersColor;
    [SerializeField]
    private int m_Index;

    private int m_HintCount;

    private PointerEventData m_PointerEventData;

    private CutDrawPartHandler m_CutDrawPartHandler;

    private bool m_MetarClicked;
    private bool m_CanClickChalk;
    private bool m_CanClickScissors;
    private bool m_Move;

    private bool m_MoveMetar;
    private bool m_MoveChalk;
    private bool m_MoveScissors;

    private bool m_MetarFirstClick;
    private bool m_ChalkFirstClick;
    private bool m_ScissorsFirstClick;

    private Vector3 m_Offset;
    private Vector3 m_StartMetarPosition;
    private Vector3 m_StarChalkPosition;
    private Vector3 m_StartScissorsPosition;

    private int m_DressIndex;
    private int m_PartIndex;

    private void Start()
    {
        m_DressIndex = PlayerPrefs.GetInt("Dress");
        m_StartMetarPosition = m_MetarObject.transform.position;
        m_StarChalkPosition = m_ChalkObject.transform.position;
        m_StartScissorsPosition = m_ScissorsObject.transform.position;
        m_ChalkObject.GetComponent<Image>().color = Color.white;

        Setup();
    }

    private void Update()
    {
        if (m_Move)
        {
            transform.Translate(Vector2.right * Time.deltaTime * m_Speed);
        }

        if (m_MoveMetar)
        {
            m_MetarObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;
        }

        if (m_MoveChalk)
        {
            m_ChalkObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;
        }

        if (m_MoveScissors)
        {
            m_ScissorsObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;
        }
    }

    public void ClickMetar()
    {
        if (!m_MetarClicked)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_Offset = m_MetarObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_MoveMetar = true;
            m_MetarFirstClick = true;
            //m_MetarObject.SetActive(false);
            //m_Metar.gameObject.SetActive(true);
            //m_MetarClicked = true;
        }
    }

    public void ShowHint()
    {
        m_Hints[m_HintCount].SetActive(true);
    }

    public void ReleaseMetar()
    {
        if (m_MetarFirstClick)
        {
            m_MoveMetar = false;

            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(m_PointerEventData, results);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.name == "MetarTrigger")
                {
                    m_MetarClicked = true;
                    m_MetarObject.SetActive(false);
                    m_Metar.gameObject.SetActive(true);
                    m_MetarObject.transform.position = m_StartMetarPosition;
                    m_MetarFirstClick = false;
                    m_MaterialDrawHandler.DisableMetarTrigger();
                }
            }
        }
    }

    public void MetarFinished()
    {
        m_HintCount++;
        m_Arrows.gameObject.SetActive(true);
        m_MaterialDrawHandler.EnableChalkTrigger();
    }

    public void ArrowsFinished()
    {
        m_CanClickChalk = true;
        m_CutDrawPartHandler.GetChalkOutline().SetActive(true);
    }

    public void ClickChalk()
    {
        if (m_CanClickChalk)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_Offset = m_ChalkObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_MoveChalk = true;
            m_ChalkFirstClick = true;
            //m_ChalkObject.SetActive(false);
            //m_Chalk.gameObject.SetActive(true);
            //m_CanClickChalk = false;
        }
    }

    public void ReleaseChalk()
    {
        if (m_ChalkFirstClick)
        {
            m_MoveChalk = false;

            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(m_PointerEventData, results);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.name == "ChalkTrigger")
                {
                    m_CutDrawPartHandler.EnableTriggers();
                    m_CanClickChalk = false;
                    m_ChalkObject.SetActive(false);
                    m_Chalk.gameObject.SetActive(true);
                    m_ChalkObject.transform.position = m_StarChalkPosition;
                    m_ChalkFirstClick = false;
                    m_MaterialDrawHandler.DisableChalkTrigger();
                }
            }
        }
    }

    public void ChalkFinished()
    {
        m_HintCount++;
        m_CanClickScissors = true;
        m_CutDrawPartHandler.GetChalkOutline().SetActive(false);
        m_MaterialDrawHandler.EnableScissorsTrigger();
        switch(m_Index)
        {
            case 0: PlayerPrefs.SetInt("DrawingMoney", 10); break;
            case 1: PlayerPrefs.SetInt("DrawingMoney", 20); break;
            case 2: PlayerPrefs.SetInt("DrawingMoney", 30); break;
        }
    }

    public void ClickScissors()
    {
        if (m_CanClickScissors)
        {
            m_Hints[m_HintCount].SetActive(false);
            m_Offset = m_ScissorsObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_MoveScissors = true;
            m_ScissorsFirstClick = true;
            //m_ScissorsObject.SetActive(false);
            //m_Scissors.gameObject.SetActive(true);
            //m_CanClickScissors = false;
        }
    }

    public void ReleaseScissors()
    {
        if (m_ScissorsFirstClick)
        {
            m_MoveScissors = false;

            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(m_PointerEventData, results);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.name == "ScissorsTrigger")
                {
                    m_CanClickScissors = false;
                    m_ScissorsObject.SetActive(false);
                    m_Scissors.gameObject.SetActive(true);
                    m_ScissorsObject.transform.position = m_StartScissorsPosition;
                    m_CutDrawPartHandler.ScissorsSelected();
                    m_ScissorsFirstClick = false;
                    m_MaterialDrawHandler.DisableScissorsTrigger();
                }
            }
        }
    }

    public void ScissorsFinished()
    {
        m_HintCount++;
        m_MaterialDrawHandler.IncreaseHintCount();
        m_CutDrawPartHandler.transform.SetParent(transform);
        m_Move = true;
        m_CutDrawPartHandler.DisableTriggers();
        switch (m_Index)
        {
            case 0: PlayerPrefs.SetInt("CuttingMoney", 10); break;
            case 1: PlayerPrefs.SetInt("CuttingMoney", 20); break;
            case 2: PlayerPrefs.SetInt("CuttingMoney", 30); break;
        }
        StartCoroutine(MaterialMoved());
    }

    IEnumerator MaterialMoved()
    {
        yield return new WaitForSeconds(1f);
        m_Move = false;
        m_MaterialDrawHandler.MaterialFinished();
        this.enabled = false;
    }

    private void Setup()
    {
        switch(m_DressIndex)
        {
            case 0:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 0; break;
                    case 2: m_PartIndex = 1; break;
                    case 3: m_PartIndex = 16; break;
                }
                break;
            case 1:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 1; break;
                    case 2: m_PartIndex = 2; break;
                    case 3: m_PartIndex = 3; break;
                }
                break;
            case 2:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 9; break;
                    case 2: m_PartIndex = 4; break;
                    case 3: m_PartIndex = 5; break;
                }
                break;
            case 3:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 6; break;
                    case 2: m_PartIndex = 7; break;
                    case 3: m_PartIndex = 8; break;
                }
                break;
            case 4:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 6; break;
                    case 2: m_PartIndex = 9; break;
                    case 3: m_PartIndex = 10; break;
                }
                break;
            case 5:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 9; break;
                    case 2: m_PartIndex = 10; break;
                    case 3: m_PartIndex = 4; break;
                }
                break;
            case 6:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 11; break;
                    case 2: m_PartIndex = 0; break;
                    case 3: m_PartIndex = 10; break;
                }
                break;
            case 7:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 12; break;
                    case 2: m_PartIndex = 3; break;
                    case 3: m_PartIndex = 9; break;
                }
                break;
            case 8:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 6; break;
                    case 2: m_PartIndex = 13; break;
                    case 3: m_PartIndex = 10; break;
                }
                break;
            case 9:
                switch (m_MaterialIndex)
                {
                    case 1: m_PartIndex = 14; break;
                    case 2: m_PartIndex = 15; break;
                    case 3: m_PartIndex = 0; break;
                }
                break;
        }

        for (int i = 0; i < m_CutDrawPartsHandler.Count; i++)
        {
            if (m_CutDrawPartsHandler[i].GetPartIndex() == m_PartIndex)
            {
                m_CutDrawPartHandler = m_CutDrawPartsHandler[i];
            }
        }

        m_CutDrawPartHandler.gameObject.SetActive(true);
        m_CutDrawPartHandler.SetCutDrawSequenceHandler(this);

        m_Metar = m_CutDrawPartHandler.GetMetar();
        m_Arrows = m_CutDrawPartHandler.GetArrows();
        m_Chalk = m_CutDrawPartHandler.GetChalk();
        m_Scissors = m_CutDrawPartHandler.GetScissors();

        m_ChalkImage = m_Chalk.GetComponent<Image>();

        if (PlayerPrefs.GetInt("Pattern" + m_MatIndex) == 8)
        {
            m_ChalkImage.color = m_ChalkColor;
            m_CutDrawPartHandler.SetNumbersColor(m_NumbersColor);
            m_Arrows.GetComponent<Image>().color = m_ChalkColor;
            m_ChalkObject.GetComponent<Image>().color = m_ChalkColor;
            m_CutDrawPartHandler.SetChalkOutlineColor(m_ChalkColor);
        }
    }
}
