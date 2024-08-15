using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int hitScore = 10;
    [SerializeField] int deathScore = 50;
    
    int score = 0;

    static ScoreKeeper instance;
    
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
 
    public void ModifyScore(int health, bool isPlayer)
    {
        if (isPlayer)
        {
            score -= hitScore;
        }
        else 
        {
            if (health < hitScore )
            {
                score += deathScore;
            }
            else
            {
                score += hitScore;
            }
        }

        // ensure score doesn't go into negative
        score = Math.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }
}
