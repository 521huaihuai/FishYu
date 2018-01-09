using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.Model;

namespace ReportFormDesign.DrawUtils
{

    /// <summary>
    /// 绘制字体的位置信息
    /// </summary>
    public enum LocationModel
    {
        Location_Center = 0,
        Location_Up = -1,
        Location_Up_Up = -2,
        Location_Down = 1,
        Location_Down_Down = 2,
        Location_Left = -3,
        Location_Right = 3,
        Location_Left_Left = -4,
        Location_Right_Right = 4,
        Location_Up_Right = 5,
        Location_Down_Right,
        Location_Up_Right_Right,
        Location_Down_Right_Right,
        Location_Down_Down_Right_Right,
    }


    public class ReportViewUtils
    {

        #region StaticColors
        //浅蓝色
        public static Color perferShallowBlue = Color.FromArgb(40, 36, 169, 255);
        //蓝色
        public static Color perferBlue = Color.FromArgb(255, 36, 169, 255);
        //粉色
        public static Color perferPink = Color.FromArgb(255, 244, 13, 100);
        //浅粉色
        public static Color perferShallowPink = Color.FromArgb(255, 100, 30, 101);
        //红色
        public static Color perferRed = Color.FromArgb(255, 244, 0, 0);
        //白色
        public static Color perferWhite = Color.White;
        //深绿色
        public static Color perferGreen = Color.FromArgb(255, 64, 116, 52);
        //黄色
        public static Color perferYellow = Color.FromArgb(255, 244, 208, 0);
        //棕色
        public static Color perferBrown = Color.FromArgb(255, 34, 8, 7);
        //紫色
        public static Color perferPurple = Color.FromArgb(255, 175, 18, 88);
        //浅紫色
        public static Color perferWhite_Shallow = Color.FromArgb(255, 201, 207, 202);
        //深蓝色
        public static Color perferBlue_Deep = Color.FromArgb(255, 3, 22, 52);

        //浅灰色
        public static Color perferShallowGray = Color.FromArgb(20, 210, 210, 210);

        //浅灰色2
        public static Color perferShallowGray2 = Color.FromArgb(60, 210, 210, 210);
        public static Color perferShallowGray3 = Color.FromArgb(180, 210, 210, 210);

        public static Color[] PerferColors = { perferBlue, perferYellow, perferRed, perferPurple, perferPink, perferBrown };

        public static Color[] PerferColors2 = { Color.FromArgb(255, 132, 202, 206), Color.FromArgb(255, 197, 186, 222), perferBlue, perferRed, perferPurple };
        #endregion

        public ReportViewUtils()
        {
        }


        /// <summary>
        /// 居中绘制字体
        /// </summary>
        public static void drawString(Graphics g, string mainText, Font TextFont, Brush TextBrush, float startX, float startY, float width, float height)
        {
            drawString(g, LocationModel.Location_Center, mainText, TextFont, TextBrush, startX, startY, width, height);
        }

        /// <summary>
        /// 绘制字体有数量限制
        /// </summary>
        /// <param name="textLimiteCount">绘制字体的最大数量</param>
        public static void drawStringWithLimiteText(Graphics g, LocationModel model, string mainText, Font TextFont, Brush TextBrush, float startX, float startY, float width, float height, int TextLimiteCount)
        {
            //throw new NotImplementedException();
            SizeF sf = g.MeasureString(mainText, TextFont);
            if (mainText.Length > TextLimiteCount)
            {
                mainText = mainText.Substring(0, TextLimiteCount) + "...";
            }
            drawString(g, model, mainText, TextFont, TextBrush, startX, startY, width, height);
        }

        /// <summary>
        /// 绘制字体
        /// </summary>
        /// <param name="g"></param>
        /// <param name="model">绘制字体位置选择</param>
        public static void drawString(Graphics g, LocationModel model, string mainText, Font TextFont, Brush TextBrush, float startX, float startY, float width, float height)
        {
            SizeF size = g.MeasureString(mainText, TextFont);
            float centerTextX = 0.0f;
            float centerTextY = 0.0f;
            switch (model)
            {
                case LocationModel.Location_Center:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Up:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = (height / 2 - size.Height) / 2;
                    break;
                case LocationModel.Location_Up_Up:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = 0;
                    break;
                case LocationModel.Location_Down:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = (height / 2 - size.Height) / 2 + height / 2;
                    break;
                case LocationModel.Location_Down_Down:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = height - size.Height;
                    break;
                case LocationModel.Location_Left:
                    centerTextX = (width / 2 - size.Width) / 2;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Right:
                    centerTextX = (width / 2 - size.Width) / 2 + width / 2;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Left_Left:
                    centerTextX = 0;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Right_Right:
                    centerTextX = width - size.Width;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Up_Right:
                    centerTextX = (width / 2 - size.Width) / 2 + width / 2;
                    centerTextY = (height / 2 - size.Height) / 2;
                    break;
                case LocationModel.Location_Down_Right:
                    centerTextX = (width / 2 - size.Width) / 2 + width / 2;
                    centerTextY = (height / 2 - size.Height) / 2 + height / 2;
                    break;
                case LocationModel.Location_Up_Right_Right:
                    centerTextX = width - size.Width;
                    centerTextY = (height / 2 - size.Height) / 2;
                    break;
                case LocationModel.Location_Down_Right_Right:
                    centerTextX = width - size.Width;
                    centerTextY = (height / 2 - size.Height) / 2 + height / 2;
                    break;
                case LocationModel.Location_Down_Down_Right_Right:
                    centerTextX = width - size.Width;
                    centerTextY = height - size.Height;
                    break;
                default:
                    break;
            }
            //绘制字体
            g.DrawString(mainText, TextFont, TextBrush, startX + centerTextX, startY + centerTextY);
        }


