using Core.State;
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
                hero.Choose();
                chosenHero = hero;
            }
            else
            {
                hero.UnChoose();
                chosenHero = null;
            }
        }
        else
        {
            hero.Choose();
            chosenHero = hero;
        }
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

