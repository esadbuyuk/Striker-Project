using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSections : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wantedSectionsList;
    [SerializeField]
    private GameObject[] currentSectionsList;
    private int activatingTurn = 0;


    public void ActivateObjects()
    {
        foreach (GameObject currentObject in currentSectionsList)
        {
            currentObject.SetActive(false);
        }
        
        activatingTurn += 1;

        if (activatingTurn >= 1)
        {
            // wantedSection.SetActive(true);
            activatingTurn = 0;

            foreach (GameObject wantedObject in wantedSectionsList)
            {
                wantedObject.SetActive(true);
            }
        }

    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }


}
