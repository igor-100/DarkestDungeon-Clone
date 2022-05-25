using Core.State;
using System;
using UnityEngine;

public class PlayerTurnState : State
{
    private FightGameplay fightGameplay;

    private IHero chosenHero;

    public PlayerTurnState(FightGameplay fightGameplay) : base(fightGameplay)
    {
        this.fightGameplay = fightGameplay;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PlayerTurnState");

        fightGameplay.playerActions.Show();
        fightGameplay.playerActions.WaitClicked += OnWaitClicked;

        chosenHero = null;

        foreach (var hero in fightGameplay.heroes)
        {
            hero.Clicked += OnHeroClicked;
        }
    }
    private void OnHeroClicked(IUnit unit)
    {
        var hero = unit as IHero;
        if (chosenHero != null)
        {
            if (chosenHero.Id != hero.Id)
            {
                chosenHero.UnChoose();
                ChooseHero(hero);
            }
            else
            {
                hero.UnChoose();
                NoHeroChosen();
            }
        }
        else
        {
            ChooseHero(hero);
        }
    }

    private void NoHeroChosen()
    {
        chosenHero = null;

        fightGameplay.playerActions.HideAttackButton();
        fightGameplay.playerActions.AttackClicked -= OnAttackClicked;
    }

    private void ChooseHero(IHero hero)
    {
        hero.Choose();
        chosenHero = hero;

        fightGameplay.playerActions.ShowAttackButton();
        fightGameplay.playerActions.AttackClicked += OnAttackClicked;
    }

    private void OnWaitClicked()
    {
        Debug.Log("OnWaitClicked");
    }

    private void OnAttackClicked()
    {
        Debug.Log("OnAttackClicked");
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

