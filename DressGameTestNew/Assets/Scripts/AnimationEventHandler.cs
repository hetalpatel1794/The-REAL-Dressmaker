using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField]
    private SewingHandler m_SewingHandler;
    [SerializeField]
    private bool m_CanCallFunction;

    public void SewingFinished()
    {
        if (m_CanCallFunction)
        {
            m_SewingHandler.SewingFinished();
            m_CanCallFunction = false;
        }
    }
}
