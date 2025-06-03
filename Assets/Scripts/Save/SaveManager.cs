using System.IO;
using Levels;
using UnityEditor.AddressableAssets.Build.Layout;
using UnityEngine;

namespace Save
{
    public class SaveManager 
    {
        private readonly string levelDataSavePath = Application.persistentDataPath + "/PlayerLevelData.json";

        public LevelEntity LoadLevel()
        {
            if (File.Exists(levelDataSavePath))
            {
                string loadLevelData = File.ReadAllText(levelDataSavePath);
                return JsonUtility.FromJson<LevelEntity>(loadLevelData);
            }
            else
            {
                LevelEntity levelEntity = new LevelEntity();
                levelEntity.Level = 1;
                string json = JsonUtility.ToJson(levelEntity);
                File.WriteAllText(levelDataSavePath, json);
                return levelEntity;
            }
        }

        public void IncreaseLevel()
        {
            LevelEntity current = LoadLevel();
            current.Level++;
            SaveLevel(current.Level);
        }

        private void SaveLevel(int level)
        {
            LevelEntity levelEntity = new LevelEntity { Level = level };
            string json = JsonUtility.ToJson(levelEntity);
            File.WriteAllText(levelDataSavePath, json);
        }
    }
}
