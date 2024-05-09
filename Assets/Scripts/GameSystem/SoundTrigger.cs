using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioClip bgm;
    public AudioClip effect;

    void Start()
    {
        Manager.Sound.Play(bgm, Define.Sound.Bgm);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Manager.Sound.Play(effect);
    }
}
