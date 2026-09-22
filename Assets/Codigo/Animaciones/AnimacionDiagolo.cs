using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class AnimacionDiagolo : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_Text textoNombre;
    public EfectoTextoMaquina textoDialogoEfecto;
    public Button botonAvanzar;

    [Header("Modelo y Animación")]
    public Animator animatorRex;

    [Header("Navegación")]
    public Navegacion sistemaNavegacion;

    [Header("Datos del Diálogo")]
    public string[] lineasDialogo = {
        "¿Hola? ¿Hay alguien ahí?",
        "¡Woah!",
        "Creo que he bajado un poco de peso ¡estoy en los huesos!",
        "Y ni hablar de que solo soy una cabeza...",
        "Dejemos eso por ahora, seguro que luego hallaremos el resto de mi cuerpo. Ehrm...",
        "¡Saludos, paleontólogo en proceso! Mis amigos y yo requerimos tu ayuda.",
        "Hace mucho tiempo intentamos digitalizarnos para evitar la extinción.",
        "Pero parece que algunos códigos... genéticos, se perdieron por el camino.",
        "Si nos ayudas a encontrar los códigos y a ordenarlos de la forma correcta...",
        "¡Digitalizaríamos a mis amigos! eso es ¿Por qué no empezamos de una vez?",
        "¿Podrías ayudarme a encontrar el código de mi amigo Carnotauro? Apuesto a que se encuentra por aquí."
    };

    [Header("Animación por línea (nombres exactos del Animator)")]
    public string[] triggersAnimacion = {
        "Craneo Habla", "Expresión sorprendido", "Habla confundido", "Craneo Habla",
        "Craneo Habla", "Craneo Habla", "Craneo Habla", "Craneo Habla",
        "Craneo Habla", "Craneo Habla", "Craneo Habla"
    };

    private int indiceActual = 0;

    void Start()
    {
        StartCoroutine(IniciarDialogo());

        if (botonAvanzar != null)
        {
            botonAvanzar.onClick.RemoveAllListeners();
            botonAvanzar.onClick.AddListener(AvanzarDialogo);
        }
    }

    IEnumerator IniciarDialogo()
    {
        // Esperamos a que termine la animación inicial
        yield return new WaitForSeconds(2f);

        // Ahora sí aparece el primer diálogo
        MostrarLinea(0);
    }

    void MostrarLinea(int index)
    {
        if (index >= lineasDialogo.Length) return;

        textoNombre.text = "???";

        if (textoDialogoEfecto != null)
        {
            textoDialogoEfecto.IniciarEfecto(lineasDialogo[index]);
        }

        ReproducirAnimacion(index);
    }

    void ReproducirAnimacion(int index)
    {
        if (animatorRex == null || index >= triggersAnimacion.Length) return;

        foreach (string t in triggersAnimacion)
            animatorRex.ResetTrigger(t);

        animatorRex.SetTrigger(triggersAnimacion[index]);
    }

    public void AvanzarDialogo()
    {
        indiceActual++;

        if (indiceActual < lineasDialogo.Length)
        {
            MostrarLinea(indiceActual);
        }
        else
        {
            if (sistemaNavegacion != null)
            {
                sistemaNavegacion.ir_a_pistas();
            }
            else
            {
                Debug.LogError("Falta asignar 'Sistema Navegacion' en el Inspector de AnimacionDiagolo");
            }
        }
    }
}