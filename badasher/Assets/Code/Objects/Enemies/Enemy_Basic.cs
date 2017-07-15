using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : _Enemy {
	// Dies to dash
	// Most basic of movement
	// Utilizes dash slowdown

	override
	public void OnDashThrough(Player player){
		TakeDamageFromAttack (player);
	}
}
