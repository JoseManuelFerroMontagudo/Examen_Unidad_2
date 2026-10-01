using UnityEngine;

public class MoverFondo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float velocidadMovimiento = 0.5f;

    private Material material;
    private Rigidbody2D jugador;

    private float offsetX;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugador = GameObject.FindGameObjectWithTag("Player")
                            .GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        offsetX += jugador.linearVelocity.x * velocidadMovimiento * 0.001f * Time.deltaTime;
        material.mainTextureOffset = new Vector2(offsetX, 0);
    }
}