using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ScriptAnimatorPlayer : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;


    public void AnimationPlayer(string nameAnimation)
    {
        animatorPlayer.Play(nameAnimation);
        
    }
}
