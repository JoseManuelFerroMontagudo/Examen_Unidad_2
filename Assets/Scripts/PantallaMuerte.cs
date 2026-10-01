using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Adjunta este script a un GameObject vacío en la escena Nivel2 (escena de muerte).
/// Muestra una pantalla roja con animación y permite reiniciar.
/// </summary>
public class PantallaMuerte : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Nombre de la escena principal para reiniciar")]
    public string escenaPrincipal = "SampleScene";

    [Tooltip("Tiempo en segundos antes de mostrar el botón de reinicio")]
    public float tiempoEspera = 2.5f;

    private CanvasGroup canvasGroup;
    private GameObject panelMuerte;
    private GameObject textMuerte;
    private GameObject botonReinicio;

    private void Start()
    {
        CrearUI();
        StartCoroutine(AnimacionMuerte());
    }

    private void CrearUI()
    {
        // Canvas
        GameObject canvasGO = new GameObject("CanvasMuerte");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;
        canvasGO.AddComponent<UnityEngine.UI.CanvasScaler>();
        canvasGO.AddComponent<UnityEngine.UI.GraphicRaycaster>();

        // Panel rojo oscuro de fondo
        panelMuerte = new GameObject("PanelMuerte");
        panelMuerte.transform.SetParent(canvasGO.transform, false);
        UnityEngine.UI.Image imgPanel = panelMuerte.AddComponent<UnityEngine.UI.Image>();
        imgPanel.color = new Color(0.15f, 0f, 0f, 0f); // rojo oscuro, empieza transparente
        RectTransform rt = panelMuerte.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        canvasGroup = panelMuerte.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        // Texto "HAS MUERTO"
        textMuerte = new GameObject("TextoMuerte");
        textMuerte.transform.SetParent(canvasGO.transform, false);
        TMPro.TextMeshProUGUI tmp = textMuerte.AddComponent<TMPro.TextMeshProUGUI>();
        tmp.text = "** HAS MUERTO **";
        tmp.fontSize = 80;
        tmp.color = new Color(1f, 0.1f, 0.1f, 0f); // rojo, empieza transparente
        tmp.alignment = TMPro.TextAlignmentOptions.Center;
        tmp.fontStyle = TMPro.FontStyles.Bold;
        RectTransform rtText = textMuerte.GetComponent<RectTransform>();
        rtText.anchorMin = new Vector2(0.1f, 0.55f);
        rtText.anchorMax = new Vector2(0.9f, 0.85f);
        rtText.offsetMin = Vector2.zero;
        rtText.offsetMax = Vector2.zero;

        // Texto subtítulo
        GameObject subTexto = new GameObject("SubTexto");
        subTexto.transform.SetParent(canvasGO.transform, false);
        TMPro.TextMeshProUGUI sub = subTexto.AddComponent<TMPro.TextMeshProUGUI>();
        sub.text = "El enemigo fue demasiado para ti...";
        sub.fontSize = 28;
        sub.color = new Color(1f, 0.5f, 0.5f, 0f);
        sub.alignment = TMPro.TextAlignmentOptions.Center;
        RectTransform rtSub = subTexto.GetComponent<RectTransform>();
        rtSub.anchorMin = new Vector2(0.1f, 0.42f);
        rtSub.anchorMax = new Vector2(0.9f, 0.56f);
        rtSub.offsetMin = Vector2.zero;
        rtSub.offsetMax = Vector2.zero;

        // Botón Reiniciar
        botonReinicio = new GameObject("BotonReinicio");
        botonReinicio.transform.SetParent(canvasGO.transform, false);
        UnityEngine.UI.Image imgBtn = botonReinicio.AddComponent<UnityEngine.UI.Image>();
        imgBtn.color = new Color(0.6f, 0f, 0f, 1f);
        UnityEngine.UI.Button btn = botonReinicio.AddComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(Reiniciar);
        RectTransform rtBtn = botonReinicio.GetComponent<RectTransform>();
        rtBtn.anchorMin = new Vector2(0.35f, 0.25f);
        rtBtn.anchorMax = new Vector2(0.65f, 0.38f);
        rtBtn.offsetMin = Vector2.zero;
        rtBtn.offsetMax = Vector2.zero;

        // Texto dentro del botón
        GameObject textoBtn = new GameObject("TextoBoton");
        textoBtn.transform.SetParent(botonReinicio.transform, false);
        TMPro.TextMeshProUGUI tmpBtn = textoBtn.AddComponent<TMPro.TextMeshProUGUI>();
        tmpBtn.text = "REINTENTAR";
        tmpBtn.fontSize = 30;
        tmpBtn.color = Color.white;
        tmpBtn.alignment = TMPro.TextAlignmentOptions.Center;
        tmpBtn.fontStyle = TMPro.FontStyles.Bold;
        RectTransform rtTxtBtn = textoBtn.GetComponent<RectTransform>();
        rtTxtBtn.anchorMin = Vector2.zero;
        rtTxtBtn.anchorMax = Vector2.one;
        rtTxtBtn.offsetMin = Vector2.zero;
        rtTxtBtn.offsetMax = Vector2.zero;

        botonReinicio.SetActive(false); // empieza oculto
    }

    private IEnumerator AnimacionMuerte()
    {
        float duracion = 1.5f;
        float tiempo = 0f;

        // Fade in del panel rojo y el texto
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;
            canvasGroup.alpha = t;

            // También anima el texto de muerte
            TMPro.TextMeshProUGUI tmp = textMuerte.GetComponent<TMPro.TextMeshProUGUI>();
            if (tmp != null)
                tmp.color = new Color(1f, 0.1f, 0.1f, t);

            // Efecto: escala del texto crece dramáticamente
            float escala = Mathf.Lerp(0.3f, 1f, t);
            textMuerte.transform.localScale = new Vector3(escala, escala, 1f);

            yield return null;
        }

        // Efecto de pulso en el texto
        StartCoroutine(PulsoTexto());

        // Espera antes de mostrar el botón
        yield return new WaitForSeconds(tiempoEspera);

        botonReinicio.SetActive(true);
    }

    private IEnumerator PulsoTexto()
    {
        TMPro.TextMeshProUGUI tmp = textMuerte.GetComponent<TMPro.TextMeshProUGUI>();
        while (true)
        {
            float t = (Mathf.Sin(Time.time * 3f) + 1f) / 2f;
            if (tmp != null)
                tmp.color = Color.Lerp(new Color(0.8f, 0f, 0f, 1f), new Color(1f, 0.4f, 0.4f, 1f), t);
            yield return null;
        }
    }

    private void Reiniciar()
    {
        SceneManager.LoadScene(escenaPrincipal);
    }
}
