// See https://aka.ms/new-console-template for more information

using System.Reflection;

var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
var fContent = File.ReadAllText(Path.Combine(path, "data.json"));
var json = System.Text.Json.JsonSerializer.Deserialize<Root>(fContent);
var connectionString =
	"data source=192.168.2.15;initial catalog=IFCMS-14020718;persist security info=True;user id=sa;password=123qwe!@#QWE;TrustServerCertificate=True;";
Microsoft.Data.SqlClient.SqlConnection cnn =
	new Microsoft.Data.SqlClient.SqlConnection(connectionString);

Microsoft.Data.SqlClient.SqlCommand cmd =
	new Microsoft.Data.SqlClient.SqlCommand("INSERT INTO Sms.BlackListWords(Word) VALUES(@Word)", cnn);
cnn.Open();
foreach (var item in json.word)
{
	cmd.Parameters.Clear();
	cmd.Parameters.AddWithValue("@Word", item);
	cmd.ExecuteNonQuery();
}
cnn.Close();
return;

Console.WriteLine("App Started");

int counter = 0;
var _Live = true;

while (_Live)
{
	try
	{
		using (HttpClient httpClient = new HttpClient())
		{
			// using (MultipartFormDataContent form = new MultipartFormDataContent())
			// {			
			// 	form.Add(new StringContent(@"992f5ddf6f"), "submit_ticket_token");
			// 	form.Add(new StringContent(@"/send-ticket-page/"), "_wp_http_referer");
			// 	form.Add(new StringContent("20"), "category");
			// 	form.Add(new StringContent("abcdefghigk1232"), "user_name");
			// 	form.Add(new StringContent("09120000001"), "mobile");
			// 	form.Add(new StringContent("vue-loader@next12312.com"), "email_address");
			// 	form.Add(new StringContent("abcdefghigk123"), "password");
			// 	form.Add(new StringContent("abcdefghigk123"), "password_verify");
			// 	form.Add(new StringContent("afasdfasdf"), "subject");
			// 	form.Add(new StringContent("<p>asdfasdfsdfassdf</p>"), "comment");
			// 	form.Add(new StringContent("ticketa_submit_ticket"), "action");

			httpClient.DefaultRequestHeaders.Add("Referer", "https://samarayaneh.ir/send-ticket-page/");
			using (HttpResponseMessage response = await
				httpClient.PostAsync("https://samarayaneh.ir/wp-admin/admin-ajax.php",
				new StringContent("submit_ticket_token=6c5d61a1d7&_wp_http_referer=%2Fsend-ticket-page%2F&fakeusernameremembered=&fakepasswordremembered='2iz2VjYA%7D%5D4zSy&category=23&subject=asdfasf987&comment=%3Cp%3Easdfasf987%3C%2Fp%3E&attachments_list=&action=ticketa_submit_ticket")))
			{
				response.EnsureSuccessStatusCode();
				string res = await response.Content.ReadAsStringAsync();
				Console.WriteLine("\ncount:" + counter++);
				if (counter == 10000)
					return;
			}

			//submit_ticket_token=992f5ddf6f&_wp_http_referer=%2Fsend-ticket-page%2F&fakeusernameremembered=&fakepasswordremembered=&category=20&user_name=abcdefghigk123&mobile=09120000000&email_address=vue-loader%40next123123.com&password=abcdefghigk123&password_verify=abcdefghigk123&subject=afasdfasdf&comment=%3Cp%3Easdfasdfsdfassdf%3C%2Fp%3E&attachments_list=&action=ticketa_submit_ticket


			// }


		}
	}
	catch (Exception ex)
	{

		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(ex.Message);
		Console.ForegroundColor = ConsoleColor.Gray;
	}
}

bool IsEnglishText(string text)
{
	return System.Text.RegularExpressions.Regex.IsMatch(text, @"^[\u0000-\u007F]+$");
}

public class Root
{
	public string author { get; set; }
	public string email { get; set; }
	public string update_time { get; set; }
	public List<string> word { get; set; }
}

