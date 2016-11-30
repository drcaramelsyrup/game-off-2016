using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int firstScore = 0;
	public int secondScore = 0;
	public int roundNum = 0;
	public float countdownTimer = 3.0F;

	// TODO: Ugh, this is really ugly to fill this in with public data, but...
	// first face
	public Wink firstLeftEye;
	public Wink firstRightEye;
	// second face
	public Wink secondLeftEye;
	public Wink secondRightEye;

	public bool roundReady = false;

	private const int COUNTDOWN_TIME = 3;
	// UI
	private Text countdownUI;
	private Text firstScoreUI;
	private Text secondScoreUI;

	// Use this for initialization
	void Start () {
		newRound ();
		// set UI elements
		GameObject canvasObj = GameObject.Find("Canvas");
		Text[] texts = canvasObj.GetComponentsInChildren<Text> ();
		foreach (Text text in texts) {
			if (text.CompareTag ("Countdown"))
				countdownUI = text.GetComponent<Text> ();
			else if (text.CompareTag ("Player1"))
				firstScoreUI = text.GetComponent<Text> ();
			else if (text.CompareTag ("Player2"))
				secondScoreUI = text.GetComponent<Text> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateUI ();

		// countdown to round being ready.
		if (!roundReady) {
			if (countdownTimer > 0) {
				countdownTimer -= Time.deltaTime;
			} else {
				// countdown for new round complete
				startRound();
			}
			return;
		}

		// game is ready!
		// just check for win/loss conditions
		checkEyeBlinked ();

	}

	void checkEyeBlinked () {
		if (firstLeftEye == null)
			return;
		if (firstRightEye == null)
			return;
		if (secondLeftEye == null)
			return;
		if (secondRightEye == null)
			return;

		bool eyeBlinked = false;

		// increment scores.
		// check one at a time to avoid odd situations.
		if (!firstLeftEye.eyeOpen || !firstRightEye.eyeOpen) {
			secondScore++;
			eyeBlinked = true;
		} else if (!secondLeftEye.eyeOpen || !secondRightEye.eyeOpen) {
			firstScore++;
			eyeBlinked = true;
		}

		if (eyeBlinked)
			newRound ();
		
	}

	void updateUI () {
		if (countdownUI == null)
			return;
		if (countdownTimer > 0) {
			countdownUI.text = ((int)countdownTimer).ToString ();
		} else {
			countdownUI.text = "GO";
		}

		firstScoreUI.text = firstScore.ToString ();
		secondScoreUI.text = secondScore.ToString ();
	}
		

	void newRound () {
		roundReady = false;
		countdownTimer = COUNTDOWN_TIME;
		roundNum++;
	}

	void startRound () {
		roundReady = true;
		Assert.IsNotNull (firstLeftEye);
		firstLeftEye.reset ();
		Assert.IsNotNull (firstRightEye);
		firstRightEye.reset ();
		Assert.IsNotNull (secondLeftEye);
		secondLeftEye.reset ();
		Assert.IsNotNull (secondRightEye);
		secondRightEye.reset ();
	}
}
