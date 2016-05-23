using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimePassed : MonoBehaviour {
    private Text timerLabel;
    private GameObject target;
    public float time = 0;
    // Use this for initialization
    void Start () {
        timerLabel = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;

        float minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        float seconds = time % 60; //Use the euclidean division for the seconds.

        //update the label value
        timerLabel.text = string.Format("  {0:00}:{1:00}", minutes, seconds);

    }
}
