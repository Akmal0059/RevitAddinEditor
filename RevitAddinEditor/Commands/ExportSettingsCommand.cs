using CustomRevitControls;
using RevitAddinEditor.Models;
using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RevitAddinEditor.Commands
{
    public class ExportSettingsCommand : CommandBase
    {
        private EditorViewModel viewModel;

        public ExportSettingsCommand(EditorViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            var panel = new RevitPanel();
            panel.Name = "test";
            panel.Id = "INDP_TST";
            panel.Items = new List<RevitControl>(viewModel.Controls);

            //Serialize, but ignore companyid
            var overrides = new XmlAttributeOverrides();
            var ignoreAttributes = new XmlAttributes();
            ignoreAttributes.XmlIgnore = true;

            var nonIgnoreAttributes = new XmlAttributes();
            nonIgnoreAttributes.XmlIgnore = false;
            var panelProps = panel.GetType().GetProperties();
            //foreach (var prop in panelProps)
            //{
            //    if (prop.Name == "Name" || prop.Name == "Id" || prop.Name == "Items")
            //        overrides.Add(typeof(RevitPanel), prop.Name, nonIgnoreAttributes);
            //    else
            //    {
            //        overrides.Add(typeof(RevitPanel), prop.Name, ignoreAttributes);
            //    }
            //}
            foreach (var item in panel.Items)
            {
                var ItemProps = item.GetType().GetProperties();
                foreach (var itemProp in ItemProps)
                {
                    try
                    {
                        if (itemProp.Name == "Content" || itemProp.Name == "Items")
                            overrides.Add(item.GetType(), itemProp.Name, nonIgnoreAttributes);
                        else
                        {
                            overrides.Add(item.GetType(), itemProp.Name, ignoreAttributes);
                        }

                        //overrides.Add(item.GetType(), itemProp.Name, ignoreAttributes);
                    }
                    catch { }
                }
            }

            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(RevitPanel), overrides);

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(@"C:\Users\user110\Documents\test\Editor\controls.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, panel);
            }
        }
    }
    class CustomXml : IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return new XmlSchema();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }

}
