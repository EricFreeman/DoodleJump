using System.IO;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public static class PlayerManager
    {
        public static string SaveGameFile =
            "{0}/savegame1.xml".ToFormat(Application.persistentDataPath);

        public static void Save(PlayerModel player)
        {
            var manager = new XmlManager<PlayerModel>();
            manager.Save(SaveGameFile, player);
        }

        public static PlayerModel Load()
        {
            var manager = new XmlManager<PlayerModel>();
            if (!File.Exists(SaveGameFile)) Reset(true);
            return manager.Load(SaveGameFile);
        }

        public static void Reset(bool makeNew = false)
        {
            var oldPlayer = makeNew ? new PlayerModel() : Load();

            var player = new PlayerModel
            {
                Money = 0,
                IsSoundEnabled = oldPlayer == null || oldPlayer.IsSoundEnabled,
                IsMusicEnabled = oldPlayer == null || oldPlayer.IsMusicEnabled
            };
            Save(player);
        }
    }
}