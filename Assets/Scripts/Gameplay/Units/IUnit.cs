using Assets.Scripts.Configurations;
using System;

public interface IUnit 
{
    int Id { get; set; }
    ECharacters CharacterType { get; set; }
    bool IsClickable { get; set; }


    event Action<IUnit> Clicked;

    void SetUnitProperties(UnitProperties unitProperties);
    void Hit();
    void Attack();
    void MoveToFightScene();
}
