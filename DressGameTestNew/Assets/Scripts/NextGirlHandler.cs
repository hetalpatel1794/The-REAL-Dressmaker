using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextGirlHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_GirlPoses;

    private int m_PoseCount;

    public void Next()
    {
        if (m_PoseCount < 2)
        {
            for (int i = 0; i < m_GirlPoses.Count; i++)
            {
                m_GirlPoses[m_PoseCount].SetActive(false);
            }

            m_PoseCount++;
            m_GirlPoses[m_PoseCount].SetActive(true);
        }
    }

    public void Previous()
    {
        if (m_PoseCount > 0)
        {
            for (int i = 0; i < m_GirlPoses.Count; i++)
            {
                m_GirlPoses[m_PoseCount].SetActive(false);
            }

            m_PoseCount--;
            m_GirlPoses[m_PoseCount].SetActive(true);
        }
    }
}
