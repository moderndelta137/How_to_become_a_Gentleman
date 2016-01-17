using UnityEngine;
using System.Collections;

public class Fight_Control_GandG : MonoBehaviour
{
    public int Player1_Index;
    public int Player2_Index;
    public GameObject Player1_G;
    public GameObject Player2_G;
    public GameObject Enemy_Prefab;
    public UILabel Feedback;
    public UILabel Timer;
    public string Current_Phase;
    private int countdown;
    public int Remain_Time;
    public float delay;
    public float Generatedelay;
    public GameObject Player1;
    public player1controller Player1_Script;
    public GameObject Player2;
    public player2controller Player2_Script;
    public GameObject Enemy;
    public enemy_controller Enemy_Script;
    public GameObject track_player1;
    public GameObject track_player2;
    public float middle_point;
    public float distance;
    private Vector3 camera_position;
    public Camera camera;
    // Use this for initialization
    void Start()
    {

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
        track_player1 = Player1;
        track_player2 = Player2;

    }

    // Update is called once per frame
    void Update()
    {
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
                        Feedback.text = "Fight!";
                    }
                    else if (countdown == -1)
                    {
                        Feedback.text = null;
                        Current_Phase = "Fight";
                        Player1_Script.controlable = true;
                        Player2_Script.controlable = true;
                        Instantiate(Enemy_Prefab, new Vector2(6, -1), Quaternion.identity);
                        Instantiate(Enemy_Prefab, new Vector2(-6, -1), Quaternion.identity);
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
                if (Generatedelay > 0)
                {
                    Generatedelay -= Time.deltaTime;
                }
                else
                {
                    Generatedelay = 10;
                    Instantiate(Enemy_Prefab, new Vector2(6, -1), Quaternion.identity);
                    Instantiate(Enemy_Prefab, new Vector2(-6, -1), Quaternion.identity);
                }
                if (Player1_Script.HP <= 0)
                {
                    Player1.SetActive(false);
                    Player1_Script.controlable = false;
                    Player2_Script.controlable = false;
                    Feedback.text = "Lose";
                    Current_Phase = "End";
                }
                else if (Player2_Script.HP <= 0)
                {
                    Player2.SetActive(false);
                    Player1_Script.controlable = false;
                    Player2_Script.controlable = false;
                    Feedback.text = "Lose";
                    Current_Phase = "End";
                }
                else
                {
                    if (Remain_Time <= 0)
                    {
                        Player1_Script.controlable = false;
                        Player2_Script.controlable = false;
                        Feedback.text = "Win";
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
