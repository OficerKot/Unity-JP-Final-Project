using TMPro;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] int time;
    private int score;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    
    }

    private void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        for (; time >= 0; time--)
        {
            timerText.text = "Time: " + time;
            yield return new WaitForSeconds(1);
        }

    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public IEnumerator AddScoreSmoothly(int scoreToAdd)
    {
        int startScore = score;
        while(score < startScore + scoreToAdd)
        {
            score += 1;
            yield return new WaitForSeconds(0.02f);
        }
    }



}
