using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Core.Enums.Enum;
using Alabo.Extensions;
using Alabo.UI;

namespace Alabo.Core.Randoms
{
    public static class RandomHelper
    {
        public static List<string> Xing = new List<string>
        {
            "赵", "钱", "孙", "李", "周", "吴", "郑", "王", "冯", "陈", "楮", "卫", "蒋", "沈", "韩", "杨",
            "朱", "秦", "尤", "许", "何", "吕", "施", "张", "孔", "曹", "严", "华", "金", "魏", "陶", "姜",
            "戚", "谢", "邹", "喻", "柏", "水", "窦", "章", "云", "苏", "潘", "葛", "奚", "范", "彭", "郎",
            "鲁", "韦", "昌", "马", "苗", "凤", "花", "方", "俞", "任", "袁", "柳", "酆", "鲍", "史", "唐",
            "费", "廉", "岑", "薛", "雷", "贺", "倪", "汤", "滕", "殷", "罗", "毕", "郝", "邬", "安", "常",
            "乐", "于", "时", "傅", "皮", "卞", "齐", "康", "伍", "余", "元", "卜", "顾", "孟", "平", "黄",
            "和", "穆", "萧", "尹", "姚", "邵", "湛", "汪", "祁", "毛", "禹", "狄", "米", "贝", "明", "臧",
            "计", "伏", "成", "戴", "谈", "宋", "茅", "庞", "熊", "纪", "舒", "屈", "项", "祝", "董", "梁",
            "杜", "阮", "蓝", "闽", "席", "季", "麻", "强", "贾", "路", "娄", "危", "江", "童", "颜", "郭",
            "梅", "盛", "林", "刁", "锺", "徐", "丘", "骆", "高", "夏", "蔡", "田", "樊", "胡", "凌", "霍",
            "虞", "万", "支", "柯", "昝", "管", "卢", "莫", "经", "房", "裘", "缪", "干", "解", "应", "宗",
            "丁", "宣", "贲", "邓", "郁", "单", "杭", "洪", "包", "诸", "左", "石", "崔", "吉", "钮", "龚",
            "程", "嵇", "邢", "滑", "裴", "陆", "荣", "翁", "荀", "羊", "於", "惠", "甄", "麹", "家", "封",
            "芮", "羿", "储", "靳", "汲", "邴", "糜", "松", "井", "段", "富", "巫", "乌", "焦", "巴", "弓",
            "牧", "隗", "山", "谷", "车", "侯", "宓", "蓬", "全", "郗", "班", "仰", "秋", "仲", "伊", "宫",
            "宁", "仇", "栾", "暴", "甘", "斜", "厉", "戎", "祖", "武", "符", "刘", "景", "詹", "束", "龙",
            "叶", "幸", "司", "韶", "郜", "黎", "蓟", "薄", "印", "宿", "白", "怀", "蒲", "邰", "从", "鄂",
            "索", "咸", "籍", "赖", "卓", "蔺", "屠", "蒙", "池", "乔", "阴", "郁", "胥", "能", "苍", "双",
            "闻", "莘", "党", "翟", "谭", "贡", "劳", "逄", "姬", "申", "扶", "堵", "冉", "宰", "郦", "雍",
            "郤", "璩", "桑", "桂", "濮", "牛", "寿", "通", "边", "扈", "燕", "冀", "郏", "浦", "尚", "农",
            "温", "别", "庄", "晏", "柴", "瞿", "阎", "充", "慕", "连", "茹", "习", "宦", "艾", "鱼", "容",
            "向", "古", "易", "慎", "戈", "廖", "庾", "终", "暨", "居", "衡", "步", "都", "耿", "满", "弘",
            "匡", "国", "文", "寇", "广", "禄", "阙", "东", "欧", "殳", "沃", "利", "蔚", "越", "夔", "隆",
            "师", "巩", "厍", "聂", "晁", "勾", "敖", "融", "冷", "訾", "辛", "阚", "那", "简", "饶", "空",
            "曾", "毋", "沙", "乜", "养", "鞠", "须", "丰", "巢", "关", "蒯", "相", "查", "后", "荆", "红",
            "游", "竺", "权", "逑", "盖", "益", "桓", "公", "仉", "督", "晋", "楚", "阎", "法", "汝", "鄢",
            "涂", "钦", "岳", "帅", "缑", "亢", "况", "后", "有", "琴", "归", "海", "墨", "哈", "谯", "笪",
            "年", "爱", "阳", "佟", "商", "牟", "佘", "佴", "伯", "赏",
            "万俟", "司马", "上官", "欧阳", "夏侯", "诸葛", "闻人", "东方", "赫连", "皇甫", "尉迟", "公羊",
            "澹台", "公冶", "宗政", "濮阳", "淳于", "单于", "太叔", "申屠", "公孙", "仲孙", "轩辕", "令狐",
            "锺离", "宇文", "长孙", "慕容", "鲜于", "闾丘", "司徒", "司空", "丌官", "司寇", "子车", "微生",
            "颛孙", "端木", "巫马", "公西", "漆雕", "乐正", "壤驷", "公良", "拓拔", "夹谷", "宰父", "谷梁",
            "段干", "百里", "东郭", "南门", "呼延", "羊舌", "梁丘", "左丘", "东门", "西门", "南宫"
        };

