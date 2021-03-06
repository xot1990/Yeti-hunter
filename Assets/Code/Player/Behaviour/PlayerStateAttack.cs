using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack : StateMachine
{
    
    private PlayerControler _controler;
    private Game _game;
    private float attackDelay = 0.2f;

    private void Awake()
    {
        OnGetClass<PlayerControler>();
        _game = Game.Get();
    }

    private void Start()
    {
    }

    public void OnGetClass<T>() where T : PlayerControler
    {
        _controler = GetComponent<T>();
    }

    public override void OnEnterState()
    {
        _controler.animator.Play("Attack");
    }

    public override void OnUpdateState()
    {
        if (!_game.isPause)
        {
            attackDelay -= Time.deltaTime;

            if (attackDelay < 0)
            {
                attackDelay = 0.2f;
                _controler.ChangeState<PlayerStateIdle>();
            }
        }
    }

    public override void OnExitState()
    {
    }

}

