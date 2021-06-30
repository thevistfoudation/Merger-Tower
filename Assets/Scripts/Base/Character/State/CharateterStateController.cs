using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class CharateterStateController : MonoBehaviour
{

    CharacterState currentState = CharacterState.Idle;

    public CharacterState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    Dictionary<CharacterState, ICharacterState> dic_CharaterState = new Dictionary<CharacterState, ICharacterState>();

    ICharacterState currentCharacter;

    public void SetState(CharacterState state)
    {
        if (!dic_CharaterState.ContainsKey(state))
        {
            Debug.LogError("Don't Add State " + state);
            return;
        }
        if (currentState == state) return;
        if (currentCharacter == null || currentCharacter.CheckCanChangeState(state))
        {
            currentState = state;
            currentCharacter = dic_CharaterState[state];
            currentCharacter.SetState(state);
            
        }
    }

    public void AddCharaterState(CharacterState state, ICharacterState character)
    {
        if (dic_CharaterState.ContainsKey(state))
        {
            dic_CharaterState[state] = character;
        }
        else
        {
            dic_CharaterState.Add(state, character);
        }
    }

}
