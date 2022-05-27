using System;
using UnityEngine;

public class PlayerActions : MonoBehaviour, IPlayerActions
{
    private IPlayerActionsView View;

    public event Action AttackClicked = () => { };
    public event Action WaitClicked = () => { };

    private void Awake()
    {
        var viewFactory = CompositionRoot.GetViewFactory();

        View = viewFactory.CreatePlayerActionsView();

        View.AttackClicked += OnAttackClicked;
        View.WaitClicked += OnWaitClicked;
    }

    private void OnAttackClicked()
    {
        AttackClicked();
    }

    private void OnWaitClicked()
    {
        WaitClicked();
    }

    public void Hide()
    {
        View.Hide();
    }

    public void Show()
    {
        View.Show();
    }

    public void ShowPickEnemyText()
    {
        View.ShowPickEnemyText();
    }

    public void HidePickEnemyText()
    {
        View.HidePickEnemyText();
    }
}
