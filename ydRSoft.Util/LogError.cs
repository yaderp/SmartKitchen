using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Util
{
    public class LogError
    {
        public async static Task SaveLog(string Texto)
        {
            try
            {
                string fileName = "log.txt";
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(baseDirectory, fileName);
                

                using (StreamWriter streamWriter = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    string linea = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " | " + Texto + "\n";
                    await streamWriter.WriteAsync(linea);
                }
            }
            catch
            {

            }
        }

        public  static void SaveProd(string Texto)
        {
            try
            {
                string fileName = "logProd.txt";
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(baseDirectory, fileName);


                using (StreamWriter streamWriter = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    string linea = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " | " + Texto + "\n";
                    streamWriter.Write(linea);
                }
            }
            catch
            {

            }
        }
    }
}
