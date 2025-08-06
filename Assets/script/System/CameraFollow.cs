using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Script para seguir o her�i ativo na cena
    [Header("Configura��es da c�mera")]

    // Her�i ativo a seguir
    public Transform target;

    // Posi��o relativa da c�mera em rela��o ao her�i
    public Vector3 offset = new Vector3(0, 15, -15);

    // Velocidade de suaviza��o
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula a posi��o desejada
        Vector3 desiredPosition = target.position + offset;

        // Suaviza o movimento da c�mera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Faz a c�mera olhar para o her�i (opcional para est�tica)
        transform.LookAt(target);
    }

    // M�todo p�blico para atualizar o alvo da c�mera
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
