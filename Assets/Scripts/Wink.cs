using UnityEngine;
using System.Collections;

public class Wink : MonoBehaviour {

	public Sprite openSprite;
	public Sprite closedSprite;
	public bool eyeOpen = false;
	public const int LEFT_MOUSE_BUTTON = 0;

	// Use this for initialization
	void Start () {
		GameManager GM = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (LEFT_MOUSE_BUTTON)) {
			Sprite nextSprite = openSprite;
			if (eyeOpen) {
				nextSprite = closedSprite;
			}
			gameObject.GetComponent<SpriteRenderer> ().sprite = nextSprite;
			eyeOpen = !eyeOpen;
		}
	}
}
