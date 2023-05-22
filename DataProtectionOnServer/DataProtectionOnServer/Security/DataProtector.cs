using System.Security.Cryptography;
using System.Text;

namespace DataProtectionOnServer.Security
{
    public class DataProtector
    {
        /*
         * Uygulamanın kullandığı pek çok kritik veri içeren dosya olabilir. Bu dosyalara; ancak uygulama erşilebilmelidir.
         * Verileri -> dosyaya yaz.
         * Dosyayı -> encrypt et
         */
        private string path;
        private byte[] entropy;


        public DataProtector(string path)
        {
            this.path = path;
            entropy = new byte[16];
            entropy = RandomNumberGenerator.GetBytes(16);
            this.path += "\\EncryptedData.halk";

        }

        public int EncryptData(string criticalData)
        {
            //Şifrele...
            //1. criticalData byte[]'a dönüştür.
            //2. Bu byte[] arrayi ProtectedData aracılığı ile şifrele
            //3. Crypto'lanmış veriyi dosyaya yaz.
            var encoded = Encoding.UTF8.GetBytes(criticalData);
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            int length = encryptDataToFile(encoded, entropy, DataProtectionScope.CurrentUser, fileStream);
            fileStream.Close();
            return length;
        }

        public string DecryptData(int length)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] decryptData = decryptDataFromFile(fileStream, entropy, DataProtectionScope.CurrentUser, length);
            fileStream.Close();
            return Encoding.UTF8.GetString(decryptData);
        }

        private byte[] decryptDataFromFile(FileStream fileStream, byte[] entropy, DataProtectionScope currentUser, int length)
        {
            byte[] inputBuffer = new byte[length];
            byte[] outputBuffer;

            if (fileStream.CanRead)
            {
                fileStream.Read(inputBuffer, 0, inputBuffer.Length);
                outputBuffer = ProtectedData.Unprotect(inputBuffer, entropy, currentUser);
            }
            else
            {
                throw new IOException("Stream okuanadı!");
            }

            return outputBuffer;
        }

        private int encryptDataToFile(byte[] encoded, byte[] entropy, DataProtectionScope currentUser, FileStream fileStream)
        {
            var encryptedData = ProtectedData.Protect(encoded, entropy, currentUser);
            int outLength = 0;
            if (fileStream.CanWrite && encryptedData != null)
            {
                fileStream.Write(encryptedData, 0, encryptedData.Length);
                outLength += encryptedData.Length;
            }
            return outLength;
        }
    }
}
