using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterFsm : MonoBehaviour
{
    private BaseState currentState;
    private BaseState nextState;

    public void InitFsm(BaseState state)
    {
        currentState = state;
    }

    public void ChangeState(BaseState state)
    {
        currentState?.InitState();
        nextState = state;
        nextState?.InitState();
    }

    public void ActionState()
    {
        currentState?.ActionState();
    }

}
