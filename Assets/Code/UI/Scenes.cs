using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scenes
{
    public static void Load(string name)
    {
        var operation = SceneManager.LoadSceneAsync(name);
        LoadingScreen.Main.TrackProgress(operation);
    }
}