using System.Xml.Linq;
using System.Security.Cryptography;
using T1Balance.Core;
using System.IO;

namespace T1Balance.Services.XmlServices
{
    public class XmlService : IXmlService
    {
        private readonly XDocument _userDoc;
        private readonly XDocument _settingsDoc;

        public XmlService()
        {
            if (File.Exists("UserInfo.xml"))
                _userDoc = XDocument.Load("UserInfo.xml");
            else
                _userDoc = new XDocument();

            if (File.Exists("appsettings.xml"))
                _settingsDoc = XDocument.Load("appsettings.xml");
            else
                _settingsDoc = new XDocument();


            XElement userInfo = _userDoc.Element("userinfo");
            if (userInfo == null)
            {
                userInfo = new XElement("userinfo");
                _userDoc.Add(userInfo);
            }
            XElement token = userInfo.Element("Token");
            if (token == null)
            {
                token = new XElement("Token");
                userInfo.Add(token);
            }
            XElement login = userInfo.Element("LastLogin");
            if (login == null)
            {
                login = new XElement("LastLogin");
                userInfo.Add(login);
            }

            XElement settingsInfo = _settingsDoc.Element("settings");
            if (settingsInfo == null)
            {
                settingsInfo = new XElement("settings");
                _settingsDoc.Add(settingsInfo);
            }
            XElement isRemember = settingsInfo.Element("IsRemember");
            if (isRemember == null)
            {
                isRemember = new XElement("IsRemember");
                settingsInfo.Add(isRemember);
            }


            _userDoc.Save("UserInfo.xml");
            _settingsDoc.Save("appsettings.xml");
        }

        public string Token
        {
            get
            {
                XElement userInfo = _userDoc.Element("userinfo");
                string token = userInfo.Element("Token").Value;

                if (!string.IsNullOrEmpty(token))
                {
                    byte[] encodedBytes = token.InByteArray();
                    byte[] plaintextBytes = ProtectedData.Unprotect(encodedBytes, null, DataProtectionScope.CurrentUser);

                    return plaintextBytes.ToHexString();
                }
                else
                    return token;
            }
            set
            {
                XElement userInfo = _userDoc.Element("userinfo");
                XElement token = userInfo.Element("Token");

                if (!string.IsNullOrEmpty(value))
                {
                    byte[] plaintextBytes = value.ToByteArray();
                    byte[] encodedBytes = ProtectedData.Protect(plaintextBytes, null, DataProtectionScope.CurrentUser);

                    token.Value = encodedBytes.InString();
                }
                else
                    token.Value = "";

                _userDoc.Save("UserInfo.xml");
            }
        }

        public string LastLogin
        {
            get
            {
                XElement userInfo = _userDoc.Element("userinfo");
                return userInfo.Element("LastLogin").Value;
            }
            set
            {
                XElement userInfo = _userDoc.Element("userinfo");
                XElement login = userInfo.Element("LastLogin");
                login.Value = value;
                _userDoc.Save("UserInfo.xml");
            }
        }

        public bool IsRemember
        {
            get
            {
                XElement settingsInfo = _settingsDoc.Element("settings");
                bool succes = false;
                bool.TryParse(settingsInfo.Element("IsRemember").Value, out succes);
                return succes;
            }
            set
            {
                XElement settingsInfo = _settingsDoc.Element("settings");
                XElement isRemember = settingsInfo.Element("IsRemember");
                isRemember.Value = value.ToString();
                _settingsDoc.Save("appsettings.xml");
            }
        }
    }
}
