using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4Archivos
{
    internal class Bmp
    {
        string pathBmp;

        public Bmp(string path)
        {
            this.pathBmp = path;

        }

        public bool IsHeaderBmp(string pathBmp)
        {
            using (FileStream fs = new FileStream(pathBmp, FileMode.Open))
            {
                //porque los primeros bytes indican que es La B y M 
                string format = ((char)fs.ReadByte()).ToString() + ((char)fs.ReadByte()).ToString());
                if (format == "BM")
                {
                    return true;
                }
                return false;
            }

        }
        public bool SizeCorrect(string pathBmp)
        {

            using (BinaryReader reader = new BinaryReader(new FileStream(pathBmp, FileMode.Open)))
            {
                //me pongo en la posicion 2 y puedo leer directamente un integer en este caso 
                reader.BaseStream.Seek(2, SeekOrigin.Begin);
                if (reader.ReadInt32() == reader.BaseStream.Length)
                {
                    return true;
                }

            }
            return false;
        }
        public bool IsBmp(string pathBmp)
        {
            if (!File.Exists(pathBmp))
            {
                return false;
            }
            if (!IsHeaderBmp(pathBmp) && !SizeCorrect(pathBmp))
            {

                return false;
            }
            return true;
        }

        public object InfoBmp(string pathBmp)
        {

            using (BinaryReader reader = new BinaryReader(new FileStream(pathBmp, FileMode.Open)))
            {

            }
        }
    }
}
