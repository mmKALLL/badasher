using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Air : _Enemy {

	public void Awake(){
		this.damage = PlayerConstants.ENEMY_BP_DAMAGE;
	}

	override
	public void OnAirDashThrough(Player player){
		TakeDamageFromAttack (player);
	}
}
