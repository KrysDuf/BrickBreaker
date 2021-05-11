using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
{   
    private int currentIndex;
    public GameManager gameManager;
    private List<ReplayRecord> actionReplayRecords = new List<ReplayRecord>();
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
             actionReplayRecords.Add(new ReplayRecord { position = transform.position});
        }
        else if(gameManager.runReplay && !gameManager.gameOver)
        {
            currentIndex++;

            if (currentIndex < actionReplayRecords.Count)
            {
                SetTransform(currentIndex);   
            }else{
                gameManager.GameOver();
                gameManager.runReplay = false;
                currentIndex = 0;
            }
        }
    }

    private void SetTransform(int index)
    {
        currentIndex = index;
        ReplayRecord actionReplayRecord = actionReplayRecords[index];

        transform.position = actionReplayRecord.position;
    }
}
