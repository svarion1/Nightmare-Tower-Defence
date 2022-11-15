using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamage : MonoBehaviour
{
    public float duration;
    public float tickDuration;
    public float tickDamage;

    public float nextTick;
    private Enemy attachedEnemy;
    
    void Start ()
    {
        nextTick = tickDuration;
        attachedEnemy = GetComponent<Enemy>();
    }
    
    void Update () {
        duration -= Time.deltaTime;
        if (duration >0)
        {
            nextTick -= Time.deltaTime;
            if(nextTick <= 0)
            {
                Debug.Log("Tick");
                attachedEnemy.TakeDamage(tickDamage);
                nextTick = tickDuration;
            }
        }
        else
        {
            Destroy(this);
        }
    }
}
