using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public enum GameState {
        menu,
        loading,
        starting,
        playing,
        ending,
        shopping
    }

    public GameState gameState;
    private bool isWinner;
    public List<GameObject> alivePlayers;
    public List<GameObject> shoppingPlayers;

    [SerializeField] private Camera spectatorCamera;
    [SerializeField] private GameObject firstPersonCanvas;

    private float startingTimer;
    private float endingTimer;
    private float shoppingTimer;
    [SerializeField] private TMP_Text startingCountdown;
    [SerializeField] private TMP_Text endingCountdown;
    [SerializeField] private TMP_Text winnerAnnouncement;
    [SerializeField] private TMP_Text alivePlayersText;
    [SerializeField] private ShopManager shopManager;


    List<GameObject> inActivePlayers = new List<GameObject>();
    List<GameObject> players = new List<GameObject>();

    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        
        startingCountdown.gameObject.SetActive(false);
        endingCountdown.gameObject.SetActive(false);
        winnerAnnouncement.gameObject.SetActive(false);
        alivePlayersText.gameObject.SetActive(false);
        firstPersonCanvas.SetActive(false);
        gameState = GameState.menu;
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();

        switch(gameState) {
            case GameState.menu:
                    
                if (players.Count > 0)
                {
                    gameState = GameState.loading;
                }
                break;
            case GameState.loading:

                if (players.Count >= GameObject.Find("LobbyManager").GetComponent<LobbyManager>().playersInLobby)
                {

                     
                    gameState = GameState.starting;
                    startingCountdown.gameObject.SetActive(true);
                    firstPersonCanvas.SetActive(true);
                    startingTimer = 6f;

                }

                break;
            case GameState.starting:
                spectatorCamera.depth = -50f;
                startingCountdown.text = Mathf.Ceil(startingTimer-1).ToString();
                startingCountdown.fontSize = 200f;
                startingTimer -= Time.deltaTime;
                if(startingTimer <= 1f) {
                    startingCountdown.text = "Fight!";
                    startingCountdown.fontSize = 300f;
                }
                if (startingTimer <= 0f) {
                    gameState = GameState.playing;
                    startingCountdown.gameObject.SetActive(false);
                    alivePlayersText.gameObject.SetActive(true);
                    alivePlayers = players;
                }

                break;
            case GameState.playing:

                alivePlayersText.text = "Alive players: " + alivePlayers.Count;

                // Create a list to hold the players that should be removed
                List<GameObject> playersToRemove = new List<GameObject>();

                // Iterate over alivePlayers to check player health
                foreach (GameObject player in alivePlayers) {
                    Debug.Log(player.GetComponent<PlayerHealth>().alive.Value);
                    if (!player.GetComponent<PlayerHealth>().alive.Value) {
                        // Add the player to the list of players to remove
                        playersToRemove.Add(player);
                    }
                }

                // Remove the players that need to be removed
                foreach (GameObject playerToRemove in playersToRemove) {
                    alivePlayers.Remove(playerToRemove);
                }









                /*
                alivePlayersText.text = "Alive players: " + alivePlayers.Count;

                // Create a list to hold the players that should be removed
                List<GameObject> playersToRemove = new List<GameObject>();

                // Iterate over alivePlayers to check player health
                foreach (GameObject player in alivePlayers)
                {

                    if (!player.GetComponent<PlayerHealth>().alive.Value)
                    {
                        // Add the player to the list of players to remove
                        playersToRemove.Add(player);
                    }
                }*/

<<<<<<< Updated upstream
                if (alivePlayers.Count == 1) {
//                    alivePlayers[0].GetComponent<PlayerHealth>().alive.Value = false;
=======
                // Remove the players that need to be removed
                foreach (GameObject playerToRemove in playersToRemove)
                {
                    alivePlayers.Remove(playerToRemove);
                }

                if (alivePlayers.Count == 1) {
>>>>>>> Stashed changes
                    winnerAnnouncement.gameObject.SetActive(true);
                    endingCountdown.gameObject.SetActive(true);
                    alivePlayersText.gameObject.SetActive(false);
                    gameState = GameState.ending;
                    endingTimer = 3f;
                }

                break;
            case GameState.ending:

                winnerAnnouncement.text = "Player won!";
                endingCountdown.text = Mathf.Ceil(endingTimer).ToString();
                endingTimer -= Time.deltaTime;
                if(endingTimer <= 0f) {
                    gameState = GameState.shopping;
                    spectatorCamera.depth = 50f;
                    shoppingTimer = 9f;
                    winnerAnnouncement.gameObject.SetActive(false);
                    foreach(GameObject player in players) {
                        if(player == alivePlayers[0]) {
                            player.GetComponent<PlayerManager>().chips += 3;
                        } else {
                            player.GetComponent<PlayerManager>().chips += 1;
                        }

                        player.GetComponent<PlayerManager>().OpenShop();
                    }
                }

                break;
            case GameState.shopping:
<<<<<<< Updated upstream
                endingCountdown.text = Mathf.Ceil(shoppingTimer).ToString();
                shoppingTimer -= Time.deltaTime;
                if(shoppingTimer <= 0f) {
=======
                List<GameObject> playersToRemoveShopping = new List<GameObject>();

                // Iterate over the shoppingPlayers list
                foreach (GameObject player in shoppingPlayers)
                {
                    if (player.GetComponent<PlayerManager>().isReady.Value)
                    {
                        // Add the player to the list of players to remove
                        playersToRemoveShopping.Add(player);
                    }
                }

                // Remove the players from the shoppingPlayers list outside of the foreach loop
                foreach (GameObject playerToRemove in playersToRemoveShopping)
                {
                    shoppingPlayers.Remove(playerToRemove);
                }
                if (shoppingPlayers.Count == 0) {
>>>>>>> Stashed changes
                    //spawna på spelare på random positioner igen



                    endingCountdown.gameObject.SetActive(false);
                    startingCountdown.gameObject.SetActive(true);
                    startingTimer = 6f;
                    gameState = GameState.starting;
                    shopManager.Ready();
                    foreach (GameObject player in players) {
                        player.GetComponent<PlayerHealth>().currentHealth.Value = 100f;
                        player.GetComponent<PlayerHealth>().alive.Value = true;
                    }
                }
                break;
        }
    }
}
