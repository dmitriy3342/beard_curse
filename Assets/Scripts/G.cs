using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



//[JsonObject(MemberSerialization.OptIn)]
//private class SerializeStatistics
//{

//    [JsonProperty]
//    public string Name { get; set; }

//}


public class G
{
    private static bool _isEnabledFreezing = false;
    public static bool isEnabledFreezing => _isEnabledFreezing;

    public static void Freeze() => _isEnabledFreezing = true;
    public static void Unfreeze() => _isEnabledFreezing = false;

    public static int fps = 0;


    private static int _attempt = 1;

    public static int attempt
    {
        get => _attempt;
        set => _attempt = value >= 1 ? value : 1;
    }


    private static float _timePlayingActive = 0;
    private static float _timePlaying = 0;

    public static float timePlaying
    {
        get => _timePlaying;
        set => _timePlaying = value;
    }


    private static int _speed = 1;
    public static int speed
    {
        get => _speed;
        set => _speed = value >= 1 ? value : 1;
    }

    public static void initLevel(int levelId)
    {

    }

    public static void Start()
    {
        Player.instance.Init();
        _speed = 1;
        _timePlaying = 0;
        _timePlayingActive = Time.time;
        Debug.Log($"Start timePlaying {_timePlaying}");
    }

    public static void Stop()
    {
        _timePlaying += Time.time - _timePlayingActive;
        Debug.Log($"Stop timePlaying {_timePlaying}");
        AddCurrentStatistic();
        SaveStatistics();
    }

    public static void Pause()
    {
        _timePlaying += Time.time - _timePlayingActive;
        G.Freeze();
        Debug.Log($"Pause timePlaying {_timePlaying}");
        SaveStatistics();

    }
    public static void Resume()
    {
        _timePlayingActive = Time.time;
        G.Unfreeze();
        Debug.Log($"Resume timePlaying {_timePlaying}");
    }


    public static void Save(string key, object o)
    {
        string jsonString = JsonConvert.SerializeObject(o);

        //string path = $"{Application.persistentDataPath}/{key}.json";
        string path = Application.persistentDataPath + $"/{key}.json";

        Debug.Log($"Path: {path}");
        Debug.Log($"Save key: {key} value: {jsonString}");


        StreamWriter writer = File.CreateText(path);
        writer.Write(jsonString);
        writer.Close();
        //File.Create(path).Dispose();
        //using (var file = File.Open(path, FileMode.OpenOrCreate))
        //{
        //    var w = new StreamWriter(file);
        //    w.Write(jsonString);
        //    w.Close();
        //    file.Close();
        //} // file is automatically closed after reaching the end of the using block


        //using (StreamWriter writer = File.CreateText(path))
        //{
        //    //using (StreamWriter writer = new StreamWriter(path, false))
        //    //{
        //        writer.Write(jsonString);
        //        //writer.Flush();
        //        writer.Close();
        //    //}
        //}

    }


    public static T Load<T>(string key,string defaultJsonValue = "{}")
    {
        string path = $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}{key}.json";
        string jsonString;
        try
        {
            if (File.Exists(path))
            {
                var reader = new StreamReader(path);
                jsonString = reader.ReadToEnd();
                if (jsonString == null)
                {
                    jsonString = defaultJsonValue;
                }
                reader.Close();
            }
            else
            {
                jsonString = defaultJsonValue;
            }
            
        }
        catch
        {
            jsonString = defaultJsonValue;
        }
        var resO = JsonConvert.DeserializeObject<T>(jsonString);
        Debug.Log($"Path: {path}");
        Debug.Log($"Load key: {key} value: {jsonString}");

        return resO;
    }

    public static void SaveStatistics()
    {
        Debug.Log("Saving statistics");
        Debug.Log($" try save StatisticsDict: {JsonConvert.SerializeObject(_statisticsDict)}");
        Save("statistics_dict", _statisticsDict);
        var obj = Load<Dictionary<int, Dictionary<string, long>>>("statistics_dict");

        Debug.Log($"PlayerPrefs GET StatisticsDict: {JsonConvert.SerializeObject(obj)}");

    }

    public static void LoadStatistics()
    {
        Debug.Log("Loading statistics");
        _statisticsDict = Load<Dictionary<int, Dictionary<string, long>>>("statistics_dict");
        Debug.Log($"PlayerPrefs GET StatisticsDict: {JsonConvert.SerializeObject(_statisticsDict)}");
        _attempt = _statisticsDict.Count + 1;

    }
    public static void AddCurrentStatistic()
    {
        _statisticsDict[G._attempt] = new Dictionary<string, long> {
                        { "attempt", G._attempt },
                        { "speed", G.speed },
                        { "result",(long) (G._timePlaying * 1000f)},
                    };
        G._attempt +=1;
        Debug.Log("Add Current Statistic");
        Debug.Log($"TimePlaying :{G._timePlaying}");
        Debug.Log($"TimePlaying :{(long)(G._timePlaying * 1000f)}");
        Debug.Log(JsonConvert.SerializeObject(_statisticsDict));
        SaveStatistics();
    }

    private static Dictionary<int, Dictionary<string, long>> _statisticsDict;

    public static Dictionary<int, Dictionary<string, long>> statisticsDict
    {
        get
        {
            //if (_statisticsDict == null)
            //{
            //    _statisticsDict = new Dictionary<int, Dictionary<string, long>>();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        _statisticsDict[i] = new Dictionary<string, long> {
            //            { "attempt", i+1 },
            //            { "speed", G.speed },
            //            { "result", U.Timestamp },
            //        };

            //    }
            //}
            return _statisticsDict;
        }
    }

}
