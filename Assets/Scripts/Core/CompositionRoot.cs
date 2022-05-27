using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    private static IUIRoot UIRoot;
    private static GameObject EnvironmentGameObject;
    private static IViewFactory ViewFactory;
    private static IGameCamera GameCamera;
    private static ISceneLoader SceneLoader;
    private static IResourceManager ResourceManager;
    private static IConfiguration Configuration;

    private static IFightGameplay FightGameplay;
    private static IPlayerActions PlayerActions;

    private void OnDestroy()
    {
        EnvironmentGameObject = null;
        GameCamera = null;
        Configuration = null;
        UIRoot = null;
        ViewFactory = null;
        FightGameplay = null;
        PlayerActions = null;

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

    public static IGameCamera GetGameCamera()
    {
        if (GameCamera == null)
        {
            var resourceManager = GetResourceManager();
            GameCamera = resourceManager.CreatePrefabInstance<IGameCamera, ECamera>(ECamera.Cameras);
        }

        return GameCamera;
    }

    public static GameObject GetEnvironment()
    {
        if (EnvironmentGameObject == null)
        {
            var resourceManager = GetResourceManager();
            EnvironmentGameObject = resourceManager.CreatePrefabInstance<ELevels>(ELevels.Env_1);
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

    public static IFightGameplay GetFightGameplay()
    {
        if (FightGameplay == null)
        {
            var gameObject = new GameObject("FightGameplay");
            FightGameplay = gameObject.AddComponent<FightGameplay>();
        }

        return FightGameplay;
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

    public static IPlayerActions GetPlayerActions()
    {
        if (PlayerActions == null)
        {
            var gameObject = new GameObject("PlayerActions");
            PlayerActions = gameObject.AddComponent<PlayerActions>();
        }

        return PlayerActions;
    }
}
