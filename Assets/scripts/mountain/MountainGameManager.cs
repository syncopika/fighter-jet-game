using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountainGameManager : MonoBehaviour
{
    public Text message;

    int numSpheresToCollect = 3;

    public void sphereCollected()
    {
        numSpheresToCollect--;

        if(numSpheresToCollect == 0)
        {
            message.text = "Congrats, you collected all 3 spheres! :D";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
