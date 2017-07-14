using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Movement : MonoBehaviour {

	private string inputAxisH = Config.HORIZONTAL_INPUT;
	private string inputAxisV = Config.VERTICAL_INPUT;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float inputH = Input.GetAxis (inputAxisH);
		float inputV = Input.GetAxis (inputAxisV);


	}
}
