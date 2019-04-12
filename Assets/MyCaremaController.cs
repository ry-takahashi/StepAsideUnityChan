﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCaremaController : MonoBehaviour {

	// UnityChanのオブジェクト
	private GameObject unitychan;

	// UnityChanとカメラの距離
	private float difference;

	// Use this for initialization
	void Start () {

		// UnityChanのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");

		// UnityChanとカメラの位置の差を求める
		this.difference = 
			this.unitychan.transform.position.z - this.transform.position.z;
		
	}
	
	// Update is called once per frame
	void Update () {

		// UnityChanの位置に合わせてカメラの位置を移動
		this.transform.position = new Vector3(0,this.transform.position.y, this.unitychan.transform.position.z - difference);
	}
}