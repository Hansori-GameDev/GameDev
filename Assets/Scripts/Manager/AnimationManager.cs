using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    float animspeed;

    public void Play(Animator anim, AnimStates.State state, AnimStates.Direction direction)
    {
        
        anim.Play($"{state.ToString()}_{direction.ToString()}");
    }
}
