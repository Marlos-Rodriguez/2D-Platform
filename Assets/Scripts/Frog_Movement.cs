using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Movement : MonoBehaviour
{
    [Header("Speed")]
    [Range(0, 20)] [SerializeField] private float speed = 5;
    [Range(0, 20)] [SerializeField] private float SpaceOfJump = 2;

    [Space]
    [Header("Transform")]

    public Transform from;
    public Transform to;

    private bool animationStop = false;

    private bool right = true;

    private Animator anim;

    private Rigidbody2D rb2d;

    private void OnDrawGizmosSelected()
    {
        if(from != null && to != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(from.position, to.position);
        }
    }

    private void Start()
    {
        if (to != null)
        {
            to.parent = null;
        }
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine("StopMove");
    }

    private void Update()
    {
        if (to != null)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(to.position, Vector2.down, 2f);

            if(groundInfo.collider == true)
            {
                if(!animationStop)
                {
                    transform.position = Vector3.MoveTowards(transform.position, to.position, speed * Time.deltaTime);
                }
            }
            else
            {
                right = !right;
                if(right)
                {
                    Flip();
                    to.position = new Vector3(from.position.x - SpaceOfJump, to.position.y, 0);
                }
                else
                {
                    Flip();
                    to.position = new Vector3(from.position.x + SpaceOfJump, to.position.y, 0);
                }
            }

            if (transform.position == to.position)
            {
                StartCoroutine("StopMove");
                if (right)
                {
                    to.position = new Vector3(from.position.x - SpaceOfJump, to.position.y, 0);
                }
                else
                {
                    to.position = new Vector3(from.position.x + SpaceOfJump, to.position.y, 0);
                }
            }
        }   
    }

    private IEnumerator StopMove()
    {
        animationStop = true;
        anim.SetBool("MoveStop", true);
        yield return new WaitForSeconds(1.1f);
        rb2d.AddForce(new Vector2(0f, 350f));
        anim.SetBool("MoveStop", false);
        animationStop = false;
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Dead()
    {
        anim.SetTrigger("Dead");
        speed = 0;
        Destroy(gameObject, .5f);
    }

}
