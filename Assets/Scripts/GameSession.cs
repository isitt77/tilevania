using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int numPlayerLives;
    [SerializeField] TextMeshProUGUI playerLivesText;
    [SerializeField] int playerScore;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        playerLivesText.text = numPlayerLives.ToString();
        scoreText.text = playerScore.ToString();
    }


    public void ProcessPlayerDeath()
    {
        if(numPlayerLives > 1)
        {
            TakeOneLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        scoreText.text = playerScore.ToString();
    }

    void TakeOneLife()
    {
        numPlayerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        playerLivesText.text = numPlayerLives.ToString();
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        // Destroy this GameSession game object.
        Destroy(gameObject);
    }
}
