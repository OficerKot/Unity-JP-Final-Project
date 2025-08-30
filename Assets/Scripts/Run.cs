using UnityEngine;

public class Run : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("Vert", 1);
        anim.SetFloat("State", 1);
    }
}
