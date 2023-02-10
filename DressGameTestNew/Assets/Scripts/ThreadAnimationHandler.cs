using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadAnimationHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Thread;

    public void SwitchThreads()
    {
        m_Thread.SetActive(true);
        gameObject.SetActive(false);
    }
}
