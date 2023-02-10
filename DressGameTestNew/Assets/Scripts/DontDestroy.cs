using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    [SerializeField]
    private Canvas m_Canvas;

    public static DontDestroy Instance;
    private DressHandler m_DressHandler;
    private Image m_DressDetails;
    private GameObject m_MagicUp;
    private GameObject m_MagicDown;
    private MoveDressHandler m_MoveDressHandler;
    private GameObject m_MoveDressToParavanButton;
    private GameObject m_ButtonsParent;

    private TryingHandler m_TryingHandler;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //else
        //{
        //    Destroy(this);
        //    return;
        //}

        DontDestroyOnLoad(this);
    }

    public void Setup(DressHandler i_DressHandler, Image i_DressDetails, GameObject i_MagicUp, GameObject i_MagicDown, MoveDressHandler i_MoveDressHandler, GameObject i_MoveDressToParavanButton, GameObject i_ButtonsParent)
    {
        m_DressHandler = i_DressHandler;
        m_DressDetails = i_DressDetails;
        m_MagicUp = i_MagicUp;
        m_MagicDown = i_MagicDown;
        m_MoveDressHandler = i_MoveDressHandler;
        m_MoveDressToParavanButton = i_MoveDressToParavanButton;
        m_ButtonsParent = i_ButtonsParent;

        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void ChangeLayer()
    {
        m_Canvas.gameObject.layer = 8;
    }

    public GameObject GetButtonsParent()
    {
        return m_ButtonsParent;
    }

    public GameObject GetDressCanvas()
    {
        return m_Canvas.gameObject;
    }

    public DressHandler GetDressHandler()
    {
        return m_DressHandler;
    }

    public RectTransform GetDressRectTransform()
    {
        return m_MoveDressHandler.GetComponent<RectTransform>();
    }

    public GameObject GetMagicDown()
    {
        return m_MagicDown;
    }

    public GameObject GetMagicUp()
    {
        return m_MagicUp;
    }

    public Image GetDressDetails()
    {
        return m_DressDetails;
    }

    public void DisableMoveDressHandler()
    {
        m_MoveDressHandler.enabled = false;
    }

    public GameObject GetMoveDressToParavanButton()
    {
        return m_MoveDressToParavanButton;
    }

    public void SetTryingHandler(TryingHandler i_TryingHandler)
    {
        m_TryingHandler = i_TryingHandler;
    }

    public void MoveDress()
    {
        m_TryingHandler.MoveDress();
    }

    public void ReleaseDress()
    {
        m_TryingHandler.ReleaseDress();
    }

    public RectTransform GetDress()
    {
        return m_MoveDressHandler.gameObject.GetComponent<RectTransform>();
    }

    public void SetLayerOrder(int i_LayerOrder)
    {
        m_Canvas.sortingOrder = i_LayerOrder;
    }
}
