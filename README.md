<p align="center">
<img src="./assets/header.png">
</p>

<div align="center">

# TioUi.Avalonia

**现代化 Avalonia 控件库**

[![License](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg?style=for-the-badge)](https://dotnet.microsoft.com/)
[![Avalonia](https://img.shields.io/badge/Avalonia-12.0.0-red.svg?style=for-the-badge)](https://avaloniaui.net/)
![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20Linux-lightgrey.svg?style=for-the-badge)
![GitHub Star](https://img.shields.io/github/stars/tiouoo/TioUi.Avalonia?logo=github&label=Star&style=for-the-badge&color=#03DC6C)
![GitHub Forks](https://img.shields.io/github/forks/tiouoo/TioUi.Avalonia?logo=github&label=Fork&style=for-the-badge&color=#03DC6C)
![GitHub Watchers](https://img.shields.io/github/watchers/tiouoo/TioUi.Avalonia?logo=github&label=Watcher&style=for-the-badge&color=#03DC6C)

</div>

## 项目简介

TioUi 二次修改于 [Semi.Avalonia](https://github.com/irihitech/Semi.Avalonia) 和 [Ursa.Avalonia](https://github.com/irihitech/Ursa.Avalonia)

## 安装

### 通过 NuGet 安装

```bash
dotnet add package TioUi.Avalonia
```

### 引入 dll

[Release · tiouoo/TioUi.Avalonia](https://github.com/tiouoo/TioUi.Avalonia/releases/tag/auto-release)

### 手动安装

1. 克隆仓库

```bash
git clone https://github.com/tiouoo/TioUi.Avalonia.git
```

2. 在你的项目中添加引用

```xml
<ProjectReference Include="path/to/TioUi/TioUi.csproj" />
```

## 快速开始

### 配置 App.axaml

在你的应用程序中引入 TioUi 主题：

```xaml
<Application x:Class="YourApp.App"
             xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:tio="https://github.com/tiouoo/TioUi.Avalonia">
    <Application.Styles>
        <!-- 引入 TioUi 主题 -->
        <tio:TioUiTheme />
    </Application.Styles>
</Application>
```

### 设置语言与主题色（可选）

```xaml
<tio:TioUiTheme Language="zh_cn" ThemeColor="CornflowerBlue" />
```

或在CodeBehide设置

```csharp
using TioUi.Common.Helpers;
using TioUi.Common.Language;

public class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        // 设置默认语言为简体中文
        LangManager.SetLanguage(Languages.zh_cn);

        // 或设置为英语
        // LangManager.SetLanguage(Languages.en_us);

        // 设置主题色
        ThemeManager.SetThemeColor(Colors.CornflowerBlue);
        // 或使用十六进制颜色值
        // ThemeManager.SetThemeColor("#1890ff");

        base.OnFrameworkInitializationCompleted();
    }
}

// 或在运行时切换
public void SetLanguage()
{
    LangManager.SetLanguage(Languages.en_us);
}

// 在运行时切换主题色
public void SetThemeColor()
{
    ThemeManager.SetThemeColor("#52c41a");
}
```

#### 自定义语言

在你的项目中创建一个新类，实现 `ILang` 接口：

```csharp
using TioUi.Common.Language;

namespace YourApp.Languages;

public class LangJaJp : ILang
{
    public string Name => "名前";
    public string FileName => "ファイル名";
    public string UpdateAt => "更新日時";
    public string Type => "種類";
    ...
}
```

使用自定义语言

```csharp
using TioUi.Common.Language;
using YourApp.Languages;

// 在应用启动时设置
public class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        // 使用自定义语言
        LangManager.SetLanguage(new LangJaJp());

        base.OnFrameworkInitializationCompleted();
    }
}

// 或在运行时切换
public void SwitchToJapanese()
{
    LangManager.SetLanguage(new LangJaJp());
}
```

## 组件列表

TioUi 提供了丰富的组件库，涵盖基础组件、数据展示、导航、数据录入、反馈、布局等多个类别。

完整的组件列表和使用方法请查看[示例项目](https://github.com/tiouoo/TioUi.Avalonia/releases/tag/auto-release)。

## 平滑滚动

TioUi 内置了基于 [SmoothScroll.Avalonia](https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia) 移植的平滑滚动能力（独立程序集 `TioUi.SmoothScroll`）。

### ScrollView（平滑滚动 + 缩放）

`ScrollView` 是继承自 `ScrollViewer` 的平滑滚动容器，支持惯性滚动、触控/鼠标拖动、捏合与 Ctrl+滚轮缩放。引入 `TioUiTheme` 后即可直接使用：

```xaml
xmlns:tio="https://github.com/tiouoo/TioUi.Avalonia"

<tio:ScrollView IsZoomEnabled="True">
    <!-- 超大的内容 -->
</tio:ScrollView>
```

可通过 `ZoomTo` / `ZoomBy` 方法或 `ZoomFactor` 属性控制缩放：

```csharp
scrollView.ZoomTo(scrollView.ZoomFactor * 1.2);
```

### 让所有 ScrollViewer 平滑滚动（可选）

TioUi 主题默认不覆盖 `ScrollViewer` 外观。如需让整个应用中所有 `ScrollViewer` 都使用平滑滚动，可在任意层级引入 `ScrollViewerSmoothTheme`：

```xaml
<Application.Styles>
    <tio:TioUiTheme />
    <tio:ScrollViewerSmoothTheme />
</Application.Styles>
```

或仅对局部生效：

```xaml
<UserControl.Styles>
    <tio:ScrollViewerSmoothTheme />
</UserControl.Styles>
```



## 示例项目

TioUi 提供了一个完整的示例项目，展示了所有组件的用法：

```bash
cd src/TioUi.Demo
dotnet run
```

## 致谢

TioUi 的开发受到了以下优秀项目的启发：

- [Semi.Avalonia](https://github.com/irihitech/Semi.Avalonia)
- [Ursa.Avalonia](https://github.com/irihitech/Ursa.Avalonia)
- [SukiUI](https://github.com/kikipoulet/SukiUI)
- [SmoothScroll.Avalonia](https://github.com/zxbmmmmmmmmm/SmoothScroll.Avalonia)（平滑滚动，MIT）

## 许可证

本项目采用 [MIT](LICENSE) 许可证。
