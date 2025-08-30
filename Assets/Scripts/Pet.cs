using System.Collections;
using TMPro;
using UnityEngine;

abstract public class Pet : MonoBehaviour
{
    Rigidbody PetRb;
    [SerializeField] protected float moveSpeed = 10;
    [SerializeField] protected float rotateSpeed = 10;
    [SerializeField] protected float jumpForce = 10;
    [SerializeField] protected float gravityModifier = 2;
    public TextMeshProUGUI textToSay;

    Animator anim;

    protected AudioSource aud;
    public AudioClip[] sounds;

    [SerializeField] protected bool onGround;

    void Start()
    {
        TryGetComponent<Animator>(out anim);
        aud = GetComponent<AudioSource>();
        PetRb = GetComponent<Rigidbody>();
        SetGravity();
    }

    void SetGravity()
    {
        if(Physics.gravity == DataManager.Instance.startGravity)
        {
            Physics.gravity *= gravityModifier;
        }
    }
   
    public virtual void Update()
    {
        InputController();
    }
    void FixedUpdate()
    {
        MoveLogic();
    }

    void MoveLogic()
    {
        Run();
        Rotation();
    }

    void Run()
    {
        float forwardInput = Input.GetAxis("Vertical");
        Debug.Log(transform.forward.normalized);
        if (anim != null) Animate(forwardInput);
        PetRb.AddForce(forwardInput * transform.forward.normalized * moveSpeed);
    }
    void Rotation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up * horizontalInput);
    }
    void Jump()
    {
        if (onGround)
        {
            PetRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    void MakeSound()
    {
        if (sounds.Length > 0)
        {
            int randIndx = Random.Range(0, sounds.Length);
            aud.PlayOneShot(sounds[randIndx]);
        }
    }

    void Animate(float forwardInput)
    {
        if (moveSpeed < 22)
        {
            anim.SetFloat("State", 0);
            anim.SetFloat("Vert", Mathf.Lerp(0, Mathf.Abs(forwardInput), 2f));
        }
        else
        {
            anim.SetFloat("Vert", 1);
            anim.SetFloat("State", Mathf.Lerp(0, 1, 2f));
        }
    }
    protected IEnumerator Say(string s, float time = 3)
    {
        textToSay.text = s;
        yield return new WaitForSeconds(time);
        textToSay.text = "";
    }
    public virtual void InputController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SuperPower();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            MakeSound();
        }

    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }
    public abstract void Interact();
    public abstract void SuperPower();
}
