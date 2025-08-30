using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] float blinkDelay = 0.4f;
    void Start()
    {
        StartCoroutine(StartBlink());
    }


}
