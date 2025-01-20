using UnityEngine;
using System.Collections; // Gerekli ad alaný eklendi

public class Enemy : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
