using UnityEngine;
using System.Collections;

public class Fight_Control : MonoBehaviour {
    public int Player1_Index;
    public int Player2_Index;
    public GameObject Player1_G;
    public GameObject Player1_S;
    public GameObject Player2_G;
    public GameObject Player2_S;
    public UILabel Feedback;
    public UILabel Timer;
    public string Current_Phase;
    private int countdown;
    public int Remain_Time;
    public float delay;
    public GameObject Player1;
    public player1controller Player1_Script;
    public GameObject Player2;
    public player2controller Player2_Script;

    public GameObject track_player1;
    public GameObject track_player2;
    public float middle_point;
    public float distance;
    private Vector3 camera_position;
    public Camera camera;
	// Use this for initialization
	void Start () {
        Player1_Index = PlayerPrefs.GetInt("Player1");
        Player2_Index = PlayerPrefs.GetInt("Player2");
        if (Player1_Index == 0)
        {
            Debug.Log("p0");
            Player1_G.SetActive(true);
            Player1_S.SetActive(false);
        }
        else if (Player1_Index == 1)
        {
            Debug.Log("p1");
            Player1_S.SetActive(true);
            Player1_G.SetActive(false);
        }
        if (Player2_Index == 0)
        {
            Player2_G.SetActive(true);
            Player2_S.SetActive(false);
        }
        else if (Player2_Index == 1)
        {
            Player2_S.SetActive(true);
            Player2_G.SetActive(false);
        }
        Player1 = GameObject.Find("player1");
        Player1_Script = Player1.GetComponent<player1controller>();
        Player2 = GameObject.Find("player2");
        Player2_Script = Player2.GetComponent<player2controller>();
        Current_Phase = "Ready";
        Feedback.text = "Ready";
        countdown = 4;
        Remain_Time = 99;
        delay = 1;
        Player1_Script.controlable = false;
        Player2_Script.controlable = false;
        track_player1 = GameObject.Find("player1");
        track_player2 = GameObject.Find("player2");
	}
	
	// Update is called once per frame
	void Update () {
        middle_point = (track_player1.transform.position.x + track_player2.transform.position.x) / 2;
        distance = Vector2.Distance(track_player1.transform.position, track_player2.transform.position);
        if (distance > 5)
        {
            camera.orthographicSize = 0.3f * distance + 0.9f;
        }
        camera_position = new Vector3(middle_point, 0, -5);
        camera.transform.position = camera_position;
        switch (Current_Phase)
        {
            case "Ready":
                
                if (delay > 0)
                {
                    delay -= Time.deltaTime;
                }
                else
                {
                    delay = 1;
                    countdown -= 1;
                    Feedback.text = countdown.ToString();
                    if (countdown == 0)
                    {
                        Feedback.text = "Action!";
                    }
                    else if (countdown == -1)
                    {
                        Feedback.text = null;
                        Current_Phase = "Fight";
                        Player1_Script.controlable = true;
                        Player2_Script.controlable = true;
                    }
                }
                break;
            case "Fight":
                if (delay > 0)
                {
                    delay -= Time.deltaTime;
                }
                else
                {
                    Remain_Time -= 1;
                    Timer.text = Remain_Time.ToString();
                    delay = 1;
                }
                if (Player1_Script.HP < 0)
                {
                    Player1.SetActive(false);
                    Player1_Script.controlable = false;
                    Player2_Script.controlable = false;
                    if(Player2_Script.HP>0){
                        Feedback.text = "P2 Win";
                    }
                    else
                    {
                        Player2.SetActive(false);
                        Feedback.text = "Draw";
                    }
                    Current_Phase = "End";
                }
                else if (Player2_Script.HP <0)
                {
                    Player2.SetActive(false);
                    Player1_Script.controlable = false;
                    Player2_Script.controlable = false;
                    if (Player1_Script.HP > 0)
                    {
                        Feedback.text = "P1 Win";
                    }
                    else
                    {
                        Player1.SetActive(false);
                        Feedback.text = "Draw";
                    }
                    Current_Phase = "End";
                }
                else
                {
                    if (Remain_Time < 0)
                    {
                        Player1_Script.controlable = false;
                        Player2_Script.controlable = false;
                        if (Player1_Script.HP > Player2_Script.HP)
                        {
                            Feedback.text = "P1 Win";
                        }
                        else if (Player1_Script.HP < Player2_Script.HP)
                        {
                            Feedback.text = "P2 Win";
                        }
                        else
                        {
                            Feedback.text = "Draw";
                        }
                        Current_Phase = "End";
                    }
                }
                break;
            case "End":
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Application.LoadLevel(1);
                }
                break;
        }
	}


}
