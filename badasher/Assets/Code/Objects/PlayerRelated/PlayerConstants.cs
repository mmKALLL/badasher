using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerConstants {
	public const int BOOST_POWER_MAX = 100;
	public const int BOOST_POWER_COST = 30;
	public const int BOOST_POWER_DEFAULT = 50;

	public const float DASH_COOLDOWN = 0.5f; // in milliseconds
	public const float DASH_DISTANCE = 1.0f; // modifier, in distance
	public const float BOOST_POWER_DISTANCE = 1.5f;


	public const float DASH_SPEED = 4.0f; // modifiers
	public const float BOOST_POWER_SPEED = 4.5f;
	public const float RUN_SPEED = 1.0f;
	public const float JUMP_POWER = 10; // modifier


	public const float BOOST_POWER_INPUT_BUFFER = 0.2f; // in millisecondsS


	public const int ENEMY_BP_DAMAGE = 20; // out of Boost Power Max


	public const int POWERUP_BOOST_AMOUNT = 40;
}
