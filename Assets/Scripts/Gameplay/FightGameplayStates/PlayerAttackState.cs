using Assets.Scripts.Configurations;
using Core.State;
using System;
using UnityEngine;

public class PlayerAttackState : State
{
    private FightGameplay fightGameplay;
    private LevelProperties.Camera cameraProperties;

    private IHero hero;
    private IEnemy enemy;
    private bool areGoingToAttack;

    public PlayerAttackState(FightGameplay fightGameplay) : base(fightGameplay)
    {
        this.fightGameplay = fightGameplay;
    }

    public override void Enter()
    {
        base.Enter();

        cameraProperties = fightGameplay.currentLevelSceneProps.CameraProps;

        hero = fightGameplay.chosenHero;
        enemy = fightGameplay.chosenEnemy;

        hero.MoveToFightScene(fightGameplay.currentLevelSceneProps.HeroFightScenePoint);
        enemy.MoveToFightScene(fightGameplay.currentLevelSceneProps.EnemyFightScenePoint);

        fightGameplay.gameCamera.SetTarget(cameraProperties.FightScenePosition);
        fightGameplay.gameCamera.SetLensSize(cameraProperties.FightSceneLensSize);

        areGoingToAttack = true;

        hero.FinishedMoving += OnChosenHeroFinishedMoving;
        enemy.FinishedMoving += OnChosenEnemyFinishedMoving;
    }

    private void OnChosenHeroFinishedMoving()
    {
        if (!enemy.IsMovingToFightScene)
        {
            if (areGoingToAttack)
            {
                StartAttacking();
            }
            else
            {
                FinishedAttacking();
            }
        }
    }

    private void OnChosenEnemyFinishedMoving()
    {
        if (!hero.IsMovingToFightScene)
        {
            if (areGoingToAttack)
            {
                StartAttacking();
            }
            else
            {
                FinishedAttacking();
            }
        }
    }

    private void FinishedAttacking()
    {
        stateMachine.ChangeState(fightGameplay.EnemyAttackState);
    }

    private void StartAttacking()
    {
        hero.Attack();
        hero.Attacked += OnHeroAttacked;
    }

    private void OnHeroAttacked()
    {
        enemy.Hit();
        enemy.ReceivedDamage += OnEnemyReceivedDamage;
        areGoingToAttack = false;
    }

    private void OnEnemyReceivedDamage()
    {
        fightGameplay.gameCamera.SetTarget(cameraProperties.InitialPosition);
        fightGameplay.gameCamera.SetLensSize(cameraProperties.InitialLensSize);

        hero.MoveToBasePlace(hero.InitialPosition);
        enemy.MoveToBasePlace(enemy.InitialPosition);
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

        hero.Attacked -= OnHeroAttacked;
        enemy.ReceivedDamage -= OnEnemyReceivedDamage;
        hero.FinishedMoving -= OnChosenHeroFinishedMoving;
        enemy.FinishedMoving -= OnChosenEnemyFinishedMoving;
    }
}

