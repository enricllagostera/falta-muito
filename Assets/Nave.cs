using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nave : MonoBehaviour
{
    public float combustivel;
    public float taxaConsumo;
    public float fogueteBaixo;
    public float fogueteLados;

    private Rigidbody2D _rb;
    private Vector2 _input;
    private float _combustivelMaximo;

    public ParticleSystem particulasBaixo, particulasEsquerda, particulasDireita;

    public Text txtCombustivel;
    public GameObject perigoInterface;
    public Image barraCombustivel;

    public float toleranciaColisao;
    public Transform prefabExplosao;

    void Start()
    {
        // preparar componentes
        _rb = GetComponent<Rigidbody2D>();
        particulasBaixo.Stop();
        particulasEsquerda.Stop();
        particulasDireita.Stop();
        _combustivelMaximo = combustivel;
    }

    void Update()
    {
        // checar input
        _input.y = Mathf.Clamp01(Input.GetAxis("Vertical"));
        _input.x = Input.GetAxis("Horizontal");
        // consumir combustivel se tiver qualquer input
        if (_input.sqrMagnitude > 0f)
        {
            combustivel = Mathf.Clamp(combustivel - Time.deltaTime * taxaConsumo, 0, _combustivelMaximo);
        }
        ControlarParticulas();
        AtualizarUI();
    }

    void FixedUpdate()
    {
        if (!TemCombustivel())
        {
            return;
        }
        // aplicar forças
        _rb.AddRelativeForce(Vector2.up * _input.y * fogueteBaixo);
        _rb.AddRelativeForce(Vector2.right * _input.x * fogueteLados);
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        // checa para ver se a nave vai explodir
        if (colisao.relativeVelocity.magnitude > toleranciaColisao)
        {
            Instantiate(prefabExplosao, transform.position, transform.rotation);
            GameObject.Destroy(gameObject);
            Jogo.RecarregarFase();
        }
    }

    bool TemCombustivel()
    {
        return (combustivel - taxaConsumo * Time.fixedDeltaTime > 0f);
    }

    void AtualizarUI()
    {
        txtCombustivel.text = "gasosa\n" + Mathf.FloorToInt(combustivel);
        barraCombustivel.rectTransform.localScale = new Vector3(combustivel / _combustivelMaximo, 1, 1);
        if (_rb.velocity.magnitude >= toleranciaColisao)
        {
            if (Mathf.Floor(Time.realtimeSinceStartup * 10) % 2 == 0)
            {
                perigoInterface.SetActive(true);
            }
            else
            {
                perigoInterface.SetActive(false);
            }
        }
        else
        {
            perigoInterface.SetActive(false);
        }
    }

    void ControlarParticulas()
    {
        if (!TemCombustivel())
        {
            particulasBaixo.Stop();
            particulasEsquerda.Stop();
            particulasDireita.Stop();
            return;
        }
        // checar direção de input e ligar particulas de foguetes
        if (_input.y > 0f)
        {
            if (!particulasBaixo.isPlaying)
            {
                particulasBaixo.Play();    
            }

        }
        else
        {
            particulasBaixo.Stop();
        }

        if (_input.x > 0f)
        {
            if (!particulasEsquerda.isPlaying)
            {
                particulasEsquerda.Play();
            }
        }
        else
        {
            particulasEsquerda.Stop();
        }

        if (_input.x < 0f)
        {
            if (!particulasDireita.isPlaying)
            {
                particulasDireita.Play();    
            }

        }
        else
        {
            particulasDireita.Stop();
        }
    }

    public void ReiniciarJogo()
    {
        Jogo.AvancarFase();
    }

    public void AlterarCombustivel(float modCombustivel)
    {
        combustivel = Mathf.Clamp(combustivel + modCombustivel, 0, _combustivelMaximo);
    }
}

