using Assets.Scripts.Configurations;
using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    private UnitProperties unitProperties;

    public int Id { get; set; }
    public ECharacters CharacterType { get; set; }
    public bool IsClickable { get; set; }


    public event Action<IUnit> Clicked = (unit) => { };

    private void Awake()
    {

    }

    private void OnMouseDown()
    {
        if (IsClickable)
        {
            Clicked(this);
        }
    }

    public void Hit(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void SetUnitProperties(UnitProperties unitProperties)
    {
        this.unitProperties = unitProperties;
    }

    public void MoveToFightScene()
    {
        throw new System.NotImplementedException();
    }

    public void Hit()
    {
        throw new System.NotImplementedException();
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
