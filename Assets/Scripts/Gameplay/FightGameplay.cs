using Assets.Scripts.Configurations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGameplay : MonoBehaviour, IFightGameplay, IStateable
{
    private const float DISTANCE_BETWEEN_UNITS = 2.5f;

    internal LevelProperties currentLevelSceneProps;
    private IResourceManager resourceManager;

    internal IPlayerActions playerActions;
    internal IGameCamera gameCamera;

    internal int roundNumber;

    internal IHero chosenHero;
    internal IEnemy chosenEnemy;
    
    internal List<IHero> heroes { get; private set; } = new List<IHero>();
    internal List<IHero> availableHeroes { get; private set; }
    internal List<IEnemy> enemies { get; private set; } = new List<IEnemy>();
    internal List<IEnemy> availableEnemies { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public PlayerTurnState PlayerTurnState { get; private set; }
    public PlayerAttackState PlayerAttackState { get; private set; }
    public EnemyAttackState EnemyAttackState { get; private set; }

    private void Awake()
    {
        resourceManager = CompositionRoot.GetResourceManager();
        gameCamera = CompositionRoot.GetGameCamera();

        playerActions = CompositionRoot.GetPlayerActions();
        playerActions.Hide();

        StateMachine = new StateMachine();
        PlayerTurnState = new PlayerTurnState(this);
        PlayerAttackState = new PlayerAttackState(this);
        EnemyAttackState = new EnemyAttackState(this);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetLevelPropsAndInit(LevelProperties levelProperties)
    {
        this.currentLevelSceneProps = levelProperties;

        InitLevel();
    }

    private void InitLevel()
    {
        gameCamera.SetTarget(currentLevelSceneProps.CameraProps.InitialPosition);
        gameCamera.SetLensSize(currentLevelSceneProps.CameraProps.InitialLensSize);

        SpawnHeroes(currentLevelSceneProps.HeroTeamSpawnPoint, currentLevelSceneProps.HeroTeamProperties);
        SpawnEnemies(currentLevelSceneProps.EnemyTeamSpawnPoint, currentLevelSceneProps.EnemyTeamProperties);

        roundNumber = 1;
        Debug.Log("new round: " + roundNumber);
        availableEnemies = new List<IEnemy>(enemies);
        availableHeroes = new List<IHero>(heroes);

        StateMachine.Initialize(PlayerTurnState);
    }

    internal void RemoveAvailableHero(IHero hero)
    {
        availableHeroes.Remove(hero);
    }

    internal void RemoveAvailableEnemy(IEnemy enemy)
    {
        availableEnemies.Remove(enemy);
    }

    internal void StartNewRound()
    {
        roundNumber++;
        Debug.Log("new round: " + roundNumber);
        availableEnemies = new List<IEnemy>(enemies);
        availableHeroes = new List<IHero>(heroes);
    }

    private void SpawnHeroes(Vector2 spawnPoint, HeroTeamProperties heroTeamProperties)
    {
        var units = heroTeamProperties.Units;

        for (int i = 0; i < units.Count; i++)
        {
            var unitProps = units[i];
            GameObject unitGo = resourceManager.CreatePrefabInstance(unitProps.CharacterType);
            unitGo.transform.position = new Vector2(spawnPoint.x + DISTANCE_BETWEEN_UNITS * i, spawnPoint.y);
            var hero = unitGo.GetComponent<IHero>();
            hero.Id = unitProps.Id;
            hero.CharacterType = unitProps.CharacterType;
            hero.InitialPosition = unitGo.transform.position;
            hero.IsClickable = true;
            heroes.Add(hero);
        }
    }

    private void SpawnEnemies(Vector2 spawnPoint, EnemyTeamProperties enemyTeamProperties)
    {
        var units = enemyTeamProperties.Units;

        for (int i = 0; i < units.Count; i++)
        {
            var unitProps = units[i];
            GameObject unitGo = resourceManager.CreatePrefabInstance(unitProps.CharacterType);
            unitGo.transform.position =  new Vector2(spawnPoint.x - DISTANCE_BETWEEN_UNITS * i, spawnPoint.y);
            unitGo.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            var enemy = unitGo.GetComponent<IEnemy>();
            enemy.Id = unitProps.Id;
            enemy.CharacterType = unitProps.CharacterType;
            enemy.InitialPosition = unitGo.transform.position;
            enemy.IsClickable = false;
            enemies.Add(enemy);
        }
    }
}
