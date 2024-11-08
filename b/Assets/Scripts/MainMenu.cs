using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefence
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private GameObject Question;
        private void Start()
        {
            continueButton.interactable = FileHandler.HasFile(MapCompletion.filename);
        }
        public void NewGame()
        {
            if (continueButton.interactable)
            {
                Question.SetActive(true);
            }
            else
            {
                FileHandler.Reset(MapCompletion.filename);
                FileHandler.Reset(Upgrades.filename);
                SceneManager.LoadScene(1);
            }
        }
        public void Continue()
        {
            SceneManager.LoadScene(1);
        }
        public void Quit()
        {
            Application.Quit();
        }
        public void AnswerIsNo()
        {
            Question.SetActive(false);
        }
        public void AnswerIsYes()
        {
            FileHandler.Reset(MapCompletion.filename);
            SceneManager.LoadScene(1);
        }

    } 
}
