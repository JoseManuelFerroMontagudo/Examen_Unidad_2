using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioNivel : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [Tooltip("Nombre exacto de la escena a la que se va a cambiar")]
    public string nombreEscena = "Nivel2";

    [Tooltip("Etiqueta (Tag) del personaje jugador")]
    public string tagJugador = "Player";

    private bool yaSeActivo = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Evita múltiples activaciones si se colisiona rápido
        if (yaSeActivo) return;

        if (collision.CompareTag(tagJugador))
        {
            yaSeActivo = true;

            // Hace desaparecer el cofre inmediatamente al tocarlo
            gameObject.SetActive(false);

            // Carga la escena objetivo (Nivel2)
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
