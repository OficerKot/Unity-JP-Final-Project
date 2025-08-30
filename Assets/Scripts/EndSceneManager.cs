using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI newRecordText;
    [SerializeField] GameObject catObj;
    [SerializeField] GameObject dogObj;

    float blinkDelay = 0.5f;

    private void Start()
    {
        int score = DataManager.Instance.GetCurScore();
        if(DataManager.Instance.IsNewRecord())
        {
            StartCoroutine(StartBlinkNewRecord());
        }

        SpawnPet();
        StartCoroutine(ShowScoreSmoothly(score));
        if (score == 0) scoreText.text = "lazy pet!";
    }
    public void Restart()
    {
        DataManager.Instance.UpdateBestScore();
        SceneManager.LoadScene(0);

    }

    public IEnumerator ShowScoreSmoothly(int score)
    {
        int curScore = 0;
        while (curScore < score)
        {
            curScore += 1;
            scoreText.text = curScore.ToString();
            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator StartBlinkNewRecord()
    {
        while (true)
        {
            newRecordText.gameObject.SetActive(true);
            yield return new WaitForSeconds(blinkDelay);
            newRecordText.gameObject.SetActive(false);
            yield return new WaitForSeconds(blinkDelay);
        }
    }
    void SpawnPet()
    {
        switch(DataManager.Instance.WhichPetIsSelected())
        {
            case DataManager.Pet.dog:
                dogObj.SetActive(true);
                break;
            case DataManager.Pet.cat:
                catObj.SetActive(true);
                break;
        }
    }

}
