using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Authorization : MonoBehaviour
{
    public event Action Error;
    
    private const string Login = "login";
    private const string Password = "password";
    
    private string _login;
    private string _password;

    public void SetLogin(string login) => 
        _login = login;

    public void SetPassword(string password) => 
        _password = password;

    public void SignIn()
    {
        if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
        {
            ErrorMessage("Login and password are required");
            return;
        }


        string url = URLLibrary.Autorization;
        Dictionary<string, string> data = new()
        {
            [Login] = _login,
            [Password] = _password
        };
        Network.Instance.Post(url, data, Succes, ErrorMessage);
    }

    private void Succes(string data)
    {
        string[] result = data.Split("|");
        if (result.Length < 2 || result[0] != "ok")
        {
            ErrorMessage("Ответ с сервера: " + data);
            return;
        }

        if (int.TryParse(result[1], out int id))
        {
            UserInfo.Instance.SetID(id);
            Debug.Log($"Успешный вход, ID: {id}");
            SceneManager.LoadScene(1);
        }
        else
        {
            ErrorMessage($"Не удалось распарсить \"{result[1]}\" в INT. Полный ответ вот такой: {data}");
        }
    }

    private void ErrorMessage(string message)
    {
        Debug.LogError(message);
        Error?.Invoke();
    }
}