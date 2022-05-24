using Assets.Scripts.Configurations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGameplay : MonoBehaviour, IFightGameplay
{
    private const float DISTANCE_BETWEEN_UNITS = 2.5f;

    private LevelProperties currentLevelSceneProps;
    private IResourceManager ResourceManager;

    private void Awake()
    {
        ResourceManager = CompositionRoot.GetResourceManager();
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
    }

    private void SpawnHeroes(Vector2 spawnPoint, HeroTeamProperties heroTeamProperties)
    {
        var units = heroTeamProperties.Units;

        for (int i = 0; i < units.Count; i++)
        {
            var unitProps = units[0];
            GameObject unitGo = ResourceManager.CreatePrefabInstance(unitProps.CharacterType);
            unitGo.transform.position =  new Vector2(spawnPoint.x + DISTANCE_BETWEEN_UNITS * i, spawnPoint.y);
            var hero = unitGo.GetComponent<IHero>();
        }
    }

    private void SpawnEnemies(Vector2 spawnPoint, EnemyTeamProperties enemyTeamProperties)
    {
        var units = enemyTeamProperties.Units;

        for (int i = 0; i < units.Count; i++)
        {
            var unitProps = units[0];
            GameObject unitGo = ResourceManager.CreatePrefabInstance(unitProps.CharacterType);
            unitGo.transform.position =  new Vector2(spawnPoint.x - DISTANCE_BETWEEN_UNITS * i, spawnPoint.y);
            unitGo.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            var enemy = unitGo.GetComponent<IEnemy>();
        }
    }
}
