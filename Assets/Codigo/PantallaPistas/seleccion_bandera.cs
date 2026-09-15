using UnityEngine;

public class SeleccionBandera : MonoBehaviour
{
    public static string bandera_seleccionada = "";

    public void seleccionar_azul()
    {
        bandera_seleccionada = "azul";
    }

    public void seleccionar_verde()
    {
        bandera_seleccionada = "verde";
    }

    public void seleccionar_naranja()
    {
        bandera_seleccionada = "naranja";
    }
}