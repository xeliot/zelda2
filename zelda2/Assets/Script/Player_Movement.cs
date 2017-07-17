using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

	private Rigidbody2D _mRigidBody;

	[SerializeField]
	private float _moveSpeed;

	// Use this for initialization
	void Start () {
		_mRigidBody = GetComponent<Rigidbody2D> ();
		_moveSpeed = 20f;

	}
	
	// Update is called once per frame
	void Update () {

		float horizontal = Input.GetAxis ("Horizontal");

		HandleMovement (horizontal);
	}

	private void HandleMovement(float horizontal){
		_mRigidBody.velocity = new Vector2 (horizontal * _moveSpeed, _mRigidBody.velocity.y);
	}
}
