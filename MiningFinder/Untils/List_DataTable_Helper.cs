using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpiderService.Until
{
    public class List_DataTable_Helper
    {
        ///
        /// 将泛类型集合List类转换成DataTable
        ///
        /// 泛类型集合
        /// 
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }


        /// <summary>
        /// DataTable转换为List&lt;Model&gt;
        /// </summary>
        public static class DataTableToListModel<T> where T : new()
        {
            public static IList<T> ConvertToModel(DataTable dt)
            {
                //定义集合
                IList<T> ts = new List<T>();
                T t = new T();
                string tempName = "";
                //获取此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (DataRow row in dt.Rows)
                {
                    t = new T();
                    foreach (PropertyInfo pi in propertys)
                    {
                        tempName = pi.Name;
                        //检查DataTable是否包含此列
                        if (dt.Columns.Contains(tempName))
                        {
                            //判断此属性是否有set
                            if (!pi.CanWrite)
                                continue;
                            object value = row[tempName];
                            if (value != DBNull.Value)
                            {
                                pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);

                            }
                                
                        }
                    }
                    ts.Add(t);
                }
                return ts;
            }
        }
    }
}
