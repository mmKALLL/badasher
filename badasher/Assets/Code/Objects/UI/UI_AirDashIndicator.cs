using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AirDashIndicator : MonoBehaviour {

	//TODO
	// Yellow button that is up when Airdash is available

	Player player;
	Image image;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		image = this.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.IsAirdashAvailable () && (player.GetAirState() == Player.AirState.air)){
			image.enabled = true;
		} else {
			image.enabled = false;
		}
	}
}
