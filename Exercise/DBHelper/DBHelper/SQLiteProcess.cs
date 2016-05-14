using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo
{
    public class SQLiteProcess
    {
        /// <summary>
        /// 新建一张user表
        /// </summary>
        public static void CreateTable()
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.AppendFormat(@"CREATE TABLE user(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                                                   name varchar(20),
                                                   email varchar(20)
                                                    )");
            SQLiteDBHelper sqliteDB = new SQLiteDBHelper();
            sqliteDB.CreateTable(strSQL.ToString());
        }

        /// <summary>
        /// 插入一条数据，返回插入的序号
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="email">Email地址</param>
        /// <returns>序号ID</returns>
        public static int Insert(string name, string email)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("INSERT INTO user(name,email) VALUES ('{0}','{1}')", name, email);
                SQLiteDBHelper sqliteDB = new SQLiteDBHelper();
                return Convert.ToInt32(sqliteDB.ExecuteScalar(strSQL.ToString()));
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        
        /// <summary>
        /// 根据序号ID更新指定的数据
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="email">email地址</param>
        /// <param name="ID">序号</param>
        /// <returns>更新的行数</returns>
        public static int Update(string name, string email, int ID)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("UPDATE user SET name = '{0}', email = '{1}' WHERE ID = {2}", name, email, ID);
                SQLiteDBHelper sqliteDB = new SQLiteDBHelper();
                return Convert.ToInt32(sqliteDB.ExecuteNonQuery(strSQL.ToString()));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据序号ID删除数据
        /// </summary>
        /// <param name="ID">序号</param>
        /// <returns>删除的行数</returns>
        public static int Delete(int ID)
        {

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("DELETE FROM user WHERE ID = {0}", ID);
                SQLiteDBHelper sqliteDB = new SQLiteDBHelper();
                return Convert.ToInt32(sqliteDB.ExecuteNonQuery(strSQL.ToString()));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>返回数据集</returns>
        public static List<User> Query()
        {

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("SELECT ID, name, email FROM user");
                SQLiteDBHelper sqliteDB = new SQLiteDBHelper();
                SQLiteDataReader reader = sqliteDB.ExecuteReader(strSQL.ToString());
                List<User> results = new List<User>();
                while (reader.Read())
                {
                    User user = new User();
                    user.ID = Convert.ToInt32(reader["ID"]);
                    user.name = reader["name"].ToString();
                    user.email = reader["email"].ToString();
                    results.Add(user);
                }
                return results;
            }
            catch
            {
                return null;
            }
        }
    }
}
