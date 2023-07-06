using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : MonoBehaviour
{
    public static void SavePlayer(PlayerScript player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "SavedData.gamedata";    //Functia SavePlayer converteste toate datele de tip savedata in sistemul binar si 
                                                                                //le depune intr-un directory de tip Read Only cu un path generat de Unity
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SaveData LoadPlayer()
    {
        string path= Application.persistentDataPath + "SavedData.gamedata";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();                  //Functia LoadPlayer converteste datele salvate in persistentDataPath din nou in tipul SaveData
                                                                                //Daca nu avem date salvate afisam o eroare, insa butoanele Load functioneaza doar daca
            FileStream stream = new FileStream(path, FileMode.Open);            //au mai fost salvate date, verificand playerprefs

            SaveData data = formatter.Deserialize(stream) as SaveData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}

