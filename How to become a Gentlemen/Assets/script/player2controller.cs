using UnityEngine;
using System.Collections;

public class player2controller : MonoBehaviour
{

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
    public enemy_controller enemy;
    public special_snowman snowman;
    public UISlider HP_Bar;
	// Use this for initialization
	void Start ()
	{
		rb = this.GetComponent<Rigidbody2D> ();
		player_a = this.GetComponent<Animator> ();
		player_a.SetBool ("Run", false);
		player = this.gameObject;//GameObject.Find("player");
        face_right = true;
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

        if (controlable)
        {
            //attack
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                player_a.SetBool("Attack", true);
                this.controlable = false;
                attack_once = true;
            }
            if (player_type == "gentleman")
            {
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    player_a.SetBool("Dodge", true);
                    this.controlable = false;
                    this.dodge_once = true;
                }
            }


            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (face_right == true)
                {
                    player.transform.eulerAngles = rotate_y_180;
                    face_right = false;
                }
                player_a.SetBool("Run", true);
                rb.velocity = Vector2.right * move_speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
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
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) )
        {
			player_a.SetBool ("Run", false);
		}

	}

	void OnTriggerEnter2D (Collider2D ob)
	{
        Debug.Log(ob.tag);
        if (ob.tag == "Enemy")
        {
            enemy = ob.GetComponentInParent<enemy_controller>();
            enemy.HP -= ATK;
            enemy.player_a.SetBool("Hit", true);
            enemy.controlable = false;

        }
        else if (ob.tag == "snowman")
        {
            snowman = ob.GetComponentInParent<special_snowman>();
            snowman.HP -= ATK;
            snowman.player_a.SetBool("Hit", true);
            snowman.controlable = false;

        }
        else if (ob.tag == "Player1")
        {
            player1 = ob.GetComponentInParent<player1controller>();
            player1.HP -= ATK;
            player1.player_a.SetBool("Hit", true);
            player1.controlable = false;

        }
	}



}
