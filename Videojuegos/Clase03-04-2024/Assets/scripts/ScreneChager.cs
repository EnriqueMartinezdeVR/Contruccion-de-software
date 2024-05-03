//Enrique Martínez de Velasco Reyna 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void GoTo(string sceneName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}