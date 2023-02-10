using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplorePageHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_ActiveObjects;
    [SerializeField]
    private List<GameObject> m_InactiveObjects;
    [SerializeField]
    private float m_MaxLeftSwipeDistance;
    [SerializeField]
    private float m_MaxRightSwipeDistance;
    [SerializeField]
    private Transform m_MainBackground;
    [SerializeField]
    private float m_Speed;

    private float m_YDistance;

    private Vector3 m_ScreenPoint;
    private Vector3 m_Offset;
    private Vector3 m_CurrentScreenPoint;
    private Vector3 m_CurrentPosition;
    private Vector3 m_OldCameraPosition;

    private Vector3 m_Ref;

    private float t;

    private bool m_Move;

    private bool m_IsActive;
    private bool m_CanClickScreen;
    private bool m_Reset;

    private void OnEnable()
    {
        m_CurrentPosition = m_MainBackground.transform.position;
    }

    private void Update()
    {
        if (m_CanClickScreen && Input.touchCount == 0)
        {
            m_CanClickScreen = false;
        }
        else if (!m_CanClickScreen && Input.touchCount > 0 && m_IsActive && EventSystem.current.currentSelectedGameObject == null)
        {
            m_ScreenPoint = Camera.main.WorldToScreenPoint(m_MainBackground.transform.position);
            Touch touchZero = Input.GetTouch(0);
            m_Offset = m_MainBackground.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(touchZero.position.x, touchZero.position.y, m_ScreenPoint.z));
            m_Move = true;
            m_CanClickScreen = true;
        }

        if (Input.touchCount > 1)
        {
            m_Move = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_Move = false;
        }

        if (m_Move && m_CanClickScreen)
        {
            Move();
        }

        if (m_Reset)
        {
            t += Time.deltaTime / 1.1f;
            m_MainBackground.transform.position = Vector3.Lerp(m_OldCameraPosition, m_CurrentPosition, t);
        }

        else
            m_MainBackground.transform.position = Vector3.Lerp(m_MainBackground.transform.position, m_CurrentPosition, Time.deltaTime * m_Speed);


        m_YDistance = 4.2f / Camera.main.orthographicSize * 0.75f;
        m_CurrentPosition.x = Mathf.Clamp(m_CurrentPosition.x, Mathf.Max(m_CurrentPosition.x, m_MaxRightSwipeDistance), Mathf.Min(m_MaxLeftSwipeDistance, m_CurrentPosition.x));
        m_CurrentPosition.y = Mathf.Clamp(m_CurrentPosition.y, Mathf.Max(-m_YDistance, m_CurrentPosition.y), Mathf.Min(m_YDistance, m_CurrentPosition.y));
    }

    public void SetCurrentPosition(Vector3 i_Position)
    {
        m_CurrentPosition = i_Position;
    }

    public void HandleAllObjectsActivate(GameObject i_CurrentObjectToActivate, GameObject i_CurrentObjectToDeactivate)
    {
        for (int i = 0; i < m_ActiveObjects.Count; i++)
        {
            if (m_ActiveObjects[i] != i_CurrentObjectToActivate)
            {
                m_ActiveObjects[i].SetActive(false);
            }
        }

        for (int i = 0; i < m_InactiveObjects.Count; i++)
        {
            if (m_InactiveObjects[i] == i_CurrentObjectToDeactivate)
            {
                m_InactiveObjects[i].SetActive(false);
            }

            else
                m_InactiveObjects[i].SetActive(true);
        }
    }

    public bool IsExploreActive()
    {
        return m_IsActive;
    }

    public void SetIsActive(bool i_IsActive)
    {
        m_IsActive = i_IsActive;

        if (!i_IsActive)
        {
            DisableAllObjects();
            EnableAllObjects();
        }
    }

    public void ResetPosition()
    {
        if (m_MainBackground.transform.position.y > 0.75f)
        {
            m_Reset = true;
            m_CurrentPosition.y = 0.75f;
            m_OldCameraPosition = m_MainBackground.transform.position;
            t = 0;
        }

        if (m_MainBackground.transform.position.y < -0.75f)
        {
            m_Reset = true;
            m_CurrentPosition.y = -0.75f;
            m_OldCameraPosition = m_MainBackground.transform.position;
            t = 0;
        }
    }

    public void DisableReset()
    {
        m_Reset = false;
    }

    private void DisableAllObjects()
    {
        for (int i = 0; i < m_ActiveObjects.Count; i++)
        {
            m_ActiveObjects[i].SetActive(false);
        }
    }

    private void EnableAllObjects()
    {
        for (int i = 0; i < m_InactiveObjects.Count; i++)
        {
            m_InactiveObjects[i].SetActive(true);
        }
    }

    private void Move()
    {
        m_CurrentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_ScreenPoint.z);

        m_CurrentPosition = Camera.main.ScreenToWorldPoint(m_CurrentScreenPoint) + m_Offset;

        m_YDistance = 4.2f / Camera.main.orthographicSize * 0.8f;

        m_CurrentPosition.x = Mathf.Clamp(m_CurrentPosition.x, Mathf.Max(m_CurrentPosition.x, m_MaxRightSwipeDistance), Mathf.Min(m_MaxLeftSwipeDistance, m_CurrentPosition.x));
        m_CurrentPosition.y = Mathf.Clamp(m_CurrentPosition.y, Mathf.Max(-m_YDistance, m_CurrentPosition.y), Mathf.Min(m_YDistance, m_CurrentPosition.y));

        //m_MainBackground.transform.position = Vector3.Lerp(m_MainBackground.transform.position, m_CurrentPosition, Time.deltaTime * m_Speed);
    }
}
