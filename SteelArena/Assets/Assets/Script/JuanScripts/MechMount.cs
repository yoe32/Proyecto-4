using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class MechMount
{
	public List<MechMountType> mountTypes = new List<MechMountType>();
	public bool allowsWeapon = false;
	public bool canWeaponRotate = true;
	public float minRotation = -15;
	public float maxRotation = 15;
	public Transform mountPoint;
}