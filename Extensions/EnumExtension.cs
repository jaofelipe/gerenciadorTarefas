using System.ComponentModel;
using System.Reflection;

namespace GerenciadorTarefas.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                  .FirstOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
