using UnityEngine;
using System.Collections;

public class enemy_controller : MonoBehaviour
{
    public GameObject Enemy_Object;
	public Rigidbody2D rb;
	public float move_speed = 2;
    public float dodge_speed;
    public float attack_speed;
    public string player_type;
	public Animator player_a;
	public GameObject player;
	public enemy cube;
	public bool invisiable = false;
	public float HP = 200;
    public int ATK;
	public float timer = 0;
    public bool controlable = true;
	private bool dodge_once = false;
    private bool attack_once = false;
    private bool get_hit = false;
	private Vector3 player_scale;
	private bool _state_switch;
    private bool face_right;
    private Vector2 rotate_y_180=new Vector2(0,180);
    private Vector2 rotate_y_0 = new Vector2(0, 0);
    public player1controller player1;
    public UISlider HP_Bar;
    public bool Move_Left;
    public bool Move_Right;
    public bool Perform_Attack;
    public bool Perform_Dodge;
    private float delay;
    public float Delay_set;
	// Use this for initialization
    void Start()
    {
        Enemy_Object = GameObject.Find("player1");
        rb = this.GetComponent<Rigidbody2D>();
        player_a = this.GetComponent<Animator>();
        player_a.SetBool("Run", false);
        player = this.gameObject;//GameObject.Find("player");
        face_right = true;
        Move_Left = false;
        Move_Right = false;
        Perform_Attack = false;
        Perform_Dodge = false;
    }

	void attack_end ()
	{
		player_a.SetBool ("Attack", false);
        attack_once = false;
	}


    void dodge_end()
	{
		player_a.SetBool ("Dodge", false);
		this.dodge_once = false;
	}
    void hit_end()
    {
        player_a.SetBool("Run", false);
        player_a.SetBool("Attack", false);
        player_a.SetBool("Dodge", false);
        player_a.SetBool("Hit", false);
    }
	void set_controlable ()
	{
		this.controlable = true;
	}
	
	void invisialbe_timer ()
	{
		this.timer = this.timer + Time.deltaTime;
		if (this.timer > 3) {
			invisiable = false;
			this.timer = 0;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
        HP_Bar.value = HP / 200;
		if (invisiable == true) {
			invisialbe_timer ();
		}
		rb.velocity = Vector2.zero;

		//dodge

        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else{
            delay=Random.Range(Delay_set,2*Delay_set);
            if (Enemy_Object.transform.position.x > gameObject.transform.position.x)
            {
                if (Random.Range(0, 10) > 1)
                {
                    Move_Right = true;
                    Move_Left = false;
                }
                else
                {
                    Move_Right = false;
                    Move_Left = false;
                }
            }
            else
            {
                if (Random.Range(0, 10) > 1)
                {
                    Move_Left = true;
                    Move_Right = false;
                }
                else
                {
                    Move_Right = false;
                    Move_Left = false;
                }
            }
            if (Random.Range(0, 10) > 2)
            {
                Perform_Attack = true;
            }
            else
            {
                Perform_Attack = false;
            }
            if (player_type == "gentleman")
            {
                if (Random.Range(0, 10) > 1)
                {
                    Perform_Dodge = true;
                }
                else
                {
                    Perform_Dodge = false;
                }
            }
        }


        if (controlable)
        {
            //attack
            if (Perform_Attack)
            {
                player_a.SetBool("Attack", true);
                this.controlable = false;
                attack_once = true;
                Perform_Attack = false;
            }
            if (player_type == "gentleman")
            {
                if (Perform_Dodge)
                {
                    player_a.SetBool("Dodge", true);
                    this.controlable = false;
                    this.dodge_once = true;
                    Perform_Dodge = false;
                }
            }


            if (Move_Right)
            {
                if (face_right == true)
                {
                    player.transform.eulerAngles = rotate_y_180;
                    face_right = false;
                }
                player_a.SetBool("Run", true);
                rb.velocity = Vector2.right * move_speed;
            }
            if (Move_Left)
            {
                if (face_right == false)
                {
                    player.transform.eulerAngles = rotate_y_0;
                    face_right = true;
                }
                player_a.SetBool("Run", true);
                rb.velocity = Vector2.left * move_speed;
            }

        }
        else
        {
            if (player_type == "gentleman")
            {
                if (dodge_once == true)
                {
                    if (face_right == true)
                        rb.velocity = Vector2.right * dodge_speed;
                    else
                        rb.velocity = Vector2.left * dodge_speed;
                }
                if (attack_once == true)
                {
                    if (face_right == false)
                        rb.velocity = Vector2.right * attack_speed;
                    else
                        rb.velocity = Vector2.left * attack_speed;
                }
            }
        }

		//stop the action
        if (Move_Right == false && Move_Left==false)
        {
			player_a.SetBool ("Run", false);
		}

	}

	void OnTriggerEnter2D (Collider2D ob)
	{
        if (ob.tag == "Player1")
        {
            player1 = ob.GetComponentInParent<player1controller>();
            player1.HP -= ATK;
            player1.player_a.SetBool("Hit", true);
            player1.controlable = false;
           
        }
	}



}
