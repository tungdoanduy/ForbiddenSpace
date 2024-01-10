using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : CustomButton
{
    [SerializeField] List<Animator> anims;

    protected override void Awake()
    {
        base.Awake();
        foreach (Animator anim in anims)
            anim.speed = 0.25f;
    }
    public override void Enter()
    {
        base.Enter();
        foreach (Animator anim in anims)
            anim.speed = 0.5f;
    }

    public override void Exit()
    {
        base.Exit();
        foreach (Animator anim in anims)
            anim.speed = 0.25f;
    }
}
