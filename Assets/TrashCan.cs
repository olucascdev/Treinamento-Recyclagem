using System.Collections;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public string correctTrashTag; // Tag esperada para o lixo correto
    public TMPro.TextMeshPro textMesh; // Mensagem de feedback visual
    public UnityEngine.Events.UnityEvent OnCorrectTrash; // Evento disparado quando o lixo está correto

    private void Start()
    {
        // Conecta o evento do TriggerZone
        var triggerZone = GetComponent<TriggerZone>();
        if (triggerZone != null)
        {
            triggerZone.OnEnterEvent.AddListener(InsideTrash);
        }
        else
        {
            Debug.LogError("TriggerZone não encontrado no GameObject!");
        }

        // Configuração inicial do TextMeshPro
        if (textMesh != null)
        {
            textMesh.text = ""; // Limpa qualquer texto inicial
        }
        else
        {
            Debug.LogError("TextMeshPro não foi atribuído no Inspector.");
        }
    }

    public void InsideTrash(GameObject go)
    {
        if (go == null)
        {
            Debug.LogError("GameObject passado para InsideTrash é nulo!");
            return;
        }

        Debug.Log($"Objeto detectado: {go.name}, Tag: {go.tag}");

        if (go.CompareTag(correctTrashTag))
        {
            Debug.Log("Lixo correto!");
            if (textMesh != null)
            {
                textMesh.text = "Lixo correto!";
            }
            go.SetActive(false); // Desativa o objeto

            // Dispara o evento para próxima etapa
            OnCorrectTrash?.Invoke();

            StartCoroutine(ClearMessageAfterDelay(2f)); // Limpa mensagem de feedback
        }
        else
        {
            Debug.LogWarning("Lixo errado!");
            if (textMesh != null)
            {
                textMesh.text = "Lixo errado! Tente novamente.";
            }
            StartCoroutine(ClearMessageAfterDelay(2f)); // Limpa mensagem após delay
        }
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (textMesh != null)
        {
            textMesh.text = ""; // Limpa a mensagem de feedback
        }
    }
}
