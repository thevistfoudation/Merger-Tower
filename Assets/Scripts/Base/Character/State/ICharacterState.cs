using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
    void SetState(CharacterState state);
    bool CheckCanChangeState(CharacterState state);
}
