using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnimacionDiagolo : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_Text textoNombre;
    public TMP_Text textoDialogo;
    public Button botonAvanzar;

    [Header("Modelo y Animaci�n")]
    public Animator animatorRex;

    [Header("Navegaci�n")]
    public Navegacion sistemaNavegacion;

    [Header("Datos del Di�logo")]
    public string[] lineasDialogo = {
        "�Hola? �Hay alguien ah�?",
        "�Woah!",
        "Creo que he bajado un poco de peso �estoy en los huesos!",
        "Y ni hablar de que solo soy una cabeza...",
        "Dejemos eso por ahora, seguro que luego hallaremos el resto de mi cuerpo. Ehrm...",
        "�Saludos, paleont�logo en proceso! Mis amigos y yo requerimos tu ayuda.",
        "Hace mucho tiempo intentamos digitalizarnos para evitar la extinci�n.",
        "Pero parece que algunos c�digos... gen�ticos, se perdieron por el camino.",
        "Si nos ayudas a encontrar los c�digos y a ordenarlos de la forma correcta...",
        "�Digitalizar�amos a mis amigos! eso es �Por qu� no empezamos de una vez?",
        "�Podr�as ayudarme a encontrar el c�digo de mi amigo Carnotauro? Apuesto a que se encuentra por aqu�."
    };

    [Header("Animaci�n por l�nea (nombres exactos del Animator)")]
    public string[] triggersAnimacion = {
        "Craneo Habla",            // 0  �Hola?...
        "Expresi�n sorprendido",   // 1  �Woah!  (luego pasa solo a Boquiabierto Est�tico)
        "Habla confundido",        // 2  ...en los huesos
        "Craneo Habla",            // 3
        "Craneo Habla",            // 4
        "Craneo Habla",            // 5
        "Craneo Habla",            // 6
        "Craneo Habla",            // 7
        "Craneo Habla",            // 8
        "Craneo Habla",            // 9
        "Craneo Habla"             // 10
    };

    private int indiceActual = 0;

    void Start()
    {
        MostrarLinea(0);

        if (botonAvanzar != null)
        {
            botonAvanzar.onClick.RemoveAllListeners();
            botonAvanzar.onClick.AddListener(AvanzarDialogo);
        }
    }

    void MostrarLinea(int index)
    {
        if (index >= lineasDialogo.Length) return;

        textoDialogo.text = lineasDialogo[index];
        textoNombre.text = "???";

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