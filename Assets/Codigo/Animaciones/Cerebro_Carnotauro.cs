using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections;

public class ControlCarnotauro : MonoBehaviour
{
    private Animator animador;

    public GameObject rawr;
    private Animator animador_rawr;

    public AudioSource audioSource;
    public AudioClip[] rawrClips;

    void Start()
    {
        animador = GetComponent<Animator>();

        animador_rawr =
            rawr.GetComponent<Animator>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null && rawr != null)
            {
                audioSource = rawr.GetComponent<AudioSource>();
            }
        }
    }

    void Update()
    {
        // TOQUE EN CELULAR
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                // Comprobar si tocamos un elemento de UI
                if (!EventSystem.current.IsPointerOverGameObject(
                    Touchscreen.current.primaryTouch.touchId.ReadValue()))
                {
                    rugir();
                }
            }
        }

        // CLIC EN PC
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                // Comprobar si el clic está sobre un elemento de UI
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    rugir();
                }
            }
        }
    }

    void rugir()
    {
        animador.SetTrigger("rugir");

        rawr.SetActive(true);

        animador_rawr.Play("RAWR");

        // Reproducir sonido aleatorio
        if (audioSource != null &&
            rawrClips != null &&
            rawrClips.Length > 0)
        {
            int idx = Random.Range(0, rawrClips.Length);

            AudioClip clip = rawrClips[idx];

            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        StartCoroutine(ocultar_rawr());
    }

    IEnumerator ocultar_rawr()
    {
        yield return new WaitForSeconds(1.83f);

        rawr.SetActive(false);
    }
}