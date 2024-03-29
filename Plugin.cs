using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using ModelReplacement;
using BepInEx.Configuration;
using System;
using System.Xml.Linq;

namespace ModelReplacement
{
    [BepInPlugin("lupey.model.cuddleteamleader", "CuddleTeamLeader", "0.1.0")]
    [BepInDependency("meow.ModelReplacementAPI", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigFile config;

        private static void InitConfig()
        {

        }
        private void Awake()
        {
            config = base.Config;
            InitConfig();
            Assets.PopulateAssets();

            ModelReplacementAPI.RegisterSuitModelReplacement("Cuddle Team Leader", typeof(ModelReplacementPlaceholder));


            Harmony harmony = new Harmony("lupey.model.cuddleteamleader");
            harmony.PatchAll();
            Logger.LogInfo($"Plugin {"lupey.model.cuddleteamleader"} is loaded!");
        }
    }
    public static class Assets
    {
        // Replace mbundle with the Asset Bundle Name from your unity project 
        public static string mainAssetBundleName = "templatebundle";
        public static AssetBundle MainAssetBundle = null;

        private static string GetAssemblyName() => Assembly.GetExecutingAssembly().GetName().Name.Replace(" ","_");
        public static void PopulateAssets()
        {
            if (MainAssetBundle == null)
            {
                Console.WriteLine(GetAssemblyName() + "." + mainAssetBundleName);
                using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetAssemblyName() + "." + mainAssetBundleName))
                {
                    MainAssetBundle = AssetBundle.LoadFromStream(assetStream);
                }

            }
        }
    }

}