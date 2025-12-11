using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTester : MonoBehaviour
{
    void Update(){
        if (Input.GetKeyDown(KeyCode.G)){
            DeathHandler.Kill("test"); // ← loads GameOver, shows "test"
        }
    }
}
