using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerHandler : MonoBehaviour
{
    [SerializeField]
    private RectTransform m_CircleIndicator;
    [SerializeField]
    private MaterialSelectionHandler m_MaterialSelectionHandler;

    private Vector2 m_Offset;
    private Vector2 m_NewPos;

    private bool m_ObjectClicked;

    private float m_Value;
    private float m_Hue;

    private int m_ColorCount;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.DeleteKey("Color" + i);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Offset = m_CircleIndicator.anchoredPosition / 100 - new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        }

        if (Input.GetMouseButton(0) && m_ObjectClicked)
        {
            m_NewPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) + m_Offset;
            m_CircleIndicator.anchoredPosition = new Vector2(Mathf.Clamp(m_NewPos.x * 100, -150, 150), 0);
            m_Value = (m_CircleIndicator.anchoredPosition.x + 150) * 360 / 300;
            //m_Hue = m_Value / 100;
            m_Hue = m_Value;
            Debug.Log(m_Value);
            //m_MaterialSelectionHandler.SetColor(Color.HSVToRGB(m_Hue, 0.5f, 1f, true));
            m_MaterialSelectionHandler.SetColor(m_Hue);
            //PlayerPrefs.SetFloat("Color" + m_ColorCount, m_Hue);
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_ObjectClicked = false;
        }
    }

    public void ObjectClicked()
    {
        m_ObjectClicked = true;
        m_MaterialSelectionHandler.EnableColor();
    }
    public void ResetColor()
    {
        m_CircleIndicator.anchoredPosition = Vector2.zero;
        m_ColorCount++;
    }
    public bool IsPickingColor()
    {
        return m_ObjectClicked;
    }
}
