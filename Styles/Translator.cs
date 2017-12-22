using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Styles
{
	class Translator
	{
		public string Translate(string s)
		{
			string line = string.Empty;
			if (!string.IsNullOrEmpty(s))
			{
				WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?key=trnsl.1.1.20171219T213603Z.ef4bd521bcd2b42a.ea1c44c9f29fa6e2c819afc7a3549c1be409a494&text=" + WebUtility.UrlEncode(s)
					+ "&lang=ru-en");

				WebResponse response = request.GetResponse();
				using (StreamReader stream = new StreamReader(response.GetResponseStream()))
				{
					line = stream.ReadToEnd();
				}
			}

			return line;
		}
	}
}
