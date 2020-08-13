using Lolipop.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Lolipop.Engine.Interface
{
    public interface IEngineActionCRUD
    {
        /// <summary>
        /// 根据id更新数据库表记录
        /// </summary>
        /// <param name="table">数据库表名</param>
        /// <param name="id">唯一键</param>
        /// <param name="args">要更新的参数</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool UpdateOneById(string table, string id, Dictionary<string, string> args);

        /// <summary>
        /// 使用模型更新数据库字段
        /// </summary>
        /// <param name="table">数据库表名</param>
        /// <param name="obj">要更新的数据模型</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool UpdateOne(string table, object obj);

        /// <summary>
        /// 向数据库中插入一条记录
        /// </summary>
        /// <param name="table">数据库表名</param>
        /// <param name="obj">要插入的数据库记录的模型</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool InsertOne(string table, object obj);

        /// <summary>
        /// 根据id删除数据库表中的记录
        /// </summary>
        /// <param name="table">数据库表名</param>
        /// <param name="id">唯一键</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool DeleteOneById(string table, string id);

        /// <summary>
        /// 根据id查找数据库表中的记录
        /// </summary>
        /// <param name="table">数据库表名</param>
        /// <param name="id">唯一键</param>
        /// <returns>成功返回对象实体键值对，失败抛出错误</returns>
        Dictionary<string, object> FindOneById(string table, string id);

        /// <summary>
        /// 根据条件键值对查找数据库表中的记录
        /// </summary>
        /// <param name="table">数据库表名</param>
        /// <param name="args">条件键值对</param>
        /// <param name="condition">用于连接条件的参数 And 或者 Or</param>
        /// <returns>成功返回对象实体键值对，失败抛出错误</returns>
        Dictionary<string, object> FindOneByCondition(string table, Dictionary<string, string> args, QueryCondition condition);

        /// <summary>
        /// 获取表中所有实体
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>所有实体键值对List</returns>
        List<Dictionary<string, object>> GetAll(string table);

        /// <summary>
        /// 获取表中所有实体
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="args">条件键值对</param>
        /// <param name="condition">用于连接条件的参数 And 或者 Or</param>
        /// <returns></returns>
        List<Dictionary<string, object>> GetAllByCondition(string table, Dictionary<string, string> args, QueryCondition condition);

        /// <summary>
        /// 执行数据库查询，返回一个DataReader
        /// </summary>
        /// <param name="command">要执行的SQL查询命令</param>
        /// <param name="callback">执行reader的委托</param>
        /// <returns>成功返回DbDataReader对象，失败抛出错误</returns>
        void ExecuteReader(string command, Action<DbDataReader> callback);

        /// <summary>
        /// 执行数据库查询，返回收影响的行数
        /// </summary>
        /// <param name="command">要执行的SQL查询命令</param>
        /// <returns>成功返回受影响的行数，失败返回-1</returns>
        int ExecuteNonQuery(string command);
    }
}
