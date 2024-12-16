using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;

public class PlaySteps : MonoBehaviour
{
    PlayableDirector director;
    public List<Step> steps;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    [System.Serializable]
    public class Step
    {
        public string name;
        public float time;
        public bool hasPlayed = false;
    }

    public void PlayStepIndex(int index)
    {
        // Verifique se o índice está dentro do intervalo válido
        if (index < 0 || index >= steps.Count)
        {
            Debug.LogError("Índice fora do intervalo da lista de steps!");
            return;
        }

        // Acessa o Step correto pela lista
        Step step = steps[index];

        if (!step.hasPlayed)
        {
            step.hasPlayed = true;

            director.Stop();
            director.time = step.time;
            director.Play();
        }
    }
}
