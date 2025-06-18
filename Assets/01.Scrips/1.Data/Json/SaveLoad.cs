using UnityEngine;
using Newtonsoft.Json;
using System.IO;

class SaveLoad : MonoBehaviour
{
    DataForSaveLoad data = new DataForSaveLoad(null, null, null, null);//
    AESCryptor aes;
    private readonly string folderPath = Application.dataPath.ToString() + "/Save";
    private readonly string filePath = Application.dataPath.ToString() + "/Save/Save.json";
    private readonly string filePathTest = Application.dataPath.ToString() + "/Save/SaveTest.json";

    private void Start()
    {        
        aes = new AESCryptor();        
    }

    public void Save()
    {
        if (Directory.Exists(folderPath) == false)
        {
            Directory.CreateDirectory(folderPath);
        }

        data = data.GetSaveData();
        //JsonSerializerSettings settings = new JsonSerializerSettings();
        //settings.Formatting = Formatting.Indented;
        //settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

        string encryptedString = aes.Encryptor(jsonString);

        File.WriteAllText(filePath, encryptedString);
        File.WriteAllText(filePathTest, jsonString);
    }

    public void Load()
    {
        if(File.Exists(filePath) == false)
        {
            Debug.LogWarning("저장 데이터 없음");
            return;
        }

        string encryptedString = File.ReadAllText(filePath);

        string jsonString = aes.Decryptor(encryptedString);

        data.GetLoadData(jsonString);
    }
}
