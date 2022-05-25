using System;

public interface IPlayerActionsView : IView
{
    event Action AttackClicked;
    event Action WaitClicked;

    void ShowAttackButton();
    void HideAttackButton();
}
