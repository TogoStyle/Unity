using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceStones : MonoBehaviour
{
    public Text textThrown;
    public Text textDestroyed;

    public Text textTurtlesDestroyed;
    public Text textSlimesDestroyed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textThrown.text = "NUMBER OF STONES: " + GameManager.currentNumberStonesThrown;
        textDestroyed.text = "DESTROYED: " + GameManager.currentNumberDestroyedStones;
        textSlimesDestroyed.text = "SLIMES DESTROYED: " + GameManager.currentNumberDestroyedSlimes;
        textTurtlesDestroyed.text = "TURTLES DESTROYED: " + GameManager.currentNumberDestroyedTurtles;
    }
}
