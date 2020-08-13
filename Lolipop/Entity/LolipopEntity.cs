using Lolipop.Engine;
using Lolipop.Entity.Enum;
using Lolipop.Entity.Interface;
using Lolipop.Fectory;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lolipop.Entity
{
    public class LolipopEntity<T>: IBaseActionCRUD<T>
    {
        // 全局Engine对象
        public static LolipopEngine Engine { get; set; } = LolipopEngineFectory.CreateNewEngine(LolipopConfiguration.Engine);

        // 获取本类的调用堆栈
        private MethodBase Context = new StackTrace().GetFrame(1).GetMethod();

        /// <summary>
        /// Lolipop内部访问数据库实体的唯一ID,不要改动！不要改动！不要改动！
        /// </summary>
        public Guid _id { get; set; } = Guid.NewGuid();

        // 表名
        public string TableName { get; set; }

        public LolipopEntity()
        {
            // 获取当前表名
            string _className = Context.ReflectedType.Name;
            this.TableName = _className.Substring(0, _className.Length - 5);
        }

        public bool Delete()
        {
            if (Engine == null)
            {
                throw new NullReferenceException("Engine is null.");
            }

            return Engine.DeleteOneById(this.TableName, this._id.ToString());
        }

        public bool Save()
        {
            if (Engine == null)
            {
                throw new NullReferenceException("Engine is null.");
            }

            return Engine.InsertOne(this.TableName, this);
        }

        public bool Update()
        {
            if (Engine == null)
            {
                throw new NullReferenceException("Engine is null.");
            }

            return Engine.UpdateOne(this.TableName, this);
        }

        public bool Delete(Guid id)
        {
            if (Engine == null)
            {
                throw new NullReferenceException("Engine is null.");
            }

            return Engine.DeleteOneById(this.TableName, id.ToString());
        }

        public T Find(Guid id)
        {
            if (Engine == null)
            {
                throw new NullReferenceException("Engine is null.");
            }

            // 实例化泛型对象
            return this.MappingObject(Engine.FindOneById(this.TableName, id.ToString()));
        }

        public T Find(Dictionary<string, string> args, QueryCondition condition = QueryCondition.And)
        {
            if (Engine == null)
            {
                throw new NullReferenceException("Engine is null.");
            }

            // 实例化泛型对象
            return this.MappingObject(Engine.FindOneByCondition(this.TableName, args, condition));
        }

        public List<T> ToList()
        {
            List<T> list = new List<T>();
            List<Dictionary<string, object>> objList = Engine.GetAll(this.TableName);
            foreach (Dictionary<string, object> dic in objList)
            {
                // 实例化泛型对象，并添加到List
                list.Add(this.MappingObject(dic));
            }
            return list;
        }

        public List<T> ToList(Dictionary<string, string> args, QueryCondition condition = QueryCondition.And)
        {
            List<T> list = new List<T>();
            List<Dictionary<string, object>> objList = Engine.GetAllByCondition(this.TableName, args, condition);
            foreach (Dictionary<string, object> dic in objList)
            {
                // 实例化泛型对象，并添加到List
                list.Add(this.MappingObject(dic));
            }
            return list;
        }

        /// <summary>
        /// 将属性键值对转化成实体对象
        /// </summary>
        /// <param name="dic">属性键值对</param>
        /// <returns>转化后的实体对象</returns>
        private T MappingObject(Dictionary<string, object> dic)
        {
            // 反射创建泛型对象
            T result = Activator.CreateInstance<T>();
            // 获取对象的属性列表
            PropertyInfo[] props = result.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                // 反射设置属性值
                switch (prop.Name)
                {
                    case "TableName":
                        prop.SetValue(result, this.TableName, null);
                        break;
                    case "_id":
                        prop.SetValue(result, new Guid((string) dic[prop.Name]), null);
                        break;
                    default:
                        prop.SetValue(result, dic[prop.Name], null);
                        break;
                }
            }
            return result;
        }

    }
}
