using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PantallaRA : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void ir_a_DinoDex()
    {
        StartCoroutine(CargarEscenaConSonido());
    }

    IEnumerator CargarEscenaConSonido()
    {
        audioSource.PlayOneShot(clickSound);

        // Espera a que termine el sonido
        yield return new WaitForSeconds(clickSound.length);

        SceneManager.LoadScene("DinoDex");
    }
}