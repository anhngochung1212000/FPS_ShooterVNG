using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    [SerializeField] TextAsset _gunConfigFile;
    static ConfigManager Instance;
    static public Dictionary<int, GunDataBase> gunData = new Dictionary<int, GunDataBase>();
    public static TextAsset GunDataAsset { get { return Instance._gunConfigFile; } }
    void Awake()
    {
        Instance = this;
    }

    public static void InitGameData()
    {
        List<GunDataBase> cardsData = JsonConvert.DeserializeObject<List<GunDataBase>>(GunDataAsset.text);
        foreach (var data in cardsData)
            gunData.Add(data.id, data);
    }
}
