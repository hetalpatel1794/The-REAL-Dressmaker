using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class ActivateObject : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ObjectToActivate;
    [SerializeField]
    private ExplorePageHandler m_ExplorePageHandler;
    [SerializeField]
    private AudioSource m_TaskAcomplishedEffect;

    private RaycastHit2D m_RaycastHit;

    private Vector3 m_MousePos;
    private Vector2 m_MousePos2D;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CheckIfObjectClicked())
        {
            if (CheckIfObjectClicked() && m_ExplorePageHandler.IsExploreActive())
            {
                HandleObjectsActivate();
            }
        }
    }

    private bool CheckIfObjectClicked()
    {
        m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_MousePos2D = new Vector2(m_MousePos.x, m_MousePos.y);

        m_RaycastHit = Physics2D.Raycast(m_MousePos2D, Vector2.zero);

        if (m_RaycastHit.collider == null)
        {
            return false;
        }

        if (m_RaycastHit.collider.gameObject == this.gameObject && EventSystem.current.currentSelectedGameObject == null)
        {
            return true;
        }

        return false;
    }

    private void HandleObjectsActivate()
    {
        m_TaskAcomplishedEffect.Play();
        m_ObjectToActivate.SetActive(true);
        m_ExplorePageHandler.HandleAllObjectsActivate(m_ObjectToActivate, gameObject);
        gameObject.SetActive(false);
    }

    public void HandleObjectsDeactivate()
    {
        m_ObjectToActivate.SetActive(true);     
        gameObject.SetActive(false);
    }
}
