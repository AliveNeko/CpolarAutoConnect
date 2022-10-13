# CpolarAutoConnect

一个一键连接 cpolar 内网穿透机器的工具

cpolar 免费版的隧道ip和端口是随机的，不想每次连接的时候都跑到网页上登录，然后复制粘贴地址和端口，所以写了这个一键启动的小工具。

## 环境要求

dotnet 6 运行环境

## 模块说明

* `CpolarAutoConnect.Core` 爬取web页面的隧道信息，支持所有平台。（可自行编译，或进行二开）
* `CpolarAutoConnect.Xshell` 使用 Core 爬取的隧道信息自动打开 xshell 并连接，仅支持 windows（貌似 xshell 只支持 windows 来着）

## 使用说明

首先在 setting.json5 中设定 cpolar 的登录名和密码（就是这个页面的[https://dashboard.cpolar.com/login](https://dashboard.cpolar.com/login)）：

```json5
{
  // 登录名
  loginName: "your cpolar dashboard loginName",
  // 密码
  password: "your cpolar dashboard password",
  // xshell 文件位置
  // 如果是 xshell 6，请自行更换安装位置
  xshellExeLocation: "C:/Program Files (x86)/NetSarang/Xshell 7/Xshell.exe",
  // 获取到隧道信息后所使用的隧道名称
  defaultTunnelName: "default",
}
```

执行 CpolarAutoConnect.Xshell 后，会爬取这个[状态](https://dashboard.cpolar.com/status)页面上的隧道信息，自动登录并打开配置文件中的默认隧道，然后以 ssh 形式连接。

cpolar 登录之后会记录 session 信息，下次执行如果该 session 还是在线状态则不会再次登录。

## 其他

* 最近使用是发现有些时候 linux 上的 cpolar 会自动断开（因为免费版只能有一个在线），后续将会更新一个检测是否断开的程序并自动反代