using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamage : MonoBehaviour
{
    public float duration;
    public float damageEvery;
    public float nextDamage;
    public float damage;

    private Enemy attachedEnemy;
    
    void Start () {
        nextDamage = damageEvery;
        attachedEnemy = GetComponent<Enemy>();
    }
    
    void Update () {
        duration -= Time.deltaTime;
        if (duration >0)
        {
            nextDamage -= Time.deltaTime;
            if(nextDamage <= 0)
            {
                attachedEnemy.TakeDamage(damage);
                nextDamage = damageEvery;
            }
        }
        else
        {
            Destroy(this);
        }
    }
}
