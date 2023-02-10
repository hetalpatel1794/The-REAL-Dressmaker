using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SelectGirls : MonoBehaviour
{
    [SerializeField]
    private float m_MinSwipeDistance;
    [SerializeField]
    private GameObject m_MovableSection;
    [SerializeField]
    private float m_MaxRightDistance;
    [SerializeField]
    private float m_MaxLeftDistance;
    [SerializeField]
    private float m_SwitchDuration;
    [SerializeField]
    private PositionsHandler m_PositionHandler;
    [SerializeField]
    private UIHandler m_UIHandler;
    [SerializeField]
    private List<Animator> m_Anims;
    [SerializeField]
    private List<String> m_AnimsStrings;
    [SerializeField]
    private List<GameObject> m_AudioSources;
    [SerializeField]
    private List<Sprite> m_GirlsSprites;
    [SerializeField]
    private List<Image> m_GirlsImages;
    [SerializeField]
    private GameObject m_Hand;

    private RectTransform m_Rect;

    private Vector3 m_FirstMousePos;
    private Vector3 m_SecondMousePos;
    private Vector2 m_NewPos;
    private Vector2 m_OldPos;

    private float t;
    private bool m_ScreenPressed;
    private bool m_Move;
    private bool m_CanSwipe;

    private bool m_GirlClicked;

    private Vector3 m_GirlClickedPosition;

    private int m_Count;

    private bool m_FirstClick;
    private float m_DoubleClickTimer;

    private void Start()
    {
        m_Rect = m_MovableSection.GetComponent<RectTransform>();
        m_OldPos = m_Rect.anchoredPosition;
        m_NewPos = m_OldPos;
        m_Count = 0;
        m_CanSwipe = true;
        t = 0;

        m_Anims[0].enabled = true;
        m_AudioSources[0].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_FirstMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            m_ScreenPressed = true;
            m_Hand.SetActive(false);

            //DetectDoubleClick();
        }

        if (m_FirstClick)
        {
            if (Time.time - m_DoubleClickTimer > 0.3f)
            {
                m_FirstClick = false;
            }
        }

        if (Input.GetMouseButtonUp(0) && m_ScreenPressed && m_CanSwipe)
        {
            m_SecondMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (Mathf.Abs(m_SecondMousePos.x - m_FirstMousePos.x) > m_MinSwipeDistance)
            {
                if (m_SecondMousePos.x > m_FirstMousePos.x)
                {
                    if (m_NewPos.x < m_MaxRightDistance)
                    {
                        if (m_Hand.activeInHierarchy)
                        {
                            m_Hand.SetActive(false);
                        }

                        m_OldPos = m_Rect.anchoredPosition;
                        m_NewPos.x += 900;
                        t = 0;
                        m_Move = true;
                        m_CanSwipe = false;
                        m_Count--;
                    }
                }

                else if (m_SecondMousePos.x < m_FirstMousePos.x)
                {
                    if (m_NewPos.x > m_MaxLeftDistance)
                    {
                        m_OldPos = m_Rect.anchoredPosition;
                        m_NewPos.x -= 900;
                        t = 0;
                        m_Move = true;
                        m_CanSwipe = false;
                        m_Count++;
                    }
                }
            }

            m_ScreenPressed = false;
        }

        if (m_Move)
        {
            t += Time.deltaTime / m_SwitchDuration;

            m_Rect.anchoredPosition = Vector3.Lerp(m_OldPos, m_NewPos, t);

            if (m_Rect.anchoredPosition == m_NewPos)
            {
                StartCoroutine(EnableSwipe());
                ResetAnimators();
                m_Move = false;
            }
        }
    }

    public void MoveLeft()
    {
        m_OldPos = m_Rect.anchoredPosition;
        m_NewPos.x += 900;
        t = 0;
        m_Move = true;
        m_CanSwipe = false;
        m_Count--;
    }

    public void MoveRight()
    {
        m_OldPos = m_Rect.anchoredPosition;
        m_NewPos.x -= 900;
        t = 0;
        m_Move = true;
        m_CanSwipe = false;
        m_Count++;
    }

    public void ClickDown()
    {
        m_GirlClicked = true;
        m_GirlClickedPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition); 
    }

    public void ClickUp()
    {
        if (m_GirlClicked)
        {
            if (Vector3.Distance(Camera.main.ScreenToViewportPoint(Input.mousePosition), m_GirlClickedPosition) < 0.2f)
            {
                SelectGirl();
            }

            //if (Camera.main.ScreenToViewportPoint(Input.mousePosition) == m_GirlClickedPosition)
            //{
            //    SelectGirl();
            //}

            m_GirlClicked = false;
        }
    }

    private void ResetAnimators()
    {
        m_Anims[m_Count].enabled = true;
        m_Anims[m_Count].Play(m_AnimsStrings[m_Count], 0, 0);
        m_AudioSources[m_Count].SetActive(true);

        for (int i = 0; i < m_Anims.Count; i++)
        {
            if (i != m_Count)
            {
                m_Anims[i].enabled = false;
                m_AudioSources[i].SetActive(false);
                m_GirlsImages[i].sprite = m_GirlsSprites[i];
            }
        }
    }

    IEnumerator EnableSwipe()
    {
        yield return new WaitForSeconds(1f);
        m_CanSwipe = true;
    }

    private void DetectDoubleClick()
    {
        if (!m_FirstClick)
        {
            m_FirstClick = true;
            m_DoubleClickTimer = Time.time;
        }

        else
        {
            SelectGirl();
            m_FirstClick = false;
        }
    }

    private void SelectGirl()
    {        
        PlayerPrefs.SetInt("Girl", m_Count);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //m_PositionHandler.SetCameraToGirlHi();
        //m_UIHandler.EnableGirls();
    }
}
