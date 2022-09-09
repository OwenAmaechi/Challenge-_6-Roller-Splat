using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
  public static GameManager singleton;
  public GameObject ball;
  public ParticleSystem levelEndParticle;
  private GroundPiece[] allGroundPieces;

  private void Start()
  {
    ball = GameObject.Find("Player");
    SetupNewLevel();
  }

  private void SetupNewLevel()
  {
    allGroundPieces = FindObjectsOfType<GroundPiece>();
  }

  private void Awake()
  {
    if (singleton == null)
      singleton = this;
    else if (singleton != this)
      Destroy(gameObject);

    DontDestroyOnLoad(gameObject);
  }

  private void OnEnable()
  {
    SceneManager.sceneLoaded += OnLevelFinishedLoading;
  }

  private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
  {
    SetupNewLevel();
  }

  public void CheckComplete()
  {
    bool isFinished = true;

    for (int i = 0; i < allGroundPieces.Length; i++)
    {
      if (allGroundPieces[i].isColored == false)
      {
        isFinished = false;
        break;
      }
    }

    if (isFinished)
    {
      StartCoroutine(LevelFinishedFireworks());
      LevelFinished();
      NextLevel();

    }
  }
  IEnumerator LevelFinishedFireworks()
  {
    yield return new WaitForSeconds(3.6f);

  }
  private void LevelFinished()
  {
    levelEndParticle.Play();

  }

  private void NextLevel()
  {
    // checks if we have reached the last level then it starts from level 1 agian
    if (SceneManager.GetActiveScene().buildIndex == 4)
    {
      SceneManager.LoadScene(0);
    }
    else
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }
}
