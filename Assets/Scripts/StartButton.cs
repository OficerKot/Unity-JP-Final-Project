using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button thisButton;
    [SerializeField] DataManager.Pet character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnClickStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        DataManager.Instance.ChoosePetToPlay(character);
        SceneManager.LoadScene(1);
    }
}
