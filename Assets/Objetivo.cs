using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe simples que detecta o jogador no trigger e chama a próxima fase.
/// </summary>
public class Objetivo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D outro)
    {
        if (LayerMask.LayerToName(outro.gameObject.layer) == "Nave")
        {
            GameObject.Destroy(outro.gameObject);
            Jogo.AvancarFase();
        }
    }
}
