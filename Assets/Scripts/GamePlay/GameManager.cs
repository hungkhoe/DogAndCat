using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using _Main.Scripts.Definition;
using UnityEngine;
using UnityEngine.Events;

namespace DogAndCat
{
    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance => instance;
        public static AudioData AudioData => instance.audioData;

        private AudioData audioData;

        public static void Awaken() { }
        static GameManager() => instance = new GameManager();

        private GameManager()
        {
            PreloadAudios();
        }

        private void PreloadAudios()
        {
            audioData = new AudioData();
        }
    }
}

