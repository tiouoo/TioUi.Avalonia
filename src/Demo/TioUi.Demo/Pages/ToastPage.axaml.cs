using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using TioUi.Common.Classes;
using TioUi.Controls;

namespace TioUi.Demo.Pages;

public partial class ToastPage : UserControl, INotifyPropertyChanged
{
    private TioToastManager? _toastManager;
    private string _lastCloseReason = "无";
    private readonly List<Toast> _activeToasts = new();

    public ToastPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public string LastCloseReason
    {
        get => _lastCloseReason;
        set
        {
            if (_lastCloseReason != value)
            {
                _lastCloseReason = value;
                OnPropertyChanged();
            }
        }
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private TioToastManager GetToastManager()
    {
        if (_toastManager != null) return _toastManager;

        _toastManager = App.RootView.ToastManager;
        return _toastManager!;
    }

    // 基础 Toast
    public void ShowInformationToast()
    {
        GetToastManager().Show("这是一条信息提示", NotificationType.Information);
    }

    public void ShowSuccessToast()
    {
        GetToastManager().Show("操作成功完成！", NotificationType.Success);
    }

    public void ShowWarningToast()
    {
        GetToastManager().Show("请注意这个警告信息", NotificationType.Warning);
    }

    public void ShowErrorToast()
    {
        GetToastManager().Show("发生了一个错误", NotificationType.Error);
    }

    // 不同持续时间
    public void ShowShortToast()
    {
        var toast = new Toast("短时间显示 (1秒)", NotificationType.Information, TimeSpan.FromSeconds(1));
        GetToastManager().Show(toast);
    }

    public void ShowNormalToast()
    {
        var toast = new Toast("正常时间显示 (3秒)", NotificationType.Success, TimeSpan.FromSeconds(3));
        GetToastManager().Show(toast);
    }

    public void ShowLongToast()
    {
        var toast = new Toast("长时间显示 (5秒)", NotificationType.Warning, TimeSpan.FromSeconds(5));
        GetToastManager().Show(toast);
    }

    public void ShowPersistentToast()
    {
        var toast = new Toast("持久显示，需要手动关闭", NotificationType.Information, TimeSpan.Zero);
        GetToastManager().Show(toast);
    }

    // 图标显示
    public void ShowToastWithIcon()
    {
        var options = new NotificationOptions
        {
            Content = "显示图标的 Toast",
            Type = NotificationType.Success,
            IsIconVisible = true
        };
        GetToastManager().Show(options);
    }

    public void ShowToastWithoutIcon()
    {
        var options = new NotificationOptions
        {
            Content = "隐藏图标的 Toast",
            Type = NotificationType.Information,
            IsIconVisible = false
        };
        GetToastManager().Show(options);
    }

    // 关闭按钮
    public void ShowToastWithCloseButton()
    {
        var toast = new Toast("带关闭按钮的 Toast", NotificationType.Information, TimeSpan.FromSeconds(5), showClose: true);
        GetToastManager().Show(toast);
    }

    public void ShowToastWithoutCloseButton()
    {
        var toast = new Toast("无关闭按钮的 Toast", NotificationType.Information, TimeSpan.FromSeconds(3), showClose: false);
        GetToastManager().Show(toast);
    }

    // 操作按钮
    public void ShowToastWithSingleButton()
    {
        var options = new NotificationOptions
        {
            Content = "带单个操作按钮的 Toast",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5),
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("确定", _ => { GetToastManager().Show("点击了确定按钮", NotificationType.Success); },
                    closeOnClick: true)
            }
        };
        GetToastManager().Show(options);
    }

    public void ShowToastWithMultipleButtons()
    {
        var options = new NotificationOptions
        {
            Content = "带多个操作按钮的 Toast",
            Type = NotificationType.Warning,
            Expiration = TimeSpan.FromSeconds(10),
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("确认", _ => { GetToastManager().Show("点击了确认", NotificationType.Success); },
                    closeOnClick: true),
                new OperateButtonEntry("取消", _ => { GetToastManager().Show("点击了取消", NotificationType.Information); },
                    closeOnClick: true),
                new OperateButtonEntry("查看详情", _ => { GetToastManager().Show("查看详情", NotificationType.Information); },
                    closeOnClick: false)
            }
        };
        GetToastManager().Show(options);
    }

    public void ShowToastWithInlineButtons()
    {
        var options = new NotificationOptions
        {
            Content = "内联按钮布局",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5),
            IsButtonsInline = true,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("是", _ => { }, closeOnClick: true),
                new OperateButtonEntry("否", _ => { }, closeOnClick: true)
            }
        };
        GetToastManager().Show(options);
    }

    public void ShowToastWithStackedButtons()
    {
        var options = new NotificationOptions
        {
            Content = "堆叠按钮布局",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5),
            IsButtonsInline = false,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("选项一", _ => { }, closeOnClick: true),
                new OperateButtonEntry("选项二", _ => { }, closeOnClick: true),
                new OperateButtonEntry("选项三", _ => { }, closeOnClick: true)
            }
        };
        GetToastManager().Show(options);
    }

    // 样式
    public void ShowLightToast()
    {
        var options = new NotificationOptions
        {
            Content = "Light 样式的 Toast",
            Type = NotificationType.Information,
            IsColorful = false
        };
        options.Classes.Clear();
        options.Classes.Add("Light");
        GetToastManager().Show(options);
    }

    public void ShowColorfulToast()
    {
        var options = new NotificationOptions
        {
            Content = "Colorful 样式的 Toast",
            Type = NotificationType.Success,
            IsColorful = true
        };
        GetToastManager().Show(options);
    }

    public void ShowDefaultToast()
    {
        var options = new NotificationOptions
        {
            Content = "默认样式的 Toast",
            Type = NotificationType.Warning,
            IsColorful = false
        };
        options.Classes.Clear();
        GetToastManager().Show(options);
    }

    // 交互事件
    public void ShowToastWithClick()
    {
        var toast = new Toast(
            "点击这个 Toast 试试",
            NotificationType.Information,
            TimeSpan.FromSeconds(5),
            onClick: () => { GetToastManager().Show("Toast 被点击了！", NotificationType.Success); }
        );
        GetToastManager().Show(toast);
    }

    public void ShowToastWithClose()
    {
        var toast = new Toast(
            "关闭时会触发事件",
            NotificationType.Information,
            TimeSpan.FromSeconds(3),
            onClose: _ => { GetToastManager().Show("Toast 已关闭", NotificationType.Information); }
        );
        GetToastManager().Show(toast);
    }

    public void ShowToastWithTouchClose()
    {
        var options = new NotificationOptions
        {
            Content = "点击任意位置关闭",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(10),
            IsTouchClose = true
        };
        GetToastManager().Show(options);
    }

    // 长文本内容
    public void ShowLongContentToast()
    {
        var options = new NotificationOptions
        {
            Content = "这是一条很长的提示信息，用于测试 Toast 组件在显示长文本时的表现效果。Toast 应该能够正确地显示和换行处理长文本内容。",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5)
        };
        GetToastManager().Show(options);
    }

    public void ShowMultiLineToast()
    {
        var options = new NotificationOptions
        {
            Content = "第一行内容\n第二行内容\n第三行内容\n这是多行文本的 Toast 示例",
            Type = NotificationType.Success,
            Expiration = TimeSpan.FromSeconds(5)
        };
        GetToastManager().Show(options);
    }

    // 综合示例
    public void ShowComplexToast()
    {
        var options = new NotificationOptions
        {
            Content = "这是一个功能完整的 Toast 示例，包含图标、多个操作按钮和事件处理",
            Type = NotificationType.Warning,
            Expiration = TimeSpan.FromSeconds(10),
            IsIconVisible = true,
            IsCloseButtonVisible = true,
            IsButtonsInline = true,
            IsColorful = true,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("重试", _ => { GetToastManager().Show("正在重试...", NotificationType.Information); },
                    closeOnClick: false),
                new OperateButtonEntry("忽略", _ => { }, closeOnClick: true)
            },
            OnClick = () => { GetToastManager().Show("Toast 被点击", NotificationType.Information); },
            OnClose = _ => { GetToastManager().Show("Toast 已关闭", NotificationType.Information); }
        };
        GetToastManager().Show(options);
    }

    public void ShowCustomToast()
    {
        var options = new NotificationOptions
        {
            Content = "自定义配置的 Toast",
            Type = NotificationType.Error,
            Expiration = TimeSpan.FromSeconds(8),
            IsIconVisible = true,
            IsCloseButtonVisible = true,
            IsCollapseButtonVisible = false,
            IsButtonsInline = false,
            IsColorful = false,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("查看日志",
                    _ => { GetToastManager().Show("打开日志查看器", NotificationType.Information); }, closeOnClick: false),
                new OperateButtonEntry("报告问题", _ => { GetToastManager().Show("打开问题报告", NotificationType.Information); },
                    closeOnClick: false),
                new OperateButtonEntry("关闭", _ => { }, closeOnClick: true)
            }
        };
        options.Classes.Clear();
        options.Classes.Add("Light");
        GetToastManager().Show(options);
    }

    public void ShowToastWithCloseReason()
    {
        var toast = new Toast(
            "关闭此 Toast 时会显示关闭原因",
            NotificationType.Information,
            TimeSpan.FromSeconds(5),
            onClose: reason =>
            {
                LastCloseReason = reason switch
                {
                    MessageCloseReason.Timeout => "超时自动关闭",
                    MessageCloseReason.UserAction => "用户手动关闭",
                    MessageCloseReason.Displaced => "被新 Toast 替换",
                    _ => "未知原因"
                };
            }
        );
        GetToastManager().Show(toast);
    }

    public void ShowMultipleToastsForClose()
    {
        // 显示多个 Toast 用于测试 Close 功能
        for (int i = 1; i <= 3; i++)
        {
            var index = i;
            var toast = new Toast(
                $"Toast #{index} - 持久显示",
                NotificationType.Information,
                TimeSpan.Zero // 持久显示
            );
            toast.OnClose = reason =>
            {
                LastCloseReason = $"Toast #{index} - {reason switch
                {
                    MessageCloseReason.Timeout => "超时",
                    MessageCloseReason.UserAction => "用户操作",
                    MessageCloseReason.Displaced => "被替换",
                    _ => "未知"
                }}";
            };
            _activeToasts.Add(toast);
            GetToastManager().Show(toast);
        }
    }

    public void CloseFirstToast()
    {
        if (_activeToasts.Count > 0)
        {
            var toast = _activeToasts[0];
            GetToastManager().Close(toast);
            _activeToasts.Remove(toast);
        }
        else
        {
            GetToastManager().Show("没有可关闭的 Toast", NotificationType.Warning);
        }
    }

    public void CloseAllToasts()
    {
        GetToastManager().CloseAll();
        _activeToasts.Clear();
        LastCloseReason = "已关闭所有 Toast";
    }

    public void ShowToastWithTimeoutReason()
    {
        var toast = new Toast(
            "2秒后自动关闭",
            NotificationType.Success,
            TimeSpan.FromSeconds(2),
            onClose: reason =>
            {
                LastCloseReason = $"{reason switch
                {
                    MessageCloseReason.Timeout => "超时自动关闭",
                    MessageCloseReason.UserAction => "用户手动关闭",
                    MessageCloseReason.Displaced => "被新 Toast 替换",
                    _ => "未知原因"
                }}";
            }
        );
        GetToastManager().Show(toast);
    }

    public void ShowToastWithUserActionReason()
    {
        var toast = new Toast(
            "请手动点击关闭按钮",
            NotificationType.Information,
            TimeSpan.Zero,
            showClose: true,
            onClose: reason =>
            {
                LastCloseReason = $"{reason switch
                {
                    MessageCloseReason.Timeout => "超时自动关闭",
                    MessageCloseReason.UserAction => "用户手动关闭",
                    MessageCloseReason.Displaced => "被新 Toast 替换",
                    _ => "未知原因"
                }}";
            }
        );
        GetToastManager().Show(toast);
    }

    public void ShowManyToastsForDisplaced()
    {
        // 快速显示多个 Toast，触发 Displaced 原因
        for (int i = 1; i <= 8; i++)
        {
            var index = i;
            var toast = new Toast(
                $"Toast #{index} - 当超过最大数量时会被替换",
                NotificationType.Information,
                TimeSpan.Zero,
                onClose: reason =>
                {
                    if (reason == MessageCloseReason.Displaced)
                    {
                        LastCloseReason = $"Toast #{index} - 被新 Toast 替换 (Displaced)";
                    }
                }
            );
            GetToastManager().Show(toast);
        }
    }

    public void ShowToastWithCloseMethod()
    {
        var toast = new Toast(
            "点击下方按钮通过 Close() 方法关闭此 Toast",
            NotificationType.Warning,
            TimeSpan.Zero,
            onClose: reason => { LastCloseReason = $"通过 Close() 方法关闭 - 原因: {reason}"; }
        );
        _activeToasts.Add(toast);
        GetToastManager().Show(toast);
    }

    public void ShowCloseReasonComparison()
    {
        // 显示三个 Toast，分别演示三种关闭原因
        var toast1 = new Toast(
            "超时关闭 (2秒) - 将显示 Timeout 原因",
            NotificationType.Information,
            TimeSpan.FromSeconds(2),
            onClose: reason => LastCloseReason = $"Toast1: {reason}"
        );
        GetToastManager().Show(toast1);

        Task.Delay(500).ContinueWith(_ =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                var toast2 = new Toast(
                    "手动关闭 - 请点击关闭按钮，将显示 UserAction 原因",
                    NotificationType.Success,
                    TimeSpan.Zero,
                    showClose: true,
                    onClose: reason => LastCloseReason = $"Toast2: {reason}"
                );
                GetToastManager().Show(toast2);
            });
        });
    }

    public void ShowAllCloseReasonTypes()
    {
        // 综合演示所有三种关闭原因
        GetToastManager().Show("演示开始：将展示三种关闭原因", NotificationType.Information);

        Task.Delay(1000).ContinueWith(_ =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                var toast1 = new Toast(
                    "1️⃣ Timeout: 3秒后自动关闭",
                    NotificationType.Success,
                    TimeSpan.FromSeconds(3),
                    onClose: reason => LastCloseReason = $"演示1: {reason} (Timeout)"
                );
                GetToastManager().Show(toast1);
            });
        });

        Task.Delay(2000).ContinueWith(_ =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                var toast2 = new Toast(
                    "2️⃣ UserAction: 请手动关闭",
                    NotificationType.Warning,
                    TimeSpan.Zero,
                    showClose: true,
                    onClose: reason => LastCloseReason = $"演示2: {reason} (UserAction)"
                );
                GetToastManager().Show(toast2);
            });
        });

        Task.Delay(3000).ContinueWith(_ =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                // 快速显示多个触发 Displaced
                for (int i = 1; i <= 6; i++)
                {
                    var index = i;
                    var toast = new Toast(
                        $"3️⃣ Displaced 测试 #{index}",
                        NotificationType.Information,
                        TimeSpan.Zero,
                        onClose: reason =>
                        {
                            if (reason == MessageCloseReason.Displaced)
                            {
                                LastCloseReason = $"演示3: Toast #{index} - {reason} (Displaced)";
                            }
                        }
                    );
                    GetToastManager().Show(toast);
                }
            });
        });
    }
}