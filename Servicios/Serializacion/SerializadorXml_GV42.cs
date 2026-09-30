using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Servicios
{
    // Servicio genérico de serialización XML (System.Xml.Serialization.XmlSerializer).
    // Es transversal: no conoce el negocio, solo convierte un objeto a/desde un archivo XML.
    public static class SerializadorXml_GV42
    {
        public static void Serializar<T>(T objeto, string rutaArchivo)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));
            if (string.IsNullOrWhiteSpace(rutaArchivo)) throw new ArgumentException("Ruta vacía.", nameof(rutaArchivo));

            string carpeta = Path.GetDirectoryName(rutaArchivo);
            if (!string.IsNullOrEmpty(carpeta) && !Directory.Exists(carpeta))
                throw new DirectoryNotFoundException(carpeta);

            var serializer = new XmlSerializer(typeof(T));
            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = new UTF8Encoding(false)
            };

            using (var writer = XmlWriter.Create(rutaArchivo, settings))
            {
                serializer.Serialize(writer, objeto);
            }
        }

        public static T Deserializar<T>(string rutaArchivo)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo)) throw new ArgumentException("Ruta vacía.", nameof(rutaArchivo));
            if (!File.Exists(rutaArchivo)) throw new FileNotFoundException(rutaArchivo);

            var serializer = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(rutaArchivo))
            {
                // CanDeserialize comprueba que el elemento raíz sea el esperado
                // (evita cargar un XML cualquiera como si fuera de usuarios).
                if (!serializer.CanDeserialize(reader))
                    throw new InvalidDataException("El archivo XML no tiene el formato esperado.");

                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
