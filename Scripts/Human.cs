using UnityEngine;
using System.Collections;

public class Human : Mover {

	enum State { Idle, Walk, Run, Jump }
	State state;
	void Start () {
	}
	
	void Update () {
		GetComponent<Animator> ().SetInteger ("State", (int)state);
	}

	void OnGUI() {
		if (GUI.Button(new Rect(20,0,60,20), "Idle")) {
			state = State.Idle;
		}
		if (GUI.Button(new Rect(20,30,60,20), "Walk")) {
			state = State.Walk;
		}
		if (GUI.Button(new Rect(20,60,60,20), "Run")) {
			state = State.Run;
		}
		if (GUI.Button(new Rect(20,90,60,20), "Jump")) {
			state = State.Jump;
		}
	}
}
