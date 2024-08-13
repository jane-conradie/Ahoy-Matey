using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem shipDamagedEffect;

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
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
}
