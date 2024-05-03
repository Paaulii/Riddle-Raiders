
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleEffectsSystem : Singleton<ParticleEffectsSystem>
{
    [SerializeField] private ParticleEffect[] particles;
    private List<GameObject> activeParticles;

    private void Awake()
    {
        activeParticles = new List<GameObject>();
    }

    public void SpawnEffect(EffectType effectType, Vector2 pos)
    {
        GameObject[] foundParticles = GetParticlesByType(effectType);

        if (foundParticles.Length == 0)
        {
            Debug.LogError($"No assets to spawn particle for this EffectType {effectType}");
            return;
        }

        GameObject particle = foundParticles[Mathf.FloorToInt(Random.Range(0, foundParticles.Length))];

        GameObject spawnedParticle = Instantiate(particle, pos, Quaternion.identity);
        activeParticles.Add(spawnedParticle);
    }

    private GameObject[] GetParticlesByType(EffectType effectType)
    {
        return particles.ToList().Where(x => x.type == effectType).SelectMany(x => x.particles).ToArray();
    }
}
