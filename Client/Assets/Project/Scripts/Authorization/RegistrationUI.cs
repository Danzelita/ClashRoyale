using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationUI : MonoBehaviour
{
    [SerializeField] private Registation _registation;
    [SerializeField] private TMP_InputField _loginField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private TMP_InputField _confirmPasswordField;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Button _signIn;
    [SerializeField] private GameObject _authorizationCanvas;
    [SerializeField] private GameObject _registationCanvas;

    private void Awake()
    {
        _loginField.onEndEdit.AddListener(_registation.SetLogin);
        _passwordField.onEndEdit.AddListener(_registation.SetPassword);
        _confirmPasswordField.onEndEdit.AddListener(_registation.SetConfirmPassword);

        _applyButton.onClick.AddListener(SignUpClick);
        
        _signIn.onClick.AddListener(SignInClick);

        _registation.Error += () =>
        {
            _applyButton.gameObject.SetActive(true);
            _signIn.gameObject.SetActive(true);
        };
        _registation.Success += () =>
        {
            _signIn.gameObject.SetActive(true);
        };
    }
    
    private void SignInClick()
    {
        _registationCanvas.gameObject.SetActive(false);
        _authorizationCanvas.gameObject.SetActive(true);
    }

    private void SignUpClick()
    {
        _applyButton.gameObject.SetActive(false);
        _signIn.gameObject.SetActive(false);
        
        _registation.SignUp();
    }
}