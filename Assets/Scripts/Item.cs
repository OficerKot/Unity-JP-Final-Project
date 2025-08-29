using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] int score;

    [SerializeField] float mass;
    [SerializeField] float soundVolume = 1;
    [SerializeField] AudioClip fallSound;
    [SerializeField] bool isStayingOnTheFloor = false;
    AudioSource aud;
    float destroyDelay = 3;
    bool isFell = false;
    

    void Start()
    {
        gameObject.tag = "Pushable";
        gameObject.AddComponent<AudioSource>();
        aud = GetComponent<AudioSource>();
        aud.volume = soundVolume;
        AddRb();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!isFell && !isStayingOnTheFloor && collision.gameObject.layer == 6)
        {
            StartCoroutine(OnFall());
            isFell = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!isFell && isStayingOnTheFloor & other.gameObject.layer == 6)
        {
            StartCoroutine(OnFall());
            isFell = true;
        }
    }

    IEnumerator OnFall()
    {
        aud.PlayOneShot(fallSound);
        StartCoroutine(GameManager.Instance.AddScoreSmoothly(score));
        yield return new WaitForSeconds(destroyDelay);
        //Destroy(gameObject);
    }

    void AddRb()
    {
        Rigidbody rb;
        if (gameObject.TryGetComponent<Rigidbody>(out rb) == false)
        {
            gameObject.AddComponent<Rigidbody>();
            rb = gameObject.GetComponent<Rigidbody>();
            rb.mass = mass;
        }
    }

}
