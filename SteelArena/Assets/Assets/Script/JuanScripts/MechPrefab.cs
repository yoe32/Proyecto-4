using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MechPrefab : MonoBehaviour
{

	public int mountedOn;
	public MechType mechType = MechType.Any;
	public List<MechSize> partSizes = new List<MechSize>();
	public MechMountType mountType = MechMountType.None;
	public List<MechMountType> mountableOn = new List<MechMountType>();
	public bool isWeapon = false;

	public List<MechMount> mounts = new List<MechMount>();
}