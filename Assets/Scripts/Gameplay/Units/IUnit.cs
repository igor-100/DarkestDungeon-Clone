using Assets.Scripts.Configurations;

public interface IUnit 
{
    void SetUnitProperties(UnitProperties unitProperties);
    void Hit(float damage);
    void MoveToFightScene();
}
