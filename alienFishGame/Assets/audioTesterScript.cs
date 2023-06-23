using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTesterScript : MonoBehaviour
{
    // temporary solution to the audio not playing issue. Now it's a singleton that will persist between all scenes. 
    public static audioTesterScript instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
