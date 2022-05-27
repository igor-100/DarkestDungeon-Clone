using System;

public interface IPlayerActions : IScreen
{
    event Action AttackClicked;
    event Action WaitClicked;

    void ShowPickEnemyText();
    void HidePickEnemyText();
}
