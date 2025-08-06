using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxEnergy = 100f;

    public float CurrentHealth { get; private set; }
    public float CurrentEnergy { get; private set; }

    public bool IsDead { get; private set; } = false;

    public float healthRegenRate = 1f;
    public float energyRegenRate = 1f;

    private InvulnerabilityWindow invulWindow;

    public delegate void CharacterDeath();
    public event CharacterDeath OnDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        CurrentEnergy = maxEnergy;
        invulWindow = GetComponent<InvulnerabilityWindow>();
    }

    private void Update()
    {
        if (IsDead) return;

        Regenerate();
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        if (invulWindow != null && invulWindow.IsInvulnerable)
            return;

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        if (IsDead || amount <= 0) return;
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
    }

    public bool UseEnergy(float amount)
    {
        if (IsDead || CurrentEnergy < amount)
            return false;
        CurrentEnergy -= amount;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, maxEnergy);
        return true;
    }

    public void RechargeEnergy(float amount)
    {
        if (IsDead || amount <= 0) return;
        CurrentEnergy += amount;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, maxEnergy);
    }

    private void Regenerate()
    {
        if (CurrentHealth < maxHealth)
            CurrentHealth += healthRegenRate * Time.deltaTime;

        if (CurrentEnergy < maxEnergy)
            CurrentEnergy += energyRegenRate * Time.deltaTime;

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, maxEnergy);
    }

    private void Die()
    {
        IsDead = true;
        Debug.Log($"{gameObject.name} morreu!");
        OnDeath?.Invoke();
        // Aqui você pode adicionar animações, desativar objetos, etc
    }
}