using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Life : MonoBehaviour
{
    Player player;
    public Healt_bar Bar;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void Life()
    {
        if (player.cherry)
        {
            player.cherry = false;
            if (player.life != player.maxLife)
            {
                player.life++;
                Bar.SetHealt(player.life);
            }
            else if (player.life == player.maxLife)
            {
                player.life = player.maxLife;
                Bar.SetHealt(player.life);
            }
        }
        if (player.damage)
        {
            StartCoroutine("DamageTime");
            player.damage = false;

            if (player.life - 1 <= 0)
            {
                player.life = 0;
                Bar.SetHealt(0);
                player.Dead();
            }
            else if (player.life > 0)
            {
                player.life--;
                Bar.SetHealt(player.life);
            }
        }
    }

    private IEnumerator DamageTime()
    {
        yield return new WaitForSeconds(.3f);
        player.damageTime = false;
    }

}
