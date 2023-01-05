using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace QuestionData 
{
    public class Action
    { 
        public string character;
        public string dialogue;
        public Action(string _character, string _dialogue)
        {
            character = _character;
            dialogue = _dialogue;
        }
    }

    public  class Question
    {
        public string character;
        public string dialogue;
        public Question(string _character, string _dialogue)
        {
            character = _character;
            dialogue = _dialogue;
        }
    }

    public class QC
    {
        public Question question{get;set;}
        public List<string> choices{get;set;}
        public List<List<Action>> actions{get;set;}

        public QC(string path, int choiceCnt)
        {
            AddQuestion(path + "/Question.txt");
            AddOptionContent(path + "/Question.txt");
            actions = new List<List<Action>>();
            for(int i = 1; i <= choiceCnt; i++)
            {
                AddAction(path + "/" +  i + "_Answer.txt");
            }
        }
        public void AddQuestion(string path)
        {
            string line = File.ReadAllText(path).Split('\n')[0];
            question = new Question(line.Split('|')[0], line.Split('|')[1]);
        }
        void AddOptionContent(string path)
        {
            // 내용 저장
            string[] line = File.ReadAllText(path).Split('\n');
            choices = new List<string>();

            for(int i = 1; i < line.Length; i++)
            {
                choices.Add(line[i]);
            }
        }
        public void AddAction(string path)
        {
            // 반응 저장
            string[] line = File.ReadAllText(path).Split('\n');
            List<Action> tmp = new List<Action>();
            
            for(int i = 0; i < line.Length; i++)
            {
                tmp.Add(new Action(line[i].Split('|')[0],line[i].Split('|')[1]));
            }
            actions.Add(tmp);
        }
        
    }   

    public class QP
    {
        public int correctTypeNum{get;set;}
        public int corrrectNum{get;set;}
        public List<Question> question{get;set;}
        public List<Action> correctAction{get;set;}
        public List<Action> nCorrectAction{get;set;}

        public QP(string path, string correctProof)
        {
            question = new List<Question>();
            correctAction = new List<Action>();
            nCorrectAction = new List<Action>();
            correctTypeNum = int.Parse(correctProof.Split('_')[0]);
            corrrectNum = int.Parse(correctProof.Split('_')[1]);
            AddQuestion(path + "/Question.txt");
            AddAction(path + "/1_Answer.txt", correctAction);
            AddAction(path + "/2_Answer.txt", nCorrectAction);
        }
        public void AddQuestion(string path)
        {
            string[] line = File.ReadAllText(path).Split('\n');
            
            for(int i = 0; i < line.Length; i++)
            {
                question.Add(new Question(line[i].Split('|')[0], line[i].Split('|')[1]));
            }
        }
        public void AddAction(string path, List<Action> actions)
        {
            string[] line = File.ReadAllText(path).Split('\n');
            //actions = new List<Action>();
            for(int i = 0; i < line.Length; i++)
            {
                actions.Add(new Action(line[i].Split('|')[0],line[i].Split('|')[1]));
            }
        }
    }
}
