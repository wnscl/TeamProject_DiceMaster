using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    DataForSaveLoad data;
    AESCryptor aes;
    private readonly string folderPath = Application.dataPath.ToString() + "/Save";
    private readonly string filePath = Application.dataPath.ToString() + "/Save/Save.json";

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

        string jsonString = JsonConvert.SerializeObject(data);

        string encryptedString = aes.Encryptor(jsonString);

        File.WriteAllText(filePath, encryptedString);
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
