using UnityEngine;
using UnityEngine.SceneManagement;

public class Navegacion : MonoBehaviour
{
   public void ir_a_pistas()
   {
      SceneManager.LoadScene("Pistas");
   }
}