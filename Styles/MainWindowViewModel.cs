using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Styles
{
	class MainWindowViewModel : INotifyPropertyChanged
	{
		private List<string> _styles;
		private List<string> _subStyles;
		public List<string> Styles
		{
			get { return _styles; }
			set
			{
				_styles = value;
				OnPropertyChanged("Styles");
			}
		}
		public List<string> SubStyles
		{
			get { return _subStyles; }
			set
			{
				_subStyles = value;
				OnPropertyChanged("SubStyles");
			}
		}
		private string _info;
		public string Info
		{
			get { return _info; }
			set
			{
				_info = value;
				OnPropertyChanged("Info");
			}
		}
		private string _translation;
		public string Translation
		{
			get { return _translation; }
			set
			{
				_translation = value;
				OnPropertyChanged("Translation");
			}
		}
		public Command StylesClick { get; set; } = new Command();
		public Command SubStylesClick { get; set; } = new Command();

		public MainWindowViewModel()
		{
			StylesClick.Function = GetSubStyles;
			SubStylesClick.Function = GetInfo;
			const string Request = "select distinct style from GeneralList";
			List<string> a = GetList(Request);
			if (a != null) Styles = new List<string>(a);
		}

		private void GetInfo(string obj)
		{
			Info = GetText($"select info from StyleDesc where substyle=\'{obj}\'");
			Translator t = new Translator();
			Translation = t.Translate(Info);
		}

		private void GetSubStyles(string obj)
		{
			SubStyles = new List<string>(GetList($"select substyle from GeneralList where style=\'{obj}\'"));
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string param)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(param));
		}
		private List<string> GetList(string q)
		{
			List<string> a = new List<string>();
			using (SqlConnection db = new SqlConnection(Properties.Settings.Default.Connect))
			{
				SqlCommand query = new SqlCommand(q, db);
				SqlDataReader reader;
				try
				{
					db.Open();
					reader = query.ExecuteReader();
					SqlDataAdapter adapter = new SqlDataAdapter(query);
					DataTable table = new DataTable();
					db.Close();
					adapter.Fill(table);
					foreach (DataRow row in table.Rows)
						a.Add((string)row.ItemArray[0]);
				}
				catch
				{
					MessageBox.Show("Ошибка базы");
					return null;
				}
			}
			return a;
		}
		private string GetText(string q)
		{
			string a;
			using (SqlConnection db = new SqlConnection(Properties.Settings.Default.Connect))
			{
				SqlCommand query = new SqlCommand(q, db);
				SqlDataReader reader;
				try
				{
					db.Open();
					reader = query.ExecuteReader();
					SqlDataAdapter adapter = new SqlDataAdapter(query);
					DataTable table = new DataTable();
					db.Close();
					adapter.Fill(table);
					a = (string)table.Rows[0].ItemArray[0];
				}
				catch
				{
					MessageBox.Show("Ошибка базы");
					return null;
				}
			}
			return a;
		}
	}
}
