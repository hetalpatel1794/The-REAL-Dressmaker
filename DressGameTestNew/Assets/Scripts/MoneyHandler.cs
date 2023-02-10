using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    public static MoneyHandler Instance;

    [SerializeField]
    private int m_OwnedMoney;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            m_OwnedMoney = PlayerPrefs.GetInt("Money");
        }
    }

    public bool CheckIfHaveMoney(int i_Price)
    {
        if (i_Price <= m_OwnedMoney)
        {
            return true;
        }

        else
            return false;
    }

    public void RemoveMoney(int i_MoneyToRemove)
    {
        m_OwnedMoney -= i_MoneyToRemove;
        PlayerPrefs.SetInt("Money", m_OwnedMoney);
    }

    public void AddMoney(int i_MoneyToRecieve)
    {
        m_OwnedMoney += i_MoneyToRecieve;
        PlayerPrefs.SetInt("Money", m_OwnedMoney);
    }

    public int GetMoney()
    {
        return m_OwnedMoney;
    }
}
