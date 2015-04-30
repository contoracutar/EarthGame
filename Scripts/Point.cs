using UnityEngine;
using System.Collections;

public class Point : Mover {
	GameObject mainCamera, earth;
	Vector3 accessZeroVector;
	Quaternion baseRot;
	float speed;

	public Quaternion GetBaseRotation () { return baseRot; }

	void Awake () {
		mainCamera = GameObject.Find ("Main Camera");
		earth = GameObject.Find ("Earth");
	}

	void Start () {
		baseRot = Quaternion.identity;
		pos = earth.transform.position + Vector3.back * earth.transform.localScale.x / 2;
		owner = earth;
		accessZeroVector = Vector3.zero;
		speed = 0.2f;
	}
	
	void Update () {
		transform.position = pos;
		if (owner) {
			velocity = Vector3.zero;
			MainCamera mCamera = mainCamera.GetComponent<MainCamera> ();
			float r = (owner.transform.localScale.x + transform.localScale.x) / 2;
			if (TouchController.touchTrigger || TouchController.touchMoveVec != Vector3.zero) {
				accessZeroVector = TouchController.touchMoveVec;
			} else {
				accessZeroVector *= 0.9f;
			}
			Vector3 y = Normalize (pos - owner.transform.position), x = Vector3.Cross (y, mCamera.AxisZ ());
			Vector3 z = Vector3.Cross (x, y), d = accessZeroVector * -speed;

			baseRot = (Quaternion.AngleAxis (d.y, x) * Quaternion.AngleAxis (-d.x, z)) * baseRot;
			pos = owner.transform.position + baseRot * Vector3.up * r;
		}
	}
}
