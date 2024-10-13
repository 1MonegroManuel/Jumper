using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChooseController : MonoBehaviour
{
    public ChooseLabelController labelPrefab; // Ahora se utiliza un prefab para instanciar cada opción
    
    public GameController gameController;
    private RectTransform rectTransform;
    private Animator animator;
    private Dictionary<int, ChooseLabelController> labels = new Dictionary<int, ChooseLabelController>();
    private float labelHeight = -1;

    void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetupChoose(ChooseScene scene)
    {
        DestroyLabels();  // Asegúrate de limpiar los labels del panel anterior

        // Lógica de instanciación de nuevos labels y mostrar el panel
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        animator.SetTrigger("Show");

        if (labelPrefab == null)
        {
            Debug.LogError("El prefab 'label' no está asignado en el Inspector.");
            return;
        }

        for (int index = 0; index < scene.labels.Count; index++)
        {
            ChooseLabelController newLabel = Instantiate(labelPrefab.gameObject, transform).GetComponent<ChooseLabelController>();

            if (newLabel == null)
            {
                Debug.LogError("La instanciación del 'ChooseLabelController' falló.");
                continue;
            }

            if (labelHeight == -1)
            {
                labelHeight = newLabel.GetHeight();
            }

            float labelPosY = CalculateLabelPosition(index, scene.labels.Count);
            Vector3 newPosition = newLabel.transform.localPosition;
            newPosition.y = labelPosY;

            RectTransform newLabelRectTransform = newLabel.GetComponent<RectTransform>();
            newLabelRectTransform.localPosition = newPosition;

            newLabel.Setup(scene.labels[index], this, labelPosY);
            newLabel.gameObject.SetActive(true);
        }

        Vector2 size = rectTransform.sizeDelta;
        size.y = (scene.labels.Count + 2) * labelHeight;
        rectTransform.sizeDelta = size;

        StartCoroutine(FadeInCanvasGroup(canvasGroup, 0.5f));
    }


    // Método para hacer la transición del alpha en el CanvasGroup
    private IEnumerator FadeInCanvasGroup(CanvasGroup canvasGroup, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }



    public void PerformChoose(GameScene scene)
    {
        if (scene != null)
        {
            Debug.Log("Cambiando a la siguiente escena: " + scene.name);

            // Asegúrate de ocultar el panel de la escena actual antes de cambiar
            HidePanel();

            // Llama al controlador para cargar la siguiente escena
            gameController.PlayScene(scene);

            // Destruye los labels para evitar superposición
            DestroyLabels();
        }
        else
        {
            Debug.LogError("La escena seleccionada es nula. Revisa las asignaciones en la ChooseScene.");
        }
    }

    private float CalculateLabelPosition(int labelIndex, int labelCount)
    {
        if (labelCount % 2 == 0)
        {
            if (labelIndex < labelCount / 2)
            {
                return labelHeight * (labelCount / 2 - labelIndex - 1) + labelHeight / 2;
            }
            else
            {
                return -1 * (labelHeight * (labelIndex - labelCount / 2) + labelHeight / 2);
            }
        }
        else
        {
            if (labelIndex < labelCount / 2)
            {
                return labelHeight * (labelCount / 2 - labelIndex);
            }
            else if (labelIndex > labelCount / 2)
            {
                return -1 * (labelHeight * (labelIndex - labelCount / 2));
            }
            else
            {
                return 0;
            }
        }
    }
    private void DestroyLabels()
    {
        foreach (KeyValuePair<int, ChooseLabelController> entry in labels)
        {
            Destroy(entry.Value.gameObject);
        }
        labels.Clear(); // Limpiar el diccionario
    }

    public void ShowPanel()
    {
        Debug.Log("Activando Trigger Show para mostrar el panel...");
        animator.SetTrigger("Show");
    }

    public void HidePanel()
    {
        Debug.Log("Activando Trigger Hide para ocultar el panel...");
        animator.SetTrigger("Hide");
    }
}
