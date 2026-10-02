using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Principal");
    }
    public void Inicio()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Nivel2");
    }
    public void Salir()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
