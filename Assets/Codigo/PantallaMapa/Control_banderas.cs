using UnityEngine;

public class ControlBanderas : MonoBehaviour
{
    public GameObject bandera_azul;
    public GameObject bandera_verde;
    public GameObject bandera_naranja;

    void Start()
    {
        bandera_azul.SetActive(false);
        bandera_verde.SetActive(false);
        bandera_naranja.SetActive(false);

        if (SeleccionBandera.bandera_seleccionada == "azul")
        {
            bandera_azul.SetActive(true);
        }

        if (SeleccionBandera.bandera_seleccionada == "verde")
        {
            bandera_verde.SetActive(true);
        }

        if (SeleccionBandera.bandera_seleccionada == "naranja")
        {
            bandera_naranja.SetActive(true);
        }
    }
}