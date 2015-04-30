using UnityEngine;
using System.Collections;

public class MainCamera : Mover {

	Point point;
	void Awake () {
		point = GameObject.Find ("Point").GetComponent<Point> ();
	}

	void Start () {
	}
	
	void Update () {
		transform.rotation = Quaternion.Slerp (transform.rotation, point.GetBaseRotation () * RotationX (0.1f), 1);
		transform.position = point.pos + AxisY () + AxisZ () * -8;
	}
}
