using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneButtons : MonoBehaviour
{
    // Start is called before the first frame update

    private readonly int GAME_SCENE = 1;
    
    public void OnClick(int a)
    {
        switch (a)
        {
            case 0:
                SceneManager.LoadScene(GAME_SCENE);
                break;
            
            case 1:
                Application.Quit(); 
                break;
        }
    }
}
