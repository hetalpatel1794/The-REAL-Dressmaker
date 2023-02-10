using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDressHandler : MonoBehaviour
{
    [SerializeField]
    private RectTransform m_Dress;
    [SerializeField]
    private float m_UnwrapDuration;
    [SerializeField]
    private Vector3 m_CorrectPos;
    [SerializeField]
    private float m_DressToManequinDuration;
    [SerializeField]
    private DressHandler m_DressHandler;
    [SerializeField]
    private PositionsHandlerLastScene m_PositionsHandlerLastScene;
    [SerializeField]
    private LacesHandler m_LacesHandler;
    [SerializeField]
    private Image m_DressBlend;

    [SerializeField]
    private SpriteRenderer m_ThreadSpriteRenderer;
    [SerializeField]
    private List<Sprite> m_ThreadSprites;
    [SerializeField]
    private SpriteRenderer m_ThreadColor;
    [SerializeField]
    private List<Color> m_ThreadColors;
    [SerializeField]
    private bool m_SetThreadColors;

    [SerializeField]
    private GameObject m_Hint;

    private int m_HintCount;

    private Vector2 m_Offset;
    private Vector3 m_OldDressScale;
    private Vector3 m_OldDressPosition;

    private Quaternion m_OldDressRot;

    private bool m_Move;
    private bool m_CanMoveDress;
    private bool m_MoveToManequin;

    private float t;

    private void Start()
    {
        PlayerPrefs.DeleteKey("LaceIndex");

        if (PlayerPrefs.GetInt("Ads") == 0)
        {
            AdsHandler.Instance.ShowBanner();
        }

        else
            AdsHandler.Instance.HideBanner();
        m_CanMoveDress = true;

        if (m_SetThreadColors)
        {
            m_ThreadSpriteRenderer.sprite = m_ThreadSprites[PlayerPrefs.GetInt("Thread")];
            m_ThreadColor.color = m_ThreadColors[PlayerPrefs.GetInt("Thread")];
        }
    }

    public void ShowHint()
    {
        switch (m_HintCount)
        {
            case 0: m_Hint.SetActive(true); break;
            case 1: m_LacesHandler.ShowHint(); break;
        }
    }

    private void Update()
    {
        if (m_Move)
        {
            m_Hint.SetActive(false);
            m_Dress.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + m_Offset;

            t += Time.deltaTime / m_UnwrapDuration;

            m_Dress.transform.localScale = Vector3.Lerp(m_OldDressScale, Vector3.one * 1.052465f, t);
            m_Dress.transform.rotation = Quaternion.Slerp(m_OldDressRot, Quaternion.identity, t);
            Color m_Col = m_DressBlend.color;
            m_Col.a = Mathf.Lerp(0, 1, t);
            m_DressBlend.color = m_Col;

            if (Vector3.Distance(m_Dress.transform.localPosition, m_CorrectPos) < 100f)
            {
                m_LacesHandler.CanClickLaces();
                m_HintCount++;
                m_OldDressPosition = m_Dress.transform.localPosition;
                t = 0;
                m_Dress.transform.rotation = Quaternion.identity;
                m_MoveToManequin = true;
                m_CanMoveDress = false;
                m_Move = false;
            }
        }

        if (m_MoveToManequin)
        {
            t += Time.deltaTime / m_DressToManequinDuration;

            m_Dress.transform.localPosition = Vector3.Lerp(m_OldDressPosition, m_CorrectPos, t);

            if (m_Dress.transform.localPosition == m_CorrectPos)
            {
                StartCoroutine(MoveCamera());
                m_MoveToManequin = false;
            }
        }
    }

    IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(0.5f);
        m_PositionsHandlerLastScene.SetCameraToLaces();
        yield return new WaitForSeconds(1f);
        m_LacesHandler.enabled = true;
    }

    public void MoveDress()
    {
        if (m_CanMoveDress)
        {
            m_Offset = m_Dress.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_OldDressScale = m_Dress.transform.localScale;
            m_OldDressRot = m_Dress.transform.rotation;
            m_Move = true;
        }
    }

    public void StopMoving()
    {
        m_Move = false;
    }
}