        private static readonly string _lastNameMan =
            "刚伟勇毅俊峰强军平保东文辉力明永健世广志义兴良海山仁波宁贵福生龙元全国胜学祥才发武新利清飞彬富顺信子杰涛昌成康星光天达安岩中茂进林有坚和彪博诚先敬震振壮会思群豪心邦承乐绍功松善厚庆磊民友裕河哲江超浩亮政谦亨奇固之轮翰朗伯宏言若鸣朋斌梁栋维启克伦翔旭鹏泽晨辰士以建家致树炎德行时泰盛雄琛钧冠策腾楠榕风航弘";

        private static readonly string _lastNameWoMan =
            "秀娟英华慧巧美娜静淑惠珠翠雅芝玉萍红娥玲芬芳燕彩春菊兰凤洁梅琳素云莲真环雪荣爱妹霞香月莺媛艳瑞凡佳嘉琼勤珍贞莉桂娣叶璧璐娅琦晶妍茜秋珊莎锦黛青倩婷姣婉娴瑾颖露瑶怡婵雁蓓纨仪荷丹蓉眉君琴蕊薇菁梦岚苑婕馨瑗琰韵融园艺咏卿聪澜纯毓悦昭冰爽琬茗羽希宁欣飘育滢馥筠柔竹霭凝鱼晓欢霄枫芸菲寒伊亚宜可姬舒影荔枝思丽墨";

        /// <summary>
        ///     随机颜色
        /// </summary>
        public static string Color
        {
            get
            {
                var list = new List<string>
                {
                    "info",
                    "success",
                    "warning",
                    "danger",
                    "primary",
                    "focus",
                    "accent",
                    "brand",
                    "metal",
                    "secondary"
                };

                var index = new Random().Next(0, list.Count - 1);
                return list[index];
            }
        }


