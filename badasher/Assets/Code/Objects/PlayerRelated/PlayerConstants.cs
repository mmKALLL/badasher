using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerConstants {
	public const int BOOST_POWER_MAX = 100;
	public const int BOOST_POWER_COST = 20;
	public const int BOOST_POWER_DEFAULT = 70;

	public const float DASH_SPEED = 24.0f; // modifiers
	public const float BOOST_POWER_SPEED = 35.0f;
	public const float RUN_SPEED = 12.0f;
	public const float JUMP_POWER = 0.7f; // modifier
	public const float JUMP_DASH_DASHDISTANCE_ADD_PERCENTAGE = 0.6f; // amount of original dashdistance to add to jumpdash
	public const float JUMP_DASH_AFTER_MOMENTUM_MODIFIER = 1.7f; // higher means less velocity after jumpDash

	public const float DASH_COOLDOWN = 0.22f; // in seconds
	public const float DASH_GROUND_END_LAG = 0.01f; // in seconds
	public const float DASH_DISTANCE = DASH_SPEED * 0.3f; // modifier, in distance
	public const float BOOST_POWER_DISTANCE = 1.1f*DASH_DISTANCE * (BOOST_POWER_SPEED / DASH_SPEED);

	public const float DASH_SLOWDOWN_PERFECT_PERCENTAGE = 0.15f;
	public const float DASH_SLOWDOWN_END_PERCENTEGE = 0.1f;
	public const float DASH_SLOWDOWN_END_MODIFIER = 1.5f;

	public const float BOOST_POWER_INPUT_BUFFER = 0.8f; // in seconds // TODO unused...


	public const int ENEMY_BP_DAMAGE = 30; // out of Boost Power Max
	public const float DAMAGE_INVUNERABILITY_TIME = 0.6f; // in seconds


	public const int POWERUP_BOOST_AMOUNT = 20;

	public const float DIRECTION_PERCENTAGE_MAX = 0.37f;
}
