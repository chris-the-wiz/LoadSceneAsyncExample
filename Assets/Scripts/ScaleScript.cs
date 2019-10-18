using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaleScript : MonoBehaviour
{
    [SerializeField] GameObject loader;
    [SerializeField] GameObject activator;

    [Range(1,33)] public int asyncUploadTimeSlice;
    [Range(8, 128)] public int asyncUploadBufferSize;
    public bool asyncUploadPersistentBuffer = true;
    [SerializeField] string SceneName;

    AsyncOperation asyncOperation;

    void Start()
    {

        QualitySettings.asyncUploadTimeSlice = asyncUploadTimeSlice;
        QualitySettings.asyncUploadBufferSize = asyncUploadBufferSize;
        QualitySettings.asyncUploadPersistentBuffer = asyncUploadPersistentBuffer;


        StartCoroutine(ScaleUp(1.0f));
    }


    IEnumerator ScaleUp(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);


            this.transform.position += new Vector3(0.1f, 0, 0);

            ScaleUp(seconds);
        }

    }

  
    void Update()
    {
        if (asyncOperation!=null)
        {
            Debug.Log("Loading Progress: " + asyncOperation.progress.ToString());
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject== loader)
        {
            Debug.Log("scene loading");
            asyncOperation = SceneManager.LoadSceneAsync(SceneName);
            asyncOperation.allowSceneActivation = false;
            asyncOperation.completed += LoadCompleted;


        }
        else if (other.gameObject == activator)
        {
           asyncOperation.allowSceneActivation = true;
        }
              
             


    }




    void LoadCompleted(AsyncOperation obj)
    {

        Debug.Log("Scenes switched successfuly");

    }
}
