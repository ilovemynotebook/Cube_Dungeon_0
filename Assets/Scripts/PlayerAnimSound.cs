using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimSound : MonoBehaviour
{
    public CharacterSound sounds;
    void Start()
    {
        if (sounds == null)
            sounds = transform.parent.GetComponent<Player>().sounds;
    }


    public void WalkSoundPlay()
    {
        sounds.Walk_AS.Play();
    }
    public void ClimbSoundPlay()
    {
        sounds.Climbing_AS.Play();
    }
    public void RunSoundPlay()
    {
        sounds.Run_AS.Play();
    }

    public void AllStop()
    {
        sounds.Walk_AS.Stop();
        sounds.Climbing_AS.Stop();
        sounds.Run_AS.Stop();
    }
}
