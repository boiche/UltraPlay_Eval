using System.Xml.Linq;
using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation.Utils
{
    public static class Extensions
    {
        public static List<T> GetLayer<T>(this XDocument document) where T : class
        {
            return typeof(T) switch
            {
                Type type when type == typeof(Sport) => document.Root.Elements(nameof(Sport)).Select(x => x.GetObjectFromElement<Sport>() as T).ToList(),
                Type type when type == typeof(Event) => document.Root.Descendants(nameof(Event)).ToList().Select(x => x.GetObjectFromElement<Event>() as T).ToList(),
                Type type when type == typeof(Match) => document.Root.Descendants(nameof(Match)).ToList().Select(x => x.GetObjectFromElement<Match>() as T).ToList(),
                Type type when type == typeof(Bet) => document.Root.Descendants(nameof(Bet)).ToList().Select(x => x.GetObjectFromElement<Bet>() as T).ToList(),
                Type type when type == typeof(Odd) => document.Root.Descendants(nameof(Odd)).ToList().Select(x => x.GetObjectFromElement<Odd>() as T).ToList(),
                _ => throw new NotSupportedException($"Type {typeof(T).Name} is not supported."),
            };
        }

        public static T? GetObjectFromElement<T>(this XElement element)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            var attribute = element.FirstAttribute;
            do
            {
                var currentProperty = typeof(T).GetProperty(attribute.Name.LocalName);
                currentProperty.SetValue(result, Convert.ChangeType(attribute.Value, Nullable.GetUnderlyingType(currentProperty.PropertyType) ?? currentProperty.PropertyType, null));
                attribute = attribute.NextAttribute;
            } while (attribute != null);

            var foreignKey = typeof(T).GetProperty($"{element.Parent.Name}ID");
            foreignKey?.SetValue(result, Convert.ChangeType(element.Parent.Attribute("ID").Value, Nullable.GetUnderlyingType(foreignKey.PropertyType) ?? foreignKey.PropertyType, null));

            return result;
        }
    }
}
