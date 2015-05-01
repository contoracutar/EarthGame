using UnityEngine;
using System.Collections;

public class MainCamera : Mover {

	Point point;
	float distance = 8, speed = 0.05f;
	void Awake () {
		point = GameObject.Find ("Point").GetComponent<Point> ();
	}

	void Start () {
	}
	
	void Update () {
		distance -= TouchController.scaleMoveLength * speed;
		distance = Constrain (distance, 8, 30);
		transform.rotation = Quaternion.Slerp (transform.rotation, point.GetBaseRotation () * RotationX (0.1f), 1);
		transform.position = point.pos + AxisY () + AxisZ () * -distance;
	}
}
