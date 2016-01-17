using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	public int HP = 100;
	//public playercontroller player;

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}
	
	//void OnTriggerEnter2D(Collider2D ob) {
	void OnCollisionStay2D(Collision2D collision) {
		//player = .GetComponent<playercontroller> ();
		//this.invisiable = false;
		/*player = collision.gameObject.GetComponent<playercontroller>();

		Debug.Log (player.timer);

		if (player.invisiable == false) {
			player.HP -= 10;
			player.invisiable = true;
		}

		Debug.Log(player.HP);
		if (player.HP == 0)
			Destroy (collision.gameObject);*/
	}
}
