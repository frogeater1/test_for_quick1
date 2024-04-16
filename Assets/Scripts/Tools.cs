using System;
using System.Diagnostics;
using System.IO;
using Demo.Unit;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Utilities
{
    public static class Tools
    {
        public static cfg.Tables tables;

        [MenuItem("Tools/Load")]
        public static void Load()
        {
#if UNITY_EDITOR_OSX
            var path = Environment.GetEnvironmentVariable("PATH")!;
            if (!path.Contains("/usr/local/bin"))
            {
                Environment.SetEnvironmentVariable("PATH", path + ":/usr/local/bin");
            }
#endif
            //注意这里有俩坑 (1) cmd.exe不执行，必须powershell.exe   (2) gen.bat必须加上"./"或".\\"，否则报错找不到文件
            var process = Process.Start(new ProcessStartInfo("powershell.exe", "./gen.bat")
            {
                WorkingDirectory = "../Table",
                UseShellExecute = false, //注意这里必须false否则无法重定向输出
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            })!;
            process.OutputDataReceived += (_, args) =>
            {
                if (args.Data != null)
                    Debug.Log(args.Data);
            };
            process.ErrorDataReceived += (_, args) =>
            {
                if (args.Data != null)
                    Debug.Log(args.Data);
            };
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.Close();
            AssetDatabase.Refresh();

            try
            {
                AssetDatabase.StartAssetEditing();

                tables = new cfg.Tables(file =>
                {
                    var textAsset = Resources.Load<TextAsset>(file);
                    return SimpleJSON.JSON.Parse(textAsset.text);
                });

                MakeCharacter();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }

            EditorUtility.DisplayDialog("结果", "load success", "ok");
        }

        private static void MakeCharacter()
        {
            foreach (var c in tables.TbCharacter.DataList)
            {
                var prefab = AssetDatabase.LoadAssetAtPath<Demo.Unit.Unit>($"Assets/Prefabs/{c.prefab}.prefab");
                var go = GameObject.Instantiate(prefab);
                try
                {
                    go.Load(c);
                    PrefabUtility.SaveAsPrefabAsset(go.gameObject, $"Assets/Imports/Prefabs/Units/{c.id}.prefab");
                }
                finally
                {
                    GameObject.DestroyImmediate(go.gameObject);
                }
            }
        }
        
    }
}