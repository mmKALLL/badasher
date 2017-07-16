using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DashCooldown : MonoBehaviour {

	//TODO
	// bar that depletes on dash use and fills according to DashCooldown

	Player player;
	Image image;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		image = this.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		image.fillAmount = 1 - (player.GetDashCooldown () / PlayerConstants.DASH_COOLDOWN);
	}
}
