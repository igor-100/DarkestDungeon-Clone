using Assets.Scripts.Configurations;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : IConfiguration
{
    private readonly List<LevelProperties> levelsProperties;

    public Configuration()
    {
        levelsProperties = new List<LevelProperties>
        {
            new LevelProperties()
            {
                Level = ELevels.Env_Forest_1,
                RespawnPoint = new Vector2(-9f, -2f),
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
}
