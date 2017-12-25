﻿// -----------------------------------------------------------------------
// <copyright company="Fireasy"
//      email="faib920@126.com"
//      qq="55570729">
//   (c) Copyright Fireasy. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Fireasy.Common.ComponentModel;
using Fireasy.Data.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Fireasy.Data.Batcher
{
    /// <summary>
    /// <see cref="DataTable"/> 的读取器。
    /// </summary>
    public class DataTableBatchReader : DbDataReader
    {
        private DataTable table;
        private DataRow current;
        private int index;

        /// <summary>
        /// 初始化 <see cref="DataTableBatchReader"/> 类的新实例。
        /// </summary>
        /// <param name="table"></param>
        public DataTableBatchReader(DataTable table)
        {
            this.table = table;
        }

        public override object this[int i] => throw new NotSupportedException();

        public override object this[string name] => throw new NotSupportedException();

        public override int Depth => 1;

        public override bool IsClosed => false;

        public override int RecordsAffected => 0;

        public override int FieldCount => table.Columns.Count;

        public override bool HasRows => table.Rows.Count > 0;

        public override void Close()
        {
        }

        public override bool GetBoolean(int i)
        {
            throw new NotSupportedException();
        }

        public override byte GetByte(int i)
        {
            throw new NotSupportedException();
        }

        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public override char GetChar(int i)
        {
            throw new NotSupportedException();
        }

        public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public override string GetDataTypeName(int i)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetDateTime(int i)
        {
            throw new NotSupportedException();
        }

        public override decimal GetDecimal(int i)
        {
            throw new NotSupportedException();
        }

        public override double GetDouble(int i)
        {
            throw new NotSupportedException();
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotSupportedException();
        }

        public override Type GetFieldType(int i)
        {
            return table.Columns[i].DataType;
        }

        public override float GetFloat(int i)
        {
            throw new NotSupportedException();
        }

        public override Guid GetGuid(int i)
        {
            throw new NotSupportedException();
        }

        public override short GetInt16(int i)
        {
            throw new NotSupportedException();
        }

        public override int GetInt32(int i)
        {
            throw new NotSupportedException();
        }

        public override long GetInt64(int i)
        {
            throw new NotSupportedException();
        }

        public override string GetName(int i)
        {
            throw new NotSupportedException();
        }

        public override int GetOrdinal(string name)
        {
            throw new NotSupportedException();
        }

        public override DataTable GetSchemaTable()
        {
            throw new NotSupportedException();
        }

        public override string GetString(int i)
        {
            throw new NotSupportedException();
        }

        public override object GetValue(int i)
        {
            return current[i];
        }

        public override int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        public override bool IsDBNull(int i)
        {
            return current[i] == DBNull.Value;
        }

        public override bool NextResult()
        {
            return false;
        }

        public override bool Read()
        {
            if (index >= table.Rows.Count)
            {
                return false;
            }

            current = table.Rows[index++];

            return true;
        }
    }

    /// <summary>
    /// <see cref="IEnumerable"/> 的读取器。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumerableBatchReader<T> : DbDataReader
    {
        private IEnumerator<T> enumerator;
        private T current;
        private bool hasRows;
        private Dictionary<string, Func<object, object>> accessories = null;
        private List<Type> propertyTypes = null;

        /// <summary>
        /// 初始化 <see cref="EnumerableBatchReader"/> 类的新实例。
        /// </summary>
        /// <param name="enumerable"></param>
        public EnumerableBatchReader(IEnumerable<T> enumerable)
        {
            InitAccessories(enumerable);
            enumerator = enumerable.GetEnumerator();
        }

        public override object this[int i] => throw new NotSupportedException();

        public override object this[string name] => throw new NotSupportedException();

        public override int Depth => 1;

        public override bool IsClosed => false;

        public override int RecordsAffected => 0;

        public override int FieldCount => accessories.Count;

        public override bool HasRows => hasRows;

        public override void Close()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                    enumerator = null;
                }
            }
        }

        public override bool GetBoolean(int i)
        {
            throw new NotSupportedException();
        }

        public override byte GetByte(int i)
        {
            throw new NotSupportedException();
        }

        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public override char GetChar(int i)
        {
            throw new NotSupportedException();
        }

        public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public override string GetDataTypeName(int i)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetDateTime(int i)
        {
            throw new NotSupportedException();
        }

        public override decimal GetDecimal(int i)
        {
            throw new NotSupportedException();
        }

        public override double GetDouble(int i)
        {
            throw new NotSupportedException();
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotSupportedException();
        }

        public override Type GetFieldType(int i)
        {
            throw new NotSupportedException();
        }

        public override float GetFloat(int i)
        {
            throw new NotSupportedException();
        }

        public override Guid GetGuid(int i)
        {
            throw new NotSupportedException();
        }

        public override short GetInt16(int i)
        {
            throw new NotSupportedException();
        }

        public override int GetInt32(int i)
        {
            throw new NotSupportedException();
        }

        public override long GetInt64(int i)
        {
            throw new NotSupportedException();
        }

        public override string GetName(int i)
        {
            throw new NotSupportedException();
        }

        public override int GetOrdinal(string name)
        {
            throw new NotSupportedException();
        }

        public override DataTable GetSchemaTable()
        {
            throw new NotSupportedException();
        }

        public override string GetString(int i)
        {
            throw new NotSupportedException();
        }

        public override object GetValue(int i)
        {
            return accessories.ElementAt(i).Value(current);
        }

        public override int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        public override bool IsDBNull(int i)
        {
            return GetValue(i) == DBNull.Value;
        }

        public override bool NextResult()
        {
            return false;
        }

        public override bool Read()
        {
            if (enumerator.MoveNext())
            {
                current = enumerator.Current;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 初始化对象访问器。
        /// </summary>
        /// <param name="list"></param>
        private void InitAccessories(IEnumerable<T> list)
        {
            var e = list.GetEnumerator();
            if (!e.MoveNext())
            {
                return;
            }

            hasRows = true;
            accessories = new Dictionary<string, Func<object, object>>();
            propertyTypes = new List<Type>();

            if (e.Current is IPropertyFieldMappingResolver resolver)
            {
                foreach (var map in resolver.GetDbMapping())
                {
                    accessories.Add(map.FieldName, map.ValueFunc);
                    propertyTypes.Add(map.PropertyType);
                }
            }
            else
            {
#if !NET35
                TypeDescriptorUtility.AddDefaultDynamicProvider();
#endif
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(e.Current))
                {
                    if (property.PropertyType.IsDbTypeSupported())
                    {
                        accessories.Add(property.Name, o => property.GetValue(o));
                        propertyTypes.Add(property.PropertyType);
                    }
                }
            }
        }
    }
}
