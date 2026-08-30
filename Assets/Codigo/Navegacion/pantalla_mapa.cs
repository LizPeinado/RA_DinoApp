using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaMapa : MonoBehaviour
{
   public void regresar_a_pistas()
   {
      SceneManager.LoadScene("Pistas");
   }
}