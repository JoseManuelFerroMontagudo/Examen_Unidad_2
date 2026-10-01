using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Adjunta este script al GameObject del enemigo (tag = "Enemy").
/// Cuando el jugador (tag = "Player") lo toca, carga la escena de muerte.
/// </summary>
public class EnemigoCuerpoACuerpo : MonoBehaviour
{
    [Header("Escena de Muerte")]
    [Tooltip("Nombre exacto de la escena de muerte en Build Settings")]
    public string escenaMuerte = "Nivel2";

    [Tooltip("Tag del jugador")]
    public string tagJugador = "Player";

    private bool yaActivo = false;

    private void Start()
    {
        // Congela la posición en Y y la rotación para que el enemigo NO caiga
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            // Si quieres que pueda moverse en X pero no caer, usa esto en su lugar:
            // rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Se llama cuando el COLLIDER del enemigo es Trigger y toca al jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (yaActivo) return;

        if (collision.CompareTag(tagJugador))
        {
            yaActivo = true;
            SceneManager.LoadScene(escenaMuerte);
        }
    }

    // Se llama si el collider NO es Trigger (colisión física directa)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (yaActivo) return;

        if (collision.gameObject.CompareTag(tagJugador))
        {
            yaActivo = true;
            SceneManager.LoadScene(escenaMuerte);
        }
    }
}
