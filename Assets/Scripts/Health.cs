using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem shipDamagedEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
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
            Destroy(gameObject);
        }
    }

    void PlayHitEffect()
    {
        if (shipDamagedEffect != null)
        {
            ParticleSystem instance = Instantiate(shipDamagedEffect, transform.position, Quaternion.identity);
            Debug.Log(transform.position);
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
}
