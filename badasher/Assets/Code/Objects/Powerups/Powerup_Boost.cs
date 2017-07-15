using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Boost : MonoBehaviour {

	private int boostAmount;

	public void Awake(){
		boostAmount = PlayerConstants.POWERUP_BOOST_AMOUNT;
	}

	override
	public void GetPowerup(Player player){
		player.GainBoostPower (boostAmount);
		Destroy (this.gameObject);
	}
}
