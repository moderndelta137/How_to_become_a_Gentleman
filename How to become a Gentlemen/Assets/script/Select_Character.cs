using UnityEngine;
using System.Collections;

public class Select_Character : MonoBehaviour {
    public bool two_Player;
    public bool Player1_Selectable;
    public bool Player2_Selectable;
    public bool Player1_Ready;
    public bool Player2_Ready;
    public GameObject Icon1;
    public GameObject Icon2;
    public GameObject Icon3;
    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;
    public GameObject Player1_Icon;
    public GameObject Player2_Icon;
    public UILabel Feedback;
    public int Player1_Index;
    public int Player2_Index;
    public Animator Blackout;
    private float delay;
    private bool ready;
	// Use this for initialization
	void Start () {
        Player1_Selectable = true;
        Player1_Index = 0;
        Player1_Icon.transform.position = Icon1.transform.position;
        Player2_Icon.SetActive(false);
        Image1.SetActive(true);
        Image2.SetActive(false);
        Image3.SetActive(false);
        Image4.SetActive(false);
        Player1_Ready = false;
        Player2_Ready = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (ready == true)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                ChangeScene();
            }
        }
        if (Player1_Selectable == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Player1_Index < 2)
                {
                    Player1_Index += 1;
                }
                else
                {
                    Player1_Index = 0;
                }
                UpdatePlayer1Icon(Player1_Index);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (Player1_Index > 0)
                {
                    Player1_Index -= 1;
                }
                else
                {
                    Player1_Index = 2;
                }
                UpdatePlayer1Icon(Player1_Index);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Player1_Ready = true;
                Player1_Selectable = false;
                if (Player1_Index == 2)
                {
                    Player1_Index = Random.Range(0, 2);
                    UpdatePlayer1Icon(Player1_Index);
                    
                }
                ReadyCheck();
            }
        }
        
        if (Player2_Selectable == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Player2_Index < 2)
                {
                    Player2_Index += 1;
                }
                else
                {
                    Player2_Index = 0;
                }
                UpdatePlayer2Icon(Player2_Index);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Player2_Index > 0)
                {
                    Player2_Index -= 1;
                }
                else
                {
                    Player2_Index = 2;
                }
                UpdatePlayer2Icon(Player2_Index);
            }
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Player2_Ready = true;
                Player2_Selectable = false;
                if (Player2_Index == 2)
                {
                    Player2_Index = Random.Range(0, 2);
                    UpdatePlayer2Icon(Player2_Index);
                    
                }
                ReadyCheck();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Player2_Selectable = true;
                Player2_Icon.SetActive(true);
                Image3.SetActive(true);
                Player2_Index = 0;
                Player2_Icon.transform.position = Icon1.transform.position;
                Player2_Ready = false;
                two_Player = true;
            }
        }
    }
    void UpdatePlayer1Icon(int index)
    {
        switch (index)
        {
            case 0:
                Player1_Icon.transform.position = Icon1.transform.position;
                Image1.SetActive(true);
                Image2.SetActive(false);
                break;
            case 1:
                Player1_Icon.transform.position = Icon2.transform.position;
                Image1.SetActive(false);
                Image2.SetActive(true);
                break;
            case 2:
                Player1_Icon.transform.position = Icon3.transform.position;
                 Image1.SetActive(false);
                Image2.SetActive(false);
                break;
        }
    }
    void UpdatePlayer2Icon(int index)
    {
        switch (index)
        {
            case 0:
                Player2_Icon.transform.position = Icon1.transform.position;
                Image3.SetActive(true);
                Image4.SetActive(false);
                break;
            case 1:
                Player2_Icon.transform.position = Icon2.transform.position;
                Image3.SetActive(false);
                Image4.SetActive(true);
                break;
            case 2:
                Player2_Icon.transform.position = Icon3.transform.position;
                Image3.SetActive(false);
                Image4.SetActive(false);
                break;
        }
    }
    void ReadyCheck()
    {
        Debug.Log("sdfsd");
        if (Player1_Ready == true && Player2_Ready == true)
        {
            Debug.Log("szcvxcsd");
            Blackout.SetBool("BlackOut", true);
            delay = 2;
            ready = true;
        }
    }
    void ChangeScene()
    {
        PlayerPrefs.SetInt("Player1", Player1_Index);
        PlayerPrefs.SetInt("Player2", Player2_Index);
        if (two_Player == true) {
            if (Player1_Index == 0 && Player2_Index == 0)
            {
                Application.LoadLevel(4);
            }
            else
            {
                Application.LoadLevel(3);
            }
        }
        else
        {
            if (Player1_Index == 0)
            {
                Player2_Index = 1;  
            }
            else if (Player1_Index == 1)
            {
                Player2_Index = 0;  
            }
            PlayerPrefs.SetInt("Player2", Player2_Index);
            Application.LoadLevel(2);
        }
    }
}
