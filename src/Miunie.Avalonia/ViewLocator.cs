using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Miunie.Avalonia.ViewModels;

namespace Miunie.Avalonia
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            var name = data.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type is null)
                return new TextBlock { Text = "Not Found: " + name };
            
            return (Control)Activator.CreateInstance(type);
        }

        public bool Match(object data)
            => data is ViewModelBase;
    }
}
