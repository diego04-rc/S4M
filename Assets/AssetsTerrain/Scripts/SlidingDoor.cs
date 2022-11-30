using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    private Animator anim;
    private bool open;
    void Start()
    {
        anim = GetComponent<Animator>();
        open = anim.GetBool("Open");
    }
    public void Operate()
    {
        open = !open;
        anim.SetBool("Open", open);
    }
    public void Deactivate()
    {
        if (open) Operate();
    }
    public void Activate()
    {
        if (!open) Operate();
    }

}
