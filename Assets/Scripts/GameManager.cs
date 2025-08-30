using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] int time;
    [SerializeField] GameObject catObj;
    [SerializeField] GameObject dogObj;
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
        switch (DataManager.Instance.WhichPetIsSelected())
        {
            case DataManager.Pet.dog:
                dogObj.SetActive(true);
                break;
            case DataManager.Pet.cat:
                catObj.SetActive(true);
                break;
        }

    }

    void EndGame()
    {
        DataManager.Instance.SetCurScore(score);
        SceneManager.LoadScene(2);
    }
    IEnumerator Timer()
    {
        for (; time >= 0; time--)
        {
            timerText.text = "Time: " + time;
            yield return new WaitForSeconds(1);
        }
        EndGame();

    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public IEnumerator AddScoreSmoothly(int scoreToAdd)
    {
        int startScore = score;
        while (score < startScore + scoreToAdd)
        {
            score += 1;
            yield return new WaitForSeconds(0.02f);
        }
    }



}
