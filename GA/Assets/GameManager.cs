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
    [SerializeField] private TMP_Text startingCountdown;
    [SerializeField] private TMP_Text endingCountdown;
    [SerializeField] private TMP_Text winnerAnnouncement;
    [SerializeField] private TMP_Text alivePlayersText;


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

                foreach(GameObject player in alivePlayers) {
                    Debug.Log(player.GetComponent<PlayerHealth>().alive.Value);
                    if(!player.GetComponent<PlayerHealth>().alive.Value) {
                        alivePlayers.Remove(player);
                    }
                }

                if (alivePlayers.Count <= 1) {
                    alivePlayers[0].GetComponent<PlayerHealth>().alive.Value = false;
                    winnerAnnouncement.gameObject.SetActive(true);
                    endingCountdown.gameObject.SetActive(true);
                    alivePlayersText.gameObject.SetActive(false);
                    gameState = GameState.ending;
                    endingTimer = 3f;
                }

                break;
            case GameState.ending:

                winnerAnnouncement.text = players[0].name + " won!";
                endingCountdown.text = Mathf.Ceil(endingTimer).ToString();
                endingTimer -= Time.deltaTime;
                if(endingTimer <= 0f) {
                    gameState = GameState.shopping;
                    spectatorCamera.depth = 50f;
                    winnerAnnouncement.gameObject.SetActive(false);
                    endingCountdown.gameObject.SetActive(false);
                    shoppingPlayers = players;
                    foreach(GameObject player in players) {
                        player.GetComponent<PlayerManager>().OpenShop();
                    }
                }

                break;
            case GameState.shopping:
                foreach(GameObject player in shoppingPlayers) {
                    if(player.GetComponent<PlayerManager>().isReady.Value) {
                        shoppingPlayers.Remove(player);
                    }
                }
                if(shoppingPlayers.Count == 0) {
                    //spawna på spelare på random positioner igen

                    startingCountdown.gameObject.SetActive(true);
                    startingTimer = 6f;
                    gameState = GameState.starting;
                    foreach(GameObject player in players) {
                        player.GetComponent<PlayerHealth>().alive.Value = true;
                    }
                }

                break;
        }
    }
}
