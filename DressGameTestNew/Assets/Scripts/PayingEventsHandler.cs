using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayingEventsHandler : MonoBehaviour
{
    [SerializeField]
    private PayingHandler m_PayingHandler;

    public void EnableMoney()
    {
        m_PayingHandler.EnableMoney();
    }

    public void EnableOpeningRegister()
    {
        m_PayingHandler.EnableOpeningRegister();
    }

    public void EnableClickingMoney()
    {
        m_PayingHandler.EnableClickingMoney();
    }

    public void CloseRegister()
    {
        m_PayingHandler.CloseRegister();
    }

    public void CanClickReceipt()
    {
        //m_PayingHandler.EnableReceipt();
    }

    public void ContinueReceipt()
    {
        //m_PayingHandler.ContinueReceipt();
    }

    public void PauseRegisterSound()
    {
        m_PayingHandler.PauseRegisterSound();
    }

    public void GetReceipt()
    {
        m_PayingHandler.GetReceipt();
    }

    public void GameEnd()
    {
        StartCoroutine(m_PayingHandler.GameEnd());
    }
}
