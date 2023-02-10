using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsHandler : MonoBehaviour
{
    [SerializeField]
    private float m_ExplorePageCameraFow;
    [SerializeField]
    private Vector3 m_GirlHiPosition;
    [SerializeField]
    private float m_GirlHiFow;
    [SerializeField]
    private Vector3 m_MagazinePosition;
    [SerializeField]
    private float m_MagazineFow;
    [SerializeField]
    private Vector3 m_MaterialsPosition;
    [SerializeField]
    private float m_MaterialsFow;
    [SerializeField]
    private Vector3 m_MeasuresPosition;
    [SerializeField]
    private float m_MeasuresFow;
    [SerializeField]
    private Vector3 m_StartPosition;
    [SerializeField]
    private float m_StartFow;

    public void SetCameraToExplore()
    {
        CameraHandler.Instance.ChangeCameraFow(m_ExplorePageCameraFow);
    }

    public void SetCameraToMainMenu()
    {
        CameraHandler.Instance.ResetCamera();
    }

    public void SetCameraToGirlHi()
    {
        CameraHandler.Instance.PositionCamera(m_GirlHiPosition, m_GirlHiFow);
    }

    public void SetCameraToMagazine()
    {
        CameraHandler.Instance.PositionCamera(m_MagazinePosition, m_MagazineFow);
    }

    public void SetCameraToMaterials()
    {
        CameraHandler.Instance.PositionCamera(m_MaterialsPosition, m_MaterialsFow);
    }

    public void SetCameraToMeasures()
    {
        CameraHandler.Instance.PositionCamera(m_MeasuresPosition, m_MeasuresFow);
    }

    public void ResetCamera()
    {
        CameraHandler.Instance.PositionCamera(m_StartPosition, m_StartFow);
    }
}
