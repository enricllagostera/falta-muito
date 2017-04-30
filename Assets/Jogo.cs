using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que controla o carregamento de fases, transições e a saída do aplicativo. 
/// Usa campos e funções estáticas para facilitar acesso.
/// </summary>
public class Jogo : MonoBehaviour
{
    private static Jogo _instancia;

    void Awake()
    {
        _instancia = this;   
    }

    void Update()
    {
        // detecta o ESC para sair do jogo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Essa função dispara a coroutine de avançar fases.
    /// </summary>
    public static void AvancarFase()
    {
        _instancia.StartCoroutine(_instancia.AvancarFaseCR());
    }

    /// <summary>
    /// Essa coroutine para permite maior controle temporal da transição para próxima fase 
    /// nos BuildSettings (volta para 0 no fim da lista).
    /// </summary>
    private IEnumerator AvancarFaseCR()
    {
        yield return new WaitForSecondsRealtime(1f);
        int level = SceneManager.GetActiveScene().buildIndex;
        level++;
        if (level >= SceneManager.sceneCountInBuildSettings)
        {
            level = 0;
        }
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// Recarrega a cena com a fase atual.
    /// </summary>
    public static void RecarregarFase()
    {
        _instancia.StartCoroutine(_instancia.RecarregarFaseCR());
    }

    private IEnumerator RecarregarFaseCR()
    {
        yield return new WaitForSecondsRealtime(1f);
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
    }
}

