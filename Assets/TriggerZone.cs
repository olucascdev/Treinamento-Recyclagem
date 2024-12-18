using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public string targetTag; // Tag do objeto que deve ser detectado
    public UnityEvent<GameObject> OnEnterEvent; // Evento disparado ao entrar no Trigger

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger detectado: {other.gameObject.name}, Tag: {other.gameObject.tag}");
        if (other.gameObject.tag == targetTag)
        {
            OnEnterEvent.Invoke(other.gameObject);
        }
    }

}
