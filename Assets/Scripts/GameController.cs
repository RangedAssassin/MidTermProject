using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameStart = new UnityEvent();
    public UnityEvent OnFinalPuzzleCompleted = new UnityEvent();

    [SerializeField] private Puzzle finalPuzzle;

    private void Start()
    {
        finalPuzzle.OnPuzzleCompleted.AddListener(GameCompleted);
    }
    public void StartGame()
    {
        //Enable player movement
        //Start timer
    }

    public void GameCompleted()
    {
        OnFinalPuzzleCompleted.Invoke();
        //save progress
        //play a cutscene
    }
}
