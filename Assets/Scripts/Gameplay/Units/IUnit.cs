using Assets.Scripts.Configurations;
using System;
using UnityEngine;

public interface IUnit 
{
    int Id { get; set; }
    ECharacters CharacterType { get; set; }
    Vector2 InitialPosition { get; set; }
    bool IsClickable { get; set; }
    bool IsMovingToFightScene { get; }
    bool IsMovingToBasePlace { get; }

    event Action<IUnit> Clicked;
    event Action FinishedMoving;
    event Action Attacked;
    event Action ReceivedDamage;

    void Highlight(float highlightValue);
    void StopHighlighting();
    void Hit();
    void Attack();
    void MoveToFightScene(Vector2 target);
    void MoveToBasePlace(Vector2 target);
}
