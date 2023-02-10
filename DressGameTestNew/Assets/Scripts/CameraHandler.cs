using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance;

    [SerializeField]
    private float m_Speed;

    private Vector3 m_StartPosition;
    private Vector3 m_NewPosition;
    private Vector3 m_OldPosition;

    private Vector3 m_Ref;
    private float m_Ref2;

    private float m_StartFow;
    private float m_NewFow;
    private float m_OldFow;

    private float t;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        m_StartPosition = transform.position;
        m_NewPosition = m_StartPosition;
        m_OldPosition = m_StartPosition;

        m_StartFow = Camera.main.orthographicSize;
        m_NewFow = m_StartFow;
        m_OldFow = m_StartFow;

        t = 0;
    }

    private void Update()
    {
        t += Time.deltaTime / m_Speed;

        transform.position = Vector3.Lerp(m_OldPosition, m_NewPosition, t);
        //transform.position = Vector3.SmoothDamp(transform.position, m_NewPosition, ref m_Ref, 0.75f);
        Camera.main.orthographicSize = Mathf.Lerp(m_OldFow, m_NewFow, t);
        //Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, m_NewFow, ref m_Ref2, 0.75f);
    }

    public void PositionCamera(Vector3 i_CameraPosition, float i_CameraFow)
    {
        m_OldPosition = transform.position;
        m_NewPosition = i_CameraPosition;
        m_OldFow = Camera.main.orthographicSize;
        m_NewFow = i_CameraFow;
        t = 0;
    }

    public void ChangeCameraFow(float i_CameraFow)
    {
        m_OldFow = Camera.main.orthographicSize;
        m_NewFow = i_CameraFow;
        t = 0;
    }

    public void ResetCamera()
    {
        m_OldPosition = transform.position;
        m_NewPosition = m_StartPosition;
        m_OldFow = Camera.main.orthographicSize;
        m_NewFow = m_StartFow;

        t = 0;
    }

    public void TeleportCamera(Vector3 i_CameraPosition, float i_CameraFow)
    {
        m_OldPosition = i_CameraPosition;
        m_NewPosition = i_CameraPosition;
        m_OldFow = i_CameraFow;
        m_NewFow = i_CameraFow;
        t = 0;
    }
}
