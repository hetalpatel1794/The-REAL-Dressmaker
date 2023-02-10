using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityHandler : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

    private float m_OpacityValue;
    private float m_NewOpacityValue;
    private float m_OldOpacityValue;
    private float t;

    private void Update()
    {
        t += Time.deltaTime / m_Speed;
        m_OpacityValue = Mathf.Lerp(m_OldOpacityValue, m_NewOpacityValue, t);
        m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, m_OpacityValue);
    }

    public void IncreaseOpacity()
    {
        m_NewOpacityValue = 1;
        m_OldOpacityValue = 0;
        t = 0;
    }

    public void DecreaseOpacity()
    {
        m_NewOpacityValue = 0;
        m_OldOpacityValue = 1;
        t = 0;
    }
}
