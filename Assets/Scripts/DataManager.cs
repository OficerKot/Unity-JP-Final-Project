using JetBrains.Annotations;
using UnityEditor.SearchService;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public enum Pet{dog, cat };

    Pet petToPlay;
    public Vector3 startGravity;
    public static DataManager Instance;

    int curScore;
    int bestScore = 0;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        startGravity = Physics.gravity;
        DontDestroyOnLoad(Instance);
    }

    public void SetCurScore(int score)
    {
        curScore = score;
    }
    
    public bool IsNewRecord()
    {
        return curScore > bestScore;
    }

    public void UpdateBestScore()
    {
        if (IsNewRecord())
        {
            bestScore = curScore;
        }
    }
    public int GetCurScore()
    {
        return curScore;
    }

    public int GetBestScore()
    {
        return bestScore;
    }
    public void ChoosePetToPlay(Pet pet)
    {
        petToPlay = pet;
    }

    public Pet WhichPetIsSelected()
    {
        return petToPlay;
    }
}
