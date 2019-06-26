using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Seperator : MonoBehaviour
{
    public Spawner spawner;
    public Vector2 downDuration;
    public float downChance;

    public Animator animator;

    float alarm;

    void Update()
    {
        if (spawner.level >= 3)
        {
            if (alarm <= 0f)
            {
                var rand = Mathf.RoundToInt(Random.Range(0f, downChance));
                if (rand == 0)
                    alarm = Mathf.RoundToInt(Random.Range(downDuration.x, downDuration.y));

                animator.SetBool("IsDown", false);
            }
            else
            {
                animator.SetBool("IsDown", true);
                alarm--;
            }
        }
    }
}
