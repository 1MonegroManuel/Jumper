using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameScene currentScene;
    public BottomBarController bottomBar;
    
    public ChooseController chooseController;
    public SpriteSwitcher spriteSwitcher;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (state == State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    PlayScene((currentScene as StoryScene).nextScene);
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

            // Cambiar el sprite según el diálogo actual de Tiffany
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
            // Cambiar el sprite basado en la emoción especificada en el campo "emotion"
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
                    spriteSwitcher.CambiarSprite("calmada"); // Emoción por defecto
                    break;
            }
        }
    }


}

