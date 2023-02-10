using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomHandler : MonoBehaviour
{
    [SerializeField]
    private float m_ZoomOutMin = 1.5f;
    [SerializeField]
    private float m_ZoomOutMax = 4.2f;
    [SerializeField]
    private CameraHandler m_CameraHandler;
    [SerializeField]
    private ExploreHandler m_ExploreHandler;
    [SerializeField]
    private float m_ZoomSpeed;

    void Update()
    {
        if (m_ExploreHandler.IsExploreActive())
        {

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                Zoom(difference * 0.01f);
            }
        }

        else
            m_CameraHandler.enabled = true;
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - (increment * m_ZoomSpeed), m_ZoomOutMin, m_ZoomOutMax);
        m_CameraHandler.enabled = false;
    }
}
