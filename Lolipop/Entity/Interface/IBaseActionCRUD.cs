using Lolipop.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lolipop.Entity.Interface
{
    interface IBaseActionCRUD<T>
    {
        /// <summary>
        /// 向数据库表中插入当前对象
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        bool Save();

        /// <summary>
        /// 使用唯一id查找数据库表
        /// </summary>
        /// <param name="_id">Lolipop唯一id</param>
        /// <returns>返回改主键值对应数据表中的实体数据</returns>
        T Find(Guid _id);

        /// <summary>
        /// 使用条件查询数据库，可以传多个键值对，默认使用and连接查询关键字
        /// 对应SQL：
        ///     Select * from xx where xx=xx and xx=xx and xx=xx...
        /// </summary>
        /// <param name="condition">查询条件, And 或者 Or</param>
        /// <param name="args">用来查询的条件的键值对数组</param>
        /// <returns>返回符合条件的对象</returns>
        T Find(Dictionary<string, string> args, QueryCondition condition = QueryCondition.And);

        /// <summary>
        /// 从数据库表中删除当前项
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        bool Delete();

        /// <summary>
        /// 使用唯一id检索并删除表中实体
        /// </summary>
        /// <param name="_id">Lolipop唯一id</param>
        /// <returns>成功返回true,失败返回false</returns>
        bool Delete(Guid _id);

        /// <summary>
        /// 更新数据库中当前项目
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        bool Update();

        /// <summary>
        /// 获取数据库表中所有数据实体
        /// </summary>
        /// <returns>数据库中所有实体列表</returns>
        List<T> ToList();

        /// <summary>
        /// 获取数据库表中所有数据实体
        /// </summary>
        /// <param name="args">用来查询的条件的键值对数组</param>
        /// <param name="condition">查询条件, And 或者 Or</param>
        /// <returns>数据库中所有实体列表</returns>
        List<T> ToList(Dictionary<string, string> args, QueryCondition condition = QueryCondition.And);
    }
}
