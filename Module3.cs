using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace IR_milestone
{
	class Module3
	{
		string connectionString = "Data Source=DONIA\\SQLEXPRESS;Initial Catalog=College;Integrated Security=True";

		public Dictionary<int, List<int>> listPositionsA = new Dictionary<int, List<int>>();
		public void exactSearch(string[] wordsList)
		{
			//assume that searchQuery = "search music"

			//select a.Position as A
			//from(select d.Term, p.ID, p.TermID, p.DocNum, p.Freq, p.Position from[Dictionary] d,[Posting] p where d.ID = p.TermID and d.Term = 'search')a , 
			// (select d.Term , p.ID ,p.TermID,p.DocNum,p.Freq,p.Position from[Dictionary] d ,[Posting] p where d.ID = p.TermID and d.Term='music') b
			//where a.DocNum=b.DocNum
			
			foreach (var b in wordsList)
			{
				
		   
			//    List<> positionA = new List;
				SqlConnection connection = new SqlConnection(connectionString);
				string command = "select a.DocNum, a.Position AS A , b.Position AS B from(select d.Term, p.ID, p.TermID, p.DocNum, p.Freq, p.Position from[Dictionary] d,[Posting] p where d.ID = p.TermID and d.Term = 'search')a ,(select d.Term , p.ID ,p.TermID,p.DocNum,p.Freq,p.Position from[Dictionary] d ,[Posting] p where d.ID = p.TermID and d.Term='music') b where a.DocNum=b.DocNum";
				SqlCommand cmd = new SqlCommand(command, connection);
				connection.Open();

				string TempPositionA = "";
				string[] TempPositionAString = new string[1000];
				List<int> PositionAList = new List<int>();

				string TempPositionB = "";
				string[] TempPositionBString = new string[1000];
				List<int> PositionBList = new List<int>();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						int documentNumber = Int32.Parse(reader["DocNum"].ToString());

						TempPositionA = reader["A"].ToString();
						TempPositionB = reader["B"].ToString();

						TempPositionAString = TempPositionA.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
						TempPositionBString = TempPositionB.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

						for (int i = 0; i < TempPositionAString.Length; i++)
						{ 
							PositionAList.Add(Int32.Parse(TempPositionAString[i]));
							PositionBList.Add(Int32.Parse(TempPositionBString[i]));
						}

					  //  EditDistance(PositionAList, PositionBList);


					}
					int z=2;
				}
			}



		}

	}
}
