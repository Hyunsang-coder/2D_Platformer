using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerlives = 3;
    [SerializeField] int coinScore = 0;
    [SerializeField] TextMeshProUGUI playerlivesTxt;
    [SerializeField] TextMeshProUGUI scoreTxt;
    void Awake()
    {
        int numberofManagers = FindObjectsOfType<GameManager>().Length;

        if (numberofManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    private void LateUpdate()
    {
        playerlivesTxt.text = "x " + playerlives.ToString();
        scoreTxt.text = "Score: " + coinScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerlives > 1)
        {
            TakeLife();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            ReloadCurrentLevel();
        }
    }

    private void TakeLife()
    {
        playerlives--;
    }

    public void ReloadCurrentLevel()
    {
        FindObjectOfType<PersistentObject>().ResetScene();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void EarnCoin()
    {
        coinScore+= 100;
    }
}
