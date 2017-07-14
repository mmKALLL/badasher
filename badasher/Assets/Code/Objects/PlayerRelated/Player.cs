using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	// Stores data and has plethora of methods for interacting with

	PlayerCollisions playCol;
	PlayerMovement playMov;

	private int boostPower;
	private float dashCooldown;
	private bool airdashAvailable = true;
	public enum DashState {none, dash, boostPower};
	public enum AirState {ground, air};
	public enum LiveState {alive, invunerable, dead};
	private DashState dashState;
	private AirState airState;
	private LiveState liveState;

	#region gets
	public int GetBoostPower(){
		return this.boostPower;
	}

	public float GetDashCooldown(){
		return this.dashCooldown;
	}

	public DashState GetDashState(){
		return this.dashState;
	}

	public AirState GetAirState(){
		return this.airState;
	}

	public LiveState GetLiveState(){
		return this.liveState;

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
		this.dashState = DashState.dash;
		StartCoroutine(DashCooldownReduce());
		playMov.PlayerDash (this);
	}

	public void PlayerBoostPower(){
		
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
		yield return new WaitForEndOfFrame();
		if (dashCooldown > 0) {
			StartCoroutine (DashCooldownReduce ());
		}
	}
}
