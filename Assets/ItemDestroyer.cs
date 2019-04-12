using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour {

	// UnityChanのオブジェクト
	private GameObject unitychan;

	// UnityChanとの距離
	private float difference;

	// Use this for initialization
	void Start () {

		// UnityChanのオブジェクトの取得
		unitychan = GameObject.Find("unitychan");

		// UnityChanとの位置の差を求める
		this.difference = 
			this.unitychan.transform.position.z - this.transform.position.z;
		
	}
	
	// Update is called once per frame
	void Update () {
		// UnityChanの位置に合わせて位置を移動
		this.transform.position = 
			new Vector3(
				0,
				this.transform.position.y, 
				this.unitychan.transform.position.z - difference);
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "CarTag" 			|| 
			other.gameObject.tag == "TrafficConeTag" 	|| 
			other.gameObject.tag == "CoinTag" 			) {

			Destroy (other.gameObject);

			Debug.Log ("消えたよ");
		}
	}
}
