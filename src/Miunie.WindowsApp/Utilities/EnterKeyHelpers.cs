using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Miunie.WindowsApp.Utilities
{
    public static class EnterKeyHelpers
    {
        public static ICommand GetEnterKeyCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(EnterKeyCommandProperty);
        }

        public static void SetEnterKeyCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(EnterKeyCommandProperty, value);
        }

        public static readonly DependencyProperty EnterKeyCommandProperty =
            DependencyProperty.RegisterAttached(
                "EnterKeyCommand",
                typeof(ICommand),
                typeof(EnterKeyHelpers),
                new PropertyMetadata(null, OnEnterKeyCommandChanged));

        static void OnEnterKeyCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ICommand command = (ICommand)e.NewValue;
            FrameworkElement fe = (FrameworkElement)target;
            Control control = (Control)target;
            control.KeyUp += (s, args) =>
            {
                if (args.Key == Windows.System.VirtualKey.Enter)
                {
                    if (control is TextBox textbox)
                    {
                        BindingExpression b = control.GetBindingExpression(TextBox.TextProperty);
                        if (b != null)
                        {
                            b.UpdateSource();
                        }
                        command.Execute(textbox.Text);
                    }

                    if (control is PasswordBox password)
                    {
                        BindingExpression b = control.GetBindingExpression(PasswordBox.PasswordProperty);
                        if (b != null)
                        {
                            b.UpdateSource();
                        }
                        command.Execute(password.Password);
                    }

                }
            };
        }
    }
}
