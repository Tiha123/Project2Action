using UnityEngine;

public class PoolableParticle : PoolBehaviour
{
    private ParticleSystem ps;

    void Awake()
    {
        if(TryGetComponent(out ps)==false)
        {
            Debug.LogWarning("Poolable ] ParticleSytem");
        }
    }

    void OnEnable()
    {
        ps.Play();
    }

    void OnDisable()
    {
        Despawn();
    }
}
