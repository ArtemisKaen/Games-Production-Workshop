using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    public void LoadLevelButton()
    {
        SceneManager.LoadScene("Prototype level");
    }

}