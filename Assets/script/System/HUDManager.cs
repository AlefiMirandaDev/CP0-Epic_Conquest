using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    // Referência ao script CharacterStats do jogador
    public CharacterStats playerStats;

    // Referências aos componentes UI
    public Slider vidaSlider;
    // Referência ao Slider de energia
    public Slider energiaSlider;
    // Referência ao TextMeshPro para exibir os cooldowns
    public TextMeshProUGUI cooldownText;

    // Simulando tempos de recarga para exemplo
    // Tempo de recarga do ataque 1
    public float cooldown1 = 5f;
    // Tempo de recarga do ataque 2
    public float cooldown2 = 8f;
    // Tempo de recarga do ataque 3
    public float cooldown3 = 12f;
    // Tempo de recarga do ataque especial
    public float cooldown4 = 20f;

    // Variáveis para armazenar os timers de cooldown
    // Timer para o cooldown do ataque 1
    private float cd1Timer;
    // Timer para o cooldown do ataque 2
    private float cd2Timer;
    // Timer para o cooldown do ataque 3
    private float cd3Timer;
    // Timer para o cooldown do ataque especial
    private float cd4Timer;

    private float lastHealth = -1;
    private float lastEnergy = -1;


    // Método chamado quando o script é ativado
    void Start()
    {
        TryInitializePlayerStats();
    }

    // Método chamado a cada frame
    void Update()
    {
        if (playerStats == null)
        {
            TryInitializePlayerStats();
            return;
        }

        UpdateHealthEnergy();
        UpdateCooldownTimers();
        UpdateCooldownText();
    }

    private void UpdateHealthEnergy()
    {
        // Atualiza a vida se mudou
        if (playerStats.CurrentHealth != lastHealth)
        {
            vidaSlider.value = Mathf.Clamp(playerStats.CurrentHealth, 0, playerStats.maxHealth);
            lastHealth = playerStats.CurrentHealth;
        }

        // Atualiza a energia se mudou
        if (playerStats.CurrentEnergy != lastEnergy)
        {
            energiaSlider.value = Mathf.Clamp(playerStats.CurrentEnergy, 0, playerStats.maxEnergy);
            lastEnergy = playerStats.CurrentEnergy;
        }
    }


    private void UpdateCooldownTimers()
    {
        if (cd1Timer > 0) cd1Timer -= Time.deltaTime;
        if (cd2Timer > 0) cd2Timer -= Time.deltaTime;
        if (cd3Timer > 0) cd3Timer -= Time.deltaTime;
        if (cd4Timer > 0) cd4Timer -= Time.deltaTime;
    }

    private void UpdateCooldownText()
    {
        cooldownText.text =
            $"Ataque1: {(cd1Timer > 0 ? cd1Timer.ToString("F1") + "s" : "Pronto")}\n" +
            $"Ataque2: {(cd2Timer > 0 ? cd2Timer.ToString("F1") + "s" : "Pronto")}\n" +
            $"Ataque3: {(cd3Timer > 0 ? cd3Timer.ToString("F1") + "s" : "Pronto")}\n" +
            $"Especial: {(cd4Timer > 0 ? cd4Timer.ToString("F1") + "s" : "Pronto")}";
    }

    private void TryInitializePlayerStats()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerStats = playerObject.GetComponent<CharacterStats>();

            if (playerStats != null)
            {
                vidaSlider.maxValue = playerStats.maxHealth;
                energiaSlider.maxValue = playerStats.maxEnergy;

                vidaSlider.value = playerStats.CurrentHealth;
                energiaSlider.value = playerStats.CurrentEnergy;

                lastHealth = playerStats.CurrentHealth;
                lastEnergy = playerStats.CurrentEnergy;
            }
        }
    }

    public void StartCooldown(int ataque)
    {
        switch (ataque)
        {
            case 1: cd1Timer = cooldown1; break;
            case 2: cd2Timer = cooldown2; break;
            case 3: cd3Timer = cooldown3; break;
            case 4: cd4Timer = cooldown4; break;
            default: Debug.LogWarning("Ataque inválido: " + ataque); break;
        }
    }
}
