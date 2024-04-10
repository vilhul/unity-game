using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public string currentWinner;


    List<GameObject> inActivePlayers = new List<GameObject>();
    List<GameObject> players = new List<GameObject>();

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
        players = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();



        switch(gameState) {
            case GameState.menu:

                break;
            case GameState.starting:
                break;
            case GameState.playing:
                Debug.Log(players.Count);
                Debug.Log(inActivePlayers.Count);
                foreach (GameObject player in players) {
                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    if(playerHealth.currentHealth.Value <= 0) {
                        inActivePlayers.Add(player);
                        player.SetActive(false);
                    }
                }

                if(players.Count <= 1) {
                    foreach (GameObject player in players) {
                        //currentWinner = player;
                        player.SetActive(false);
                    }
                    gameState = GameState.ending; break;
                }

                break;
            case GameState.ending:
                Debug.Log(players[0]. + " won!");


                break;
            case GameState.shopping:
                break;
        }
    }
}
