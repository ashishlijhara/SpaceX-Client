using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Unity.Entities;
using System;
using System.Globalization;
using System.Linq;

public class Orbit : MonoBehaviour
{
    ReadCSV csvReader;
    DataTable csvData;
#pragma warning disable 0649
    [SerializeField]
    GameObject car;

    [SerializeField]
    string fileName;
#pragma warning restore 0649

    FixedLenQueue<Vector3> lineLocs;

    [SerializeField]
    Material lineMat;
    LineRenderer lr;

    int numInstantiated = 0;

    double div = Mathf.Pow(10, 4.2f);

    Vector3 startPosition;
    Vector3 nextPoint;

    float nextDuration = 0.1f;
    float elapsedDuration = 0;

    int currRow = 0;
    const int maxQCap = 20;

    // Start is called before the first frame update
    void Start()
    {
        csvReader = new ReadCSV();
        lineLocs = new FixedLenQueue<Vector3>(maxQCap);
        lr = GetComponent<LineRenderer>();

        csvReader.Read(fileName, out csvData, ReadCSV.FileFrom.StramingAssets);

        Vector3 vec = VectorForUnity(csvData.Rows[numInstantiated++]);
        startPosition = car.transform.position = nextPoint = vec;

        lineLocs.Add(vec);
    }


    double GetDouble(object s)
    {
        double val;
        double.TryParse(s.ToString(),out val);
        return val;
    }

    Vector3 VectorForUnity(DataRow row)
    {
        Vector3Double vec = OrbitHelper.OrbitalStateVectors(GetDouble(row[2]), GetDouble(row[3]), GetDouble(row[4]), GetDouble(row[5]), GetDouble(row[6]), GetDouble(row[8]));//Calculations.CalculateOrbitalPosition(GetDouble(row[2]), GetDouble(row[3]), GetDouble(row[4]), GetDouble(row[5]), GetDouble(row[6]), GetDouble(row[8]));

        Vector3 v = (vec / div).ToFLoat();//new Vector3((float)(vec.x / div), (float)(vec.y / div), (float)(vec.z / div));
        return v;
    }

    private void Update()
    {
        elapsedDuration += Time.deltaTime / nextDuration;
        car.transform.position = Vector3.Lerp(startPosition, nextPoint, elapsedDuration);
        float diff = nextDuration-elapsedDuration;
        if (diff < 0.1)
        {
            if (++currRow == csvData.Rows.Count-1) {
                currRow = 0;
                ProcessPoint(1);
                return;
            }

            float duration = 0;
            DateTime nextDate = DateTime.ParseExact(csvData.Rows[currRow + 1][1].ToString(), "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
            DateTime currDate = DateTime.ParseExact(csvData.Rows[currRow][1].ToString(), "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
            duration = (float)(nextDate - currDate).TotalHours / 24;

            if (duration == 0)
                duration = 1;
            ProcessPoint(duration);
        }
        
        //Render Tail
        List<Vector3> linePoints = lineLocs.ToList();
        linePoints.Add(car.transform.position);
        lr.positionCount = linePoints.Count;
        lr.SetPositions(linePoints.ToArray());

    }

    void ProcessPoint(float duration)
    {
        int prevRow = currRow - 1;
        if (currRow == 0)
            prevRow = csvData.Rows.Count - 1;
        Vector3 vec = VectorForUnity(csvData.Rows[prevRow]);
        lineLocs.Add(vec);
        vec = VectorForUnity(csvData.Rows[currRow]);
        SetDestination(vec, duration);
    }

    public void SetDestination(Vector3 destination, float time)
    {
        elapsedDuration = 0;
        startPosition = car.transform.position;
        nextDuration = time;
        nextPoint = destination;
    }
}

