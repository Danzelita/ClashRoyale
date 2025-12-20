using System;
using System.Collections.Generic;
using UnityEngine;

public class Registation : MonoBehaviour
{
    public event Action Error;
    public event Action Success;
    
    private const string Login = "login";
    private const string Password = "password";
    
    private string _login;
    private string _password;
    private string _confirmPassword;

    public void SetLogin(string login) => 
        _login = login;

    public void SetPassword(string password) => 
        _password = password;
    
    public void SetConfirmPassword(string confirmPassword) =>
        _confirmPassword = confirmPassword;

    public void SignUp()
    {
        if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_confirmPassword))
        {
            ErrorMessage("Login and password are required");
            return;
        }

        if (_password != _confirmPassword)
        {
            ErrorMessage($"Password and confirm password are different: {_password} != {_confirmPassword}");
            return;
        }

        string url = URLLibrary.Registration;
        Dictionary<string, string> data = new()
        {
            [Login] = _login,
            [Password] = _password
        };
        Network.Instance.Post(url, data, Succes, ErrorMessage);
    }

    private void Succes(string data)
    {
        if (data != "ok")
        {
            ErrorMessage("Ответ с сервера: " + data);
            return;
        }
        Debug.Log("Успешная регистрация");
        Success?.Invoke();
    }

    private void ErrorMessage(string message)
    {
        Debug.LogError(message);
        Error?.Invoke();
    }
}
