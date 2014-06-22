/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="EnumExtentions.cs">
// Copyright (c) 2014
// </copyright>
//
// <author>Betting Pascal, Schneider Mathias, Schlemelch Manuel</author>
// <date>02-06-2014</date>
//
// <professor>Prof. Dr. Josef Poesl</professor>
// <studyCourse>Angewandte Informatik</studyCourse>
// <branchOfStudy>Industrieinformatik</branchOfStudy>
// <subject>Oberflaechenprogrammierung</subject>
//
// <summary></summary>
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

namespace MathArts
{
    /// <summary>
    /// Extention for enums - using description attribute for converting enum value into friendly string
    /// SOURCE:
    /// http://stackoverflow.com/questions/1415140/can-my-enums-have-friendly-names - user343550 21.06.2014
    /// </summary>
    public static class EnumExtensions
    {
        //To avoid collisions, every Enum type has its own hash table
        private static readonly Dictionary<Type, Dictionary<object, string>> enumToStringDictionary = new Dictionary<Type, Dictionary<object, string>>();
        private static readonly Dictionary<Type, Dictionary<string, object>> stringToEnumDictionary = new Dictionary<Type, Dictionary<string, object>>();

        /// <summary>
        /// create dictionarys of all enums [enum type,description/value] (better implementation would have been a convertion on demand)
        /// </summary>
        static EnumExtensions()
        {
            //let's collect the enums we care about
            List<Type> enumTypeList = new List<Type>();

            //probe this assembly for all enums
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Type[] exportedTypes = assembly.GetExportedTypes();

            foreach (Type type in exportedTypes)
            {
                if (type.IsEnum)
                    enumTypeList.Add(type);
            }

            //for each enum in our list, populate the appropriate dictionaries
            foreach (Type type in enumTypeList)
            {
                //add dictionaries for this type
                EnumExtensions.enumToStringDictionary.Add(type, new Dictionary<object, string>());
                EnumExtensions.stringToEnumDictionary.Add(type, new Dictionary<string, object>());

                Array values = Enum.GetValues(type);

                //its ok to manipulate 'value' as object, since when we convert we're given the type to cast to
                foreach (object value in values)
                {
                    System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

                    //check for an attribute 
                    System.ComponentModel.DescriptionAttribute attribute =
                           Attribute.GetCustomAttribute(fieldInfo,
                             typeof(System.ComponentModel.DescriptionAttribute)) as System.ComponentModel.DescriptionAttribute;

                    //populate our dictionaries
                    if (attribute != null)
                    {
                        EnumExtensions.enumToStringDictionary[type].Add(value, attribute.Description);
                        EnumExtensions.stringToEnumDictionary[type].Add(attribute.Description, value);
                    }
                    else
                    {
                        EnumExtensions.enumToStringDictionary[type].Add(value, value.ToString());
                        EnumExtensions.stringToEnumDictionary[type].Add(value.ToString(), value);
                    }
                }
            }
        }

        /// <summary>
        /// get all description/values of a enum in a list
        /// SOURCE: http://codereview.stackexchange.com/questions/12173/how-can-an-enumeration-with-descriptions-be-cast-into-a-dictionary/12178#12178
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<string> GetDescriptionToList(this Type EnumerationType)
        {
            Dictionary<int, string> enumDict = Enum.GetValues(EnumerationType)
                .Cast<object>()
                .ToDictionary(k => (int)k, v => ((Enum)v).GetDescription());

            List<string> enumDescriptions = new List<string>();
            foreach (KeyValuePair<int, string> _description in enumDict) enumDescriptions.Add(_description.Value);
            return enumDescriptions;
        }

        /// <summary>
        /// get description of a single enum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string aString = EnumExtensions.enumToStringDictionary[type][value];
            return aString;
        }

        /// <summary>
        /// convert a description to its enum type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            Type type = typeof(T);
            try
            {
                return (T)EnumExtensions.stringToEnumDictionary[type][value];
            }
            catch (ArgumentException)
            {
                return default(T);
            }
        }
    }
}
