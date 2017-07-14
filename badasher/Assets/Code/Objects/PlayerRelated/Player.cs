using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	// Stores data and has plethora of methods for interacting with

	PlayerCollisions playCol;
	PlayerMovement playMov;

	private int boostPower;
	private int dashCooldown = 0;
	private bool airdashAvailable = true;
	private enum DashState {none, dash, boostPower};
	private enum AirState {ground, air};
	private enum LiveState {alive, invunerable, dead};
	private DashState dashState;
	private AirState airstate;
	private LiveState liveState;

	#region gets
	public int GetBoostPower(){
		return this.boostPower;
	}

	public int GetBoostPower(){
		return this.dashCooldown = 0;
	}
	#endregion

	public void Awake(){
		dashCooldown = PlayerConstants.BOOST_POWER_DEFAULT;
		playMov = this.GetComponent<PlayerMovement> ();
		playCol = this.GetComponent<PlayerCollisions> ();
	}

	public void Start(){
		boostPower = PlayerConstants.BOOST_POWER_DEFAULT;
	}

	public void PlayerDash(){
		// called to do everything dash related
		StartCoroutine(DashCooldownReduce());
		playMov.PlayerDash ();
	}

	public void GainBoostPower(int gainAmount){
		this.boostPower += gainAmount;
		if (boostPower > PlayerConstants.BOOST_POWER_MAX) {
			boostPower = PlayerConstants.BOOST_POWER_MAX;
		}
	}

	public void TakeDamage(int damageAmount){
		boostPower -= damageAmount;
		if (boostPower < 0) {
			// player is dead
		}
	}

	public bool SpendBoostPower(){
		if (boostPower > PlayerConstants.BOOST_POWER_COST) {
			// activate boost power
			return true;
		}
		return false;
	}


	private IEnumerator DashCooldownReduce(){
		dashCooldown -= Time.deltaTime;
		yield return WaitForEndOfFrame;
		if (dashCooldown > 0) {
			StartCoroutine (DashCooldownReduce ());
		}
	}
}
