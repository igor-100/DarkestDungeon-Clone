﻿using Assets.Scripts.Configurations;
using System.Collections;
using UnityEngine;

public class Hero : MonoBehaviour, IHero
{
    private UnitProperties heroProperties;

    private void Awake()
    {

    }

    public void Hit(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void SetUnitProperties(UnitProperties unitProperties)
    {
        this.heroProperties = unitProperties;
    }

    public void MoveToFightScene()
    {
        throw new System.NotImplementedException();
    }
}
