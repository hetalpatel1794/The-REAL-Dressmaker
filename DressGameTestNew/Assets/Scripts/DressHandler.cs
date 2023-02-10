using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressHandler : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> m_DressBlends;
    [SerializeField]
    private List<Sprite> m_DressParts1;
    [SerializeField]
    private List<Sprite> m_DressParts2;
    [SerializeField]
    private List<Sprite> m_DressParts3;
    [SerializeField]
    private List<Sprite> m_DressDetailsSprites;
    [SerializeField]
    private List<Sprite> m_ButtonsDetailsSprites;
    [SerializeField]
    private Image m_ButtonsDetails;
    [SerializeField]
    private List<GameObject> m_ButtonsPositions;

    [SerializeField]
    private Image m_DressBlend;
    [SerializeField]
    private Image m_DressPart1;
    [SerializeField]
    private Image m_DressPart2;
    [SerializeField]
    private Image m_DressPart3;
    [SerializeField]
    private Image m_DressDetails;
    [SerializeField]
    private Image m_DressLace;

    [SerializeField]
    private List<Sprite> m_LacesDress1;
    [SerializeField]
    private List<Sprite> m_LacesDress2;
    [SerializeField]
    private List<Sprite> m_LacesDress3;
    [SerializeField]
    private List<Sprite> m_LacesDress4;
    [SerializeField]
    private List<Sprite> m_LacesDress5;
    [SerializeField]
    private List<Sprite> m_LacesDress6;
    [SerializeField]
    private List<Sprite> m_LacesDress7;
    [SerializeField]
    private List<Sprite> m_LacesDress8;
    [SerializeField]
    private List<Sprite> m_LacesDress9;
    [SerializeField]
    private List<Sprite> m_LacesDress10;

    [SerializeField]
    private List<Sprite> m_Patterns;
    [SerializeField]
    private List<Image> m_PatternImages;
    [SerializeField]
    private List<Image> m_ColorImages;

    [SerializeField]
    private float m_DetailsDuration;

    [SerializeField]
    private Image m_Wrinkles;

    private int m_DressIndex;
    private int m_LaceIndex;
    private bool m_SetOpacity;

    private float t;
    private float m_StartValue;
    private float m_EndValue;

    private void Start()
    {
        m_DressIndex = PlayerPrefs.GetInt("Dress");

        m_Wrinkles.sprite = m_DressBlends[m_DressIndex];
        m_DressBlend.sprite = m_DressBlends[m_DressIndex];
        m_DressPart1.sprite = m_DressParts1[m_DressIndex];
        m_DressPart2.sprite = m_DressParts2[m_DressIndex];
        m_DressPart3.sprite = m_DressParts3[m_DressIndex];
        m_DressDetails.sprite = m_DressDetailsSprites[m_DressIndex];

        for (int i = 0; i < m_PatternImages.Count; i++)
        {
            m_PatternImages[i].sprite = m_Patterns[PlayerPrefs.GetInt("Pattern" + i)];

            if (PlayerPrefs.HasKey("Color" + i))
            {
                m_ColorImages[i].gameObject.SetActive(true);

                if (PlayerPrefs.GetInt("Pattern" + i) == 8)
                {
                    m_ColorImages[i].materialForRendering.SetFloat("_HsvBright", PlayerPrefs.GetFloat("Color" + i));
                }

                else
                {
                    m_ColorImages[i].materialForRendering.SetFloat("_HsvShift", PlayerPrefs.GetFloat("Color" + i));
                }
            }
        }

        m_StartValue = 0;
        m_EndValue = 1;
    }

    private void Update()
    {
        if (m_SetOpacity)
        {
            t += Time.deltaTime / m_DetailsDuration;
            Color m_Col = m_DressDetails.color;
            m_Col.a = Mathf.Lerp(m_StartValue, m_EndValue, t);
            m_DressDetails.color = m_Col;
        }
    }

    public void ResetDetails()
    {
        t = 0;
        m_SetOpacity = false;
        Color m_Temp = m_DressDetails.color;
        m_Temp.a = 0;
        m_DressDetails.color = m_Temp;
    }

    public bool DetailsFinished()
    {
        return m_DressDetails.color.a == 1;
    }

    public void SetEndDetails()
    {
        Color m_Col = m_DressDetails.color;
        m_Col.a = 1;
        m_DressDetails.color = m_Col;
    }

    public void SetDressDetails()
    {
        m_DressDetails.sprite = m_DressDetailsSprites[m_DressIndex];
    }

    public void SetLace(int i_LaceIndex)
    {
        m_LaceIndex = i_LaceIndex;
        SetDressLace();
    }

    public void SetButtonsDetails()
    {
        m_ButtonsDetails.sprite = m_ButtonsDetailsSprites[m_DressIndex];
        m_ButtonsDetails.gameObject.SetActive(true);
        m_ButtonsPositions[m_DressIndex].SetActive(true);
    }

    public void RemoveButtonDetails()
    {
        m_ButtonsDetails.gameObject.SetActive(false);
        m_ButtonsPositions[m_DressIndex].SetActive(false);
    }

    private void SetDressLace()
    {
        switch(m_DressIndex)
        {
            case 0: m_DressLace.sprite = m_LacesDress1[m_LaceIndex]; break;
            case 1: m_DressLace.sprite = m_LacesDress2[m_LaceIndex]; break;
            case 2: m_DressLace.sprite = m_LacesDress3[m_LaceIndex]; break;
            case 3: m_DressLace.sprite = m_LacesDress4[m_LaceIndex]; break;
            case 4: m_DressLace.sprite = m_LacesDress5[m_LaceIndex]; break;
            case 5: m_DressLace.sprite = m_LacesDress6[m_LaceIndex]; break;
            case 6: m_DressLace.sprite = m_LacesDress7[m_LaceIndex]; break;
            case 7: m_DressLace.sprite = m_LacesDress8[m_LaceIndex]; break;
            case 8: m_DressLace.sprite = m_LacesDress9[m_LaceIndex]; break;
            case 9: m_DressLace.sprite = m_LacesDress10[m_LaceIndex]; break;
        }

        m_DressLace.gameObject.SetActive(true);
    }

    public void SetDetails()
    {
        m_SetOpacity = true;
    }

    public void StopDetails()
    {
        m_SetOpacity = false;
    }
}
