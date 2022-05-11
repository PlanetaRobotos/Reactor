using System;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;

namespace _Core.Scripts.Managers.Save
{
    public class XmlSave : ISave
    {
        // private string encryptionKey = "MAKV2SPBNI99212";
        private string saveFileName = "GameSettingsSave.xml";
        private readonly string saveFilePath;
        private readonly Hashtable clearedSaveTable = new Hashtable();
        private readonly Hashtable unclearedSaveTable = new Hashtable();

        public XmlSave()
        {
            // Debug.Log($"{Application.persistentDataPath}");
            saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
        }

        public void Save()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("SavedData");
            xmlDoc.AppendChild(root);

            XmlElement clearedElement = xmlDoc.CreateElement("ClearedData");
            XmlElement unclearedElement = xmlDoc.CreateElement("UnclearedData");
            root.AppendChild(clearedElement);
            root.AppendChild(unclearedElement);

            AddChildren(xmlDoc, clearedElement, clearedSaveTable);
            AddChildren(xmlDoc, unclearedElement, unclearedSaveTable);

            xmlDoc.Save(saveFilePath);
        }

        private void AddChildren(XmlDocument xmlDoc, XmlElement xmlElement, Hashtable hashtable)
        {
            foreach (DictionaryEntry item in hashtable)
            {
                XmlElement saveNode = xmlDoc.CreateElement(item.Key.ToString());
                saveNode.InnerText = item.Value.ToString();
                xmlElement.AppendChild(saveNode);
            }
        }

        public void Load()
        {
            if (!File.Exists(saveFilePath)) return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(saveFilePath);
            XmlNode clearedNode = xmlDoc.DocumentElement.FirstChild;
            XmlNode unclearedNode = xmlDoc.DocumentElement.LastChild;

            foreach (XmlNode node in clearedNode)
                clearedSaveTable[node.Name] = node.InnerText;

            foreach (XmlNode node in unclearedNode)
                unclearedSaveTable[node.Name] = node.InnerText;
        }

        public void SetValue(string key, string value, bool isCleared = true)
        {
            if (isCleared)
                clearedSaveTable[key] = value;
            else
                unclearedSaveTable[key] = value;
            
            Save();
        }

        public int GetInt(string key)
        {
            return Convert.ToInt32(clearedSaveTable[key]);
        }

        public long GetInt64(string key)
        {
            return Convert.ToInt64(clearedSaveTable[key]);
        }

        public string GetString(string key)
        {
            return clearedSaveTable[key].ToString();
        }

        public bool GetBool(string key)
        {
            return Convert.ToBoolean(clearedSaveTable[key]);
        }

        public double GetDouble(string key)
        {
            return Convert.ToDouble(clearedSaveTable[key]);
        }

        public float GetFloat(string key)
        {
            return Convert.ToSingle(clearedSaveTable[key]);
        }

        // public void DeleteSave()
        // {
        // File.Delete(saveFilePath);
        // }
        
        public void DeleteSave()
        {
            if (!File.Exists(saveFilePath)) return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(saveFilePath);
            // XmlNode clearedNode = 
            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                if (node.Name == "ClearedData") 
                    xmlDoc.DocumentElement.RemoveChild(node);
            }

            xmlDoc.Save(saveFilePath);
            ClearHashTable();
        }

        public void ClearHashTable()
        {
            clearedSaveTable.Clear();
        }

        public bool HasKey(string key)
        {
            return clearedSaveTable.ContainsKey(key);
        }

        // private void Encrypt(string path, MemoryStream xmlStream)
        // {
        //     using Aes encryptor = Aes.Create();
        //     Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey,
        //         new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
        //     encryptor.Key = pdb.GetBytes(32);
        //     encryptor.IV = pdb.GetBytes(16);
        //     using FileStream fsOutput = new FileStream(path, FileMode.Create);
        //     using CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(),
        //         CryptoStreamMode.Write);
        //     int data;
        //     while ((data = xmlStream.ReadByte()) != -1) cs.WriteByte((byte) data);
        // }
        //
        // private void Decrypt(string path, MemoryStream xmlStream)
        // {
        //     using Aes encryptor = Aes.Create();
        //     Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey,
        //         new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
        //     encryptor.Key = pdb.GetBytes(32);
        //     encryptor.IV = pdb.GetBytes(16);
        //     using FileStream fsInput = new FileStream(path, FileMode.Open);
        //     using CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read);
        //     int data;
        //     while ((data = cs.ReadByte()) != -1) 
        //         xmlStream.WriteByte((byte) data);
        //
        //     xmlStream.Flush();
        //     xmlStream.Position = 0;
        // }
    }
}