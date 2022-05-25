public class ViewFactory : IViewFactory
{
    private IUIRoot UIRoot;
    private IResourceManager ResourceManager;

    public ViewFactory(IUIRoot uiRoot, IResourceManager resourceManager)
    {
        UIRoot = uiRoot;
        ResourceManager = resourceManager;
    }

    public IPlayerActionsView CreatePlayerActionsView()
    {
        var view = ResourceManager.CreatePrefabInstance<IPlayerActionsView, EViews>(EViews.PlayerActionsView);
        view.SetParent(UIRoot.OverlayCanvas);

        return view;
    }
}
