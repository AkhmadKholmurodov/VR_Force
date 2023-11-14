using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("bullet"))
        {
            /*void OnGUI()
            {
                //Display UI info on the GUI button
                if (GUI.Button(new Rect(0, 0, 400, 50), "YOU DIED!! Click Button to Try Again"))
                {
                    //Reload the scene
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                }
            }*/
            //Reload the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
