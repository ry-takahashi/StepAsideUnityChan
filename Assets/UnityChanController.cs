﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

	// アニメーションする為のコンポーネントを入れる
	private Animator myAnimator;

	// Unityちゃんを移動させるコンポーネントを入れる
	private Rigidbody myRigidbody;

	private float forwardForce = 800.0f;

	private float turnForce = 500.0f;

	private float upForce = 500.0f;

	private float mavableRange = 3.4f;

	private float coefficent = 0.95f;

	// ゲーム終了の判定
	private bool isEnd = false;

	// ゲーム終了時に表示するテキスト
	private GameObject stateText;

	// スコア表示するテキスト
	private GameObject scoreText;

	// 得点
	private int score = 0;

	// 左ボタン押下の判定
	private bool isLButtonDown = false;

	// 右ボタン押下の判定
	private bool isRButtonDown = false;


	// Use this for initialization
	void Start () {
		// Animatorコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		// 走るアニメーションを開始
		this.myAnimator.SetFloat("Speed",1);

		// Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody> ();

		// シーン中のstateTextオブジェクトを取得
		this.stateText = GameObject.Find("GameResultText");

		// シーン中のscoreTextオブジェクトを取得
		this.scoreText = GameObject.Find("ScoreText");
	}
	
	// Update is called once per frame
	void Update () {

		// ゲーム終了ならUnitychanの動きを減衰する
		if (this.isEnd) {
			this.forwardForce *= this.coefficent;
			this.turnForce *= this.coefficent;
			this.upForce *= this.coefficent;
			this.myAnimator.speed *= this.coefficent;
		}

		// UnityChanに前方向の力を加える
		this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);

		// UnityChanを矢印キーまたはボタンに応じて左右に移動させる
		if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown )&& -this.mavableRange < this.transform.position.x) {
			// 左に移動
			this.myRigidbody.AddForce (-this.turnForce, 0, 0);

		} else if ((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown )&& this.mavableRange > this.transform.position.x) {
			// 右に移動
			this.myRigidbody.AddForce (this.turnForce, 0, 0);

		}

		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}

		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {

			this.myAnimator.SetBool ("Jump", true);

			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	void OnTriggerEnter(Collider other){

		// 障害物に衝突した場合
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;

			// stateTextにGAME OVERを表示
			this.stateText.GetComponent<Text>().text = "GAME OVER";
		}

		// ゴールに到達した場合
		if (other.gameObject.tag == "GoalTag") {
			this.isEnd = true;

			// stateTextにGAME CLEARを表示
			this.stateText.GetComponent<Text>().text = "CLEAR!!";
		}

		// コインに衝突した場合
		if (other.gameObject.tag == "CoinTag") {

			// スコアを加算
			this.score += 10;

			// ScoreTextに獲得した点数を表示
			this.scoreText.GetComponent<Text>().text = 
				"Score " + this.score + "pt";

			// パーティクルを再生
			GetComponent<ParticleSystem>().Play();

			// 接触したコインのオブジェクトを破棄
			Destroy(other.gameObject);
		}
	}

	// ジャンプボタンを押した場合の処理
	public void GetMyJumpButtonDown(){
		if (this.transform.position.y < 0.5f) {
			
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	// 左ボタンを押し続けた場合の処理
	public void GetMyLeftButtonDown(){
		this.isLButtonDown = true;
	}

	// 左ボタンを離した場合の処理
	public void GetMyLeftButtonUp(){
		this.isLButtonDown = false;
	}

	// 右ボタンを押し続けた場合の処理
	public void GetMyRightButtonDown(){
		this.isRButtonDown = true;
	}

	// 右ボタンを離した場合の処理
	public void GetMyRightButtonUp(){
		this.isRButtonDown = false;
	}
}
