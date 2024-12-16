using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Para usar TextMeshPro

public class TrashCan : MonoBehaviour
{
    public string correctTrashTag; // Tag que identifica o tipo correto de lixo
    public TextMeshPro textMesh;  // Referência ao TextMeshPro no mundo

    private void Start()
    {
        // Conecta o evento de entrada no TriggerZone ao método InsideTrash
        GetComponent<TriggerZone>().OnEnterEvent.AddListener(InsideTrash);

        // Certifica-se de que o texto inicial está vazio
        if (textMesh != null)
        {
            textMesh.text = "";
        }
        else
        {
            Debug.LogError("TextMeshPro não foi atribuído no Inspector.");
        }
    }

    public void InsideTrash(GameObject go)
    {
        Debug.Log($"Objeto detectado: {go.name}, Tag: {go.tag}"); // Para verificar o objeto e sua tag

        if (go.CompareTag(correctTrashTag)) // Verifica se o lixo tem a tag correta
        {
            Debug.Log("Lixo correto!");
            textMesh.text = "Lixo correto!"; // Exibe mensagem no mundo
            go.SetActive(false); // Desativa o lixo
            StartCoroutine(ClearMessageAfterDelay(2f)); // Limpa o texto após 2 segundos
        }
        else
        {
            Debug.LogWarning("Lixo errado!");
            textMesh.text = "Lixo errado! Reiniciando..."; // Mensagem de erro
            StartCoroutine(RestartGameAfterDelay(2f)); // Reinicia após 2 segundos
        }
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textMesh.text = ""; // Limpa o texto
    }

    private IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
