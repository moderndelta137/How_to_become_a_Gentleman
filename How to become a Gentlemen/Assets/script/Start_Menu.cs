using UnityEngine;
using System.Collections;

public class Start_Menu : MonoBehaviour {
    public GameObject Background1;
    public GameObject Background2;
    public float Background_Speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Background1.transform.Translate(Vector3.left * Background_Speed);
        Background2.transform.Translate(Vector3.left * Background_Speed);
        if (Background1.transform.position.x < -33)
        {
            Background1.transform.position = new Vector3(Background2.transform.position.x+46.7f, -0.075f, 0);
        }
        if (Background2.transform.position.x < -33)
        {
            Background2.transform.position = new Vector3(Background1.transform.position.x+46.7f, -0.075f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.LoadLevel(1);
        }
	}
}
