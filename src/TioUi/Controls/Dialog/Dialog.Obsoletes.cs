using Avalonia.Controls;
using TioUi.Common;

namespace TioUi.Controls;

public static partial class Dialog
{
    /// <summary>
    ///     Show a Modal Dialog Window with standard style.
    /// </summary>
    [Obsolete("This will be removed in TioUi 2.0 lifecycle. Please use Dialog.ShowStandardAsync instead.")]
    public static Task<DialogResult> ShowModal<TView, TViewModel>(TViewModel vm, Window? owner = null,
        DialogOptions? options = null)
        where TView : Control, new()
    {
        return ShowStandardAsync<TView, TViewModel>(vm, owner, options);
    }

    /// <summary>
    ///     Show a Modal Dialog Window with standard style.
    /// </summary>
    [Obsolete("This will be removed in TioUi 2.0 lifecycle. Please use Dialog.ShowStandardAsync instead.")]
    public static Task<DialogResult> ShowModal(Control view, object? vm, Window? owner = null,
        DialogOptions? options = null)
    {
        return ShowStandardAsync(view, vm, owner, options);
    }

    /// <summary>
    ///     Show a Modal Dialog Window with all content fully customized.
    /// </summary>
    [Obsolete("This will be removed in TioUi 2.0 lifecycle. Please use Dialog.ShowCustomAsync instead.")]
    public static Task<TResult?> ShowCustomModal<TView, TViewModel, TResult>(TViewModel vm, Window? owner = null,
        DialogOptions? options = null)
        where TView : Control, new()
    {
        return ShowCustomAsync<TView, TViewModel, TResult>(vm, owner, options);
    }

    /// <summary>
    ///     Show a Modal Dialog Window with all content fully customized.
    /// </summary>
    [Obsolete("This will be removed in TioUi 2.0 lifecycle. Please use Dialog.ShowCustomAsync instead.")]
    public static Task<TResult?> ShowCustomModal<TResult>(Control view, object? vm, Window? owner = null,
        DialogOptions? options = null)
    {
        return ShowCustomAsync<TResult>(view, vm, owner, options);
    }
}
