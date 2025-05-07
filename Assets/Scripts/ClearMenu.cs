using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearMenu : MonoBehaviour
{
    public void OnClickTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
