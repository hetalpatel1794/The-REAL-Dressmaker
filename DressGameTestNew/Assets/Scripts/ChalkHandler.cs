using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChalkHandler : MonoBehaviour
{
    [SerializeField]
    private Animator m_Anim;

    private int m_Index;
    private bool m_AnimPaused;

    public void PauseAnim()
    {
        m_Anim.speed = 0;
        m_AnimPaused = true;
    }

    public void ContinueAnim(int i_Index)
    {
        if (i_Index == m_Index && m_AnimPaused && m_Anim.speed == 0)
        {
            m_Anim.speed = 1;
            m_AnimPaused = false;
            StartCoroutine(IncreaseCount());
        }
    }

    IEnumerator IncreaseCount()
    {
        yield return new WaitForSeconds(0.15f);
        m_Index++;
    }
}
