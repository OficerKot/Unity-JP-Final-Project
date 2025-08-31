using System.Collections;
using TMPro;
using UnityEngine;

public class Cat : Pet //INHERITANCE
{
    [SerializeField] float hitForce;

    //for superpower
    [SerializeField] float speedBoost = 1.4f;
    [SerializeField] ParticleSystem boostEffect;
    [SerializeField] AudioClip hitSound;
    float boostTime = 5;
    float boostDelay = 10;
    float hitRange = 0.3f;

    Vector3 hitOffset;
    bool canBoost = true;
    bool isBoosting = false;

    private void Awake()
    {

        SoundManager.Instance.PlayCatMusic();
    }


    public override void SuperPower() // POLYMORPHISM &  ABSTRACTION
    {
        if (canBoost && !isBoosting)
        {
            StartCoroutine(Boost());
            StartCoroutine(Say("Yoohoo!", boostTime));
        }
        if (!canBoost)
        {
            StartCoroutine(Say("I'm tired..."));
        }
    }

    public override void Interact() // POLYMORPHISM & // ABSTRACTION
    {
        TryPushObject();
    }

    void TryPushObject()
    {
        hitOffset = transform.forward * 0.3f;
        // Определяем направление толчка (вперед от персонажа)
        Vector3 pushDirection = transform.forward;

        // Бросаем луч вперед для обнаружения объектов
        Collider[] colliders = Physics.OverlapSphere(transform.position + hitOffset, hitRange);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && rb.tag == "Pushable")
            {
                if (rb != null && rb.tag == "Pushable")
                {
                    // Применяем силу толчка
                    rb.AddForce(pushDirection + new Vector3(0, 0.5f, 0) * hitForce, ForceMode.Impulse);

                    // Можно добавить визуальный эффект или звук
                    aud.PlayOneShot(hitSound, 0.4f);
                    Debug.Log("Толкаем объект: " + hit.name);
                }
            }
        }
    }
    IEnumerator Boost()
    {
        moveSpeed *= speedBoost;
        isBoosting = true;
        boostEffect.Play();
        yield return new WaitForSeconds(boostTime);
        boostEffect.Stop();
        isBoosting = false;
        moveSpeed /= speedBoost;
        StartCoroutine(BoostDelay());
    }

    IEnumerator BoostDelay()
    {
        canBoost = false;
        yield return new WaitForSeconds(boostDelay);
        canBoost = true;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + hitOffset, hitRange);

    }
}
