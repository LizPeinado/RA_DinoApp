using UnityEngine;

public class ControlInfo : MonoBehaviour
{
    public GameObject cuadro_info;

    public void mostrar_info()
    {
        if (cuadro_info.activeSelf)
        {
            cuadro_info.SetActive(false);
        }
        else
        {
            cuadro_info.SetActive(true);
        }
    }
}