using UnityEngine;

namespace Assets.Scripts.Configurations
{
    public struct LevelProperties
    {
        public ELevels Level;
        public Vector2 HeroTeamSpawnPoint;
        public Vector2 EnemyTeamSpawnPoint;
        public Vector2 CentralFightScenePoint;
        public HeroTeamProperties HeroTeamProperties;
        public EnemyTeamProperties EnemyTeamProperties;
    }
}
