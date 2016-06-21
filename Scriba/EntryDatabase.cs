using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Scriba {
	public class EntryDatabase : IDisposable {
		private static SQLiteConnection _db;

		public void Initialize() {
			// make sure our database exists
			bool dbWasEmpty = false;
			if(!System.IO.File.Exists("scribus.db")) {
				dbWasEmpty = true;
				SQLiteConnection.CreateFile("scribus.db");
			}

			// and connect to it!
			_db = new SQLiteConnection("Data Source=scribus.db");
			_db.Open();

			// initialize the db?
			if(dbWasEmpty) InitializeDB();
		}

		private void InitializeDB() {
			using(SQLiteCommand command = new SQLiteCommand("create table entries(entry text, date_created datetime)", _db)) {
				command.ExecuteNonQuery();
			}
		}

		public void Add(Entry entry) {
			using(SQLiteCommand command = new SQLiteCommand("insert into entries(entry, date_created) values(@entry, @date_created)", _db)) {
				command.Parameters.Add(new SQLiteParameter("entry", entry.Note));
				command.Parameters.Add(new SQLiteParameter("date_created", entry.Time));
				command.ExecuteNonQuery();
			}
		}

		public List<Entry> GetLatest(int maxRecords) {
			List<Entry> results = new List<Entry>();
			using(SQLiteCommand command = new SQLiteCommand("select * from (select * from entries order by date_created desc limit @maxRecords) order by date_created asc", _db)) {
				command.Parameters.Add(new SQLiteParameter("maxRecords", maxRecords));
				using(SQLiteDataReader reader = command.ExecuteReader()) {
					while(reader.Read()) {
						results.Add(new Entry((string)reader["entry"], (DateTime)reader["date_created"]));
					}
				}
			}
			return results;
		}

		public void Dispose() {
			if (_db == null) return;
			_db.Close();
			_db.Dispose();
		}
	}
}
