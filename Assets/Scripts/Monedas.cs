using UnityEngine;
using TMPro;
public class Monedas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text textoPuntos;
    public static int puntos = 0;
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
