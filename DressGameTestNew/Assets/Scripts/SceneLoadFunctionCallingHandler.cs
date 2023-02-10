using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadFunctionCallingHandler : MonoBehaviour
{
    [SerializeField]
    private bool m_CallButtonsFunction;
    [SerializeField]
    private ButtonsHandler m_ButtonsHandler;
    [SerializeField]
    private NeedleSewingHandler m_NeedleSewingHandler;
    [SerializeField]
    private bool m_CallNeedleSewingFunction;

    public void SetButtonsHandler(ButtonsHandler i_ButtonsHandler)
    {
        m_ButtonsHandler = i_ButtonsHandler;
    }
    public void CallFunction()
    {
        if (m_CallButtonsFunction)
        {
            m_ButtonsHandler.SetParent();
        }

        if (m_CallNeedleSewingFunction)
        {
            m_NeedleSewingHandler.SetButtonsAndDetails();
        }
    }
}
