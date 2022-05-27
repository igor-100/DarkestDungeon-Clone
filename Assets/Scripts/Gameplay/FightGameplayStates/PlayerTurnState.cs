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

        chosenHero = null;

        foreach (var hero in fightGameplay.availableHeroes)
        {
            hero.IsClickable = true;
            hero.Highlight(0.1f);
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
                chosenHero.Highlight(0.1f);
                ChooseHero(hero);
            }
            else
            {
                hero.Highlight(0.1f);
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
        fightGameplay.playerActions.Hide();

        fightGameplay.playerActions.AttackClicked -= OnAttackClicked;
        fightGameplay.playerActions.WaitClicked -= OnWaitClicked;

        chosenHero = null;
    }

    private void ChooseHero(IHero hero)
    {
        fightGameplay.playerActions.Show();

        hero.Highlight(0.2f);
        chosenHero = hero;

        fightGameplay.playerActions.AttackClicked += OnAttackClicked;
        fightGameplay.playerActions.WaitClicked += OnWaitClicked;
    }

    private void OnWaitClicked()
    {
        chosenHero.StopHighlighting();

        stateMachine.ChangeState(fightGameplay.EnemyAttackState);
    }

    private void OnAttackClicked()
    {
        fightGameplay.chosenHero = chosenHero;
        fightGameplay.playerActions.ShowPickEnemyText();

        foreach (var enemy in fightGameplay.enemies)
        {
            enemy.Highlight(0.1f);
            enemy.IsClickable = true;
            enemy.Clicked += OnEnemyClicked;
        }

        foreach (var hero in fightGameplay.availableHeroes)
        {
            hero.StopHighlighting();
            hero.IsClickable = false;
            hero.Clicked -= OnHeroClicked;
        }
    }

    private void OnEnemyClicked(IUnit unit)
    {
        chosenHero.StopHighlighting();

        fightGameplay.chosenEnemy = unit as IEnemy;

        stateMachine.ChangeState(fightGameplay.PlayerAttackState);
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

        fightGameplay.RemoveAvailableHero(chosenHero);

        fightGameplay.playerActions.Hide();
        fightGameplay.playerActions.WaitClicked -= OnWaitClicked;
        fightGameplay.playerActions.AttackClicked -= OnAttackClicked;

        foreach (var enemy in fightGameplay.enemies)
        {
            enemy.StopHighlighting();
            enemy.IsClickable = false;
            enemy.Clicked -= OnEnemyClicked;
        }
        foreach (var hero in fightGameplay.heroes)
        {
            hero.StopHighlighting();
            hero.IsClickable = false;
            hero.Clicked -= OnHeroClicked;
        }
    }
}

