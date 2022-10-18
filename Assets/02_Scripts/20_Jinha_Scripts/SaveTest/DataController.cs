using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class DataController : MonoBehaviour
{
    //싱글톤 선언
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static DataController _instance; 
    public static DataController Instance 
    { 
        get 
        { 
            if (!_instance) 
            { 
                _container = new GameObject();
                _container.name = "DataController"; 
                _instance = _container.AddComponent(typeof(DataController)) as DataController; 
                DontDestroyOnLoad(_container); 
            } 
            return _instance; 
        } 
    } 
    
    //게임 데이터 파일 이름 설정
    public string GameDataFileName = "TestGameData.json"; 
    
    // "원하는 영문 이름.json"
    public GameData _gameData; 
    public GameData gameData 
    {
        get 
        { 
            // 게임이 시작되면 자동실행
            if(_gameData == null)
            { 
                LoadGameData(); 
                SaveGameData();
            }
            return _gameData;
        }
    } 
    
    private void Start() 
    {
        LoadGameData(); 
        SaveGameData();
    } 
    
    // 저장된 게임 불러오기
    public void LoadGameData() 
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        
        if (File.Exists(filePath))
        { 
            print("게임 불러오기"); 
            string FromJsonData = File.ReadAllText(filePath); 
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        
        else 
        { 
            print("새로운 파일 생성");            
            _gameData = new GameData();
        } 
    } 
    
    //게임 저장하기
    public void SaveGameData() 
    { 
        string ToJsonData = JsonUtility.ToJson(gameData); 
        string filePath = Application.persistentDataPath + GameDataFileName; 

        // 파일 덮어쓰기
        File.WriteAllText(filePath, ToJsonData);
        
        //잘 저장됐는지 확인 
        print("저장완료"); 
        print("2는 " + gameData.isClear2); 
        print("3는 " + gameData.isClear3); 
        print("4는 " + gameData.isClear4);
        print("5는 " + gameData.isClear5); 
    } 
    
    // 게임을 종료하면 자동저장되도록
    private void OnApplicationQuit()
    { 
        SaveGameData(); 
    } 
} 