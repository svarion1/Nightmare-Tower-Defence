using UnityEngine;

public sealed class ContinuousDamageEffect : Effect
{
    public float tickDamage;

    protected sealed override void ApplyEffect()
    {
        attachedEnemy.TakeDamage(tickDamage);
    }
}
