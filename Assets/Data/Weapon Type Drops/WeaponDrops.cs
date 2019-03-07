using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Drop Data", order = 1)]
public class WeaponDrops : ScriptableObject {

    public List<GameObject> pickups;

    public GameObject guaranteeDrop;

    public weaponType weapon = weaponType.normal;


}
