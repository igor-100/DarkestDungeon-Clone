using Assets.Scripts.Configurations;
using Core.State;
using UnityEngine;

public class EnemyAttackState : State
{
    private FightGameplay fightGameplay;
    private LevelProperties.Camera cameraProperties;

    private IHero hero;
    private IEnemy enemy;

    private bool areGoingToAttack;

    public EnemyAttackState(FightGameplay fightGameplay) : base(fightGameplay)
    {
        this.fightGameplay = fightGameplay;
    }

    public override void Enter()
    {
        base.Enter();

        cameraProperties = fightGameplay.currentLevelSceneProps.CameraProps;

        fightGameplay.gameCamera.SetTarget(cameraProperties.FightScenePosition);
        fightGameplay.gameCamera.SetLensSize(cameraProperties.FightSceneLensSize);

        if (fightGameplay.availableEnemies.Count == 0 || fightGameplay.availableHeroes.Count == 0)
        {
            fightGameplay.StartNewRound();
        }

        int enemyId = Random.Range(0, fightGameplay.availableEnemies.Count);
        enemy = fightGameplay.availableEnemies[enemyId];

        int heroId = Random.Range(0, fightGameplay.heroes.Count);
        hero = fightGameplay.heroes[heroId];

        fightGameplay.RemoveAvailableEnemy(enemy);

        hero.MoveToFightScene(fightGameplay.currentLevelSceneProps.HeroFightScenePoint);
        enemy.MoveToFightScene(fightGameplay.currentLevelSceneProps.EnemyFightScenePoint);

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
        stateMachine.ChangeState(fightGameplay.PlayerTurnState);
    }

    private void StartAttacking()
    {
        enemy.Attack();
        enemy.Attacked += OnEnemyAttacked;
    }

    private void OnEnemyAttacked()
    {
        hero.Hit();
        hero.ReceivedDamage += OnHeroReceivedDamage;
        areGoingToAttack = false;
    }

    private void OnHeroReceivedDamage()
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

        hero.ReceivedDamage -= OnHeroReceivedDamage;
        enemy.Attacked -= OnEnemyAttacked;
        hero.FinishedMoving -= OnChosenHeroFinishedMoving;
        enemy.FinishedMoving -= OnChosenEnemyFinishedMoving;
    }
}

