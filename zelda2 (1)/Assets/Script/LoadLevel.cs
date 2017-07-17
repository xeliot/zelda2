using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
	//leave type as 0 and enter as true and none of my edits will apply
	[SerializeField]
	[Tooltip("Level to load on collision (Case sensitive!)")]
	private string levelToLoad;

	[SerializeField]
	[Tooltip("Time to wait between collsion and when scene loading starts (0 to make it not wait)")]
	[Range(0,50)]
	private float waitTime;

	[SerializeField]
	[Tooltip("Tag of the player/thing you want this to work with")]
	private string tagToCheck = "Player";

	[SerializeField]
	[Tooltip("Type of placement: 0 normal, 1 radius, 2 hardcode")]
	private int Type;

	[SerializeField]
	[Tooltip("Only use with hardCode")]
	private Vector3 hardPosition;

	[SerializeField]
	[Tooltip("Only use with radius")]
	private float radius;
	// May be usefull if we want to activate something after the player picks up something
	public bool isInteractable = true;
	public bool enter = true;

	private GameObject player;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (enter) {
			if (!isInteractable)
				return;
		
			if (other.gameObject.tag == tagToCheck) {
				player = other.gameObject;
				if (waitTime != 0F) {
					StartCoroutine (LoadLevelWithDelay (waitTime));
				} else {
					StartCoroutine (LoadLevelWithoutDelay ());
				}
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (!enter) {
			if (!isInteractable)
				return;

			if (other.gameObject.tag == tagToCheck) {
				player = other.gameObject;
				if (waitTime != 0F) {
					StartCoroutine (LoadLevelWithDelay (waitTime));
				} else {
					StartCoroutine (LoadLevelWithoutDelay ());
				}
			}
		}
	}
	IEnumerator LoadLevelWithDelay(float time)
	{
		yield return new WaitForSeconds (time);
		StartCoroutine (LoadLevelWithoutDelay ());
		yield return null;
	}
	IEnumerator LoadLevelWithoutDelay()
	{
		// Splits up the work load
		switch (Type) {
		case 1:
			Vector3 dir = player.transform.position.normalized;
			StaticPlayer.spawnPosition = dir * radius;
			break;
		case 2:
			StaticPlayer.spawnPosition = hardPosition;
			break;
		//runs on case 0
		default:
			//player position will not assign if z is not zero
			//so this will not move the player from where they originally were in the scene
			StaticPlayer.spawnPosition =  Vector3.forward;
			break;
		}
		Debug.Log (StaticPlayer.spawnPosition);
		if (this.GetComponent<AudioSource>() != null && (levelToLoad == "MapOuter" || levelToLoad == "MapInner")) {
			StaticPlayer.worldTime = Time.time;
			StaticPlayer.musicTime = this.GetComponent<AudioSource> ().time;
		} else {
			StaticPlayer.musicTime = 0;
		}
		SceneManager.LoadScene (levelToLoad);
		//I am not sure if this line will run so I put it before and after just in case
		if (this.GetComponent<AudioSource>() != null && (levelToLoad == "MapOuter" || levelToLoad == "MapInner")) {
			StaticPlayer.worldTime = Time.time;
			StaticPlayer.musicTime = this.GetComponent<AudioSource> ().time;
		}

		yield return null;
	}
	void Awake ()
	{
		if (this.GetComponent<AudioSource> () != null) {
			Debug.Log ("play");
			this.GetComponent<AudioSource> ().time = (Time.time - StaticPlayer.worldTime + StaticPlayer.musicTime) % this.GetComponent<AudioSource> ().clip.length;

			this.GetComponent<AudioSource> ().Play ();
		}
	}
}
