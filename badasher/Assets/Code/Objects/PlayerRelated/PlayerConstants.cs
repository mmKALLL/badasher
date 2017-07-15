using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerConstants {
	public const int BOOST_POWER_MAX = 100;
	public const int BOOST_POWER_COST = 30;
	public const int BOOST_POWER_DEFAULT = 50;

	public const float DASH_COOLDOWN = 0.5f; // in seconds
	public const float DASH_GROUND_END_LAG = 0.18f; // in seconds
	public const float DASH_DISTANCE = 1.0f; // modifier, in distance
	public const float BOOST_POWER_DISTANCE = 1.5f;

	public const float DASH_SLOWDOWN_PERFECT_PERCENTAGE = 0.15f;
	public const float DASH_SLOWDOWN_END_PERCENTEGE = 0.1f;
	public const float DASH_SLOWDOWN_END_MODIFIER = 1.5f;


	public const float DASH_SPEED = 4.0f; // modifiers
	public const float BOOST_POWER_SPEED = 4.5f;
	public const float RUN_SPEED = 1.0f;
	public const float JUMP_POWER = 10; // modifier
	public const float JUMP_DASH_DASHDISTANCE_ADD_PERCENTAGE = 0.2f; // amount of original dashdistance to add to jumpdash


	public const float BOOST_POWER_INPUT_BUFFER = 0.2f; // in seconds


	public const int ENEMY_BP_DAMAGE = 20; // out of Boost Power Max
	public const float DAMAGE_INVUNERABILITY_TIME = 0.3f; // in seconds


	public const int POWERUP_BOOST_AMOUNT = 40;

	public const float DIRECTION_PERCENTAGE_MAX = 80;
}
