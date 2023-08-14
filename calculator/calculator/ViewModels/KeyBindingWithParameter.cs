using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace calculator.ViewModels
{
    public class KeyBindingWithParameter : KeyBinding
    {
        public static readonly DependencyProperty CustomCommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(KeyBindingWithParameter));

        public object CustomCommandParameter
        {
            get => GetValue(CustomCommandParameterProperty);
            set => SetValue(CustomCommandParameterProperty, value);
        }

    }

}
