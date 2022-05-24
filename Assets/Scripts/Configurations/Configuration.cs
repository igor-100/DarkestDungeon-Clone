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
                RespawnPoint = new Vector2(-9f, -2f)
            }
        };
    }

    public List<LevelProperties> GetLevelsProperties() => levelsProperties;
}
