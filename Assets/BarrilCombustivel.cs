using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cuida de detectar se a nave tocou no power-up e de mostrar na interface.
/// </summary>
public class BarrilCombustivel : MonoBehaviour
{
    public float combustivel;

    void Start()
    {
        GetComponentInChildren<Text>().text = combustivel.ToString("00");
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        // garante que é o jogador
        if (LayerMask.LayerToName(outro.gameObject.layer) == "Nave")
        {
            outro.GetComponent<Nave>().AlterarCombustivel(combustivel);
            GameObject.Destroy(gameObject);
        }
    }
}

