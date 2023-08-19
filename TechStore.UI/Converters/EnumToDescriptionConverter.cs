using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace TechStore.UI.Converters;

/// <summary>
/// Класс для преобразования перечисления в строку для пользователя
/// </summary>
public sealed class EnumToDescriptionConverter : MarkupExtension, IValueConverter
{
    private string GetEnumDescription(Enum enumObj)
    {
        var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

        var attribArray = fieldInfo.GetCustomAttributes(false);

        if (attribArray.Length == 0)
        {
            return enumObj.ToString();
        }
        else
        {
            var attrib = attribArray[0] as DescriptionAttribute;
            return attrib.Description;
        }
    }

    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Enum myEnum = (Enum)value;
        string description = GetEnumDescription(myEnum);
        return description;
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return string.Empty;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
        => this;
}
