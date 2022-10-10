using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject RightMarker;
    [SerializeField] GameObject LeftMarker;
    [SerializeField] GameObject RightMasked;
    [SerializeField] GameObject LeftMasked;

    private int sceneToLoad = 0;

    public void PVP1()
    {
        RightMarker.gameObject.transform.position = new Vector3(4, 1, -4);
        RightMasked.gameObject.transform.position = new Vector3(10, 1, -4);
        LeftMarker.gameObject.transform.position = new Vector3(-10, 1, -4);
        LeftMasked.gameObject.transform.position = new Vector3(-4, 1, -4);
        sceneToLoad = 1;
    }

    public void PVP2()
    {
        RightMarker.gameObject.transform.position = new Vector3(10, 1, -4);
        RightMasked.gameObject.transform.position = new Vector3(4, 1, -4);
        LeftMarker.gameObject.transform.position = new Vector3(-4, 1, -4);
        LeftMasked.gameObject.transform.position = new Vector3(-10, 1, -4);
        sceneToLoad = 2;
    }

    public void PVP3()
    {
        RightMarker.gameObject.transform.position = new Vector3(10, 1, -4);
        RightMasked.gameObject.transform.position = new Vector3(4, 1, -4);
        LeftMarker.gameObject.transform.position = new Vector3(-10, 1, -4);
        LeftMasked.gameObject.transform.position = new Vector3(-4, 1, -4);
        sceneToLoad = 3;
    }

    public void PVE1()
    {
        RightMarker.gameObject.transform.position = new Vector3(4, 1, -4);
        RightMasked.gameObject.transform.position = new Vector3(10, 1, -4);
        LeftMarker.gameObject.transform.position = new Vector3(-10, 1, -4);
        LeftMasked.gameObject.transform.position = new Vector3(-4, 1, -4);
        sceneToLoad = 4;
    }

    public void PVE2()
    {
        RightMarker.gameObject.transform.position = new Vector3(10, 1, -4);
        RightMasked.gameObject.transform.position = new Vector3(4, 1, -4);
        LeftMarker.gameObject.transform.position = new Vector3(-4, 1, -4);
        LeftMasked.gameObject.transform.position = new Vector3(-10, 1, -4);
        sceneToLoad = 5;
    }

    public void PVE3()
    {
        RightMarker.gameObject.transform.position = new Vector3(4, 1, -4);
        RightMasked.gameObject.transform.position = new Vector3(10, 1, -4);
        LeftMarker.gameObject.transform.position = new Vector3(-4, 1, -4);
        LeftMasked.gameObject.transform.position = new Vector3(-10, 1, -4);
        sceneToLoad = 6;
    }

    public void LoadGame()
    {
        switch (sceneToLoad)
        {
            case 1:
                SceneManager.LoadScene("Level 1");
                break;
            case 2:
                SceneManager.LoadScene("Level 2");
                break;
            case 3:
                SceneManager.LoadScene("Level 3");
                break;
            case 4:
                SceneManager.LoadScene("Level 4");
                break;
            case 5:
                SceneManager.LoadScene("Level 5");
                break;
            case 6:
                SceneManager.LoadScene("Level 6");
                break;
            default:
                return;
        }
    }
}
