using UnityEngine;

public class LevelScene : MonoBehaviour
{
    private IGameCamera GameCam;
    private IPlayerInput PlayerInput;
    private IConfiguration Configuration;
    private ISceneLoader SceneLoader;
    private IPlayer Player;

    private Camera gameCamComponent;

    private void Awake()
    {
        GameCam = CompositionRoot.GetGameCamera();
        PlayerInput = CompositionRoot.GetPlayerInput();
        Configuration = CompositionRoot.GetConfiguration();
        SceneLoader = CompositionRoot.GetSceneLoader();
        Player = CompositionRoot.GetPlayer();

        var environment = CompositionRoot.GetEnvironment();

        //var uiRoot = CompositionRoot.GetUIRoot();

        Player.Died += OnPlayerDied;
    }

    private void Start()
    {
        GameCam.SetTarget(Player.Transform);
        Player.Transform.position = Configuration.GetLevelsProperties()[0].RespawnPoint;
    }


    private void OnPlayerDied()
    {
        SceneLoader.RestartScene();
    }

    private void OnPlayerDying()
    {
        PlayerInput.Disable();
    }
}
