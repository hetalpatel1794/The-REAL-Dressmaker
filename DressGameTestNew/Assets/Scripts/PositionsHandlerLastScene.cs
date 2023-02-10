using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsHandlerLastScene : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_SewingZoomInPosition;
    [SerializeField]
    private float m_SewingZoomInFow;
    [SerializeField]
    private Vector3 m_SewingZoomOutPosition;
    [SerializeField]
    private float m_SewingZoomOutFow;
    [SerializeField]
    private Vector3 m_IroningPosition;
    [SerializeField]
    private float m_IroningFow;
    [SerializeField]
    private Vector3 m_DressIroningPosition;
    [SerializeField]
    private float m_DressIroningFow;
    [SerializeField]
    private Vector3 m_ManequinPosition;
    [SerializeField]
    private float m_ManequinFow;
    [SerializeField]
    private Vector3 m_ButtonsPosition;
    [SerializeField]
    private float m_ButtonsFow;
    [SerializeField]
    private Vector3 m_NeedleSewingPosition;
    [SerializeField]
    private float m_NeedleSewingFow;
    [SerializeField]
    private Vector3 m_TryingPosition;
    [SerializeField]
    private float m_TryingFow;
    [SerializeField]
    private Vector3 m_MirrorPosition;
    [SerializeField]
    private float m_MirrorFow;
    [SerializeField]
    private Vector3 m_GirlZoomPosition;
    [SerializeField]
    private float m_GirlZoomFow;
    [SerializeField]
    private Vector3 m_PayingPosition;
    [SerializeField]
    private float m_PayingFow;
    [SerializeField]
    private Vector3 m_NeedleSewingFirstPartPosition;
    [SerializeField]
    private float m_NeedleSewingFirstPartFow;
    [SerializeField]
    private Vector3 m_LacesPositions;
    [SerializeField]
    private float m_LacesFow;

    public void SetCameraToSewingZoomIn()
    {
        CameraHandler.Instance.PositionCamera(m_SewingZoomInPosition, m_SewingZoomInFow);
    }

    public void SetCameraToSewingZoomOut()
    {
        CameraHandler.Instance.PositionCamera(m_SewingZoomOutPosition, m_SewingZoomOutFow);
    }

    public void SetCameraToIroning()
    {
        CameraHandler.Instance.PositionCamera(m_IroningPosition, m_IroningFow);
    }

    public void SetCameraToDressIroning()
    {
        CameraHandler.Instance.PositionCamera(m_DressIroningPosition, m_DressIroningFow);
    }

    public void SetCameraToManequin()
    {
        CameraHandler.Instance.PositionCamera(m_ManequinPosition, m_ManequinFow);
    }

    public void SetCameraToButtons()
    {
        CameraHandler.Instance.PositionCamera(m_ButtonsPosition, m_ButtonsFow);
    }

    public void SetCameraToNeedleSewing()
    {
        CameraHandler.Instance.PositionCamera(m_NeedleSewingPosition, m_NeedleSewingFow);
    }

    public void SetCameraToTrying()
    {
        CameraHandler.Instance.PositionCamera(m_TryingPosition, m_TryingFow);
    }

    public void SetCameraToMirror()
    {
        CameraHandler.Instance.PositionCamera(m_MirrorPosition, m_MirrorFow);
    }

    public void SetCameraToGirlZoom()
    {
        CameraHandler.Instance.PositionCamera(m_GirlZoomPosition, m_GirlZoomFow);
    }

    public void SetCameraToPaying()
    {
        CameraHandler.Instance.TeleportCamera(m_PayingPosition, m_PayingFow);
    }

    public void SetCameraToSewingFirstPart()
    {
        CameraHandler.Instance.PositionCamera(m_NeedleSewingFirstPartPosition, m_NeedleSewingFirstPartFow);
    }

    public void SetCameraToLaces()
    {
        CameraHandler.Instance.PositionCamera(m_LacesPositions, m_LacesFow);
    }
}
