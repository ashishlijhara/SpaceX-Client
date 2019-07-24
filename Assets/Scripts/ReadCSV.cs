using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ReadCSV
{
    public string[] headers;
    public enum FileFrom { Assets, PersistantStorage, StramingAssets };

    public void Read(string fileName, out DataTable csvData, FileFrom from = FileFrom.Assets)
    {
        switch (from)
        {
            case FileFrom.Assets:
                break;
            case FileFrom.PersistantStorage:
                fileName = Application.persistentDataPath + "/" + fileName;
                break;
            case FileFrom.StramingAssets:
                fileName = Application.streamingAssetsPath + "/" + fileName;
                break;
        }

        string data = null;
#if UNITY_EDITOR || UNITY_IOS
        data = File.ReadAllText(fileName);
#elif UNITY_ANDROID
        UnityWebRequest reader = UnityWebRequest.Get(fileName);
        reader.SendWebRequest();
        while (!reader.isDone) {
        }
        data = reader.downloadHandler.text;
#endif
        string[] lines = data.Split('\n');
        headers = SplitLine(lines[0]);
        lines = lines.Where((val, idx) => idx != 0).ToArray();
        csvData = new DataTable();

        foreach (string header in headers)
            csvData.Columns.Add(header);

        foreach (string line in lines)
        {
            string[] lineData = SplitLine(line);
            csvData.Rows.Add(lineData);
        }
    }

    string[] SplitLine(string line)
    {
        return (line.Trim()).Split(',');
    }
}


