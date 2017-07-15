using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	// Stores data and has plethora of methods for interacting with

	PlayerCollisions playCol;
	PlayerMovement playMov;
	Rigidbody2D playRig;

	private int boostPower;
	private float dashCooldown;
	private bool airdashAvailable = true;
	public enum DashState {none, dash, boostPower}; // none = default run. DON'T ADD states that aren't a type of dash here.
	public enum AirState {ground, air};
	public enum LiveState {alive, invunerable, dead};
	private DashState dashState;
	private AirState airState;
	private LiveState liveState;

	public float dashDistanceRemaining;
	private Vector3 dashStartingPos;

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
		dashCooldown = PlayerConstants.DASH_COOLDOWN;
		playMov = this.GetComponent<PlayerMovement> ();
		playCol = this.GetComponent<PlayerCollisions> ();
		playRig = this.GetComponent<Rigidbody2D> ();
	}

	public void Start(){
		boostPower = PlayerConstants.BOOST_POWER_DEFAULT;
	}


	public void Update(){
		switch (dashState) {
		case DashState.none: // basic run
			switch (airState){
			case AirState.ground:
				// use method for moving forward
				break;
			case AirState.air:
				// use method for falling down
				break;
			}
			break;
		case DashState.dash:
			switch (airState){
			case AirState.ground:
				playMov.PlayerDashUpdate (this, dashStartingPos, playRig, dashDistanceRemaining);
				break;
			case AirState.air:
				// use method for airdash (needs multipurpose
				break;
			}
			break;
		case DashState.boostPower:
			/*switch (airState) {
			case AirState.ground:
				break;
			case AirState.air:
				break;
			}*/
			playMov.PlayerBoostUpdate (this, dashStartingPos, playRig, dashDistanceRemaining);
			break;
		}
	}

	public void PlayerDash(){
		// called to do everything dash related
		if (this.airState == AirState.air) {
			if (airdashAvailable == true) {
				airdashAvailable = false;
			} else {
				return;
			}
		}
		this.dashState = DashState.dash;
		StartCoroutine(DashCooldownReduce());
		dashStartingPos = this.transform.position;
		dashDistanceRemaining = PlayerConstants.DASH_DISTANCE; 	// doesn't actually matter, unless player goes to DashJump on 
																// the same frame he starts dashing which shouldn't happen anyway
	}

	public void PlayerBoostPower(){
		if (SpendBoostPower) {
			this.dashState = DashState.boostPower;
			dashStartingPos = this.transform.position;
			dashDistanceRemaining = PlayerConstants.BOOST_POWER_DISTANCE;
		}
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
			this.liveState == LiveState.dead;
		}
	}

	public bool SpendBoostPower(){
		if (boostPower >= PlayerConstants.BOOST_POWER_COST) {
			return true;
		}
		return false;
	}


	private IEnumerator DashCooldownReduce(){
		while (dashCooldown > 0) {
			dashCooldown -= Time.deltaTime;
			yield return null;
		}
		yield return new WaitForEndOfFrame();
	}
}
