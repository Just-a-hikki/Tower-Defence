using SpaceShooter;
using System;
using System.Xml.Schema;
using UnityEngine;

namespace TowerDefence
{
    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        public const string filename = "completion_data";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        [SerializeField] private EpisodeScore[] completionData;
        public int TotalScore { private set; get; }
        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }
            Debug.Log(levelScore);
        }
        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
            foreach (var episodeScore in completionData)
            {
                TotalScore += episodeScore.score;
            }
        }
        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in completionData)
            {
                if (item.episode == currentEpisode)
                {
                    if (levelScore > item.score)
                    {
                        TotalScore += levelScore - item.score;
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, completionData);
                    }
                }
            }
        }
        public int GetEpisodeScore(Episode m_episode)
        {
            foreach (var data in completionData)
            {
                if(data.episode == m_episode)
                {
                    return data.score;
                }
            }
            return 0;
        }
    }
}