        /// <summary>
        ///     随机头像
        /// </summary>
        public static string Avator
        {
            get
            {
                var list = new List<string>
                {
                    "https://wx.qlogo.cn/mmopen/vi_32/DYAIOgq83eoBjkfxWn1XibfQCGxay4xYCGrBdaEqfibX7KAY6W3WPMmkhghStPzbc5Gqrert3y1HKzCTUMiaQXUAw/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTLepzwkdGg5UvjN0unsk9K65OlLZZ3W8WxqOp4fe1qBoWWibgSMaMicom94tMLThHqMicZsMhWj9KsTw/132",
                    "https://wx.qlogo.cn/mmhead/C0ETvO6dZsQEkKiaq0ewMCbGSJcYKTibyicCFdldl5Yz2g/132",
                    "https://wx.qlogo.cn/mmhead/C0ETvO6dZsQEkKiaq0ewMCbGSJcYKTibyicCFdldl5Yz2g/13",
                    "https://wx.qlogo.cn/mmhead/C0ETvO6dZsQEkKiaq0ewMCbGSJcYKTibyicCFdldl5Yz2g/13",
                    "https://wx.qlogo.cn/mmopen/vi_32/DYAIOgq83epF9mPLKSEaSSGaRVg9q0oicmS50yvXLiaztWz0AeMOnc1XtYChWMvjjQjg73LicdJCy1sUia09njryQQ/132",
                    "https://wx.qlogo.cn/mmhead/5sEgeNoIv33ocSiaXVR453UF0QrAmr2AsR8zjLkT1GXg/132",
                    "https://wx.qlogo.cn/mmhead/TZjhSicXo7ia5BKGgk3DNy3VOOfK5o1DDAXJjMxMqClrM/132",
                    "https://wx.qlogo.cn/mmhead/OdV8p2693u04Q4Un70fv4hmmdMaOFk6aycmia8GfLubk/132",
                    "https://wx.qlogo.cn/mmhead/dibPnAYQhlKgzicicrFhe8T91mceQROw6tcGrxtvicEhktw/132",
                    "https://wx.qlogo.cn/mmhead/NVscISQ1icjE2ic4gKXfx5HajElCnAMJQPRr3W3UQEHZM/132",
                    "https://wx.qlogo.cn/mmhead/OGKDhUTWLygLCHEC4W3MrQmUxJEA12icnt8boxKYW3YQ/132",
                    "https://wx.qlogo.cn/mmhead/iamMBhOyuml45ThcG46F2pAdgfibFAPgrOKqVrHTiaDDbs/132",
                    "https://wx.qlogo.cn/mmhead/ons53q1m8NV2jjn6ic6yKicdyHD53Px9vJZrqeBVJqNz0/132",
                    "https://wx.qlogo.cn/mmhead/jK4Nic9nJarGc1IsRFic7rcFEg894kMViaQoZmnM8tQwGc/132",
                    "https://wx.qlogo.cn/mmhead/uahnRibzdjhSazs1GzXqmFZ3BOg4dCBU4mZAic3dQY07s/132",
                    "https://wx.qlogo.cn/mmhead/aA7RWlyhCsvzH3hZaC6ewksZH2gdEy3KmpTZ6icPiadqE/132",
                    "https://wx.qlogo.cn/mmhead/4RUGvRCNErBLSKY9HTic9oibzsh3r11miarXMKbeFWRBJ4/132",
                    "https://wx.qlogo.cn/mmhead/XBu6rjtdhtCcrAPKNLfiaaVwMSaaOGDx8kzewniaicmVicM/132",
                    "https://wx.qlogo.cn/mmhead/O5Iric47vxnbuzhYicLy4c09zqpsUfqoxGCqKLNJGo2rQ/132",
                    "https://wx.qlogo.cn/mmhead/bgJRyYjNoic9EtDbZGfdCyIUdLicDMp0PaZHDzJwEOQSk/132",
                    "https://wx.qlogo.cn/mmhead/Xiarz4Ie8iaUgXXJd2UicZBCAWSMawn62fvRMhwRaHa1vM/132",
                    "https://wx.qlogo.cn/mmhead/jJ69NIgLHMpl4BZgYkemibIFTldQczsvax7vNIve1M0E/132",
                    "https://wx.qlogo.cn/mmhead/e9FicR67bDbriaEicWGrKDgyluM70iawFibWgEWurnA1gvJU/132",
                    "https://wx.qlogo.cn/mmhead/QL0t0JS6qxxYXnO2Jq7WwPNiaWCARsHub1CaqH6H5icjM/132",
                    "https://wx.qlogo.cn/mmhead/2uIMP8VaFOtlzbDMh6JKQL0TjWNMbqzRHCCk90LSk7s/132",
                    "https://wx.qlogo.cn/mmhead/BfOhoY95m6mtXOK4VyWVvIlJ8PS7oYUv4iaic2RibUUhzE/132",
                    "https://wx.qlogo.cn/mmhead/ibKgNjpmV8bggqkLibmIXBnVcBvozPHg8bGUm3KCibl20g/132",
                    "https://wx.qlogo.cn/mmhead/C89coUlRLgCU0VYy1qS0xNyGJd1VHaU4fNq3DBiaXoeU/132",
                    "https://wx.qlogo.cn/mmhead/CUd0uxTU9r3aOAxaOJibPNoKSUqiabXy4SzeTz11loyuM/132",
                    "https://wx.qlogo.cn/mmhead/9o7S7vaIicCJJQ8691cH0UVYWFmUjLFouLhnbdfxRRI4/132",
                    "https://wx.qlogo.cn/mmhead/8XzNt9MibvztYhibPDroPyJibIN7jzRKnt0ez7XudH0hk4/132",
                    "https://wx.qlogo.cn/mmhead/okwqiaqTIPvms3DJQIyMD5pDiaZyOeX5TnFhtdxFPdiaLg/132",
                    "https://wx.qlogo.cn/mmhead/iaqjt8ToZibLujNYf2XmvVkDTNibxXD2UYY4SuTDYc8Tw4/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/8Fl5yx72MRAOVzMEUC1QEuibiaA52EcZ0hPtsgicjqpNgT4qxxH7vYchqCXQUxXB9gLYLmjrSlP8HFL1Pd06Fgc6w/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTLKb6jIe0GBkrOZZ8Dp3C0X299bAziaM8VtcWrx7IVHguOwbCEqrwFia1UlBTGopZeymGRWhsAj1Rpw/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/a5hTHxwoEWOtpicHjRfOA1YaFbElWwCHdymXP8QjxHf7YLJvT9CaWvyvjX2CA0mAXJ0GuEtX4O3HAYbagjaOB8Q/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/HKLsu3OLFqkw0B4W9W9qm4HEsEmIzUcD8JTBrdfpOfcSt63Emuz2txe6XON4jANe56tVzERk2CaGnOibc3aX1Qg/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/vfoObwkJknPZLSXfSiav51l95Bgr0mFy8VXYWHYxkIJyPzE47xJnboZvlOvn4sEd74jibbibic4r6DcCNBCc57aqtg/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTKxezOOPvxyYG0C0ncEw3wIPvuVjmR3jENK3uFytrECYyAw1xqMzkoFSdyytm96I8JPAZ7bSWDSYw/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTLicBeounDPewLgIQM33kxJ2ejfWbheic5CvaBBvJ8GMv4p7JOBBJG4czmJKP6u6kmTkr5cCdC7SBxQ/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTIJpYbCzMvX0wRvRK6MhWiaob3ZGmDWx7Ern4syTMa9x79draSoScAkEXWH9vTlEAZEcdzDnEqAPng/132",
                    "https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTLT1XcqKqUjrT7nmLvRIalQOD9c2mFv0wA9ccgGm9PqXBWvfWyXvhh2XddZFNN9ATcbTsCX5W8Cicw/132"
                };

                var index = new Random().Next(0, list.Count - 1);
                return list[index];
            }
        }

