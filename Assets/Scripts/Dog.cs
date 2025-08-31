using System.Collections;
using UnityEngine;


public class Dog : Pet
{
    //interactions
    float grabRange = 1f;
    public Transform mouth;
    GameObject heldItem;
    bool isHolding;
    [SerializeField] AudioClip grabSound;

    //for superpower
    [SerializeField] ParticleSystem boostEffect;
    float explosionRadius = 1;
    [SerializeField] float explosionForce = 10;
    float explosionUpForce = 4;
    float boostTime = 7;
    float boostDelay = 12;
    bool isBoosting;
    bool canBoost = true;

   

    private void Awake()
    {
        SoundManager.Instance.PlayDogMusic();
    }
    public override void Interact()
    {
        if (!isHolding)
        {
            TryGrabObject();
        }
        else ReleaseItem();
    }

    public override void Update()
    {
        InputController();

        if ((isBoosting))
        {
            CreateExplosion();
        }
    }
    public override void SuperPower()
    {
        if (canBoost && !isBoosting) // ������ ������� �������, ������ �������
        {
            StartCoroutine(Boost());
            StartCoroutine(Say("Let�s throw everything!", boostTime));
        }

        if (!canBoost)
        {
            StartCoroutine(Say("I'm tired..."));
        }

    }

    void CreateExplosion()
    {
        // ������� ��� ���������� � �������
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && rb.tag == "Pushable")
            {
                Vector3 direction = (hit.transform.position - transform.position).normalized;

                // ��������� �������������� ���� (� �������)
                Vector3 horizontalForce = new Vector3(direction.x, 0, direction.z) * explosionForce;

                // ��������� ������������ ���� (�����)
                Vector3 verticalForce = Vector3.up * explosionUpForce;

                // ����������� ����
                rb.AddForce(horizontalForce + verticalForce, ForceMode.Impulse);

                // ��������� ��������� �������� ��� �������
                rb.AddTorque(Random.insideUnitSphere * explosionForce * 0.3f, ForceMode.Impulse);
            }
        }
    }

    IEnumerator Boost()
    {
        isBoosting = true;
        boostEffect.Play();
        yield return new WaitForSeconds(boostTime);
        boostEffect.Stop();
        isBoosting = false;
        canBoost = false;
        StartCoroutine(BoostDelay());
    }

    IEnumerator BoostDelay()
    {
        yield return new WaitForSeconds(boostDelay);
        canBoost = true;
    }

    void TryGrabObject()
    {
        // ���������� ����������� ������ (������ �� ���������)
        Vector3 grabDirection = transform.forward;

        // ������� ��� ������ ��� ����������� ��������
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabRange);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Pushable"))
            {
                GrabItem(col.gameObject);
                break; // ����� ������ ���������� �������
            }
        }

    }

    void GrabItem(GameObject item)
    {
        heldItem = item;
        isHolding = true;
        // ��������� ������ � ��������
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            heldItem.transform.SetParent(mouth);
            heldItem.transform.position = mouth.position;
        }

        Collider[] allColliders = item.GetComponentsInChildren<Collider>();

        // ��������� ������ ���������
        foreach (Collider col in allColliders)
        {
            col.enabled = false;
        }

        // ������ ������� �������� � ����� (�����������)

        aud.PlayOneShot(grabSound, 0.3f);
        Debug.Log("��������� �������: " + heldItem.name);
    }

    void ReleaseItem()
    {
        if (heldItem != null && isHolding)
        {
            Collider heldItemCollider = heldItem.GetComponent<Collider>();
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            Collider[] allColliders = heldItem.GetComponentsInChildren<Collider>();

            // �������� ������ ���������
            foreach (Collider col in allColliders)
            {
                col.enabled = true;
            }
            heldItem.gameObject.transform.SetParent(null);
            heldItem = null;
            heldItemCollider.enabled = true;
        }
        isHolding = false;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }


}