        /// <summary>
        /// 限制字数到指定宽度
        /// </summary>
        /// <param name="width">限制字数的指定宽度</param>
        /// <returns></returns>
        public static string LimiteText(Graphics g, System.Drawing.Font font_Text, string text, int width)
        {
            int Length = text.Length;
            int times = 0;
            while (isOvertTextShowCount(g, text, font_Text, width))
            {
                Length--;
                times++;
                text = text.Substring(0, Length);
                if (times == 1)
                {
                    text = text + "...";
                    Length += 3;
                }
            }
            if (times > 0)
            {
                Length -= 2;
                text = text.Substring(0, Length);
                text = text + "...";
            }
            return text;
        }


        /// <summary>
        /// 创建圆角矩形绘制路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="cornerRadius">角度</param>
        /// <returns></returns>
       public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            if (rect.Height <= 1)
            {
                rect.Height = 1;
            }
            if (rect.Width <= 1)
            {
                rect.Width = 1;
            }
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }


        /// <summary>
        /// 获取数据中的最大值,用于标定y轴的最大数
        /// </summary>
       public static float getMaxNumFromData(float[] datas, float PermMax = 50)
        {
            //throw new NotImplementedException();
            if (datas == null)
            {
                throw new Exception("数据源空");
            }
            //最小为50刻度
            float max = 50;
            foreach (float item in datas)
            {

                if (max < (item))
                {
                    max = (item);
                }
            }
            return (int)(max / PermMax) * PermMax + PermMax;
        }

        /// <summary>
        /// 对数据进行排序, 从小到大
        /// </summary>
        public static string[] SortTextAndData(string[] TextAndData)
        {
            for (int i = 1; i < TextAndData.Length / 2; i++)
            {
               
                for (int j = 0; j < TextAndData.Length / 2 - i; j++)
                {
                    string d1 = TextAndData[2 * j + 1];
                    string dx1 = TextAndData[2 * j];
                    int c1 = int.Parse(d1);

                    string d2 = TextAndData[2 * (j + 1) + 1];
                    int c2 = int.Parse(d2);
                    if (c1 < c2)
                    {
                        TextAndData[2 * j] = TextAndData[2 * (j + 1)];
                        TextAndData[2 * j + 1] = d2;

                        TextAndData[2 * (j + 1)] = dx1;
                        TextAndData[2 * (j + 1) + 1] = d1;
                    }
                }
            }
            return TextAndData;
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        public static Random CreateRandom()
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            return rd;
        }


        /// <summary>
        /// 对象拷贝
        /// </summary>
        /// <param name="obj">被复制对象</param>
        /// <returns>新对象</returns>
        public static object CopyOjbect(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            Object targetDeepCopyObj;
            Type targetType = obj.GetType();
            //值类型  
            if (targetType.IsValueType == true)
            {
                targetDeepCopyObj = obj;
            }
            //引用类型   
            else
            {
                targetDeepCopyObj = System.Activator.CreateInstance(targetType);   //创建引用对象   
                System.Reflection.MemberInfo[] memberCollection = obj.GetType().GetMembers();

                foreach (System.Reflection.MemberInfo member in memberCollection)
                {
                    //拷贝字段
                    if (member.MemberType == System.Reflection.MemberTypes.Field)
                    {
                        System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)member;
                        Object fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable)
                        {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            field.SetValue(targetDeepCopyObj, CopyOjbect(fieldValue));
                        }

                    }//拷贝属性
                    else if (member.MemberType == System.Reflection.MemberTypes.Property)
                    {
                        System.Reflection.PropertyInfo myProperty = (System.Reflection.PropertyInfo)member;

                        System.Reflection.MethodInfo info = myProperty.GetSetMethod(false);
                        if (info != null)
                        {
                            try
                            {
                                object propertyValue = myProperty.GetValue(obj, null);
                                if (propertyValue is ICloneable)
                                {
                                    myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                                }
                                else
                                {
                                    myProperty.SetValue(targetDeepCopyObj, CopyOjbect(propertyValue), null);
                                }
                            }
                            catch (System.Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            return targetDeepCopyObj;
        }


        private static bool isOvertTextShowCount(Graphics g, string text, Font font_Text, int width)
        {
            SizeF sf = g.MeasureString(text, font_Text);
            if (sf.Width > width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int PermMax { get; set; }
    }
    
}
