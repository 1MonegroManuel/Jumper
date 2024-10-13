using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameScene currentScene;
    public BottomBarController bottomBar;
    public ChooseController chooseController;
    public SpriteSwitcher spriteSwitcher;

    // Sonido de final
    public AudioClip finalSound;
    private AudioSource audioSource;

    private State state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }

    void Start()
    {
        if (currentScene is StoryScene)
        {
            StoryScene storyScene = currentScene as StoryScene;
            bottomBar.PlayScene(storyScene);
        }

        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (state == State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    // Verificar si es la última escena (es decir, no hay una escena siguiente)
                    if ((currentScene as StoryScene).nextScene == null)
                    {
                        GameManager.ReduceHealth(25);
                        GameManager.Coins += 15;
                        GameManager.UpdateCoinText();
                        GameManager.UpdateHealthText();

                        // Reproducir el sonido final
                        PlayFinalSound();
                    }
                    else
                    {
                        // Cargar la siguiente escena
                        PlayScene((currentScene as StoryScene).nextScene);
                    }
                }
                else
                {
                    // Cambiar el sprite de Tiffany según el diálogo actual
                    var currentSentence = (currentScene as StoryScene).sentences[bottomBar.GetCurrentSentenceIndex()];
                    CambiarSpriteTiffany(currentSentence);
                    bottomBar.PlayNextSentence();
                }
            }
        }
    }

    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        if (scene is StoryScene)
        {
            StoryScene storyScene = scene as StoryScene;
            yield return new WaitForSeconds(1f);
            bottomBar.ClearText();
            bottomBar.Show();
            yield return new WaitForSeconds(1f);
            bottomBar.PlayScene(storyScene);

            var currentSentence = storyScene.sentences[bottomBar.GetCurrentSentenceIndex()];
            CambiarSpriteTiffany(currentSentence);
            state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            state = State.CHOOSE;
            chooseController.SetupChoose(scene as ChooseScene);
        }
    }

    private void CambiarSpriteTiffany(StoryScene.Sentence sentence)
    {
        if (sentence.speaker.speakerName == "Tiffany")
        {
            switch (sentence.emotion.ToLower())
            {
                case "hablando":
                    spriteSwitcher.CambiarSprite("hablando");
                    break;
                case "sonriendo":
                    spriteSwitcher.CambiarSprite("sonriendo");
                    break;
                case "calmada":
                    spriteSwitcher.CambiarSprite("calmada");
                    break;
                case "molesta":
                    spriteSwitcher.CambiarSprite("molesta");
                    break;
                case "enojada":
                    spriteSwitcher.CambiarSprite("enojada");
                    break;
                default:
                    spriteSwitcher.CambiarSprite("calmada");
                    break;
            }
        }
    }

    // Método para reproducir el sonido final
    private void PlayFinalSound()
    {
        if (audioSource != null && finalSound != null)
        {
            audioSource.PlayOneShot(finalSound);
            StartCoroutine(WaitForSoundToEnd());  // Esperar a que el sonido termine antes de cargar la escena
        }
        else
        {
            // Si no hay sonido, cargar la escena inmediatamente
            SceneManager.LoadScene("MiniMarket");
        }
    }

    private IEnumerator WaitForSoundToEnd()
    {
        // Esperar la duración del sonido antes de cambiar de escena
        yield return new WaitForSeconds(finalSound.length);
        SceneManager.LoadScene("MiniMarket");
    }
}
