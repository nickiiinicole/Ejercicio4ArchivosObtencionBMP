using System;
using System.IO;
using System.Text;

namespace Ejercicio4Archivos
{
    internal class Bmp
    {
        string pathBmp;

        public Bmp(string path)
        {
            this.pathBmp = path;
        }

        // Método para verificar si el archivo es BMP
        public bool IsHeaderBmp()
        {
            if (!File.Exists(pathBmp))
            {
                return false;
            }

            try
            {
                using (FileStream fs = new FileStream(pathBmp, FileMode.Open, FileAccess.Read))
                {
                    // Leer los primeros dos bytes para verificar la cabecera "BM"
                    byte[] header = new byte[2];
                    fs.Read(header, 0, 2);
                    string format = Encoding.ASCII.GetString(header);
                    return format == "BM";
                }
            }
            catch (Exception e) when (e is ArgumentException || e is ArgumentNullException)
            {
                Console.WriteLine($"{e.Message}");
            }
            return false;
        }

        public bool SizeCorrect()
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(pathBmp, FileMode.Open)))
                {
                    reader.BaseStream.Seek(2, SeekOrigin.Begin); // Posicionarse en el byte 2
                    int fileSize = reader.ReadInt32(); // Leer el tamaño del archivo
                    return fileSize == reader.BaseStream.Length;
                }
            }
            catch (Exception e) when (e is IOException || e is ArgumentException)
            {
                Console.WriteLine($"{e.Message}");
            }
            return false;
        }

        public bool IsBmp()
        {
            if (!File.Exists(pathBmp))
            {
                return false;
            }

            return IsHeaderBmp() && SizeCorrect();
        }

        public object InfoBmp()
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(pathBmp, FileMode.Open)))
                {
                    reader.BaseStream.Seek(14, SeekOrigin.Begin);

                    int width = reader.ReadInt32();
                    int height = reader.ReadInt32();
                    short planes = reader.ReadInt16();
                    short bpp = reader.ReadInt16();
                    int compression = reader.ReadInt32();
                    reader.BaseStream.Seek(38, SeekOrigin.Begin);
                    int resolutionX = reader.ReadInt32();
                    int resolutionY = reader.ReadInt32();


                    return new
                    {
                        Width = width,
                        Height = height,
                        IsCompressed = compression == 0,
                        BitsPerPixel = bpp,
                        HorizontalResolution = resolutionX,
                        VerticalResolution = resolutionY
                    };
                }
            }
            catch (Exception e) when (e is IOException || e is ArgumentException)
            {
                Console.WriteLine($"Error al leer la información del BMP: {e.Message}");
            }

            return null;
        }
    }
}
