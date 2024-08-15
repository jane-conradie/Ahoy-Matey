using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem shipDamagedEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            audioPlayer.PlayDamageClip();
            PlayHitEffect();
            ShakeCamera();
            // destroy the damage dealer
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            if (isPlayer)
            {
                levelManager.LoadGameOver();
            }
            Die();
        }

        scoreKeeper.ModifyScore(health, isPlayer);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (shipDamagedEffect != null)
        {
            ParticleSystem instance = Instantiate(shipDamagedEffect, transform.position, Quaternion.identity);

            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

     void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
           cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
