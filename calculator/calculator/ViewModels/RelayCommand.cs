using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace calculator.ViewModels
{
    public class RelayCommand : ICommand
    {
        #region [필드]
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;
        #endregion

        #region [생성자]
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        // 파라미터가 없는 액션을 위한 오버로드 생성자
        public RelayCommand(Action execute, Predicate<object> canExecute = null)
            : this(o => execute(), canExecute)
        {
        }
        #endregion

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #region [public 메서드]
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        #endregion
    }
}
