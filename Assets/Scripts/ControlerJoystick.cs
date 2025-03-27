using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJoyStick : MonoBehaviour
{
    Rigidbody2D _personagem;
    public float _velocidade = 15;
    public float _velocidadeDePulo = 5;
    private float horizontalMove;
    bool _estaNoChao;
    bool _podePuloDuplo;
    bool _virandoRosto = true;
    public float delaySegundoPulo;
    bool moverEsquerda;
    bool moverDireita;

    void Start()
    {
        _personagem = GetComponent<Rigidbody2D>();
        moverEsquerda = false;
        moverDireita = false;
    }

    public void PointerDownleft()
    {
        moverEsquerda = true;
    }

    public void PointerUpleft()
    {
        moverEsquerda = false;
    }

    public void PointerDownRight()
    {
        moverDireita = true;
    }

    public void PointerUpRight()
    {
        moverDireita = false;
    }

    void FixedUpdate()
    {
        horizontalMove = 0;

        // Determina a direção horizontal do movimento
        if (moverEsquerda)
        {
            horizontalMove = -_velocidade;
        }
        if (moverDireita)
        {
            horizontalMove = _velocidade;
        }

        // Aplica a velocidade no Rigidbody, mantendo a velocidade vertical do personagem
        _personagem.linearVelocity = new Vector2(horizontalMove, _personagem.linearVelocity.y);

        // Gira o personagem conforme o movimento
        if ((_virandoRosto && horizontalMove < 0) || (!_virandoRosto && horizontalMove > 0))
        {
            OlhandoParaOsLados();
        }
    }

    void OlhandoParaOsLados()
    {
        _virandoRosto = !_virandoRosto;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; // Inverte o eixo X para virar o personagem
        transform.localScale = scaler;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("chao"))
        {
            _estaNoChao = true;
            _podePuloDuplo = false; // Reseta o pulo duplo ao tocar o chão
        }
    }

    public void JumButton()
    {
        if (_estaNoChao)
        {
            _estaNoChao = false;
            _personagem.linearVelocity = Vector2.up * _velocidadeDePulo;
            Invoke("EnableDoubleJump", delaySegundoPulo);
        }
        else if (_podePuloDuplo)
        {
            _personagem.linearVelocity = Vector2.up * _velocidadeDePulo;
            _podePuloDuplo = false;            
        }
    }

    void EnableDoubleJump()
    {
        _podePuloDuplo = true; // Habilita o pulo duplo após o tempo de delay
    }
}
