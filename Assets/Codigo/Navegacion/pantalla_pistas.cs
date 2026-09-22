using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PantallaPistas : MonoBehaviour
{
    public TMP_InputField campo_codigo;

    // Sonido para botones y código correcto
    public AudioClip clickSound;
    // Sonido para código incorrecto
    public AudioClip errorSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void ir_a_mapa()
    {
        StartCoroutine(CargarEscenaConSonido("Mapa", clickSound));
    }

    public void comprobar_codigo()
    {
        string codigo = campo_codigo.text.ToUpper();

        if (codigo.Length == 3 && codigo == "REX")
        {
            // Código correcto: reproduce clickSound y carga escena
            StartCoroutine(CargarEscenaConSonido("EscenaAR", clickSound));
        }
        else
        {
            Debug.Log("Codigo incorrecto");
            // Código incorrecto: reproduce errorSound
            if (errorSound != null)
            {
                audioSource.PlayOneShot(errorSound);
            }
        }
    }

    // Corrutina reutilizable que recibe el sonido a usar
    IEnumerator CargarEscenaConSonido(string nombreEscena, AudioClip sonido)
    {
        if (sonido != null)
        {
            audioSource.PlayOneShot(sonido);

            // Espera a que termine el sonido
            yield return new WaitForSeconds(sonido.length);
        }
        else
        {
            // Pausa mínima de seguridad
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.LoadScene(nombreEscena);
    }
}