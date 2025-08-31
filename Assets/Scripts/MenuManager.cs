using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] Camera cam;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject MainUI;
    bool isRotated;
    float targetYRotation;
    float targetXRotation;
    float newY;
    float newX;
    private void Start()
    {
        int score = DataManager.Instance.GetBestScore();
        targetYRotation = cam.transform.eulerAngles.y;
        targetXRotation = cam.transform.eulerAngles.x;
        isRotated = true;
        bestScoreText.text = score.ToString();
    }

    void Update()
    {
        // Плавное вращение
        float currentY = cam.transform.eulerAngles.y;
        float currentX = cam.transform.eulerAngles.x;
        newY = Mathf.LerpAngle(currentY, targetYRotation, rotationSpeed * Time.deltaTime);
        newX = Mathf.LerpAngle(currentX, targetXRotation, rotationSpeed * Time.deltaTime);
        cam.transform.rotation = Quaternion.Euler(newX, newY, cam.transform.eulerAngles.z);
    }

    public void RotateCamera180()
    {
        isRotated = !isRotated;
        targetYRotation = isRotated ? 182.181f : 0f;
        targetXRotation = isRotated ? 20.54f : 5f;
        MainUI.SetActive(isRotated);

    }
}
