using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterState currentState;

    public IdleState idleState = new IdleState();
    public MovingState movingState = new MovingState();

    public LayerMask whatStopsMovement;

    public Transform movePoint;

    void Start()
    {
        currentState = idleState;

        currentState.EnterState(this);
    }

    public void SwitchState(CharacterState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }
}
