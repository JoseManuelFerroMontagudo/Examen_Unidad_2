using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class MoverJugador : MonoBehaviour
{
    private Vector2 movimiento;
    private Rigidbody2D rb;
    public float velocidad = 9f;
    public float fuerza_salto = 10f;
    private int puntos = 5;
    private Animator animator;
    [SerializeField] private Collider2D hitbox1;
    [SerializeField] private Collider2D hitbox2;

    // Funciones para el collider 1 (opcionales, usadas por animaciones)
    public void EnableHitbox1()  { if (hitbox1 != null) hitbox1.enabled = true; }
    public void DisableHitbox1() { if (hitbox1 != null) hitbox1.enabled = false; }

    // Funciones para el collider 2
    public void EnableHitbox2()  { if (hitbox2 != null) hitbox2.enabled = true; }
    public void DisableHitbox2() { if (hitbox2 != null) hitbox2.enabled = false; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void OnMove(InputValue inputValue)
    {
        movimiento = inputValue.Get<Vector2>();
        animator.SetFloat("Caminar", Mathf.Abs(movimiento.x));
    }
    void OnJump()
    {
        //rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        rb.linearVelocity = new Vector2(rb.linearVelocityX, fuerza_salto);
        animator.SetBool("Saltar", true);
    }
    void OnAttack()
    {
        animator.SetTrigger("Pelear");
    }
    void OnAttack2()
    {
        animator.SetTrigger("Pelear2");
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimiento.x * velocidad, rb.linearVelocityY);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Roca"))
        {
            puntos--;
            if (puntos > 0)
            {
                Debug.Log("Existe Colision, Puntos: " + puntos);
            }
            else if (puntos == 0)
            {
                Debug.Log("Muerto: Puntos" + puntos);
            }
        }
        if (collision.gameObject.CompareTag("Suelo"))
        {
            animator.SetBool("Saltar", false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}