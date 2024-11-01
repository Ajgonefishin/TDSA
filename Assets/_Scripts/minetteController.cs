using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class minetteController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCollisionEnter2D (Collision2D collision) {
        SceneManager.LoadScene("WinScreen");
        Scene winScene = SceneManager.GetSceneByName("WinScreen");
        SceneManager.SetActiveScene(winScene);
    }
}
