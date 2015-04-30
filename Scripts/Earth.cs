using UnityEngine;
using System.Collections;

public class Earth : Mover {

	GameObject human;
	void Start () {
		human = (GameObject)Resources.Load ("human00");
	}
	
	void Update () {
		if (TouchController.rayHitObj) {
			Instantiate (human, TouchController.rayHitPoint, Quaternion.identity);
		}
	}
}
