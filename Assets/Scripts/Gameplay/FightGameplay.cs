using Assets.Scripts.Configurations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGameplay : MonoBehaviour, IFightGameplay, IStateable
{
    private const float DISTANCE_BETWEEN_UNITS = 2.5f;

    private LevelProperties currentLevelSceneProps;
    private IResourceManager ResourceManager;

    internal List<IHero> heroes = new List<IHero>();
    internal List<IEnemy> enemies = new List<IEnemy>();

    public StateMachine StateMachine { get; private set; }
    public PlayerTurnState PlayerTurnState { get; private set; }

    private void Awake()
    {
        ResourceManager = CompositionRoot.GetResourceManager();

        StateMachine = new StateMachine();

        PlayerTurnState = new PlayerTurnState(this);
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
        SpawnHeroes(currentLevelSceneProps.HeroTeamSpawnPoint, currentLevelSceneProps.HeroTeamProperties);
        SpawnEnemies(currentLevelSceneProps.EnemyTeamSpawnPoint, currentLevelSceneProps.EnemyTeamProperties);

        StateMachine.Initialize(PlayerTurnState);
    }

    private void SpawnHeroes(Vector2 spawnPoint, HeroTeamProperties heroTeamProperties)
    {
        var units = heroTeamProperties.Units;

        for (int i = 0; i < units.Count; i++)
        {
            var unitProps = units[i];
            GameObject unitGo = ResourceManager.CreatePrefabInstance(unitProps.CharacterType);
            unitGo.transform.position =  new Vector2(spawnPoint.x + DISTANCE_BETWEEN_UNITS * i, spawnPoint.y);
            var hero = unitGo.GetComponent<IHero>();
            hero.Id = unitProps.Id;
            hero.CharacterType = unitProps.CharacterType;
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
            GameObject unitGo = ResourceManager.CreatePrefabInstance(unitProps.CharacterType);
            unitGo.transform.position =  new Vector2(spawnPoint.x - DISTANCE_BETWEEN_UNITS * i, spawnPoint.y);
            unitGo.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            var enemy = unitGo.GetComponent<IEnemy>();
            enemy.Id = unitProps.Id;
            enemy.CharacterType = unitProps.CharacterType;
            enemy.IsClickable = false;
            enemies.Add(enemy);
        }
    }
}
