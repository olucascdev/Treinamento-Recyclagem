using UnityEngine;
using UnityEngine.SceneManagement;

public class LixeiraLogic : MonoBehaviour
{
    public string tipoLixeira; // Tipo esperado para esta lixeira (ex.: "Papel", "Vidro", etc.)
    public GameObject mensagemAcerto; // Mensagem exibida ao acertar
    public GameObject mensagemErro; // Mensagem exibida ao errar

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o item tem a tag correspondente ao tipo da lixeira
        if (other.CompareTag(tipoLixeira))
        {
            Debug.Log($"Acertou! {other.name} colocado na lixeira correta: {tipoLixeira}");
            MostrarMensagem(mensagemAcerto);
            Destroy(other.gameObject); // Destroi o lixo ou desativa, caso prefira reutilizar
        }
        else
        {
            Debug.Log($"Errou! {other.name} não pertence à lixeira: {tipoLixeira}");
            MostrarMensagem(mensagemErro);
            ReiniciarTreinamento();
        }
    }

    private void MostrarMensagem(GameObject mensagem)
    {
        if (mensagem != null)
        {
            mensagem.SetActive(true);
            Invoke(nameof(EsconderMensagens), 2f); // Oculta a mensagem após 2 segundos
        }
    }

    private void EsconderMensagens()
    {
        if (mensagemAcerto != null) mensagemAcerto.SetActive(false);
        if (mensagemErro != null) mensagemErro.SetActive(false);
    }

    private void ReiniciarTreinamento()
    {
        Debug.Log("Reiniciando o treinamento...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia a cena atual
    }
}
