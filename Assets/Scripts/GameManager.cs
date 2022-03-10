using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                var gameObject = new GameObject("Game Manager", typeof(GameManager));
                instance = gameObject.GetComponent<GameManager>();
            }
            return instance;
        }
    }

    private Queue<Player> currentPlayers;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        currentPlayers = new Queue<Player>();
    }

    public static void ResetPlayers()
    {
        Instance.currentPlayers = new Queue<Player>();
    }

    public static int GetPlayerCount() => Instance.currentPlayers.Count;

    public static void AddPlayer(Player newPlayer)
    {
        Instance.currentPlayers.Enqueue(newPlayer);
    }

    public static Player PeekNextPlayer() => Instance.currentPlayers.Peek();

    public static Player GetNextPlayer()
    {
        var nextPlayer = Instance.currentPlayers.Dequeue();
        Instance.currentPlayers.Enqueue(nextPlayer);

        return nextPlayer;
    }
}
