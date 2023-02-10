using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MainUI;
    [SerializeField]
    private GameObject m_ExploreUI;
    [SerializeField]
    private PositionsHandler m_PositionHandler;
    [SerializeField]
    private ExplorePageHandler m_Explore1;
    [SerializeField]
    private ExplorePageHandler m_Explore2;
    [SerializeField]
    private GameObject m_Background1;
    [SerializeField]
    private GameObject m_Background2;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private GameObject m_ExploreSwitch;
    [SerializeField]
    private GameObject m_Explore1Button;
    [SerializeField]
    private GameObject m_Explore2Button;
    [SerializeField]
    private Animator m_Background1Anim;
    [SerializeField]
    private Animator m_Background2Anim;
    [SerializeField]
    private SpriteRenderer m_Background1Sprite;
    [SerializeField]
    private SpriteRenderer m_Background2Sprite;
    [SerializeField]
    private List<OpacityHandler> m_OpacityHandlers;
    [SerializeField]
    private GameObject m_Arrows;

    private Vector3 m_Background1StartPosition;
    private Vector3 m_Background2StartPosition;

    private bool m_ExploreActive;

    private bool m_CanSwitch;

    private float t;

    private void Start()
    {
        m_Background1StartPosition = m_Background1.transform.localPosition;
        m_Background2StartPosition = m_Background2.transform.localPosition;
        m_CanSwitch = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_ExploreActive)
        {
            m_Arrows.SetActive(false);
        }
    }

    public void ResetBackgrounds()
    {
        m_Background1Anim.gameObject.SetActive(true);
        m_Background1Anim.SetTrigger("increase");
        m_Background2Anim.gameObject.SetActive(false);
        m_Explore1.gameObject.SetActive(true);
        m_Explore1.enabled = false;
        m_Background1Anim.transform.localPosition = new Vector3(0.25f, 0, 0);
    }

    public void SwitchExplorePages()
    {
        if (m_CanSwitch)
        {
            StartCoroutine(Switch());
            m_CanSwitch = false;
        }
    }

    IEnumerator Switch()
    {
        if (m_Explore1.IsExploreActive())
        {
            m_Background1Anim.SetTrigger("decrease");
            m_Background2Anim.SetTrigger("increase");

            for (int i = 0; i < m_OpacityHandlers.Count; i++)
            {
                m_OpacityHandlers[i].IncreaseOpacity();
            }

            StartCoroutine(DisableWithADelay(m_Explore1.gameObject));
            m_Explore1.SetIsActive(false);
            m_Explore2.SetIsActive(true);
            m_Explore2.gameObject.SetActive(true);

            m_Explore1Button.SetActive(false);
            m_Explore2Button.SetActive(true);

            Vector3 m_AdjustPosition = new Vector3(m_Background2StartPosition.x, m_Background1.transform.position.y, m_Background2StartPosition.z);
            m_Explore2.SetCurrentPosition(m_AdjustPosition);
            m_Background2.transform.position = m_AdjustPosition;
        }

        else if (m_Explore2.IsExploreActive())
        {
            m_Background1Anim.SetTrigger("increase");
            m_Background2Anim.SetTrigger("decrease");

            for (int i = 0; i < m_OpacityHandlers.Count; i++)
            {
                m_OpacityHandlers[i].DecreaseOpacity();
            }

            StartCoroutine(DisableWithADelay(m_Explore2.gameObject));
            m_Explore1.SetIsActive(true);
            m_Explore1.gameObject.SetActive(true);
            m_Explore2.SetIsActive(false);

            m_Explore1Button.SetActive(true);
            m_Explore2Button.SetActive(false);
            
            Vector3 m_AdjustPosition = new Vector3(m_Background1StartPosition.x, m_Background2.transform.position.y, m_Background1StartPosition.z);
            m_Explore1.SetCurrentPosition(m_AdjustPosition);
            m_Background1.transform.position = m_AdjustPosition;
        }

        yield return new WaitForSeconds(3f);
        m_CanSwitch = true;
    }

    IEnumerator DisableWithADelay(GameObject m_ObjectToDisable)
    {
        yield return new WaitForSeconds(1.5f);
        m_ObjectToDisable.SetActive(false);
    }

    public void EnableExplore()
    {
        m_MainUI.SetActive(false);
        m_ExploreUI.SetActive(true);
        m_PositionHandler.SetCameraToExplore();

        m_Explore1.DisableReset();
        m_Explore2.DisableReset();

        m_ExploreActive = true;

        if (m_Background1Sprite.color.a != 0)
        {
            m_Explore1.SetIsActive(true);
            m_Explore1Button.SetActive(true);
        }

        else if (m_Background2Sprite.color.a != 0)
        {
            m_Explore2.SetIsActive(true);
            m_Explore2Button.SetActive(true);
        }
        if (!RadioHandler.Instance)
            return;
        RadioHandler.Instance.EnableNotes();
    }

    public void DisableExplore()
    {
        m_PositionHandler.SetCameraToMainMenu();
        m_Explore1Button.SetActive(false);
        m_Explore2Button.SetActive(false);

        m_MainUI.SetActive(true);
        m_ExploreUI.SetActive(false);

        m_Explore1.SetIsActive(false);
        m_Explore2.SetIsActive(false);

        m_Explore1.ResetPosition();
        m_Explore2.ResetPosition();

        m_ExploreActive = false;
        m_Arrows.SetActive(true);
        if (!RadioHandler.Instance)
            return;
        RadioHandler.Instance.DisableNotes();
    }

    public bool IsExploreActive()
    {
        return m_ExploreActive;
    }
}
