using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerDash : MonoBehaviour
{


    public float dashDistance = 5f;
    public float dashCooldown = 2f;
    public float dashDuration = 0.2f;
    public float invulnerableTime = 0.3f;

    private float dashTimer;
    private bool isDashing = false;
    private Vector3 dashDirection;

    private CharacterController controller;
    private PlayerController playerController;
    private InvulnerabilityWindow invul;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        invul = GetComponent<InvulnerabilityWindow>();
    }

    void Update()
    {
        dashTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer >= dashCooldown && !isDashing)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        dashTimer = 0f;
        isDashing = true;

        //usa a direção do input, se for disponível
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Direção baseada no movimento atual do PlayerController (se estiver usando vetores de movimento)
        dashDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (dashDirection == Vector3.zero)
        {
            // Se não houver input, usa a direção atual do jogador
            dashDirection = transform.forward;
        }

        // Desativa o controle do jogador durante o dash
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Ativa invulnerabilidade, se houver esse script no jogador
        if (invul != null)
        {
            invul.Activate(invulnerableTime);
        }

        // Realiza o dash
        float elapsed = 0f;

        // Move o jogador na direção do dash
        while (elapsed < dashDuration)
        {
            controller.Move(dashDirection * (dashDistance / dashDuration) * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reativa controle do jogador
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Reseta o estado de dashing
        isDashing = false;
    }
}
