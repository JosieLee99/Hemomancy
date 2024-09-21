using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksList : MonoBehaviour
{
    //public GameObject playerGO;
    public Player player;

    public GameObject currentAttack;

    public GameObject aoeAttack;
    public GameObject straightAttack;

    public void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void InstantiateAttack(int order)
    {
        if(order == 1)
        {
            currentAttack = Instantiate(aoeAttack, player.gameObject.transform.position, Quaternion.identity);
            //currentAttack.transform.parent = player.transform;
        }
        else if (order == 2)
        {
            switch(player.directionFaced)
            {
                case 2:
                    currentAttack = Instantiate(straightAttack, player.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                    //currentAttack.transform.parent = player.transform;
                    break;
                case 4:
                    currentAttack = Instantiate(straightAttack, player.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                    //currentAttack.transform.parent = player.transform;
                    break;
                case 6:
                    currentAttack = Instantiate(straightAttack, player.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    //currentAttack.transform.parent = player.transform;
                    break;
                case 8:
                    currentAttack = Instantiate(straightAttack, player.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 270)));
                    //currentAttack.transform.parent = player.transform;
                    break;
            }
        }
    }
}
