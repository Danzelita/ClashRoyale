using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationUI : MonoBehaviour
{
    [SerializeField] private Authorization _authorization;
    [SerializeField] private TMP_InputField _loginField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _signUpButton;
    [SerializeField] private GameObject _authorizationCanvas;
    [SerializeField] private GameObject _registationCanvas;

    private void Awake()
    {
        _loginField.onEndEdit.AddListener(_authorization.SetLogin);
        _passwordField.onEndEdit.AddListener(_authorization.SetPassword);

        _signInButton.onClick.AddListener(SignInClick);
        
        _signUpButton.onClick.AddListener(SignUpClick);

        _authorization.Error += () =>
        {
            _signInButton.gameObject.SetActive(true);
            _signUpButton.gameObject.SetActive(true);
        };
    }

    private void SignUpClick()
    {
        _authorizationCanvas.gameObject.SetActive(false);
        _registationCanvas.gameObject.SetActive(true);
    }

    private void SignInClick()
    {
        _signInButton.gameObject.SetActive(false);
        _signUpButton.gameObject.SetActive(false);
        
        _authorization.SignIn();
    }
}