using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CutDrawPartHandler : MonoBehaviour
{
    [SerializeField]
    private int m_PartIndex;

    [SerializeField]
    private Animator m_Metar;
    [SerializeField]
    private Animator m_Arrows;
    [SerializeField]
    private Animator m_Chalk;
    [SerializeField]
    private Animator m_Scissors;
    [SerializeField]
    private GameObject m_Triggers;
    [SerializeField]
    private ScissorsHandler m_ScissorsHandler;

    [SerializeField]
    private List<NumbersHandler> m_NumbersHandlers;
    [SerializeField]
    private List<TextMeshProUGUI> m_Numbers;
    [SerializeField]
    private GameObject m_ChalkOutline;

    private Image m_ChalkOutlineImage;

    private void Start()
    {
        m_ChalkOutlineImage = m_ChalkOutline.GetComponent<Image>();
    }

    public int GetPartIndex()
    {
        return m_PartIndex;
    }

    public Animator GetMetar()
    {
        return m_Metar;
    }

    public Animator GetArrows()
    {
        return m_Arrows;
    }

    public Animator GetChalk()
    {
        return m_Chalk;
    }

    public Animator GetScissors()
    {
        return m_Scissors;
    }

    public void SetCutDrawSequenceHandler(CutDrawSequenceHandler i_CutDrawSequenceHandler)
    {
        for (int i = 0; i < m_NumbersHandlers.Count; i++)
        {
            m_NumbersHandlers[i].SetCutDrawSequenceHandler(i_CutDrawSequenceHandler);
        }

        //m_Triggers.SetActive(true);
    }

    public void EnableTriggers()
    {
        m_Triggers.SetActive(true);
    }

    public void DisableTriggers()
    {
        m_Triggers.SetActive(false);
    }

    public void ScissorsSelected()
    {
        m_ScissorsHandler.ScissorsSelected();
    }

    public void SetNumbersColor(Color i_Color)
    {
        for (int i = 0; i < m_Numbers.Count; i++)
        {
            m_Numbers[i].color = i_Color;
        }
    }

    public GameObject GetChalkOutline()
    {
        return m_ChalkOutline;
    }

    public void SetChalkOutlineColor(Color i_Color)
    {
        m_ChalkOutlineImage.color = i_Color;
    }
}
