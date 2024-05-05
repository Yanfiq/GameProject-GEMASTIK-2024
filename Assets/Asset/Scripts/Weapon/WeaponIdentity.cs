using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 0)]
public class WeaponIdentity : ScriptableObject
{
    public string Name;
    public float dmg;
    public float fireRate;
}
