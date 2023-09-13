using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    private bool Jump = false;
    public GameObject Player;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void AnimatorParameters()
    {
        anim.SetBool("jump", Jump);
    }

    public void JumpingPlayer()
    {
        Jump = true;
        AnimatorParameters();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            JumpingPlayer();
        }
    }
}