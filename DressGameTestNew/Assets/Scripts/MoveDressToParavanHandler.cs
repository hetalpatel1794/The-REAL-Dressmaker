using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveDressToParavanHandler : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
    public void MoveDress()
    {
        DontDestroy.Instance.MoveDress();
    }

    public void ReleaseDress()
    {
        DontDestroy.Instance.ReleaseDress();
    }
}
