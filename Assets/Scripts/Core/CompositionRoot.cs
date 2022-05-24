using UnityEngine;
using UnityEngine.EventSystems;

public class CompositionRoot : MonoBehaviour
{
    private static IUIRoot UIRoot;
    private static GameObject EnvironmentGameObject;
    private static IPlayerInput PlayerInput;
    private static IViewFactory ViewFactory;
    private static IGameCamera GameCamera;
    private static ISceneLoader SceneLoader;
    //private static IPlayer Player;
    private static IResourceManager ResourceManager;
    private static IConfiguration Configuration;

    private void OnDestroy()
    {
        EnvironmentGameObject = null;
        PlayerInput = null;
        //Player = null;
        GameCamera = null;
        Configuration = null;
        UIRoot = null;
        ViewFactory = null;

        var resourceManager = GetResourceManager();
        resourceManager.ResetPools();
    }

    public static IResourceManager GetResourceManager()
    {
        if (ResourceManager == null)
        {
            ResourceManager = new ResourceManager();
        }

        return ResourceManager;
    }

    //public static IPlayer GetPlayer()
    //{
    //    if (Player == null)
    //    {
    //        var resourceManager = GetResourceManager();
    //        Player = resourceManager.CreatePrefabInstance<IPlayer, ECharacters>(ECharacters.Marty);
    //    }

    //    return Player;
    //}

    public static IGameCamera GetGameCamera()
    {
        if (GameCamera == null)
        {
            var resourceManager = GetResourceManager();
            GameCamera = resourceManager.CreatePrefabInstance<IGameCamera, EComponents>(EComponents.Cameras);
        }

        return GameCamera;
    }

    public static GameObject GetEnvironment()
    {
        if (EnvironmentGameObject == null)
        {
            var resourceManager = GetResourceManager();
            EnvironmentGameObject = resourceManager.CreatePrefabInstance<ELevels>(ELevels.Env_Forest_1);
        }

        return EnvironmentGameObject;
    }

    public static ISceneLoader GetSceneLoader()
    {
        if (SceneLoader == null)
        {
            var resourceManager = GetResourceManager();
            SceneLoader = resourceManager.CreatePrefabInstance<ISceneLoader, EComponents>(EComponents.SceneLoader);
        }

        return SceneLoader;
    }

    public static IConfiguration GetConfiguration()
    {
        if (Configuration == null)
        {
            Configuration = new Configuration();
        }

        return Configuration;
    }

    public static IPlayerInput GetPlayerInput()
    {
        if (PlayerInput == null)
        {
            var gameObject = new GameObject("PlayerInput");
            PlayerInput = gameObject.AddComponent<PlayerInput>();
        }

        return PlayerInput;
    }

    public static IUIRoot GetUIRoot()
    {
        if (UIRoot == null)
        {
            var resourceManager = GetResourceManager();
            UIRoot = resourceManager.CreatePrefabInstance<IUIRoot, EComponents>(EComponents.UIRoot);
        }

        return UIRoot;
    }

    public static IViewFactory GetViewFactory()
    {
        if (ViewFactory == null)
        {
            var uiRoot = GetUIRoot();
            var resourceManager = GetResourceManager();

            ViewFactory = new ViewFactory(uiRoot, resourceManager);
        }

        return ViewFactory;
    }
}
