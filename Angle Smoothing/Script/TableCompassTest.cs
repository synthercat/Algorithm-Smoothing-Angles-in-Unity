using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCompassTest : MonoBehaviour
{
    public Material myNewmaterial;
    public Material myNewmaterial2;
    public Material myNewmaterial3;

    public int[] Quadrants;
    public GameObject velona;

    public float[] original;
    public float[] afterOffset;
    public float[] trimmed;

    float minValueForCompassList;
    List<float> values = new List<float>();

    void Start()
    {
        //Copy original values to list and let's get started already!
        values.Clear();
        for (int i = 0; i < original.Length; i++) values.Add(original[i]);

        //Show the original values in RED
        new GameObject("---START ORIGINAL---");
        foreach (float value in values)
        {
            Instantiate(velona, Vector3.zero, Quaternion.Euler(-90f, value, 0f));
        }

        //STEP 1 Find the median quadrant by deviding the circle into 8 sections
        int myOffset = Measure8Quadrants(values);
        Debug.Log("Measure8Quadrants= " + myOffset);

        //STEP 2 Offset the values according to where most values are found under step 1
        values = OffsetAngles(values, myOffset);

        //STEP 3 Sort the values in order to remove peak values to get rid of false peak readings
        values.Sort();
        //Now the inspector will show the values after offset under the tab "After Offset"
        afterOffset = values.ToArray();

        //Show the offset values in GREEN
        new GameObject("---END ORIGINAL---START OFFSET---");
        foreach (float value in values)
        {
            GameObject myNewGameObject = Instantiate(velona, Vector3.zero, Quaternion.Euler(-90f, value, 0f)) as GameObject;
            myNewGameObject.GetComponent<MeshRenderer>().material = myNewmaterial;
        }

        new GameObject("---END OFFSET---START TRIMMED---");

        //STEP 4 Remove the highest and lowest 20% of the values
        int trimmer = values.Count / 5;
        Debug.Log("Trimmer:" + trimmer);
        values = values.GetRange(trimmer, trimmer * 3);
        
        //Now the inspector will show the values after trimming under the tab "Trimmed"
        trimmed = values.ToArray();

        //Show the remaining values in BLUE
        foreach (float value in values)
        {
            GameObject myNewGameObject = Instantiate(velona, Vector3.zero, Quaternion.Euler(-90f, value, 0f)) as GameObject;
            myNewGameObject.GetComponent<MeshRenderer>().material = myNewmaterial2;
        }

        //STEP 5 Find the average of the remaining values
        int counterForAverage = 0;
        float sumForAverage = 0;

       foreach (float value in values)
        {
            counterForAverage++;
            sumForAverage += value;
        }
        Debug.Log("sumForAverage" + sumForAverage);
        Debug.Log("counterForAverage" + counterForAverage);
        sumForAverage /= counterForAverage;
        Debug.Log("sumForAverage" + sumForAverage);

        //STEP 6 Undo the offset we did in step 2
        sumForAverage -= 45f * myOffset;
        Debug.Log("The solution is: " + sumForAverage);

        //Display solution in WHITE
        GameObject solution = Instantiate(velona, Vector3.zero, Quaternion.Euler(-90f, sumForAverage, 0f)) as GameObject;
        solution.GetComponent<MeshRenderer>().material = myNewmaterial3;
    }



    int Measure8Quadrants(List<float> myList)
    {
        Quadrants = new int[8];

        foreach (float currentvalue in values)
        {
            if (currentvalue >= 315f || currentvalue < 45f) Quadrants[4]++;
            if (currentvalue < 90f) Quadrants[3]++;
            if (currentvalue >= 45f && currentvalue < 135f) Quadrants[2]++;
            if (currentvalue >= 90f && currentvalue < 180f) Quadrants[1]++;
            if (currentvalue >= 135f && currentvalue < 225f) Quadrants[0]++;
            if (currentvalue >= 180f && currentvalue < 270f) Quadrants[7]++;
            if (currentvalue >= 225f && currentvalue < 315f) Quadrants[6]++;
            if (currentvalue >= 270f) Quadrants[5]++;
        }

        //Find strongest Quadrant
        int result = 0;
        int maxValue = Quadrants[0];
        for (int i = 1; i < 8; i++)
        {
            if (Quadrants[i] > maxValue)
            {
                maxValue = Quadrants[i];
                result = i;
            }
        }
        return result;
    }

    List<float> OffsetAngles(List<float> myList, int offsetFactor)
    { 
        int limit = myList.Count;
        for (int i = 0; i < limit; i++)
        {
            myList[i] += 45f * offsetFactor;
            if (myList[i] > 360) myList[i] -= 360f;
            if (myList[i] < 0  ) myList[i] += 360f;
        }
        return myList;
    }
}

