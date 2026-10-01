using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Servicios
{
    // Servicio genérico de serialización XML (System.Xml.Serialization.XmlSerializer).
    // Es transversal: no conoce el negocio, solo convierte un objeto a/desde un archivo XML.
    // El archivo contiene ÚNICAMENTE los datos del objeto serializado.
    public static class SerializadorXml_GV42
    {
        #region Métodos públicos

        // hojaEstiloCss (opcional): nombre del .css que se referencia con la instrucción de
        // procesamiento estándar <?xml-stylesheet type="text/css" href="..."?>. Es solo una
        // indicación de presentación para el visor: XmlSerializer la ignora al des-serializar.
        public static void Serializar<T>(T objeto, string rutaArchivo, string hojaEstiloCss = null)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));
            if (string.IsNullOrWhiteSpace(rutaArchivo)) throw new ArgumentException("Ruta vacía.", nameof(rutaArchivo));

            string carpeta = Path.GetDirectoryName(rutaArchivo);
            if (!string.IsNullOrEmpty(carpeta) && !Directory.Exists(carpeta))
                throw new DirectoryNotFoundException(carpeta);

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = new UTF8Encoding(false)
            };

            using (XmlWriter writer = XmlWriter.Create(rutaArchivo, settings))
            {
                writer.WriteStartDocument();
                if (!string.IsNullOrWhiteSpace(hojaEstiloCss))
                    writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/css\" href=\"" + hojaEstiloCss + "\"");

                new XmlSerializer(typeof(T)).Serialize(writer, objeto);
            }
        }

        public static T Deserializar<T>(string rutaArchivo)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo)) throw new ArgumentException("Ruta vacía.", nameof(rutaArchivo));
            if (!File.Exists(rutaArchivo)) throw new FileNotFoundException(rutaArchivo);

            var serializer = new XmlSerializer(typeof(T));
            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit };

            using (XmlReader reader = XmlReader.Create(rutaArchivo, settings))
            {
                // CanDeserialize posiciona en el elemento raíz y comprueba que sea el esperado
                // (evita cargar un XML cualquiera como si fuera de usuarios).
                if (!serializer.CanDeserialize(reader))
                    throw new InvalidDataException("El archivo XML no tiene el formato esperado.");

                return (T)serializer.Deserialize(reader);
            }
        }

        #endregion
    }
}
