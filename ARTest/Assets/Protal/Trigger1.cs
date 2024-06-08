using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    Animator animator;
    // AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.B)) startAnim();
        // if (Input.GetKey(KeyCode.N)) renewAnim();
    }
    public void startAnim()
    {
        // animator.SetBool("B_Start", true);
        // animator.SetBool("B_Reverse", false);

        animator.SetTrigger("T_Start");
        animator.ResetTrigger("T_Reverse");
        // music.Play();
    }
    public void renewAnim()
    {
        // animator.SetBool("B_Start", false);
        // animator.SetBool("B_Reverse", true);
        // music.Stop();
        animator.SetTrigger("T_Reverse");
        animator.ResetTrigger("T_Start");
    }
}
