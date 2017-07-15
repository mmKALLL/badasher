using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : _Enemy {
	// Dies to dash
	// Most basic of movement
	// Utilizes dash slowdown

	public void Awake(){
		this.damage = PlayerConstants.ENEMY_BP_DAMAGE;
	}


	override
	public void OnDashThrough (Player player){
		TakeDamageFromAttack(player);
	}
}
