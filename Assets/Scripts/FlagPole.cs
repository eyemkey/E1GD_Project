using UnityEngine;
using System.Collections; 

public class FlagPole : MonoBehaviour
{

    [SerializeField] private float startPoint; 
    [SerializeField] private float endPoint; 
    [SerializeField] private NewSceneLoader sceneLoader; 
    [SerializeField] private GameObject flag; 
    private float interp = 0f; //where in [start, end]: ranges from 0 to 1
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(LowerFlag()); 
        }
    }
    IEnumerator LowerFlag()
    {
        while (interp < 1.0f)
        {
            interp += Time.deltaTime; 
            if(interp > 1.0f) 
            {
                interp = 1.0f; 
            }

            flag.transform.position = 
                new Vector3(flag.transform.position.x, 
                            Mathf.Lerp(startPoint, endPoint, interp),
                            flag.transform.position.z);
            yield return null;
        }
        sceneLoader.LoadScene(); 
    }
}
