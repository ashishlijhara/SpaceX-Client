using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchHelper : MonoBehaviour
{
    public void SwitchToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
