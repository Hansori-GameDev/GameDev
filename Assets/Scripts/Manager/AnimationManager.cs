using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    float animspeed;

    AnimStates.Direction GetDirection(float dir)
    {
        AnimStates.Direction _dir;
        if (dir <= 45.0 && dir >= -45.0) _dir = AnimStates.Direction.Up;
        else if (dir >= 45.0 && dir <= 135.0) _dir = AnimStates.Direction.Right;
        else if (dir <= -45.0 && dir >= -135.0) _dir = AnimStates.Direction.Left;
        else _dir = AnimStates.Direction.Down;

        return _dir;
    }

    public void Play(Animator anim, AnimStates.State state, float dir)
    {
        AnimStates.Direction _dir;
        _dir = GetDirection(dir);

        anim.transform.rotation = Quaternion.Euler(0, 0, 0);
        anim.Play($"{state.ToString()}_{_dir.ToString()}");
    }

}
