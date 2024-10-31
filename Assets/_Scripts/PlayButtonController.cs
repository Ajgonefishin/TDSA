using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnMouseDown() {
        SceneManager.LoadScene("Level 1 Final");
        Scene level = SceneManager.GetSceneByName("Level 1 Final");
        SceneManager.SetActiveScene(level);
    }
}
