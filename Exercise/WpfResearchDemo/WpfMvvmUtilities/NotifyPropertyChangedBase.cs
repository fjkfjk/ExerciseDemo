using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvmUtilities
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> expr)
        {
            var bodyExpr = expr.Body as System.Linq.Expressions.MemberExpression;
            if (bodyExpr == null)
            {
                throw new ArgumentException("Expression must be a MemberExpression!", "expr");
            }
            var propInfo = bodyExpr.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException("Expression must be a PropertyExpression!", "expr");
            }

            this.OnPropertyChanged(propInfo.Name);
        }
    }
}
