using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum_Controller : MonoBehaviour
{
    [Range(0, 20)] [SerializeField] private float speed = 5;

    public Transform start, end;
    private Vector3 v_Start, v_End;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        end.parent = null;
        v_Start = transform.position;
        v_End = end.position;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, end.position, speed * Time.deltaTime);
        
        if(transform.position == end.position)
        {
            end.position = (end.position == v_Start) ? v_End : v_Start;
            Flip();
        }

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
