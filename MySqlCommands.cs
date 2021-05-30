/*
 * Author: Satyam Khanna
 * Email: satyamkhanna66@gmail.com
 */

//Include Section Starts
using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Windows;
//Include Section Ends

namespace MySqlConnector
{
	public class MySqlLibrary
	{
		//Class Field Section Starts
		protected string databaseLocalhost = "";
		protected string databaseUsername = "";
		protected string databasePassword = "";
		protected string databaseName = "";
		private StringBuilder connectionDetails = new StringBuilder();
		private StringBuilder returnedData = new StringBuilder();
        //Class Field Section Ends


        //Class Constructor Section Starts
        public MySqlLibrary(string dbhost, string dbusername, string dbpassword, string dbname)
		{
			databaseLocalhost = dbhost;
			databaseUsername = dbusername;
			databasePassword = dbpassword;
			databaseName = dbname;
		}
		//Class Constructor Section Ends


		//Class Member Section Starts
		protected internal bool OpenConnection(MySqlConnection mySqlConnection)
		{
			try
			{
				mySqlConnection.Open();
				return true;
			}
			catch (System.InvalidOperationException)
			{
				return false;
			}
			catch (MySqlException)
			{
				return false;
			}
		}
		protected internal void CloseConnection(MySqlConnection mySqlConnection)
		{
			mySqlConnection.Close();
		}
		protected internal string ExecuteSqlQuery(StringBuilder query)
		{
			connectionDetails.Append("Server=");
			connectionDetails.Append(databaseLocalhost);
			connectionDetails.Append(";Uid=");
			connectionDetails.Append(databaseUsername);
			connectionDetails.Append(";Pwd=");
			connectionDetails.Append(databasePassword);
			connectionDetails.Append(";database=");
			connectionDetails.Append(databaseName);

			try
			{
				using (var connection = new MySqlConnection(connectionDetails.ToString()))
				{
					connection.Open();
					using (var cmd = connection.CreateCommand())
					{
						cmd.CommandText = query.ToString();
						using (var reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								var rowCount = reader.FieldCount;
								for (int i = 0; i < rowCount; i++)
								{
									if (reader[i] is null)
										returnedData.AppendLine("null");
									else
										returnedData.AppendLine(reader[i].ToString());
								}

							}
						}
					}
				}
				return returnedData.ToString();
			}
			catch (InvalidOperationException)
			{
				return null;
			}
            catch (MySqlException)
            {
				return null;
            }
		}
		//Class Member Section End
	}
}
