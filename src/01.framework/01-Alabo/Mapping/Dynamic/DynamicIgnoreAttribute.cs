using System;

namespace Alabo.Mapping.Dynamic {

    /// <summary>
    ///     ʹ�ö�̬����ʱ�����Ը����ԣ�һ�㲻��Ҫ���ĵ��ֶ������ø����ԣ���������Id,UserId�����ʱ���
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DynamicIgnoreAttribute : Attribute {
    }
}