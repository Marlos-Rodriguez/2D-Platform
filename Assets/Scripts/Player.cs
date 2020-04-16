using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Move")]
    // Amount of force added when the player jumps.
    [Range(400, 800)] [SerializeField] private float jumpForce = 400f;
    // How much to smooth out the movement
    [Range(0, .3f)] [SerializeField] private float Smoothing = .05f;

    [Space]

    [Header("Life")]
    [Range(0, 10)] public int life;
    [Range(0, 10)] public int maxLife = 3;
    public Healt_bar Bar;


    [Space]

    [Header("Diamonds")]
    public int diamonds = 0;
    public bool diamondsCheck = false;

    [Space]

    public bool cherry = false;
    public bool damage = false;
    public bool damageTime = false;

    //Values for gravity
    private float fallGravity = 3f;
    private float lowJumpGravity = 1.6f;

    //Objects for Movement
    private Rigidbody2D rg2d;
    public bool grounded;
    //Example Vector for Movement
    private Vector3 velocity = Vector3.zero;

    //values for the move
    private float move;
    public bool jump = false;
    private bool faceSize = true;

    //Animator
    private Animator anim;

    private CapsuleCollider2D capsule;

    public CinemachineVirtualCamera cam;


    void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider2D>();
        life = maxLife;
        Bar.SetMaxHealt(life);
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");

        if (grounded && Input.GetButtonDown("Jump")) jump = true;

        //Gravity jump Effect
        if(rg2d.velocity.y < 0)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Falling", true);
            rg2d.velocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.deltaTime;
        } 
        else if(rg2d.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            anim.SetBool("Jump", true);
            rg2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpGravity - 1) * Time.deltaTime;
        }

        if(grounded) anim.SetBool("Falling", false);

        if (move > 0 && !faceSize)
        {
            Flip();
        }
        else if (move < 0 && faceSize)
        {
            // ... flip the player.
            Flip();
        }

        GetComponent<Player_Life>().Life();
        GetComponent<Player_Diamond>().DiamondController();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        Move(move, jump);
    }

    public void Move(float move, bool m_Jump)
    {

        if(m_Jump)
        {
            jump = false;
            anim.SetBool("Jump", true);
            rg2d.AddForce(new Vector2(0f, jumpForce));
        }

        if(!damageTime)
        {
            anim.SetFloat("Move", Mathf.Abs(move));
            Vector3 targetVelocity = new Vector2(move * 10f, rg2d.velocity.y);

            rg2d.velocity = Vector3.SmoothDamp(rg2d.velocity, targetVelocity, ref velocity, Smoothing);
        }
    }

    public void JumpDamage(float damageSize)
    {
        rg2d.AddForce(new Vector2(damageSize, 0f), ForceMode2D.Impulse);
    }

    private void Flip()
    {
        faceSize = !faceSize;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Dead()
    {
        anim.SetTrigger("Dead");
        rg2d.AddForce(new Vector2(0f, jumpForce));
        cam.Follow = null;
        damageTime = true;
        jump = false;
        capsule.isTrigger = true;
        StartCoroutine("Restart");
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Test_Scene");
    }
}
