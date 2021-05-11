using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
{   
    private int index;
    public GameManager gameManager;
    private List<ReplayRecord> replayRecords = new List<ReplayRecord>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate(){
        if (!gameManager.gameOver && !gameManager.recordingFinished)
        {
             replayRecords.Add(new ReplayRecord { position = transform.position});
        }
        else if(gameManager.runReplay && !gameManager.gameOver)
        {
            index++;

            if (index < replayRecords.Count)
            {
                SetTransform(index);   
            }else{
                gameManager.GameOver();
                gameManager.runReplay = false;
                index = 0;
            }
        }
    }

    private void SetTransform(int i)
    {
        index = i;
        ReplayRecord replayRecord = replayRecords[index];

        transform.position = replayRecord.position;
    }
}
