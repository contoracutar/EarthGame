using UnityEngine;
using System.Collections;

public class TouchController : Mover {

	Vector3 standPos;
	public static bool touchTrigger;		//押してるかどうか
	public static Vector3 swipeVec;			//スワイプのベクトル
	public static Vector3 swipeVecNormal;	//上の単位ベクトル
	public static Vector3 touchMoveVec;		//前フレームとのベクトルの差分
	public static Vector3 rayHitPoint;		//タッチした先の位置を取得
	public static GameObject rayHitObj;		//タッチした先にある3Dオブジェクトを取得(離すまでオブジェクトを保持)

	void Awake () {
	}

	void Start () {
		touchTrigger = false;
		swipeVec = swipeVecNormal = Vector3.zero;
		rayHitObj = null;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			touchTrigger = true;
			pos = prevPos = standPos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
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
	}
}
