using Lolipop.Engine.Interface;
using Lolipop.Entity.Enum;
using Lolipop.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lolipop.Engine
{
    public abstract class LolipopEngine : IEngineActionCRUD
    {
        public abstract DbConnection Connection { get; set; }
        public abstract DbCommand Command { get; set; }

        public LolipopEngine()
        {
        }

        public virtual string[] MapProperties(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();
            List<string> list = new List<string>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "TableName")
                {
                    Console.WriteLine($"==================key name is { prop.Name }, value { prop.GetValue(obj, null).ToString() }");
                    list.Add(prop.GetValue(obj, null).ToString());
                }
            }
            return list.ToArray();
        }

        public virtual string[] MapPropertyKeys(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();
            List<string> list = new List<string>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "TableName")
                {
                    list.Add(prop.Name);
                }
            }
            return list.ToArray();
        }

        public bool UpdateOne(string table, object obj)
        {
            string[] properties = this.MapProperties(obj);
            string[] keys = this.MapPropertyKeys(obj);
            string temp = string.Empty;
            string id = string.Empty;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].Equals("_id")) id = properties[i];
                temp += $", { keys[i] } = '{ properties[i] }'";
            }
            string command = $"update { table } set { temp.Substring(2) } where _id = '{ id }'";
            return this.ExecuteNonQuery(command) != -1;
        }

        public bool UpdateOneById(string table, string id, Dictionary<string, string> args)
        {
            string temp = string.Empty;
            foreach (var key in args.Keys)
            {
                temp += $", { key } = '{ args[key] }'";
            }
            string command = $"update { table } set { temp.Substring(2) } where _id = '{ id }'";
            return this.ExecuteNonQuery(command) != -1;
        }

        public bool InsertOne(string table, object obj)
        {
            string command = $"insert into { table } ({ LolipopUtils.Join(",", this.MapPropertyKeys(obj)) }) values('{ LolipopUtils.Join("','", this.MapProperties(obj)) }')";
            return this.ExecuteNonQuery(command) != -1;
        }

        public bool DeleteOneById(string table, string id)
        {
            string command = $"delete from { table } where _id = '{ id }'";
            return this.ExecuteNonQuery(command) != -1;
        }

        public int ExecuteNonQuery(string command)
        {
            LolipopUtils.Debug("SQL Command", command);
            int result = -1;
            try
            {
                this.Connection?.Open();
                this.Command.CommandText = command;
                this.Command.Connection = this.Connection;                
                result = this.Command.ExecuteNonQuery();
            }
            catch (DbException)
            {
                throw;
            }
            finally
            {
                this.Connection?.Close();
            }
            return result;
        }

        public void ExecuteReader(string command, Action<DbDataReader> callback)
        {
            LolipopUtils.Debug("SQL Command", command);
            try
            {
                this.Connection?.Open();
                this.Command.CommandText = command;
                this.Command.Connection = this.Connection;
                callback(this.Command.ExecuteReader());
            }
            catch (DbException)
            {
                throw;
            }
            finally
            {
                this.Connection?.Close();
            }
        }

        public Dictionary<string, object> FindOneById(string table, string id)
        {
            string command = $"select * from { table } where _id = '{ id }'";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            this.ExecuteReader(command, reader => {
                // 读取一条
                reader.Read();
                if (reader.HasRows)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dic.Add(reader.GetName(i), reader[i]);
                    }
                }
            });
            return dic;
        }

        public Dictionary<string, object> FindOneByCondition(string table, Dictionary<string, string> args, QueryCondition condition = QueryCondition.And)
        {
            string temp = string.Empty;
            string op = Enum.GetName(typeof(QueryCondition), condition).ToLower();
            foreach (var key in args.Keys)
            {
                temp += $"{ op } { key } = '{ args[key] }' ";
            }
            string command = $"select * from { table } where { temp.Substring(op.Length + 1) }";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            this.ExecuteReader(command, reader => {
                // 读取一条
                reader.Read();
                if (reader.HasRows)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dic.Add(reader.GetName(i), reader[i]);
                    }
                }
            });
            return dic;
        }

        public List<Dictionary<string, object>> GetAll(string table)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            string command = $"select * from { table }";
            this.ExecuteReader(command, reader => {
                while (reader.Read())
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dic.Add(reader.GetName(i), reader[i]);
                    }
                    list.Add(dic);
                }
            });
            return list;
        }

        public List<Dictionary<string, object>> GetAllByCondition(string table, Dictionary<string, string> args, QueryCondition condition = QueryCondition.And)
        {
            string temp = string.Empty;
            string op = Enum.GetName(typeof(QueryCondition), condition).ToLower();
            foreach (var key in args.Keys)
            {
                temp += $"{ op } { key } = '{ args[key] }' ";
            }
            string command = $"select * from { table } where { temp.Substring(op.Length + 1) }";
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            this.ExecuteReader(command, reader => {
                while (reader.Read())
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dic.Add(reader.GetName(i), reader[i]);
                    }
                    list.Add(dic);
                }
            });
            return list;
        }
    }
}
