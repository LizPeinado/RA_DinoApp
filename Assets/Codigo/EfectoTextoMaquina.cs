using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EfectoTextoMaquina : MonoBehaviour
{
    private TMP_Text textoComponente;
    private AudioSource audioSource;
    private Coroutine corutinaEscritura;

    [Header("Configuraci�n de Escritura")]
    [Tooltip("Tiempo de espera entre cada letra")]
    public float velocidadEscritura = 0.03f;

    [Header("Frecuencia del Sonido (Estilo Animal Crossing)")]
    [Range(1, 5)]
    [Tooltip("�Cada cu�ntas letras suena? 1 = suena siempre, 2 = una s� y una no, 3 = cada tres letras...")]
    public int reproducirCadaNLetras = 2; // <-- �ESTO EVITA LA SATURACI�N!

    [Header("Configuraci�n de Sonido")]
    public AudioClip[] sonidosBlip;
    [Range(0.1f, 0.5f)] public float variacionPitch = 0.12f;
    
    private float pitchOriginal;

    void Awake()
    {
        textoComponente = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        pitchOriginal = audioSource.pitch;
    }

    public void IniciarEfecto(string textoCompleto)
    {
        if (corutinaEscritura != null)
        {
            StopCoroutine(corutinaEscritura);
        }
        corutinaEscritura = StartCoroutine(EscribirTexto(textoCompleto));
    }

    private IEnumerator EscribirTexto(string textoCompleto)
    {
        textoComponente.text = "";
        int contadorLetras = 0; // Cuenta cu�ntas letras v�lidas se van escribiendo

        foreach (char letra in textoCompleto.ToCharArray())
        {
            textoComponente.text += letra;

            // Evitamos espacios y signos de puntuaci�n mudos
            if (!char.IsWhiteSpace(letra) && !char.IsPunctuation(letra) && sonidosBlip.Length > 0)
            {
                contadorLetras++;

                // Solo suena si el residuo es cero (Ej: si es 2, sonar� en la letra 2, 4, 6...)
                if (contadorLetras % reproducirCadaNLetras == 0)
                {
                    ReproducirSonidoLetra();
                }
            }

            yield return new WaitForSeconds(velocidadEscritura);
        }
    }

    private void ReproducirSonidoLetra()
    {
        // Variaci�n sutil de pitch para que suene org�nico
        audioSource.pitch = pitchOriginal + Random.Range(-variacionPitch, variacionPitch);

        AudioClip clipAleatorio = sonidosBlip[Random.Range(0, sonidosBlip.Length)];
        audioSource.PlayOneShot(clipAleatorio);
    }
}
