using System.Collections;
using UnityEngine;

public class ControlInicio : MonoBehaviour
{
    public GameObject anim_carga;
    public GameObject inicio;

    void Start()
    {
        StartCoroutine(mostrar_inicio());
    }

    IEnumerator mostrar_inicio()
    {
        yield return new WaitForSeconds(2f);

        anim_carga.SetActive(false);
        inicio.SetActive(true);
    }
}