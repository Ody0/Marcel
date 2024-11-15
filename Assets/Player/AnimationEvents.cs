using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public ParticleSystem rightLeg;
    public ParticleSystem leftLeg;


    public void Step(string step)
    {
        if (step == "left")
        {
            leftLeg.Play();
        }
        else
        {
            rightLeg.Play();
        }
    }
}
