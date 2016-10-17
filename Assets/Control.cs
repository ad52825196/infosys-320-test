using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
using Pathfinding.Serialization.JsonFx;

public class Control : MonoBehaviour {
    public string Website;
    public GameObject sphere;

    // Use this for initialization
    void Start() {
        Website += "?zumo-api-version=2.0.0";
        StartCoroutine(GetData());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator GetData() {
        UnityWebRequest www = UnityWebRequest.Get(Website);
        yield return www.Send();
        if (www.isError) {
            Debug.Log("There is an error in your network connection.");
        } else {
            Generate(www.downloadHandler.text);
        }
    }

    void Generate(string json) {
        GameObject newObject;
        GameObject child;
        Vector3 coordinate;
        Point[] points = JsonReader.Deserialize<Point[]>(json);

        foreach (Point point in points) {
            Debug.Log("The Tree ID is: " + point.TreeID);
            coordinate = new Vector3(float.Parse(point.X), float.Parse(point.Y), float.Parse(point.Z));
            newObject = (GameObject) Instantiate(sphere, coordinate, Quaternion.Euler(0, 0, 0));
            for (int i = 0; i < newObject.transform.childCount; i++) {
                child = newObject.transform.GetChild(i).gameObject;
                switch (child.name) {
                    case "Label":
                        child.GetComponent<TextMesh>().text = String.Format("**Label** Tree ID: {0}", point.TreeID);
                        break;
                    case "Data":
                        child.GetComponent<TextMesh>().text = String.Format("Data:" + Environment.NewLine + "Location: {0}" + Environment.NewLine + "Ecological Value: {1}  Historical Significance: {2}" + Environment.NewLine + "X: {3}  Y: {4}  Z: {5}", point.Location, point.EcologicalValue, point.HistoricalSignificance, point.X, point.Y, point.Z);
                        break;
                }
            }
        }
    }
}
