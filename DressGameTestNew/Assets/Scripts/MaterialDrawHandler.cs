using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MaterialDrawHandler : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> m_Patterns;
    [SerializeField]
    private List<Image> m_PatternImages;
    [SerializeField]
    private List<Image> m_ColorImages;
    [SerializeField]
    private List<RectTransform> m_Materials;
    [SerializeField]
    private Vector2 m_NewMaterialPosition;
    [SerializeField]
    private float m_MaterialMoveSpeed;
    [SerializeField]
    private float m_MinSwipeDistance;
    [SerializeField]
    private List<Animator> m_MaterialAnimators;
    [SerializeField]
    private List<Animator> m_MaterialMaskAnimators;
    [SerializeField]
    private List<CutDrawSequenceHandler> m_CutDrawSequenceHandlers;
    [SerializeField]
    private GameObject m_LevelFinishedPage;
    [SerializeField]
    private GameObject m_Effects;
    [SerializeField]
    private GameObject m_NextPartButton;
    [SerializeField]
    private GameObject m_Stars;
    [SerializeField]
    private List<GameObject> m_Hints;
    [SerializeField]
    private GameObject m_MetarTrigger;
    [SerializeField]
    private GameObject m_ChalkTrigger;
    [SerializeField]
    private GameObject m_ScissorsTrigger;
    [SerializeField]
    private EventSystem m_EventSystem;
    [SerializeField]
    private GameObject m_MaterialTrigger;

    private int m_HintCount;

    private RectTransform m_CurrentMaterial;

    private Vector2 m_OldMaterialPosition;
    private Vector3 m_FirstMousePos;
    private Vector3 m_SecondMousePos;

    private Vector3 m_Offset;

    private bool m_MoveMaterial;
    private bool m_CanUnrollMaterial;
    private bool m_CanMoveMaterial;
    private bool m_ScreenPressed;
    private bool m_CanReleaseMaterial;

    [SerializeField]
    private int m_MaterialCount;
    private float t;

    private PointerEventData m_PointerEventData;

    private CutDrawPartHandler m_CutDrawPartHandler;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();

        for (int i = 0; i < m_PatternImages.Count; i++)
        {
            m_PatternImages[i].sprite = m_Patterns[PlayerPrefs.GetInt("Pattern" + i)];
            m_ColorImages[i] = m_PatternImages[i];

            if (PlayerPrefs.HasKey("Color" + i))
            {
                //m_ColorImages[i].gameObject.SetActive(true);

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

        m_CurrentMaterial = m_Materials[m_MaterialCount];
        m_CanMoveMaterial = true;
        m_CanReleaseMaterial = true;
    }

    public void ShowHint()
    {
        switch(m_HintCount)
        {
            case 0: m_Hints[0].SetActive(true); break;
            case 1: m_Hints[3].SetActive(true); break;
            case 2: m_CutDrawSequenceHandlers[m_MaterialCount].ShowHint(); break;
            case 3: m_Hints[1].SetActive(true); break;
            case 4: m_Hints[3].SetActive(true); break;
            case 5: m_CutDrawSequenceHandlers[m_MaterialCount].ShowHint(); break;
            case 6: m_Hints[2].SetActive(true); break;
            case 7: m_Hints[3].SetActive(true); break;
            case 8: m_CutDrawSequenceHandlers[m_MaterialCount].ShowHint(); break;
        }
    }

    public void IncreaseHintCount()
    {
        m_HintCount++;
        //m_CanReleaseMaterial = true;
    }

    private void Update()
    {
        if (m_MoveMaterial)
        {
            //MoveMaterial();
            m_CurrentMaterial.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;
        }

        if (m_CanUnrollMaterial)
        {
            UnrollMaterial();
        }
    }

    public void ClickMaterial(int i_MaterialIndex)
    {
        if (m_CanMoveMaterial && i_MaterialIndex == m_MaterialCount)
        {        
            switch(m_HintCount)
            {
                case 0: m_Hints[0].SetActive(false); break;
                case 3: m_Hints[1].SetActive(false); break;
                case 6: m_Hints[2].SetActive(false); break;
            }

            //m_HintCount++;
            t = 0;
            m_CanReleaseMaterial = true;
            //m_CurrentMaterial = m_Materials[m_MaterialCount];
            m_Offset = m_CurrentMaterial.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_OldMaterialPosition = m_CurrentMaterial.anchoredPosition;
            m_MoveMaterial = true;
            //m_CanMoveMaterial = false;
        }
    }

    public void ReleaseMaterial()
    {
        m_MoveMaterial = false;

        if (m_CanReleaseMaterial)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(m_PointerEventData, results);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.name == "MaterialTrigger")
                {
                    m_CurrentMaterial.anchoredPosition = m_NewMaterialPosition;
                    m_CanMoveMaterial = false;
                    m_MoveMaterial = false;
                    m_HintCount++;
                    m_CanUnrollMaterial = true;
                    m_CanReleaseMaterial = false;
                    m_MaterialTrigger.SetActive(false);
                }
            }
        }
    }

    //private void MoveMaterial()
    //{
    //    t += Time.deltaTime / m_MaterialMoveSpeed;
    //    m_CurrentMaterial.anchoredPosition = Vector2.Lerp(m_OldMaterialPosition, m_NewMaterialPosition, t);

    //    if (m_CurrentMaterial.anchoredPosition == m_NewMaterialPosition)
    //    {
    //        m_CanUnrollMaterial = true;
    //        m_MoveMaterial = false;
    //    }
    //}

    private void UnrollMaterial()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_ScreenPressed = true;
            m_FirstMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && m_ScreenPressed)
        {
            m_SecondMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (Mathf.Abs(m_SecondMousePos.x - m_FirstMousePos.x) > m_MinSwipeDistance)
            {
                if (m_SecondMousePos.x > m_FirstMousePos.x && m_CanUnrollMaterial)
                {
                    m_Hints[3].SetActive(false);
                    m_HintCount++;
                    m_MaterialAnimators[m_MaterialCount].SetTrigger("unroll");
                    m_MaterialMaskAnimators[m_MaterialCount].SetTrigger("unroll");
                    StartCoroutine(EnableMaterialInteraction());
                    m_CanUnrollMaterial = false;
                    m_MetarTrigger.SetActive(true);
                }
            }
        }
    }

    public void DisableMetarTrigger()
    {
        m_MetarTrigger.SetActive(false);
    }

    public void EnableChalkTrigger()
    {
        m_ChalkTrigger.SetActive(true);
    }

    public void DisableChalkTrigger()
    {
        m_ChalkTrigger.SetActive(false);
    }

    public void EnableScissorsTrigger()
    {
        m_ScissorsTrigger.SetActive(true);
    }

    public void DisableScissorsTrigger()
    {
        m_ScissorsTrigger.SetActive(false);
    }

    IEnumerator EnableMaterialInteraction()
    {
        yield return new WaitForSeconds(0.5f);
        m_CutDrawSequenceHandlers[m_MaterialCount].enabled = true;
    }

    public void MaterialFinished()
    {
        if (m_MaterialCount < 2)
        {
            m_MaterialCount++;
            m_CanMoveMaterial = true;
            //m_CanReleaseMaterial = true;
            m_CurrentMaterial = m_Materials[m_MaterialCount];
            m_MaterialTrigger.SetActive(true);
        }

        else
        {
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(0.2f);
        m_Stars.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_Effects.SetActive(true);
        m_LevelFinishedPage.SetActive(true);
        m_NextPartButton.SetActive(true);
    }

    public void NextPart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickMetar()
    {
        if (m_CutDrawSequenceHandlers[m_MaterialCount].enabled)
        {
            m_CutDrawSequenceHandlers[m_MaterialCount].ClickMetar();
        }
    }

    public void ReleaseMetar()
    {
        m_CutDrawSequenceHandlers[m_MaterialCount].ReleaseMetar();
    }

    public void ClickChalk()
    {
        if (m_CutDrawSequenceHandlers[m_MaterialCount].enabled)
        {
            m_CutDrawSequenceHandlers[m_MaterialCount].ClickChalk();
        }
    }

    public void ReleaseChalk()
    {
        m_CutDrawSequenceHandlers[m_MaterialCount].ReleaseChalk();
    }

    public void ClickScissors()
    {
        if (m_CutDrawSequenceHandlers[m_MaterialCount].enabled)
        {
            m_CutDrawSequenceHandlers[m_MaterialCount].ClickScissors();
        }
    }

    public void ReleaseScissors()
    {
        m_CutDrawSequenceHandlers[m_MaterialCount].ReleaseScissors();
    }
}
