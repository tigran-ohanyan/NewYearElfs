using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour, IMovable
{
    public float Speed => 3f;
    public Transform Transform => transform;
}
