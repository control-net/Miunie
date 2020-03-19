// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Miunie.WindowsApp.Utilities
{
    public static class EnterKeyHelpers
    {
        public static readonly DependencyProperty EnterKeyCommandProperty =
            DependencyProperty.RegisterAttached(
                "EnterKeyCommand",
                typeof(ICommand),
                typeof(EnterKeyHelpers),
                new PropertyMetadata(null, OnEnterKeyCommandChanged));

        public static ICommand GetEnterKeyCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(EnterKeyCommandProperty);
        }

        public static void SetEnterKeyCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(EnterKeyCommandProperty, value);
        }

        private static void OnEnterKeyCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
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
