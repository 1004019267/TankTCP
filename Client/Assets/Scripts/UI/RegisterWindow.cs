using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TankMsg;
using System;

public class RegisterWindow : BaseWindow
{
    Button btnRegister;
    Button btnReturn;

    InputField inputAccount;
    InputField inputPwd;
    InputField inputNick;
    // Use this for initialization
    public void Init()
    {
        btnReturn = _transform.Find("BtnReturn").GetComponent<Button>();
        btnRegister = _transform.Find("BtnRegister").GetComponent<Button>();
        inputAccount = _transform.Find("InputAccount").GetComponent<InputField>();
        inputPwd = _transform.Find("InputPwd").GetComponent<InputField>();
        inputNick = _transform.Find("InputNick").GetComponent<InputField>();

        btnRegister.onClick.AddListener(OnRegisterBtnClick);
        btnReturn.onClick.AddListener(OnReturnBtnClick);
    }

    private void OnReturnBtnClick()
    {
        UIManager.instance.Close<RegisterWindow>();
        UIManager.instance.Open<LoginWindow>().Init();
    }

    void OnRegisterBtnClick()
    {
        if (inputAccount.text.Length > 12 || inputAccount.text.Length < 3) return;
        if (inputPwd.text.Length > 12 || inputPwd.text.Length < 3) return;
        if (inputNick.text.Length > 7 || inputNick.text.Length < 3) return;

        ReqRegister req = new ReqRegister();
        req.account = inputAccount.text;
        req.pwd = inputPwd.text;
        req.nickname = inputNick.text;

        NetClient.instance.Send<ReqRegister>((int)MsgID.ReqRegister, req);
    }
}
