using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets / PlayerData")]
public class PlayerData : ScriptableObject
{
    public float movementSpeed;
    public float rotationSpeed;
}
