using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    private string poolName;
    public ParticleSystem[] particles;
    public Texture2D[] textures;
    public float lifespan = 1.0f;
    protected Transform transBullet;
    public AudioClip[] clips;
    public AudioSource audioSources;
    void Awake()
    {
        transBullet = transform;
        particles = gameObject.GetComponentsInChildren<ParticleSystem>();
    }
    // Use this for initialization
    public void OnSpawned()  // Might be able to make this private and still work?
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }
        // Start the timer as soon as this instance is spawned.
        audioSources.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
        this.StartCoroutine("TimedDespawn");
    }

    private IEnumerator TimedDespawn()
    {
        // Wait for 'lifespan' (seconds) then despawn this instance.
        yield return new WaitForSeconds(this.lifespan);
        PoolManager.Pools[poolName].Despawn(transBullet);
    }

    public void OnDespawned()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();
        }
        StopCoroutine("TimedDespawn");
    }

    public virtual void Settup(string _poolName = "PoolBulletPlayer", bool isCreateHold = true)
    {
        poolName = _poolName;
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        if (!isCreateHold)
        {
            if (mr != null)
                mr.gameObject.SetActive(false);
            return;
        }

        if (mr != null)
        {
            mr.gameObject.SetActive(false);
            mr.material.mainTexture = textures[Random.Range(0, textures.Length)];
        }
    }
}
