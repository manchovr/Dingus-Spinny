using BepInEx;
using BepInEx.Configuration;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DingusSpinny
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private AssetBundle bundle;
        private GameObject asset;
        private GameObject spawnedDingus;
        private ConfigEntry<float> configSpinSpeed;

        void Update()
        {
            if (spawnedDingus != null)
            {
                spawnedDingus.transform.Rotate(0f, configSpinSpeed.Value * Time.deltaTime, 0f, Space.World);
            }
        }
        void OnPlayerSpawned()
        {
            spawnedDingus = Instantiate(asset, new Vector3(-65.7588f, 11.6926f, -80.1358f), Quaternion.Euler(270f, 234f, 0f));
            Destroy(spawnedDingus.GetComponent<Collider>());
        }
        void Start()
        {
            bundle = LoadAssetBundle("DingusSpinny.assets.dingus");
            asset = bundle.LoadAsset<GameObject>("dingus");
            GorillaTagger.OnPlayerSpawned(OnPlayerSpawned);

            configSpinSpeed = Config.Bind("General", "Spin Speed", 90f, "dingus spinny speed yeah!");
        }
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
    }
}
