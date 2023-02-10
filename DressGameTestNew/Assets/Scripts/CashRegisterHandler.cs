using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class CashRegisterHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Buttons;
    [SerializeField]
    private TextMeshProUGUI m_TopText;
    [SerializeField]
    private TextMeshProUGUI m_BottomText;
    [SerializeField]
    private AudioSource m_BeepSound;
    [SerializeField]
    private AudioSource m_RegisterOpen;
    [SerializeField]
    private Animator m_Anim;
    [SerializeField]
    private Sprite m_Register;
    [SerializeField]
    private Image m_RegisterImage;

    private string m_OldValue;
    private string m_NewValue;

    private int m_NumberCount;

    private bool m_RegisterOpened;

    private float m_PreviousValue;
    private float m_SumValue;

    private string m_Operation;

    private void Start()
    {
        m_SumValue = 0;
        m_Operation = "";
    }

    public void PressButton(int i_ButtonIndex)
    {
        m_BeepSound.Play();
        m_Buttons[i_ButtonIndex].SetActive(false);
        StartCoroutine(ReleaseButton(i_ButtonIndex));
    }

    IEnumerator ReleaseButton(int i_ButtonIndex)
    {
        yield return new WaitForSeconds(0.25f);
        m_Buttons[i_ButtonIndex].SetActive(true);
    }

    public void EnterValue(string i_Value)
    {
        if (m_NumberCount < 5)
        {
            m_NewValue = m_OldValue + i_Value;
            m_BottomText.text = m_NewValue;
            m_OldValue = m_NewValue;
            m_PreviousValue = int.Parse(m_OldValue);

            if (i_Value == "00")
            {
                m_NumberCount += 2;
            }
            
            else
                m_NumberCount++;
        }
    }

    public void ClearValues()
    {
        m_NewValue = "";
        m_OldValue = "";
        m_TopText.text = "0";
        m_BottomText.text = "0";
        m_NumberCount = 0;
    }

    public void ClearAll()
    {
        m_NewValue = "";
        m_OldValue = "";
        m_TopText.text = "0";
        m_BottomText.text = "0";
        m_NumberCount = 0;
        m_Operation = "";
        m_SumValue = 0;
        m_PreviousValue = 0;
    }

    public void PutTopValue()
    {
        //if (m_PreviousValue != 0)
        //{
        //    m_TopText.text = m_PreviousValue.ToString();
        //}

        m_TopText.text = m_BottomText.text;

        if (!m_RegisterOpened)
        {
            m_RegisterOpen.Play();
            m_Anim.enabled = true;
            m_Anim.Play("Register", 0, 0);
            StartCoroutine(ResetRegister());
            m_RegisterOpened = true;
        }
    }

    IEnumerator ResetRegister()
    {
        yield return new WaitForSeconds(2f);
        m_RegisterOpened = false;
        m_Anim.enabled = false;
        m_RegisterImage.sprite = m_Register;
    }

    public void Add()
    {
        Calculate();
        ClearValues();
        m_Operation = "Add";
        m_BottomText.text = "+";
    }

    public void Subtract()
    {
        Calculate();
        ClearValues();
        m_Operation = "Subtract";
        m_BottomText.text = "-";
    }

    public void Multiply()
    {
        Calculate();
        ClearValues();
        m_Operation = "Multiply";
        m_BottomText.text = "x";
    }

    public void ShowSum()
    {
        Calculate();
        m_BottomText.text = m_SumValue.ToString();
        m_Operation = "";
        m_PreviousValue = m_SumValue;
        m_OldValue = m_PreviousValue.ToString();
        m_SumValue = 0;
    }

    private void Calculate()
    {
        switch (m_Operation)
        {
            case "": m_SumValue = m_PreviousValue; break;
            case "Add": m_SumValue += m_PreviousValue; break;
            case "Subtract": m_SumValue -= m_PreviousValue; break;
            case "Multiply": m_SumValue *= m_PreviousValue; break;
        }
    }
}
