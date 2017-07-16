using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetChecker : MonoBehaviour {


	Player player;
	Animator playerAnimator;

	public void Start(){
		player = gameObject.GetComponentInParent<Player> ();
		playerAnimator = gameObject.GetComponentInParent<Animator> ();
	}

	public void OnTriggerExit2D (Collider2D other){
		if (other.CompareTag ("Ground")) {
			player.SetAirState (Player.AirState.air);
			playerAnimator.SetBool ("InAir", true);
		}
	}
}
