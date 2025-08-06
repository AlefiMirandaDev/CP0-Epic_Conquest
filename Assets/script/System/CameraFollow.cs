using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Script para seguir o herói ativo na cena
    [Header("Configurações da câmera")]

    // Herói ativo a seguir
    public Transform target;

    // Posição relativa da câmera em relação ao herói
    public Vector3 offset = new Vector3(0, 15, -15);

    // Velocidade de suavização
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula a posição desejada
        Vector3 desiredPosition = target.position + offset;

        // Suaviza o movimento da câmera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Faz a câmera olhar para o herói (opcional para estética)
        transform.LookAt(target);
    }

    // Método público para atualizar o alvo da câmera
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
