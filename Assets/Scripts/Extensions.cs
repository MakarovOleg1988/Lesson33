using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace Ai.manager
{
    public static class Extensions
    {
        private static readonly string c_ConfigPath = "//Resources//Config.xml";
        private static readonly string c_ConfigPath2 = "//Resources//";
        private const string _weaponGroups = "Weapons";
        private const string _weaponGroup = "Weapon";
        private const string _movementsGroup = "Movements";
        private const string _movementGroup = "Movement";

        private static Dictionary<ActionGroup, List<ActionType>> _groups = new Dictionary<ActionGroup, List<ActionType>>();
        private static Dictionary<WeaponGroup, List<WeaponType>> _weapons = new Dictionary<WeaponGroup, List<WeaponType>>();

        //Метод запускается автоматически после загрузке сцены
        [RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Configuration()
        {
            Debug.Log("Запуск конфигурации");
            try
            {
                var _root = XDocument.Load(Application.dataPath + c_ConfigPath).Root;
                _groups = new Dictionary<ActionGroup, List<ActionType>>();
                _weapons = new Dictionary<WeaponGroup, List<WeaponType>>();

                var _array = Enum.GetValues(typeof(ActionGroup));
                var _array2 = Enum.GetValues(typeof(WeaponType));
                
                foreach (var el in _array)
                {
                    _groups.Add((ActionGroup)el, new List<ActionType>());
                    _weapons.Add((WeaponGroup)el, new List<WeaponType>());

                    foreach (var element in _root.Element(_weaponGroups).Elements(_weaponGroup))
                    {
                        var group = (ActionGroup)Enum.Parse(typeof(ActionGroup), element.Attribute("name").Value);
                        var actions = element.Attribute("Action").Value.Split(' ');
                        foreach (var action in actions)
                        {
                            _groups[group].Add((ActionType)Enum.Parse(typeof(ActionType), action));
                        }
                    }

                    foreach (var element in _root.Element(_movementsGroup).Elements(_movementGroup))
                    {
                        var group = (ActionGroup)Enum.Parse(typeof(ActionGroup), element.Attribute("State").Value);
                        var actions = element.Attribute("Action").Value.Split(' ');
                        foreach (var action in actions)
                        {
                            _groups[group].Add((ActionType)Enum.Parse(typeof(ActionType), action));
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Debug.LogError("В конфигурации ошибка");

                //Данная функция не попадает в конечный билд 
                #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
                #endif
                
                throw e;
            }

            //Добавление нового документа XML и нового элемента
            var newElement = new XElement("Group3");
            newElement.Add(new XAttribute("Name3", "Param3"));
            var _newdoc = new XDocument();
            _newdoc.Add(newElement);
            _newdoc.Save(c_ConfigPath2);
        }
    }
}
