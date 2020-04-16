using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Diamond : MonoBehaviour
{
    private Player player;

    public TextMeshProUGUI Gems;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void DiamondController()
    {
        if (player.diamondsCheck)
        {
            player.diamondsCheck = false;

            player.diamonds++;

            Gems.SetText("x " + player.diamonds);
        }
    }
}
