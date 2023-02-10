using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Numbers;
    [SerializeField]
    private GameObject m_MetarObject;
    [SerializeField]
    private GameObject m_ChalkObject;
    [SerializeField]
    private GameObject m_ScissorsObject;
    [SerializeField]
    private AudioSource m_MetarSound;
    [SerializeField]
    private AudioSource m_ScissorsSound;

    private CutDrawSequenceHandler m_CutDrawSequenceHandler;

    public void ActivateNumber(int i_Number)
    {
        m_Numbers[i_Number].SetActive(true);
    }

    public void DeactivateNumber(int i_Number)
    {
        m_Numbers[i_Number].SetActive(false);
    }

    public void PlayMetarSound()
    {
        m_MetarSound.Play();
    }

    public void PlayScissors()
    {
        m_ScissorsSound.Play();
    }

    public void StopScissors()
    {
        m_ScissorsSound.Stop();
    }

    public void Metar()
    {
        m_MetarObject.SetActive(true);
        gameObject.SetActive(false);
        m_CutDrawSequenceHandler.MetarFinished();
    }

    public void Chalk()
    {
        m_ChalkObject.SetActive(true);
        m_CutDrawSequenceHandler.ChalkFinished();
    }

    public void Scissors()
    {
        m_ScissorsObject.SetActive(true);
        m_CutDrawSequenceHandler.ScissorsFinished();
    }

    public void Arrows()
    {
        m_CutDrawSequenceHandler.ArrowsFinished();
    }

    public void SetCutDrawSequenceHandler(CutDrawSequenceHandler i_CutDrawSequenceHandler)
    {
        m_CutDrawSequenceHandler = i_CutDrawSequenceHandler;
    }
}