        /// <summary>
        ///     随机图标
        /// </summary>
        public static string Icon
        {
            get
            {
                var index = new Random().Next(1, 80);
                index.IntToEnum<Flaticon>(out var flaticon);
                return flaticon.GetIcon();
            }
        }

        /// <summary>
        ///     随机数生成器
        /// </summary>
        public static Random Generator { get; } = new Random(SystemRandomInt());

        /// <summary>
        ///     使用RNGCryptoServiceProvider生成真正随机的二进制数据
        /// </summary>
        public static byte[] SystemRandomBytes()
        {
            var bytes = Guid.NewGuid().ToByteArray();
            return bytes;
        }

        /// <summary>
        ///     使用RNGCryptoServiceProvider生成真正随机的整数
        /// </summary>
        public static int SystemRandomInt()
        {
            return BitConverter.ToInt32(SystemRandomBytes(), 0);
        }

        /// <summary>
        ///     随机产生6位的密码
        /// </summary>
        public static string PassWord()
        {
            var str = new Random().Next(100000, 999999);

            return str.ToString().PadLeft(6, '0');
        }

        /// <summary>
        ///     支付密码 6位数字支付密码
        /// </summary>
        /// <returns></returns>
        public static string PayPassWord()
        {
            var str = new Random().Next(100000, 999999);
            return str.ToString().PadLeft(6, '0');
        }

