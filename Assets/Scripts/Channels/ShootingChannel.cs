using System;
using UnityEngine;

[CreateAssetMenu(fileName ="ShootingChannel", menuName ="Channels/ShootingChannel", order = 0)]
public class ShootingChannel : ScriptableObject
{
    public Action<Vector3> Shoot;
}
