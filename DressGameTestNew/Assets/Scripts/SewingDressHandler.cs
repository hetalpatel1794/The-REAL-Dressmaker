using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SewingDressHandler : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> m_Patterns;
    [SerializeField]
    private List<Image> m_PatternImages;
    [SerializeField]
    private List<Image> m_ColorImages;
    [SerializeField]
    private float m_Speed;

    private bool m_MovePatterns;

    private void Start()
    {
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
    }

    private void Update()
    {
        if (m_MovePatterns)
        {
            for (int i = 0; i < m_PatternImages.Count; i++)
            {
                m_PatternImages[i].transform.Translate(Vector2.up * Time.deltaTime * m_Speed);
            }
        }
    }

    public void MovePatterns()
    {
        m_MovePatterns = true;
    }

    public void StopPatterns()
    {
        m_MovePatterns = false;
    }
}
