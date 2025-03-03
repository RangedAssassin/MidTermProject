using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameStart = new UnityEvent();
    public UnityEvent OnFinalPuzzleCompleted = new UnityEvent();

    [SerializeField] private Puzzle finalPuzzle;

    private void Start()
    {
        finalPuzzle?.OnPuzzleCompleted?.AddListener(GameCompleted);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void GameCompleted()
    {
        OnFinalPuzzleCompleted.Invoke();
        //save progress
        //play a cutscene
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the name of your main menu scene
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting"); // This log is useful for testing in the editor
    }
}
