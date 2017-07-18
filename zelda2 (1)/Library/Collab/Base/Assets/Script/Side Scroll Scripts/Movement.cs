using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	private Rigidbody2D _mRigidBody;

	[SerializeField]
	private float _moveSpeed;

	private bool _facingRight = true;

	private int _playerJumpPower = 350;

	[SerializeField]
	private GameObject _projectilePrefab;

	private List<GameObject> _projectiles = new List<GameObject> ();
	private List<int> _bulletCounter = new List<int> ();

	private float projectileVelocity = 10;
	private bool bulletMovingRight = true;

	private Animator _animator;

	[SerializeField]
	private GameObject camera;

	public bool isEnabled = true;

	// Use this for initialization
	void Start () {
		if (StaticPlayer.spawnPosition.z == 0) {
			this.transform.position = StaticPlayer.spawnPosition;
		}
		_mRigidBody = GetComponent<Rigidbody2D> ();
		_moveSpeed = 5f;
		_animator = GetComponent<Animator> ();
		_animator.SetBool ("isCrouching", false);

	}
	// Update is called once per frame
	void Update ()
	{
		if (!isEnabled)
			return;
		float horizontal = Input.GetAxis ("Horizontal");


		if (horizontal < 0.0f && _facingRight == true) {	
			FlipPlayer ();
		} else if (horizontal > 0.0f && _facingRight == false) {
			FlipPlayer ();
		}

		if (Input.GetKeyDown ("w") && _mRigidBody.velocity.y == 0) {
			Jump ();
		}
		if (Input.GetKeyDown (KeyCode.S) && _mRigidBody.velocity.y == 0) {
			//_animator.SetBool ("isCrouching", true);
			_animator.Play("Crouch");
		}
		if (_projectiles.Count == 0) {
			if (Input.GetKeyDown ("space")) {
				GameObject bullet = (GameObject)Instantiate (_projectilePrefab, transform.position, Quaternion.identity);
				if (!_facingRight) {
					bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * -1f, bullet.transform.localScale.y, bullet.transform.localScale.z);
				}
				//we are crouching so lower the bullet
				if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Crouch")) {
					bullet.transform.position += new Vector3 (0, -0.7f, 0);
				}
				_projectiles.Add (bullet);
				_bulletCounter.Add (1);
				AudioSource audio = GetComponent<AudioSource> ();
				audio.Play ();
				if (_facingRight) {
					bulletMovingRight = true;
				} else {
					bulletMovingRight = false;
				}
			}
		}

		for (int i = 0; i < _projectiles.Count; i++) {
			GameObject currentBullet = _projectiles [i];
			if (currentBullet != null) {
				_bulletCounter [i] += 1;
				/*
				if (_facingRight) {
					currentBullet.transform.Translate (new Vector3 (1, 0) * Time.deltaTime * projectileVelocity);
				} else {
					currentBullet.transform.Translate (new Vector3 (-1, 0) * Time.deltaTime * projectileVelocity);
				}
				*/
				if (bulletMovingRight) {
					currentBullet.transform.Translate (new Vector3 (1, 0) * Time.deltaTime * projectileVelocity);
				} else {
					currentBullet.transform.Translate (new Vector3 (1, 0) * Time.deltaTime * projectileVelocity * -1);
				}
				if (_bulletCounter[i] == 15) {
					_projectiles.Remove (currentBullet);
					_bulletCounter.RemoveAt (i);
					DestroyObject (currentBullet);
				}
			}
		}

		HandleMovement (horizontal);
	}

	private void Jump(){
		_mRigidBody.AddForce (Vector2.up * _playerJumpPower);
	}

	private void FlipPlayer(){
		_facingRight = !_facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
		Vector2 cameraLocalScale = camera.transform.localScale;
		cameraLocalScale.x *= -1;
		camera.transform.localScale = cameraLocalScale;
	}

	private void HandleMovement(float horizontal){
		_mRigidBody.velocity = new Vector2 (horizontal*_moveSpeed, _mRigidBody.velocity.y);
	}
}
