using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Game
{
    public static Vector2 WorkLocation => new Vector2(-26, -10);
    public static Vector2 MansionLocation => new Vector2(182, -110);
    public static Vector2 PaintingLocation => new Vector2(182, -73);
    public static Vector2 BasementLocation => new Vector2(182, 39);

    public const int SafeMaxAdd = 512305;
    public const int SafeMinAdd = 79879;



    public static void ResetFlag(string flag)
    {
        if (PlayerPrefs.HasKey(flag))
        {
            PlayerPrefs.DeleteKey(flag);
            PlayerPrefs.Save();
        }
    }
    public static void SetFlag(string flag, bool value = true)
    {
        PlayerPrefs.SetInt(flag, value ? 1 : 0);
        PlayerPrefs.Save();
    }
    public static bool CheckFlag(string flag)
    {
        return PlayerPrefs.HasKey(flag) && PlayerPrefs.GetInt(flag, 0) == 1;
    }

    public static void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }




    public static void ResetString(string flag)
    {
        if (PlayerPrefs.HasKey(flag))
        {
            PlayerPrefs.DeleteKey(flag);
            PlayerPrefs.Save();
        }
    }
    public static string GetOrGenerateString(string flag, Func<string> defaultValue)
    {
        if(!PlayerPrefs.HasKey(flag))
        {
            var value = defaultValue();
            PlayerPrefs.SetString(flag, value);
            PlayerPrefs.Save();
            return value;
        }

        return PlayerPrefs.GetString(flag);
    }

    public static void SetString(string flag, string value)
    {
        PlayerPrefs.SetString(flag, value);
        PlayerPrefs.Save();
    }

    public static bool HasFlag(string flag)
    {
        return PlayerPrefs.HasKey(flag);
    }
}

public static class Flag
{
    public static void ResetAll()
    {
        Game.ResetFlag(AvailableSave);
        Game.ResetFlag(ArrivedToHouse);
        Game.ResetFlag(MovedPainting);

        Game.ResetString(Note);
        Game.ResetString(NoteAdd);
        Game.ResetString(NoteValue);
    }

    public const string AvailableSave = "_availableSave";


    public const string ArrivedToHouse = "_arrived";
    public const string MovedPainting = "_reachedSatansLair";


    public const string Note = "_noteEquation";
    public const string NoteAdd = "_noteAdd";

    public const string NoteValue = "_noteValue";
}
