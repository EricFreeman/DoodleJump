using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class BackgroundMusic : MonoBehaviour
    {
        public List<AudioClip> Songs;

        private int _lastSong;
        private bool _canPlayMusic;

        void Start()
        {
            var p = PlayerManager.Load();
            _canPlayMusic = p.IsMusicEnabled;

            // so it won't only play first song evey time level starts
            _lastSong = -1;

            DontDestroyOnLoad(gameObject);
            if(!AlreadyExists())
                PlayNewSong();
        }

        void Update()
        {
            if(!audio.isPlaying && _canPlayMusic) PlayNewSong();
        }

        private bool AlreadyExists()
        {
            // because this object isn't destroyed when switching levels,
            // there should already be one in the scene on load unless
            // you're starting a specific level from the editor

            if (FindObjectsOfType<BackgroundMusic>().Count() > 1)
            {
                DestroyImmediate(gameObject);
                return true;
            }

            return false;
        }

        private void PlayNewSong()
        {
            var newSong = _lastSong;
            var rand = new System.Random(DateTime.Now.Millisecond);

            while (newSong == _lastSong)
                newSong = rand.Next(0, Songs.Count);

            audio.clip = Songs[newSong];
            audio.Play();
            _lastSong = newSong;
        }
    }
}