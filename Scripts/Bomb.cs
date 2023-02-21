using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bomb : MonoBehaviour
{
    public int bombState = 0; // 0=idle, 1=fuse
    private float fuseTimer;
    public float fuseLength = 2.0f;

    private ParticleSystem sparks;
    private ParticleSystem explosion;
    private Component[] comparray;

    public AudioClip clip;
    public AudioClip fuse_burn;
    // Start is called before the first frame update
    void Start()
    {
        comparray = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in comparray)
        {
            if (p.gameObject.name == "Explosion") explosion = p;
            if (p.gameObject.name == "Sparks") sparks = p;
        }
        bombState = 0;
        fuseTimer = fuseLength;
        sparks.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (bombState == 1)
        {
            if (sparks.isStopped)
            {
                sparks.Play();
                AudioSource.PlayClipAtPoint(fuse_burn, Camera.main.transform.position);
            }

            fuseTimer -= Time.deltaTime;
            if (fuseTimer <= 0.0f)
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                explosion.transform.SetParent(null);
                explosion.Play();
                DamageNearbyObjects(gameObject.transform);
                Destroy(gameObject);
            }
        }
    }
    void DamageNearbyObjects(Transform tr)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(tr.position, 1.5f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Spiker")
            {
                Scoring.gamescore += 50;
                Destroy(colliders[i].gameObject);
            }
            if (colliders[i].gameObject.tag == "Robot")
            {
                Scoring.gamescore += 100;
                Destroy(colliders[i].gameObject);
            }
        }
    }
}
