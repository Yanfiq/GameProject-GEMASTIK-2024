using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
