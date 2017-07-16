using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BoostPowerBar : MonoBehaviour {

	// TODO
	// Boostpower bar that updates when boost power is changed

	Player player;
	Image image;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		image = this.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		image.fillAmount = (float)player.GetBoostPower () / (float)PlayerConstants.BOOST_POWER_MAX;
	}
}
