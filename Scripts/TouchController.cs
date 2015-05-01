using UnityEngine;
using System.Collections;

public class TouchController : Mover {

	Vector3 standPos, mouseOrTouchVec;
	bool [] touches = new bool[2];
	float scaleLength;

	public static float scaleMoveLength;	//二本指でスワイプで前フレームとの二点間の距離の差分
	public static bool touchTrigger;		//押してるかどうか
	public static Vector3 swipeVec;			//スワイプの移動ベクトル
	public static Vector3 swipeVecNormal;	//上の単位ベクトル
	public static Vector3 touchMoveVec;		//前フレームとのベクトルの差分
	public static Vector3 rayHitPoint;		//タッチした先の位置を取得
	public static GameObject rayHitObj;		//タッチした先にある3Dオブジェクトを取得(離すまでオブジェクトを保持)

	void Awake () {
	}

	void Start () {
		touchTrigger = false;
		swipeVec = swipeVecNormal = mouseOrTouchVec = Vector3.zero;
		rayHitObj = null;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			mouseOrTouchVec = Input.GetTouch (0).position;	//or Input.mousePosition;
			touchTrigger = true;
			pos = prevPos = standPos = mouseOrTouchVec;
			Ray ray = Camera.main.ScreenPointToRay (mouseOrTouchVec);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				rayHitObj = hit.collider.gameObject;
				rayHitPoint = hit.point;
			}
		} else if (Input.GetMouseButtonUp (0)) {
			touchTrigger = false;
			rayHitObj = null;
		}
		if (touchTrigger) {
			if (pos != prevPos) {
				touchMoveVec = pos - prevPos;
				prevPos = pos;
			} else {
				touchMoveVec = Vector3.zero;
			}
			pos = Input.mousePosition;
			swipeVec = pos - standPos + Vector3.up * 0.001f;
			swipeVecNormal = Normalize (swipeVec);
		} else {
			pos = prevPos = swipeVec = swipeVecNormal = touchMoveVec = Vector3.zero;
		}

		float leng;
		for (int i = 0; i < 2; i++) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {
				touches [i] = true;
				scaleLength = leng = Length (Input.GetTouch (0).position - Input.GetTouch (1).position);
			}
			if (Input.GetTouch (i).phase == TouchPhase.Ended) {
				touches [i] = false;
			}
		}
		if (touches [0] && touches [1]) {
			leng = Length (Input.GetTouch (0).position - Input.GetTouch (1).position);
			scaleMoveLength = leng - scaleLength;
			scaleLength = leng;
		} else {
			scaleMoveLength = 0;
		}
		Debug.Log (touchMoveVec);
	}
}
