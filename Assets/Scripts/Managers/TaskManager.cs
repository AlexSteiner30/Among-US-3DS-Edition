using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour {
    [SerializeField] public string[] shortTasks;
    [SerializeField] public string[] commonTasks;
    [SerializeField] public string[] longTasks;

    [SerializeField] private int taskCompletedRequirement;
    [SerializeField] private int taskCompletedCount;

    [SerializeField] private Text taskList;
    [SerializeField] private GameObject buttons;

    public void TaskCompleted(GameObject task)
    {
        StartCoroutine(PlaySoundTaskComplete(task));
    }

    IEnumerator PlaySoundTaskComplete(GameObject task)
    {
        GameObject.Find("TaskCompleteSound").GetComponent<AudioSource>().Play();

        GameObject.Find("GameManager").GetComponent<GameManager>().DisableUseButton();
        task.GetComponent<Collider2D>().isTrigger = false;

        yield return new WaitForSeconds(1);

        taskList.gameObject.SetActive(true);
        taskList.text.Replace(task.GetComponent<GameTask>().taskName, "");

        buttons.SetActive(true);

        Destroy(task.GetComponent<GameTask>().panel);
        task.GetComponent<GameTask>().enabled = false;

        taskCompletedCount++;

        if (taskCompletedCount == taskCompletedRequirement)
        {
            SceneManager.LoadScene(3); // win
        }

        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
    }
}
