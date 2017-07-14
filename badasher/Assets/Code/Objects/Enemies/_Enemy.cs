using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Enemy : MonoBehaviour {
	// superclass for enemies
	// contains just the collision

	protected int damage;

	public abstract void OnRunThrough (Player player);
	public abstract void OnDashThrough (Player player);
	public abstract void OnAirDashThrough (Player player);
}