        /// <summary>
        ///     生成10位数的编号
        /// </summary>
        public static string GenerateSerial()
        {
            //把guid转换为两个long int
            var bytes = Guid.NewGuid().ToByteArray();
            var high = BitConverter.ToUInt64(bytes, 0);
            var low = BitConverter.ToUInt64(bytes, 8);
            //转换为10位数字
            var mixed = (high ^ low) % 8999999999 + 1000000000;
            return mixed.ToString();
        }

        /// <summary>
        ///     生成随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <param name="chars">包含的字符，默认是a-zA-Z0-9</param>
        public static string RandomString(int length,
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            var buffer = new char[length];
            for (var n = 0; n < length; ++n) {
                buffer[n] = chars[Generator.Next(chars.Length)];
            }

            return new string(buffer);
        }

        /// <summary>
        ///     随机产生邮箱
        /// </summary>
        public static string Email()
        {
            var emmail = RandomString(5).ToLower() + new Random().Next(10000, 999999);
            return emmail + "@5ug.com";
        }

        /// <summary>
        ///     随机生成手机号码
        /// </summary>
        public static string Mobile()
        {
            string[] tel =
            {
                "133", "153", "180", "181", "189", "134", "135", "136", "137", "138", "139", "150", "151", "152", "157",
                "158", "159", "187", "188", "130", "131", "132", "155", "156", "185", "186", "145"
            };
            var telList = tel.ToList();

            var rd = new Random();
            var index = rd.Next(0, telList.Count);
            var phonePrix = telList[index];
            var phoneLast = new Random().Next(10000000, 99999999);
            return phonePrix + phoneLast;
        }


        /// <summary>
        ///     随机生成手机号码
        /// </summary>
        public static string MobileHiden()
        {
            string[] tel =
            {
                "133", "153", "180", "181", "189", "134", "135", "136", "137", "138", "139", "150", "151", "152", "157",
                "158", "159", "187", "188", "130", "131", "132", "155", "156", "185", "186", "145"
            };
            var telList = tel.ToList();

            var rd = new Random();
            var index = rd.Next(0, telList.Count);
            var phonePrix = telList[index];
            var phoneLast = new Random().Next(1000, 9999);
            return phonePrix + "****" + phoneLast;
        }

        /// <summary>
        ///     随机生成整数
        /// </summary>
        /// <param name="min">最小数范围</param>
        /// <param name="max">最大数范围</param>
        public static int Number(int min, int max)
        {
            var num = new Random().Next(min, max);
            return num;
        }

        /// <summary>
        ///     获取随机性别
        /// </summary>
        /// <returns></returns>
        public static Sex GetSex()
        {
            var i = new Random().Next(0, 100000);
            var sex = Sex.Man;
            if (i % 2 == 0) {
                sex = Sex.WoMan;
            }

            return sex;
        }

        /// <summary>
        ///     随机名字
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public static string Name(Sex sex)
        {
            var nameList = _lastNameMan.ToCharArray().ToList(); //151的男士名
            if (sex == Sex.WoMan) {
                nameList = _lastNameWoMan.ToCharArray().ToList(); //151个女士名
            }

            var lastNameIndex = new Random().Next(0, nameList.Count);
            var lastName = nameList[lastNameIndex];
            var xingIndex = new Random().Next(0, Xing.Count);
            var xing = Xing[xingIndex];
            var firstNameIndex = new Random().Next(0, nameList.Count);
            var firstName = nameList[firstNameIndex];
            return xing + firstName + lastName;
        }

        public static string XingRandom()
        {
            var xingIndex = new Random().Next(0, Xing.Count);
            var xing = Xing[xingIndex];
            return xing;
        }
    }
}