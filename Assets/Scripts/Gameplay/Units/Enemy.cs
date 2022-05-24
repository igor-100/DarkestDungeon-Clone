using Assets.Scripts.Configurations;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private UnitProperties enemyProperties;

    private void Awake()
    {

    }

    public void Hit(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void SetUnitProperties(UnitProperties unitProperties)
    {
        this.enemyProperties = unitProperties;
    }

    public void MoveToFightScene()
    {
        throw new System.NotImplementedException();
    }
}
