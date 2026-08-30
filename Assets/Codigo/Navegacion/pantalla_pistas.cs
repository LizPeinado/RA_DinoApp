using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PantallaPistas : MonoBehaviour
{
    public TMP_InputField campo_codigo;

    public void ir_a_mapa()
    {
        SceneManager.LoadScene("Mapa");
    }

    public void comprobar_codigo()
    {
        string codigo = campo_codigo.text.ToUpper();

        if (codigo.Length == 3 && codigo == "REX")
        {
            SceneManager.LoadScene("EscenaAR");
        }
        else
        {
            Debug.Log("Codigo incorrecto");
        }
    }
}