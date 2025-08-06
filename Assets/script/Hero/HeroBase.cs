using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe base abstrata para heróis

public abstract class HeroBase : MonoBehaviour
{
    // Variáveis básicas do herói
    [Header("Status Básicos")]
    public string heroName;
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public int baseAttack;
    public int baseDefense;

    // Referência para o HUD
    protected HUDManager hud;

    // Métodos abstratos que devem ser implementados pelas subclasses
    protected virtual void Start()
    {
        // Inicializa valores atuais com máximo
        currentHealth = maxHealth;
        currentMana = maxMana;

        // Busca o HUDManager na cena
        hud = FindObjectOfType<HUDManager>();
    }

    // Métodos de ataque abstratos que devem ser implementados pelas subclasses
    public virtual void TakeDamage(int damage)
    {
        // Verifica se o herói está invulnerável
        int finalDamage = Mathf.Max(damage - baseDefense, 0);
        // Aplica dano ao herói, considerando defesa
        currentHealth -= finalDamage;

        Debug.Log($"{heroName} recebeu {finalDamage} de dano.");

        // Atualiza o HUD se estiver presente
        if (currentHealth <= 0)
        {
            // Se a vida chegar a zero, chama o método de morte
            Die();
        }
    }

    // Método para curar o herói
    public virtual void Heal(int amount)
    {
        // Verifica se a cura não ultrapassa o máximo
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        // Atualiza o HUD se estiver presente
        Debug.Log($"{heroName} curou {amount} de vida.");
    }

    // Método para usar mana
    public bool UseMana(int amount)
    {
        // Verifica se há mana suficiente
        if (currentMana < amount) return false;
        currentMana -= amount;
        return true;
    }

    // Método para regenerar mana
    public virtual void Die()
    {
        // Define a vida atual como zero
        Debug.Log($"{heroName} morreu!");
        // Aqui pode colocar animação, desabilitar controles etc
    }

    // Método abstrato para lidar com a entrada do jogador
    public abstract void HandleInput();

    // Método chamado a cada frame para lidar com a entrada
    protected virtual void Update()
    {
        // Chama o método de entrada do jogador
        HandleInput();
    }
}