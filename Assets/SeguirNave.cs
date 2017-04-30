using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movimento de câmera em X, com limitação nas pontas e usando SoothDamp. 
/// Ela cuida de detectar e ligar com a Nave.
/// </summary>
public class SeguirNave : MonoBehaviour
{
    public float velocidadeMaxEmX;
    public float minX, maxX;

    private Nave _nave;
    private Transform _tr;
    private float _velocidadeAtual;

    void Start()
    {
        _nave = GameObject.FindObjectOfType<Nave>();
        _tr = transform;
        _velocidadeAtual = 0f;
    }

    void Update()
    {
        // como a nave pode ser destruida durante a fase,
        // é melhor verificar sempre antes de processar
        if (_nave == null)
        {
            return;
        }
        float novaPosX = Mathf.SmoothDamp(_tr.position.x, _nave.transform.position.x, ref _velocidadeAtual, Time.smoothDeltaTime, velocidadeMaxEmX);
        novaPosX = Mathf.Clamp(novaPosX, minX, maxX);
        Vector3 pos = _tr.position;
        pos.x = novaPosX;
        _tr.position = pos;
    }
}
