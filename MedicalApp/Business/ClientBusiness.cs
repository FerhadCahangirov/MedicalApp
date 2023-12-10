using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using DBConnection;
using MedicalApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace MedicalApp.Business
{
    public class ClientBusiness
    {
        public static (List<Client>, int countOfClients) GetClients(int page, int size)
        {
            
            string sqlstirg = string.Format("select [ClientID], [ClientFullName], [ClientApplyDate], [ClientGender], [ClientBirthDate], [ClientPhoneNumber], [ClientComplaint] from Client order by [ClientID] asc offset {0} rows fetch next {1} rows only;", page, size);

            SqlDataReader reader = DBHelper.ExecuteReader(sqlstirg, CommandType.Text);

            List<Client> clients = new List<Client>();

            while (reader.Read())
            {
                clients.Add(new Client
                {
                    ClientID = Guid.Parse(reader.GetString(reader.GetOrdinal("ClientID"))),
                    ClientFullName = reader.GetString(reader.GetOrdinal("ClientFullName")),
                    ClientBirthDate = reader.GetString(reader.GetOrdinal("ClientBirthDate")), 
                    ClientApplyDate = reader.GetString(reader.GetOrdinal("ClientApplyDate")),
                    ClientGender = reader.GetBoolean(reader.GetOrdinal("ClientGender")),
                    ClientPhoneNumber = reader.GetString(reader.GetOrdinal("ClientPhoneNumber")),
                    ClientComplaint = reader.GetString(reader.GetOrdinal("ClientComplaint")),
                }) ;
            }
            reader.Close();
            return (clients, clients.Count);
        }

        public static string GetNumberOfClients()
        {
            const string sqlstring = "select count(*) from Client";
            object clientCountObject = DBHelper.ExecuteScalar(sqlstring, CommandType.Text);

            return clientCountObject.ToString();
        }

        public static int AddClient(Client client)
        {
            string sqlString = string.Format("insert into Client (ClientID, ClientFullName, ClientBirthDate, ClientGender, ClientPhoneNumber, ClientComplaint, ClientAdviceText, ClientHarmfullHabitats, ClientApplyDate) values ('{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}')", client.ClientID.ToString(), client.ClientFullName, client.ClientBirthDate, client.ClientGender ? 1 : 0, client.ClientPhoneNumber, client.ClientComplaint, client.ClientAdviceText, client.ClientHarmfullHabitats, client
                .ClientApplyDate);


             int executionResult = DBHelper.ExecuteNonQuery(sqlString, CommandType.Text);

            return executionResult;
        }

        public static int DeleteClient(string id)
        {
            string sqlString = string.Format("delete from Client where ClientID='{0}'", id);

            int executionResult = DBHelper.ExecuteNonQuery(sqlString, CommandType.Text);

            return executionResult;
        }

        public static int UpdateClient(Client client)
        {

            string sqlString = string.Format("UPDATE Client SET ClientFullName = '{0}', ClientBirthDate='{1}', ClientPhoneNumber='{2}', ClientGender={3}, ClientComplaint='{4}', ClientHarmfullHabitats='{5}', ClientAdviceText='{6}' WHERE ClientID='{7}';", client.ClientFullName, client.ClientBirthDate, client.ClientPhoneNumber, client.ClientGender ? 1 : 0, client.ClientComplaint, client.ClientHarmfullHabitats, client.ClientAdviceText, client.ClientID.ToString());

            int executionResult = DBHelper.ExecuteNonQuery(sqlString, CommandType.Text);

            return executionResult;
        }

        public static Client GetClient(string id)
        {
            string sqlString = string.Format("select * from Client as C where C.ClientID = '{0}';", id);

            SqlDataReader reader = DBHelper.ExecuteReader(sqlString, CommandType.Text);

            Client client = new Client();

            while (reader.Read())
            {
                client.ClientID = Guid.Parse(reader.GetString(reader.GetOrdinal("ClientID")));
                client.ClientFullName = reader.GetString(reader.GetOrdinal("ClientFullName"));
                client.ClientBirthDate = reader.GetString(reader.GetOrdinal("ClientBirthDate"));
                client.ClientApplyDate = reader.GetString(reader.GetOrdinal("ClientApplyDate"));
                client.ClientGender = reader.GetBoolean(reader.GetOrdinal("ClientGender"));
                client.ClientPhoneNumber = reader.GetString(reader.GetOrdinal("ClientPhoneNumber"));
                client.ClientComplaint = reader.GetString(reader.GetOrdinal("ClientComplaint"));
                client.ClientHarmfullHabitats = reader.GetString(reader.GetOrdinal("ClientHarmfullHabitats"));
                client.ClientAdviceText = reader.GetString(reader.GetOrdinal("ClientAdviceText"));
            }

            return client;
        }

        public static string GetClientsTotalCountByFilteredSearch(string content)
        {
            string sqlString = String.Format("SELECT COUNT(*) FROM Client as c WHERE c.ClientFullName LIKE '{0}%' OR c.ClientFullName LIKE '%{0}%';", content);
            object clientCountObject = DBHelper.ExecuteScalar(sqlString, CommandType.Text);
            return clientCountObject.ToString();

        }

        public static (List<Client>, int) GetClientByFilteredSearch(string content, int page, int size)
        {
            string sqlString = String.Format("SELECT * FROM Client as c WHERE c.ClientFullName LIKE '{0}%' OR c.ClientFullName LIKE '%{0}%' ORDER BY CASE WHEN c.ClientFullName LIKE '{0}%' THEN 1 WHEN c.ClientFullName LIKE '%{0}%' THEN 2 END offset {1} rows fetch next {2} rows only;", content, page, size);

            SqlDataReader reader = DBHelper.ExecuteReader(sqlString, CommandType.Text);

            List<Client> clients = new List<Client>();

            while (reader.Read())
            {
                clients.Add(new Client
                {
                    ClientID = Guid.Parse(reader.GetString(reader.GetOrdinal("ClientID"))),
                    ClientFullName = reader.GetString(reader.GetOrdinal("ClientFullName")),
                    ClientBirthDate = reader.GetString(reader.GetOrdinal("ClientBirthDate")),
                    ClientApplyDate = reader.GetString(reader.GetOrdinal("ClientApplyDate")),
                    ClientGender = reader.GetBoolean(reader.GetOrdinal("ClientGender")),
                    ClientPhoneNumber = reader.GetString(reader.GetOrdinal("ClientPhoneNumber")),
                    ClientComplaint = reader.GetString(reader.GetOrdinal("ClientComplaint")),
                });
            }
            reader.Close();
            return (clients, clients.Count);
        }
    }
}
