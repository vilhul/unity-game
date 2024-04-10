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
    private bool isWinner;
    private float playersAlive;
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
                    
                if (players.Count > 0)
                {
                    gameState = GameState.starting;
                }
                break;
            case GameState.starting:

                if (players.Count >= GameObject.Find("LobbyManager").GetComponent<LobbyManager>().playersInLobby)
                {

                     
                    gameState = GameState.playing;

                }

                break;
            case GameState.playing:
                Debug.Log("Playing");
                playersAlive = 0;
                foreach (GameObject player in players)
                {
                    PlayerHealth playerScript = player.GetComponent<PlayerHealth>();
                    if (playerScript != null)
                    {
                       if (playerScript.alive.Value) {
                            playersAlive++;
                        }
                    }

                    if (playersAlive == 1)
                    {
                        gameState = GameState.ending;

                    }
                }

                break;
            case GameState.ending:

                Debug.Log("GameState.Ending");

                break;
            case GameState.shopping:


                break;
        }
    }
}
