using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ControlCarnotauro : MonoBehaviour
{
    private Animator animador;

    public GameObject rawr;
    private Animator animador_rawr;

    void Start()
    {
        animador = GetComponent<Animator>();

        animador_rawr =
            rawr.GetComponent<Animator>();
    }

    void Update()
    {
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                rugir();
            }
        }

        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                rugir();
            }
        }
    }

    void rugir()
    {
        animador.SetTrigger("rugir");

        rawr.SetActive(true);

        animador_rawr.Play("RAWR");

        StartCoroutine(ocultar_rawr());
    }

    IEnumerator ocultar_rawr()
    {
        yield return new WaitForSeconds(1.83f);

        rawr.SetActive(false);
    }
}