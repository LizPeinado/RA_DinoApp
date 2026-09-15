using UnityEngine;

public class BotonLupa : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject cuadro_texto;

    public void mostrar_cuadro_texto()
    {
        if (cuadro_texto.activeSelf)
        {
            cuadro_texto.SetActive(false);
        }
        else
        {
            cuadro_texto.SetActive(true);
        }
    }
}
