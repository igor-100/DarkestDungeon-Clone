using UnityEngine;

namespace Assets.Scripts.Configurations
{
    public struct LevelProperties
    {
        public ELevels Level;
        public Camera CameraProps;
        public Vector2 HeroTeamSpawnPoint;
        public Vector2 EnemyTeamSpawnPoint;
        public Vector2 HeroFightScenePoint;
        public Vector2 EnemyFightScenePoint;
        public HeroTeamProperties HeroTeamProperties;
        public EnemyTeamProperties EnemyTeamProperties;

        public struct Camera
        {
            public Vector3 InitialPosition;
            public float InitialLensSize;
            public Vector3 FightScenePosition;
            public float FightSceneLensSize;
        }
    }
}
