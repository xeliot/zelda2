using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticPlayer : object {
	public static Vector3 spawnPosition = Vector3.forward;
	private static float health = Config.MAX_HEALTH;
	private static float magic;
	private static float exp;
	public static float nextExp;
	//used for smooth move from inner to outer
	public static float musicTime = 0;
	//used for music time
	public static float worldTime = 0;
	//0 health //1 strength //2 magic
	public static float[] levels;

	public static void setHealth(float h){
		health = h;
	}

	public static float getHealth(){
		return health;
	}

	public static void setMagic(float m){
		magic = m;
	}

	public static float getMagic(){
		return magic;
	}

	public static void setExp(float e){
		exp = e;
	}

	public static float getExp(){
		return exp;
	}


}
