using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovingState : NPCState
{
    public float moveSpeed = 5f;
    public bool shouldMove = false;

    public override void EnterState(NPC npc, Player player, WorldManager worldManager)
    {
        if (Vector3.Distance(player.gameObject.transform.position, npc.gameObject.transform.position) <= 2.5f)
        {
            Unit npcUnit = npc.GetComponent<Unit>();
            Unit playerUnit = player.GetComponent<Unit>();

            playerUnit.currentHP -= npcUnit.damage;

            worldManager.SwitchState(worldManager.playerTurnState);
            npc.SwitchState(npc.npcIdleState);

        }
        else
        {
            shouldMove = true;
        }
    }

    public override void UpdateState(NPC npc, WorldManager worldManager)
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    shouldMove = true;
        //}

        Move(npc, worldManager);
    }

    public override void OnCollisionEnter2D(NPC npc)
    {

    }

    public void Move(NPC npc, WorldManager worldManager)
    {
        if (shouldMove == true)
        {
            if (npc.path.Count > 0)
            {
                foreach (Vector2 npci in npc.path)
                {
                    int i = 0;

                    if (i > 0)
                    {
                        npc.path.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                int x = 0;
                npc.gameObject.transform.position = Vector3.MoveTowards(npc.gameObject.transform.position, new Vector3(npc.path[x].x, npc.path[x].y, -2), moveSpeed * Time.deltaTime);

                if (Vector2.Distance(npc.gameObject.transform.position, npc.path[x]) < 0.01f)
                {
                    Debug.Log("1 tile traveled");

                    npc.path.RemoveAt(x);

                    shouldMove = false;

                    worldManager.SwitchState(worldManager.playerTurnState);
                    npc.SwitchState(npc.npcIdleState);

                }
            }
        }
    }
}
