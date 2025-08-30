using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;

    private void Start()
    {
        int score = DataManager.Instance.GetBestScore();
        bestScoreText.text = score.ToString();
    }
}
