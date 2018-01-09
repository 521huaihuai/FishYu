using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ReportFormDesign.Animals
{
    public abstract class Animalion
    {
        public IAnimal AnimalInterface { get; set; }

        /// <summary>
        /// 是否结束动画
        /// </summary>
        public bool IsEndAnimalion { get; set; }

        /// <summary>
        /// 是否准备动画, 动画在允许的情况下， 动画起始动作
        /// </summary>
        public bool IsPrepareAnimaled { get; set; }

        private int index;
        /// <summary>
        /// 动画的执行一次需要的时间
        /// </summary>
        public int DelayTime { get; set; }

        /// <summary>
        /// 动画持续时间(默认8秒)
        /// </summary>
        public float AnimalionTime { get; set; }


        private bool IsPrepareAnimalion;


        public Animalion()
        {
            DelayTime = 40;
            AnimalionTime = 8.0f;
            IsPrepareAnimaled = false;
            IsPrepareAnimalion = true;
            //IsAllowDrawAnimal = true;
        }


        /// <summary>
        /// 开始动画
        /// </summary>
        public void AnimalionRender()
        {
            if (IsPrepareAnimalion)
            {
                if (AnimalInterface != null)
                {
                    AnimalInterface.PrepareAnimalion();
                    IsEndAnimalion = false;
                    IsPrepareAnimalion = false;
                }
            }
            //开始动画
            if (AnimalInterface != null)
            {
                AnimalInterface.StartAnimalion();
            }
            //Console.WriteLine("动画中Animaling" + index);
            index++;
            AnimalionUsedTimeMilliseconds = index * DelayTime * 1.0f;
            AnimalionUsedTime = AnimalionUsedTimeMilliseconds / 1000;
            if (AnimalionUsedTime >= AnimalionTime)
            {
                IsPrepareAnimaled = false;
                IsPrepareAnimalion = true;
                index = 0;
                //动画结束
                if (AnimalInterface != null)
                {
                    AnimalInterface.EndAnimation();
                    IsEndAnimalion = true;
                    AnimalionUsedTime = AnimalionTime;
                }
            }
           
        }

        /// <summary>
        /// 动画已消耗的时间
        /// </summary>
        public float AnimalionUsedTime { get; set; }
        
        /// <summary>
        /// 动画已消耗的时间
        /// </summary>
        public float AnimalionUsedTimeMilliseconds{ get; set; }

        public void stopAnimalion()
        {
            IsPrepareAnimaled = false;
            IsPrepareAnimalion = true;
            index = 0;
            //动画结束
            if (AnimalInterface != null)
            {
                AnimalInterface.EndAnimation();
                IsEndAnimalion = true;
                AnimalionUsedTime = AnimalionTime;
            }
        }

        public bool IsAnimailing { get; set; }
    }
}
