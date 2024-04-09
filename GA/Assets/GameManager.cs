using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    



    public enum GameState {
        menu,
        starting,
        playing,
        ending,
        shopping
    }

    public GameState gameState;



    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        gameState = GameState.menu;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(gameState == GameState.shopping)
    }
}
