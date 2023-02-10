using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RadioHandler : MonoBehaviour
{
    public static RadioHandler Instance;

    [SerializeField]
    private GameObject m_StationButton;
    [SerializeField]
    private GameObject m_RadioOff;
    [SerializeField]
    private GameObject m_RadioOn;
    [SerializeField]
    private GameObject m_Line;
    [SerializeField]
    private AudioSource m_Song;
    [SerializeField]
    private List<AudioClip> m_RadioSongs;
    [SerializeField]
    private GameObject m_NoteParticles;
    [SerializeField]
    private GameObject m_LinkButton;

    private float m_RotValue;

    private Vector3 m_NewLinePosition;
    private Vector3 m_OldLinePosition;

    private float t;

    private bool m_TurnedOn;
    private bool m_ChangeForward;

    private RaycastHit2D m_RaycastHit;

    private Vector3 m_MousePos;
    private Vector2 m_MousePos2D;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        //DontDestroyOnLoad(this);
        m_NewLinePosition = m_Line.transform.localPosition;
        m_OldLinePosition = m_NewLinePosition;
        m_ChangeForward = true;

        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            TurnOn();
        }

        else
        {
            TurnOff();
        }
    }

    public void TurnOff()
    {
        m_Song.volume = 0;
    }

    public void TurnOn()
    {
        m_Song.volume = 1;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (CheckIfObjectClicked())
                {
                    HandleRadio();
                }

                else if (GetObjectTag() == "StationButton")
                {
                    ChangeStation();
                }

                else if (GetObjectTag() == "Link")
                {
                    Application.OpenURL("https://www.youtube.com/watch?v=eeNq1G7YKB8");
                }
            }

            if (m_TurnedOn)
            {
                t += Time.deltaTime / 1.2f;

                m_StationButton.transform.rotation = Quaternion.Slerp(m_StationButton.transform.rotation, Quaternion.Euler(0, 0, m_RotValue), t);
                m_Line.transform.localPosition = Vector3.Lerp(m_OldLinePosition, m_NewLinePosition, t);
            }
        }
    }
    public void ChangeStation()
    {
        if (m_TurnedOn)
        {
            if (m_ChangeForward)
            {
                m_RotValue -= 30;
                m_NewLinePosition.x += 0.14f;
                m_OldLinePosition = m_Line.transform.localPosition;

                if (m_RotValue < -90)
                {
                    m_ChangeForward = !m_ChangeForward;
                }
            }

            else
            {
                m_RotValue += 30;
                m_NewLinePosition.x -= 0.14f;
                m_OldLinePosition = m_Line.transform.localPosition;

                if (m_RotValue > -30)
                {
                    m_ChangeForward = !m_ChangeForward;
                }
            }

            t = 0;

            ChangeSong();
        }
    }

    public void PlaySong()
    {
        if (!m_TurnedOn)
        {
            HandleRadio();
        }
    }

    public void DisableNotes()
    {
        if (!m_NoteParticles)
            return;
        m_NoteParticles.SetActive(false);
    }

    public void EnableNotes()
    {
        if (!m_NoteParticles)
            return;
        if (m_TurnedOn)
        {
            m_NoteParticles.SetActive(true);
        }
    }

    private void ChangeSong()
    {
        switch (m_RotValue)
        {
            case 0:
                m_Song.clip = m_RadioSongs[0];
                m_LinkButton.SetActive(true);
                break;
            case -30:
                m_Song.clip = m_RadioSongs[1];
                m_LinkButton.SetActive(false);
                break;
            case -60:
                m_Song.clip = m_RadioSongs[2];
                m_LinkButton.SetActive(false);
                break;
            case -90:
                m_Song.clip = m_RadioSongs[3];
                m_LinkButton.SetActive(false);
                break;
            case -120:
                m_Song.clip = m_RadioSongs[4];
                m_LinkButton.SetActive(false);
                break;
        }

        m_Song.Play();
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

        //if (m_RaycastHit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
        //{
        //    return true;
        //}

        if (m_RaycastHit.collider.tag == "Radio" && !EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        return false;
    }

    private string GetObjectTag()
    {
        string m_Tag = "";

        m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_MousePos2D = new Vector2(m_MousePos.x, m_MousePos.y);

        m_RaycastHit = Physics2D.Raycast(m_MousePos2D, Vector2.zero);

        if (m_RaycastHit.collider == null)
        {
            m_Tag = "";
            return m_Tag;
        }

        m_Tag = m_RaycastHit.collider.tag;

        return m_Tag;
    }

    private void HandleRadio()
    {
        if (!m_RadioOff || !m_RadioOn || !m_NoteParticles)
            return;

        if (!m_TurnedOn)
        {
            m_RadioOff.SetActive(false);
            m_RadioOn.SetActive(true);
            ChangeSong();
            m_TurnedOn = true;
            m_NoteParticles.SetActive(true);
        }

        else
        {
            m_LinkButton.SetActive(false);
            m_RadioOn.SetActive(false);
            m_RadioOff.SetActive(true);
            m_Song.Stop();
            m_TurnedOn = false;
            m_NoteParticles.SetActive(false);
        }
    }
}
