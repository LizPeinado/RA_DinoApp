using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PantallaMapa : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void regresar_a_pistas()
    {
        StartCoroutine(CargarEscenaConSonido());
    }

    IEnumerator CargarEscenaConSonido()
    {
        audioSource.PlayOneShot(clickSound);

        // Espera a que termine el sonido
        yield return new WaitForSeconds(clickSound.length);

        SceneManager.LoadScene("Pistas");
    }
}