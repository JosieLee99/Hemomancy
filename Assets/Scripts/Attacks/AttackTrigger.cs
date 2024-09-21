using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public WorldManager worldManager;

    public Player player;
    public Unit playerUnit;

    public void Start()
    {
        worldManager = FindObjectOfType<WorldManager>();

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerUnit = GameObject.FindWithTag("Player").GetComponent<Unit>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "NPC")
        {
            Unit npcUnit = collider.gameObject.GetComponent<Unit>();

            npcUnit.currentHP -= playerUnit.damage;

            player.SwitchState(player.idleState);
            worldManager.SwitchState(worldManager.moveAllState);
        }
    }

    public void DestroyAttack()
    {
        player.SwitchState(player.idleState);

        if(gameObject.transform.parent)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
