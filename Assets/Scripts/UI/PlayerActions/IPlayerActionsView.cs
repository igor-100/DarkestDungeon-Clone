using System;

public interface IPlayerActionsView : IView
{
    event Action AttackClicked;
    event Action WaitClicked;

    void ShowPickEnemyText();
    void HidePickEnemyText();
}
