# CpolarAutoConnect

[中文文档](https://github.com/LiXin-Link/CpolarAutoConnect/blob/main/README_zh.md)

A convenient tool for one click connection to machine which use cpolar

The cpolar free version's ip and port is random, this tool can avoid every time copy ip:port from the web

## Environmental requirements

dotnet 6 

## Module Description

* `CpolarAutoConnect.Core` get tunnel info from html, all platform support
* `CpolarAutoConnect.Xshell` use the tunnel info from core, auto open xshell and connect ssh.(only windows support) 

## Manual

set your cpolar loginName and password in setting.json5.(you can get in this page [https://dashboard.cpolar.com/login](https://dashboard.cpolar.com/login))：

```json5
{
  // loginName, maybe a email
  loginName: "your cpolar dashboard loginName",
  // password
  password: "your cpolar dashboard password",
  // xshell executable file location
  // this example is xshell 7's default location, change to your location
  xshellExeLocation: "C:/Program Files (x86)/NetSarang/Xshell 7/Xshell.exe",
  // the name of tunnel which to be connected
  defaultTunnelName: "default",
}
```

after execute CpolarAutoConnect.Xshell，the tool will get tunnels info from this page:[status](https://dashboard.cpolar.com/status)，and then auto start xshell and connect the ssh.

the tool will write the session info to a file named session in it's folder, and next time you start the tool, it will use the sieeion and not relogin.


## Other

* recently I use cpolar, I find it will disconnect some times(because the free version is only one online), so I will make a auto connect tool later, may be I will...