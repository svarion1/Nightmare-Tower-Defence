using UnityEngine;

// Effect base class can't be added as component cause it's abstract
public abstract class Effect : MonoBehaviour
{
    public float duration;
    public float tickDuration;

    protected float nextTick;
    protected Enemy attachedEnemy;
    
    protected void Start ()
    {
        nextTick = tickDuration;
        attachedEnemy = GetComponent<Enemy>();
    }
    
    protected void Update () {
        duration -= Time.deltaTime;
        if (duration >0)
        {
            nextTick -= Time.deltaTime;
            if(nextTick <= 0)
            {
                Debug.Log("Effect Tick");
                ApplyEffect();
                nextTick = tickDuration;
            }
        }
        else
        {
            Destroy(this);
        }
    }

    protected abstract void ApplyEffect();
}
