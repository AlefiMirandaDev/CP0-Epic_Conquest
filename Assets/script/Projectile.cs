using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20f;
    public float lifetime = 5f;
    public LayerMask enemyLayers;

    private float lifeTimer;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayers) != 0)
        {
            CharacterStats enemyStats = other.GetComponent<CharacterStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}