using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    Player player;
    [Range(30, 80)] [SerializeField] private float hurtForce = 5;

    public Opossum_Controller opossum;
    public Frog_Movement frog;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Ground":
                player.grounded = true;
                break;
            case "Frog":
                if (!player.grounded &&
                    other.gameObject.transform.position.y < transform.position.y)
                {
                    player.jump = true;
                    frog.Dead();
                }
                else
                {
                    if(other.gameObject.transform.position.x > transform.position.x)
                    {
                        //Enemy Right I have to move left
                        player.JumpDamage(-hurtForce);
                        player.damage = true;
                        player.damageTime = true;
                    }
                    else
                    {
                        //Enemy Left I have to move Right
                        player.JumpDamage(hurtForce);
                        player.damage = true;
                        player.damageTime = true;
                    }
                }
                break;
            case "Opossum":
                if (!player.grounded &&
                    other.gameObject.transform.position.y < transform.position.y)
                {
                    player.jump = true;
                    opossum.Dead();
                }
                else
                {
                    if (other.gameObject.transform.position.x > transform.position.x)
                    {
                        //Enemy Right I have to move left
                        player.JumpDamage(-hurtForce);
                        player.damage = true;
                        player.damageTime = true;
                    }
                    else
                    {
                        //Enemy Left I have to move Right
                        player.JumpDamage(hurtForce);
                        player.damage = true;
                        player.damageTime = true;
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        switch(other.gameObject.tag)
        {
            case "Cherry":
                player.cherry = true;
                Destroy(other.gameObject);
                break;
            case "Diamond":
                player.diamondsCheck = true;
                Destroy(other.gameObject);
                break;
            case "House":
                player.StartCoroutine("Restart");
                break;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            player.grounded = false;
        }
    }
}
