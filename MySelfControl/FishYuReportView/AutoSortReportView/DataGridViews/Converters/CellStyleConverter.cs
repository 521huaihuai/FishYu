/* ======================================================================== 
* 本类功能概述:
* 
* 作者：zjm 
* 时间：2018/1/15 15:33:22 
* 文件名：CellStyleConverter 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.Converters
{
    public class CellStyleConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context,
        Type sourceType)
        {
            if (sourceType == typeof(string)) return true;//字符串，如："Jonny,Sun,33" 
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) return true;
            if (destinationType == typeof(InstanceDescriptor)) return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string s = value as string;

            if (s == null) return base.ConvertFrom(context, culture, value);

            ////字符串，如："Jonny,Sun,33" 
            //string[] ps = s.Split(new char[] { char.Parse(",") });

            //if (ps.Length != 3)
            //    throw new ArgumentException("Failed to parse Text");
            //解析字符串并实例化对象 
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
        {
            ////将对象转换为字符串，如："Jonny,Sun,33" 
            //if ((destinationType == typeof(string)) && (value is FishYuCellStyle))
            //    return ((FishYuCellStyle)value).Alignment + "," + ((FishYuCellStyle)value).BackColor + "," + ((FishYuCellStyle)value).Font.ToString();

            //生成设计时的构造器代码 
            // this.testComponent1.Person = new CSFramework.MyTypeConverter.Person("Jonny", "Sun", 33); 
            if (destinationType == typeof(InstanceDescriptor) & value is FishYuCellStyle)
            {
                FishYuCellStyle p = (FishYuCellStyle)value;
                ConstructorInfo ctor = typeof(FishYuCellStyle).GetConstructor(
                new Type[] { typeof(Alignment), typeof(Color), typeof(Font), typeof(Color), typeof(Color), typeof(Color) });
                return new InstanceDescriptor(ctor, new object[] { p.Alignment, p.BackColor, p.Font, p.ForeColor, p.SelectBackColor, p.SelectForeColor });
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context,
        IDictionary propertyValues)
        {
            FishYuCellStyle fishYuCellStyle = new FishYuCellStyle();
            fishYuCellStyle.Alignment = (Alignment)propertyValues["Alignment"];
            fishYuCellStyle.BackColor = (Color)propertyValues["BackColor"];
            fishYuCellStyle.Font = (Font)propertyValues["Font"];
            fishYuCellStyle.ForeColor = (Color)propertyValues["ForeColor"];
            fishYuCellStyle.SelectBackColor = (Color)propertyValues["SelectBackColor"];
            fishYuCellStyle.SelectForeColor = (Color)propertyValues["SelectForeColor"];
            return fishYuCellStyle;//创建实例 
        }

        public override PropertyDescriptorCollection GetProperties(
        ITypeDescriptorContext context,
        object value, Attribute[] attributes)
        {
            if (value is FishYuCellStyle)
                return TypeDescriptor.GetProperties(value, attributes);

            return base.GetProperties(context, value, attributes);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

    }
}