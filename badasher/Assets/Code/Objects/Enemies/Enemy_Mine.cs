using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mine : _Enemy {

	public void Awake(){
		this.damage = PlayerConstants.ENEMY_BP_DAMAGE;
	}
}
