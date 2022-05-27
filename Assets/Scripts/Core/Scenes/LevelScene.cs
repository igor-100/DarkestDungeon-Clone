using UnityEngine;

public class LevelScene : MonoBehaviour
{
    private void Awake()
    {
        var gameCam = CompositionRoot.GetGameCamera();
        var configuration = CompositionRoot.GetConfiguration();
        var sceneLoader = CompositionRoot.GetSceneLoader();

        var environment = CompositionRoot.GetEnvironment();
        var fightGameplay = CompositionRoot.GetFightGameplay();
        fightGameplay.SetLevelPropsAndInit(configuration.GetLevelsProperties()[0]);

        var uiRoot = CompositionRoot.GetUIRoot();
    }
}
