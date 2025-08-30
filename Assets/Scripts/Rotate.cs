using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * (transform.forward + transform.right));
    }
}
