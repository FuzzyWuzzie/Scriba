using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Scriba {
	public class EntryDatabase : IDisposable {
		private static SQLiteConnection db;

		public EntryDatabase() {
		}

		public void Initialize() {
			// make sure our database exists
			bool dbWasEmpty = false;
			if(!System.IO.File.Exists("scribus.db")) {
				dbWasEmpty = true;
				SQLiteConnection.CreateFile("scribus.db");
			}

			// and connect to it!
			db = new SQLiteConnection("Data Source=scribus.db");
			db.Open();

			// initialize the db?
			if(dbWasEmpty) InitializeDB();
		}

		private void InitializeDB() {
			using(SQLiteCommand command = new SQLiteCommand("create table entries(entry text, date_created datetime)", db)) {
				command.ExecuteNonQuery();
			}
		}

		public void Add(Entry entry) {
			using(SQLiteCommand command = new SQLiteCommand("insert into entries(entry, date_created) values(@entry, @date_created)", db)) {
				command.Parameters.Add(new SQLiteParameter("entry", entry.entry));
				command.Parameters.Add(new SQLiteParameter("date_created", entry.time));
				command.ExecuteNonQuery();
			}
		}

		public List<Entry> GetLatest(int maxRecords) {
			List<Entry> results = new List<Entry>();
			using(SQLiteCommand command = new SQLiteCommand("select * from (select * from entries order by date_created desc limit @maxRecords) order by date_created asc", db)) {
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
			if(db != null) {
				db.Close();
				db.Dispose();
			}
		}
	}
}
