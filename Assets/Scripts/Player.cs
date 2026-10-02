using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

using Debug = UnityEngine.Debug;

public class Jugador : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector2 movimiento;
    public float velocidad = 6f;
    public float fuerzaFuerza = 6f;
    private int vida = 2;
    private Animator animator;
    [SerializeField] private Collider2D hitbox1;
    [SerializeField] private Collider2D hitbox2;
    public TMP_Text textoVida;

    // Funciones para el collider 1
    public void EnableHitbox1() => hitbox1.enabled = true;
    public void DisableHitbox1() => hitbox1.enabled = false;

    // Funciones para el collider 2
    public void EnableHitbox2() => hitbox2.enabled = true;
    public void DisableHitbox2() => hitbox2.enabled = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnMove(InputValue inputValue)
    {
        movimiento = inputValue.Get<Vector2>();
        animator.SetFloat("Caminar", Mathf.Abs(movimiento.x));
    }

    void OnJump()
    {
        rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocityX, fuerzaFuerza);
        animator.SetBool("Saltar", true);
    }
    void OnAttack()
    {
        if (Mathf.Abs(movimiento.x) > 0.01f) return;
        animator.SetTrigger("Pelear");
    }

    void OnAttack2()
    {
        if (Mathf.Abs(movimiento.x) > 0.01f) return;
        animator.SetTrigger("Pelear2");
    }
    void FixedUpdate()
    {
        if (movimiento.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movimiento.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        rigidbody2D.linearVelocity = new Vector2(movimiento.x * velocidad, rigidbody2D.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Piso"))
        {
            animator.SetBool("Saltar", false);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            vida--;
            if (vida > 0)
            {
                Debug.Log("Existe Colision, vida: " + vida);
                textoVida.text = "Vida: " + vida;
            }
            else if (vida == 0)
            {
                SceneManager.LoadScene("Game Over");
                Debug.Log("Muerto: vida" + vida);
            }
        }
    }

}