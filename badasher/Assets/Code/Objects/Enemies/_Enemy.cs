using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Enemy : MonoBehaviour {
	// superclass for enemies
	// contains just the collision

	protected int damage;

	public void OnRunThrough (Player player){

	}
	public void OnDashThrough (Player player){
		
	}
	public void OnAirDashThrough (Player player){
		
	}

	// default method for colliding with enemy
	public void DamagePlayerDefault(Player player) {
		player.TakeDamage (this.damage);
	}
}
