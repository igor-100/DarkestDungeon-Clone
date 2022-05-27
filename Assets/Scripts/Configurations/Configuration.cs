using Assets.Scripts.Configurations;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : IConfiguration
{
    private readonly List<LevelProperties> levelsProperties;
    private readonly CameraProperties cameraProperties;
    private readonly MinerProperties minerProperties;

    public Configuration()
    {
        cameraProperties = new CameraProperties()
        {
            LensSizeUpdateSpeed = 5f,
            PositionUpdateSpeed = 5f
        };

        minerProperties = new MinerProperties()
        {
            DamageAnim = "Damage",
            AttackAnim = "PickaxeCharge",
            IdleAnim = "Idle",
            FillPhaseShaderParam = "_FillPhase",
            MoveSpeed = 12f,
            ScaleChangingSpeed = 10f,
            BaseLocalScale = new Vector3(0.9f, 0.9f, 0),
            FightLocalScale = new Vector3(1.15f, 1.15f, 0)
        };

        levelsProperties = new List<LevelProperties>
        {
            new LevelProperties()
            {
                Level = ELevels.Env_1,
                CameraProps = new LevelProperties.Camera()
                {
                    InitialPosition = new Vector3(0, 0, -50f),
                    InitialLensSize = 9f,
                    FightScenePosition = new Vector3(0, -4f, -50f),
                    FightSceneLensSize = 6f
                },
                HeroTeamSpawnPoint = new Vector2(-12f, -7.5f),
                EnemyTeamSpawnPoint = new Vector2(12f, -7.5f),
                HeroFightScenePoint = new Vector2(-2f, -8f),
                EnemyFightScenePoint = new Vector2(2f, -8f),
                HeroTeamProperties = new HeroTeamProperties()
                {
                    Units = new List<UnitProperties>()
                    {
                        new UnitProperties()
                        {
                            Id = 0,
                            CharacterType = ECharacters.Hero_Miner
                        },
                        new UnitProperties()
                        {
                            Id = 1,
                            CharacterType = ECharacters.Hero_Miner
                        },
                        new UnitProperties()
                        {
                            Id = 2,
                            CharacterType = ECharacters.Hero_Miner
                        },
                        new UnitProperties()
                        {
                            Id = 3,
                            CharacterType = ECharacters.Hero_Miner
                        },
                    }
                },
                EnemyTeamProperties = new EnemyTeamProperties()
                {
                    Units = new List<UnitProperties>()
                    {
                        new UnitProperties()
                        {
                            Id = 0,
                            CharacterType = ECharacters.Enemy_Miner
                        },
                        new UnitProperties()
                        {
                            Id = 1,
                            CharacterType = ECharacters.Enemy_Miner
                        },
                        new UnitProperties()
                        {
                            Id = 2,
                            CharacterType = ECharacters.Enemy_Miner
                        },
                        new UnitProperties()
                        {
                            Id = 3,
                            CharacterType = ECharacters.Enemy_Miner
                        },
                    }
                }
            }
        };
    }

    public List<LevelProperties> GetLevelsProperties() => levelsProperties;
    public CameraProperties GetCameraProperties() => cameraProperties;
    public MinerProperties GetMinerProperties() => minerProperties;
}
