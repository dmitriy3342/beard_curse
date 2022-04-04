using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
/// <summary>
/// Utils class
/// Класс для утилит для удобства работы используется сокращенный вариант названия U
/// </summary>


[Serializable]
class FunctionNotDefinedForParams : Exception
{
    public FunctionNotDefinedForParams() : base() { }
    public FunctionNotDefinedForParams(string message) : base(message) { }
    public FunctionNotDefinedForParams(string message, Exception inner) : base(message, inner) { }
}


static class Translate
{
    public static string lang = "en";

    static Dictionary<string, Dictionary<string, string>> _langs;

    public static Dictionary<string, Dictionary<string, string>> langs
    {
        get
        {
            if (_langs == null)
            {
                _langs = new Dictionary<string, Dictionary<string, string>>();
                _langs.Add("en", en);
                _langs.Add("ru", ru);
            }
            return _langs;
        }
    }

    public static string translate(string s)
    {
        string res;
        if (langs[lang].TryGetValue(s, out res)) return res;
        if (lang != "en") Debug.LogWarning($"Not found translate for lang {lang} {s}");
        return s;
    }

    public static void setLang(string s)
    {
        lang = s;
        Debug.Log($"Set lang as {lang}");
    }

    static Dictionary<string, string> en = new Dictionary<string, string>
    {
        { "LANG","EN" },
        { "ABOUT DESCRIPTION","Beard Curse , help prevent lawlessness, try to stop the budding of the beard"}
    };
    static Dictionary<string, string> ru = new Dictionary<string, string> {
        { "LANG","RU" },
        { "MENU","МЕНЮ" },
        { "RESUME","ПРОДОЛЖИТЬ" },
        { "RESTART","ПЕРЕИГРАТЬ" },
        { "PLAY GAME","ИГРАТЬ" },
        { "PROGRESS","ПРОГРЕСС" },
        { "EXIT","ВЫЙТИ" },
        { "ABOUT","О ПРОГРАММЕ" },
        { "SETTINGS","НАСТРОЙКИ" },
        { "LEVEL","УРОВЕНЬ" },
        { "PAUSE","ПАУЗА" },
        { "TOTAL TIME","ВРЕМЯ НА УРОВНЕ" },
        { "RESULT","РЕЗУЛЬТАТ" },
        { "ABOUT DESCRIPTION","Проклятие Бороды, помогите предотвратить беспредел, попытайтесь остановить почкование бороды"}
    };
}



public static class U
{

    static public long time2ms(string time)
    {

        string pattern = @"\b([0-9]+):([0-9]+):([0-9]+).([0-9]+)\b";

        foreach (Match match in Regex.Matches(time, pattern, RegexOptions.IgnoreCase))
        {
            long milliseconds = int.Parse(match.Groups[4].Value);
            milliseconds += int.Parse(match.Groups[3].Value) * 1000;
            milliseconds += int.Parse(match.Groups[2].Value) * 60000;
            milliseconds += int.Parse(match.Groups[1].Value) * 3600000;
            return milliseconds;
        }
        throw new Exception("Not valid format value!");
    }

    //zero_if_less_then_10
    private static string zilt10(long value) => value < 10 ? $"0{value}" : $"{value}";

    public static string ms2time(long _microseconds)
    {
        long microseconds = _microseconds;
        long hours = microseconds / 3600000;
        microseconds -= hours * 3600000;
        long minutes = microseconds / 60000;
        microseconds -= minutes * 60000;
        long seconds = microseconds / 1000;
        microseconds -= seconds * 1000;
        microseconds += 1000;
        string microseconds_text = microseconds.ToString();
        microseconds_text = microseconds_text.Substring(1);
        return $"{zilt10(hours)}:{zilt10(minutes)}:{zilt10(seconds)}.{microseconds_text}";
    }


    public static DateTime Timestamp2DateTime(long timestamp) => DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;

    public static long DateTime2Timestamp(DateTime dt) => (long)Math.Round(dt.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds);

    public static long Timestamp => DateTime2Timestamp(DateTime.UtcNow);




    // ________________HAS________________
    public static bool has(string @object, string value) => @object.IndexOf(value) != -1;

    public static bool has(object[] @objects, object value)
    {
        foreach (object o in @objects)
        {
            if (o.Equals(value)) return true;
        }
        return false;
    }

    public static bool has(object @object, object value) => throw new FunctionNotDefinedForParams(@object.GetType().Name);

    // ________________TRANSLATE________________
    public static string t(string s) => Translate.translate(s);

    public static void setLang(string s) => Translate.setLang(s);
}