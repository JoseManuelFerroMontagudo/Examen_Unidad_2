using UnityEngine;
using TMPro;
public class Monedas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text textoPuntos;
    public static int puntos = 0;
    void Awake()
    {
        // Reiniciamos los puntos a 0 solo si es la primera moneda que se inicializa en la escena
        // Esto evita que las demás monedas vuelvan a resetear el contador a 0
        if (GameObject.FindObjectsByType<Monedas>(FindObjectsSortMode.None)[0] == this)
        {
            puntos = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            puntos++;
            textoPuntos.text = "Puntos: " + puntos;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
