using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Enemy : MonoBehaviour {
	// superclass for enemies
	// contains just the collision

	protected int damage;


	// override these in childclasses
	virtual
	public void OnRunThrough (Player player){
		DamagePlayerDefault (player);
	}
	virtual
	public void OnDashThrough (Player player){
		DamagePlayerDefault (player);
	}
	virtual
	public void OnAirDashThrough (Player player){
		DamagePlayerDefault (player);
	}

	// default method for colliding with enemy
	public void DamagePlayerDefault(Player player) {
		player.TakeDamage (this.damage);
	}


	public void TakeDamageFromAttack(Player player){
		Destroy (this);
	}
}
