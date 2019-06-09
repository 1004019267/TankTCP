using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankMsg;
using UnityEngine.UI;
using System;

public class LoginWindow : BaseWindow
{
    public Button btnLogin;
    public Button btnRegister;

    InputField inputAccount;
    InputField inputPwd;

  
    public void Init()
    {     
        inputAccount = _transform.Find("InputAccount").GetComponent<InputField>();
        inputPwd = _transform.Find("InputPwd").GetComponent<InputField>();

        btnLogin = _transform.Find("BtnLogin").GetComponent<Button>();
        btnLogin.onClick.AddListener(OnBtnLoginClick);

        btnRegister = _transform.Find("BtnRegister").GetComponent<Button>();
        btnRegister.onClick.AddListener(OnBtnRegisterClick);
    }

    private void OnBtnRegisterClick()
    {
        UIManager.instance.Close<LoginWindow>();
        UIManager.instance.Open<RegisterWindow>().Init();
    }

    private void OnBtnLoginClick()
    {
        if (inputAccount.text.Length > 12) return;
        if (inputPwd.text.Length > 12) return;

        ReqLogin req = new ReqLogin();
        req.account = inputAccount.text;
        req.pwd = inputPwd.text;

        NetClient.instance.Send<ReqLogin>((int)MsgID.ReqLogin, req);
    }
}
