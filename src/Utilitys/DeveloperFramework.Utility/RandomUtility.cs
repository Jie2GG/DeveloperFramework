﻿using System;
using System.Text;
using System.Threading;

namespace DeveloperFramework.Utility
{
	/// <summary>
	/// 随机实用程序
	/// </summary>
	public static class RandomUtility
	{
		private readonly static Random _random = new Random (Guid.NewGuid ().GetHashCode ());
		private readonly static string[] _areaArray = { "中国 黑龙江省 齐齐哈尔市", "中国 黑龙江省 黑河市", "中国 黑龙江省 鹤岗市", "中国 黑龙江省 鸡西市", "中国 黑龙江省 绥化市", "中国 黑龙江省 牡丹江市", "中国 黑龙江省 大庆市", "中国 黑龙江省 大兴安岭地区", "中国 黑龙江省 哈尔滨市", "中国 黑龙江省 双鸭山市", "中国 黑龙江省 佳木斯市", "中国 黑龙江省 伊春市", "中国 黑龙江省 七台河市", "中国 香港特别行政区 香港特别行政区 ", "中国 青海省 黄南藏族自治州", "中国 青海省 西宁市", "中国 青海省 玉树藏族自治州", "中国 青海省 海西蒙古族藏族自治州", "中国 青海省 海南藏族自治州", "中国 青海省 海北藏族自治州", "中国 青海省 海东地区", "中国 青海省 果洛藏族自治州", "中国 陕西省 铜川市", "中国 陕西省 西安市", "中国 陕西省 渭南市", "中国 陕西省 汉中市", "中国 陕西省 榆林市", "中国 陕西省 延安市", "中国 陕西省 宝鸡市", "中国 陕西省 安康市", "中国 陕西省 商洛市", "中国 陕西省 咸阳市", "中国 重庆市重庆市", "中国 辽宁省 鞍山市", "中国 辽宁省 阜新市", "中国 辽宁省 锦州市", "中国 辽宁省 铁岭市", "中国 辽宁省 辽阳市", "中国 辽宁省 葫芦岛市", "中国 辽宁省 营口市", "中国 辽宁省 盘锦市", "中国 辽宁省 沈阳市", "中国 辽宁省 本溪市", "中国 辽宁省 朝阳市", "中国 辽宁省 抚顺市", "中国 辽宁省 大连市", "中国 辽宁省 丹东市", "中国 贵州省 黔西南布依族苗族自治州", "中国 贵州省 黔南布依族苗族自治州", "中国 贵州省 黔东南苗族侗族自治州", "中国 贵州省 铜仁地区", "中国 贵州省 遵义市", "中国 贵州省 贵阳市", "中国 贵州省 毕节地区", "中国 贵州省 安顺市", "中国 贵州省 六盘水市", "中国 西藏自治区 阿里地区", "中国 西藏自治区 那曲地区", "中国 西藏自治区 林芝地区", "中国 西藏自治区 昌都地区", "中国 西藏自治区 日喀则地区", "中国 西藏自治区 拉萨市", "中国 西藏自治区 山南地区", "中国 福建省 龙岩市", "中国 福建省 莆田市", "中国 福建省 福州市", "中国 福建省 漳州市", "中国 福建省 泉州市", "中国 福建省 宁德市", "中国 福建省 厦门市", "中国 福建省 南平市", "中国 福建省 三明市", "中国 甘肃省 陇南市", "中国 甘肃省 金昌市", "中国 甘肃省 酒泉市", "中国 甘肃省 白银市", "中国 甘肃省 甘南藏族自治州", "中国 甘肃省 武威市", "中国 甘肃省 张掖市", "中国 甘肃省 庆阳市", "中国 甘肃省 平凉市", "中国 甘肃省 定西市", "中国 甘肃省 天水市", "中国 甘肃省 嘉峪关市", "中国 甘肃省 兰州市", "中国 甘肃省 临夏回族自治州", "中国 澳门特别行政区 澳门特别行政区 ", "中国 湖南省 长沙市", "中国 湖南省 郴州市", "中国 湖南省 邵阳市", "中国 湖南省 衡阳市", "中国 湖南省 益阳市", "中国 湖南省 湘西土家族苗族自治州", "中国 湖南省 湘潭市", "中国 湖南省 永州市", "中国 湖南省 株洲市", "中国 湖南省 怀化市", "中国 湖南省 张家界市", "中国 湖南省 常德市", "中国 湖南省 岳阳市", "中国 湖南省 娄底市", "中国 湖北省 黄石市", "中国 湖北省 黄冈市", "中国 湖北省 随州市", "中国 湖北省 鄂州市", "中国 湖北省 襄樊市", "中国 湖北省 荆门市", "中国 湖北省 荆州市", "中国 湖北省 神农架", "中国 湖北省 武汉市", "中国 湖北省 恩施土家族苗族自治州", "中国 湖北省 宜昌市", "中国 湖北省 孝感市", "中国 湖北省 咸宁市", "中国 湖北省 十堰市", "中国 海南省 海口市", "中国 海南省 三亚市", "中国 浙江省 金华市", "中国 浙江省 衢州市", "中国 浙江省 舟山市", "中国 浙江省 绍兴市", "中国 浙江省 湖州市", "中国 浙江省 温州市", "中国 浙江省 杭州市", "中国 浙江省 宁波市", "中国 浙江省 嘉兴市", "中国 浙江省 台州市", "中国 浙江省 丽水市", "中国 河南省 鹤壁市", "中国 河南省 驻马店市", "中国 河南省 郑州市", "中国 河南省 许昌市", "中国 河南省 焦作市", "中国 河南省 濮阳市", "中国 河南省 漯河市", "中国 河南省 洛阳市", "中国 河南省 新乡市", "中国 河南省 开封市", "中国 河南省 平顶山市", "中国 河南省 安阳市", "中国 河南省 商丘市", "中国 河南省 周口市", "中国 河南省 南阳市", "中国 河南省 信阳市", "中国 河南省 三门峡市", "中国 河北省 邯郸市", "中国 河北省 邢台市", "中国 河北省 衡水市", "中国 河北省 秦皇岛市", "中国 河北省 石家庄市", "中国 河北省 沧州市", "中国 河北省 承德市", "中国 河北省 张家口市", "中国 河北省 廊坊市", "中国 河北省 唐山市", "中国 河北省 保定市", "中国 江西省 鹰潭市", "中国 江西省 赣州市", "中国 江西省 萍乡市", "中国 江西省 景德镇市", "中国 江西省 新余市", "中国 江西省 抚州市", "中国 江西省 宜春市", "中国 江西省 吉安市", "中国 江西省 南昌市", "中国 江西省 九江市", "中国 江西省 上饶市", "中国 江苏省 镇江市", "中国 江苏省 连云港市", "中国 江苏省 苏州市", "中国 江苏省 盐城市", "中国 江苏省 淮安市", "中国 江苏省 泰州市", "中国 江苏省 无锡市", "中国 江苏省 扬州市", "中国 江苏省 徐州市", "中国 江苏省 常州市", "中国 江苏省 宿迁市", "中国 江苏省 南通市", "中国 江苏省 南京市", "中国 新疆维吾尔自治区 阿拉尔市", "中国 新疆维吾尔自治区 阿勒泰地区", "中国 新疆维吾尔自治区 阿克苏地区", "中国 新疆维吾尔自治区 石河子市", "中国 新疆维吾尔自治区 昌吉回族自治州", "中国 新疆维吾尔自治区 巴音郭楞蒙古自治州", "中国 新疆维吾尔自治区 塔城地区", "中国 新疆维吾尔自治区 图木舒克市", "中国 新疆维吾尔自治区 喀什地区", "中国 新疆维吾尔自治区 哈密地区", "中国 新疆维吾尔自治区 和田地区", "中国 新疆维吾尔自治区 吐鲁番地区", "中国 新疆维吾尔自治区 博尔塔拉蒙古自治州", "中国 新疆维吾尔自治区 克拉玛依市", "中国 新疆维吾尔自治区 克孜勒苏柯尔克孜自治州", "中国 新疆维吾尔自治区 伊犁哈萨克自治州", "中国 新疆维吾尔自治区 五家渠市", "中国 新疆维吾尔自治区 乌鲁木齐市", "中国 广西壮族自治区 防城港市", "中国 广西壮族自治区 钦州市", "中国 广西壮族自治区 贺州市", "中国 广西壮族自治区 贵港市", "中国 广西壮族自治区 百色市", "中国 广西壮族自治区 玉林市", "中国 广西壮族自治区 河池市", "中国 广西壮族自治区 梧州市", "中国 广西壮族自治区 桂林市", "中国 广西壮族自治区 柳州市", "中国 广西壮族自治区 来宾市", "中国 广西壮族自治区 崇左市", "中国 广西壮族自治区 南宁市", "中国 广西壮族自治区 北海市", "中国 广东省 韶关市", "中国 广东省 阳江市", "中国 广东省 茂名市", "中国 广东省 肇庆市", "中国 广东省 珠海市", "中国 广东省 潮州市", "中国 广东省 湛江市", "中国 广东省 清远市", "中国 广东省 深圳市", "中国 广东省 河源市", "中国 广东省 江门市", "中国 广东省 汕尾市", "中国 广东省 汕头市", "中国 广东省 梅州市", "中国 广东省 揭阳市", "中国 广东省 惠州市", "中国 广东省 广州市", "中国 广东省 佛山市", "中国 广东省 云浮市", "中国 广东省 中山市", "中国 广东省 东莞市", "中国 山西省 阳泉市", "中国 山西省 长治市", "中国 山西省 运城市", "中国 山西省 朔州市", "中国 山西省 晋城市", "中国 山西省 晋中市", "中国 山西省 忻州市", "中国 山西省 太原市", "中国 山西省 大同市", "中国 山西省 吕梁市", "中国 山西省 临汾市", "中国 山东省 青岛市", "中国 山东省 菏泽市", "中国 山东省 莱芜市", "中国 山东省 聊城市", "中国 山东省 烟台市", "中国 山东省 潍坊市", "中国 山东省 滨州市", "中国 山东省 淄博市", "中国 山东省 济宁市", "中国 山东省 济南市", "中国 山东省 泰安市", "中国 山东省 枣庄市", "中国 山东省 日照市", "中国 山东省 德州市", "中国 山东省 威海市", "中国 山东省 临沂市", "中国 山东省 东营市", "中国 安徽省 黄山市", "中国 安徽省 马鞍山市", "中国 安徽省 阜阳市", "中国 安徽省 铜陵市", "中国 安徽省 蚌埠市", "中国 安徽省 芜湖市", "中国 安徽省 滁州市", "中国 安徽省 淮南市", "中国 安徽省 淮北市", "中国 安徽省 池州市", "中国 安徽省 巢湖市", "中国 安徽省 宿州市", "中国 安徽省 宣城市", "中国 安徽省 安庆市", "中国 安徽省 合肥市", "中国 安徽省 六安市", "中国 安徽省 亳州市", "中国 宁夏回族自治区 银川市", "中国 宁夏回族自治区 石嘴山市", "中国 宁夏回族自治区 固原市", "中国 宁夏回族自治区 吴忠市", "中国 宁夏回族自治区 中卫市", "中国 天津市天津市", "中国 四川省 雅安市", "中国 四川省 阿坝藏族羌族自治州", "中国 四川省 遂宁市", "中国 四川省 达州市", "中国 四川省 资阳市", "中国 四川省 自贡市", "中国 四川省 绵阳市", "中国 四川省 眉山市", "中国 四川省 甘孜藏族自治州", "中国 四川省 泸州市", "中国 四川省 攀枝花市", "中国 四川省 成都市", "中国 四川省 德阳市", "中国 四川省 广安市", "中国 四川省 广元市", "中国 四川省 巴中市", "中国 四川省 宜宾市", "中国 四川省 南充市", "中国 四川省 凉山彝族自治州", "中国 四川省 内江市", "中国 四川省 乐山市", "中国 吉林省 长春市", "中国 吉林省 通化市", "中国 吉林省 辽源市", "中国 吉林省 白山市", "中国 吉林省 白城市", "中国 吉林省 松原市", "中国 吉林省 延边朝鲜族自治州", "中国 吉林省 四平市", "中国 吉林省 吉林市", "中国 台湾省 台湾省 ", "中国 北京市 北京市", "中国 内蒙古自治区 阿拉善盟", "中国 内蒙古自治区 锡林郭勒盟", "中国 内蒙古自治区 鄂尔多斯市", "中国 内蒙古自治区 通辽市", "中国 内蒙古自治区 赤峰市", "中国 内蒙古自治区 巴彦淖尔市", "中国 内蒙古自治区 呼和浩特市", "中国 内蒙古自治区 呼伦贝尔市", "中国 内蒙古自治区 包头市", "中国 内蒙古自治区 兴安盟", "中国 内蒙古自治区 乌海市", "中国 内蒙古自治区 乌兰察布市", "中国 云南省 迪庆藏族自治州", "中国 云南省 西双版纳傣族自治州", "中国 云南省 红河哈尼族彝族自治州", "中国 云南省 玉溪市", "中国 云南省 楚雄彝族自治州", "中国 云南省 曲靖市", "中国 云南省 普洱市", "中国 云南省 昭通市", "中国 云南省 昆明市", "中国 云南省 文山壮族苗族自治州", "中国 云南省 怒江傈僳族自治州", "中国 云南省 德宏傣族景颇族自治州", "中国 云南省 大理白族自治州", "中国 云南省 保山市", "中国 云南省 丽江市", "中国 云南省 临沧市", "中国 上海市 上海市" };
		private readonly static string[] _surnameArray = { "赵", "钱", "孙", "李", "周", "吴", "郑", "王", "冯", "陈", "楮", "卫", "蒋", "沈", "韩", "杨", "朱", "秦", "尤", "许", "何", "吕", "施", "张", "孔", "曹", "严", "华", "金", "魏", "陶", "姜", "戚", "谢", "邹", "喻", "柏", "水", "窦", "章", "云", "苏", "潘", "葛", "奚", "范", "彭", "郎", "鲁", "韦", "昌", "马", "苗", "凤", "花", "方", "俞", "任", "袁", "柳", "酆", "鲍", "史", "唐", "费", "廉", "岑", "薛", "雷", "贺", "倪", "汤", "滕", "殷", "罗", "毕", "郝", "邬", "安", "常", "乐", "于", "时", "傅", "皮", "卞", "齐", "康", "伍", "余", "元", "卜", "顾", "孟", "平", "黄", "和", "穆", "萧", "尹", "姚", "邵", "湛", "汪", "祁", "毛", "禹", "狄", "米", "贝", "明", "臧", "计", "伏", "成", "戴", "谈", "宋", "茅", "庞", "熊", "纪", "舒", "屈", "项", "祝", "董", "梁", "杜", "阮", "蓝", "闽", "席", "季", "麻", "强", "贾", "路", "娄", "危", "江", "童", "颜", "郭", "梅", "盛", "林", "刁", "锺", "徐", "丘", "骆", "高", "夏", "蔡", "田", "樊", "胡", "凌", "霍", "虞", "万", "支", "柯", "昝", "管", "卢", "莫", "经", "房", "裘", "缪", "干", "解", "应", "宗", "丁", "宣", "贲", "邓", "郁", "单", "杭", "洪", "包", "诸", "左", "石", "崔", "吉", "钮", "龚", "程", "嵇", "邢", "滑", "裴", "陆", "荣", "翁", "荀", "羊", "於", "惠", "甄", "麹", "家", "封", "芮", "羿", "储", "靳", "汲", "邴", "糜", "松", "井", "段", "富", "巫", "乌", "焦", "巴", "弓", "牧", "隗", "山", "谷", "车", "侯", "宓", "蓬", "全", "郗", "班", "仰", "秋", "仲", "伊", "宫", "宁", "仇", "栾", "暴", "甘", "斜", "厉", "戎", "祖", "武", "符", "刘", "景", "詹", "束", "龙", "叶", "幸", "司", "韶", "郜", "黎", "蓟", "薄", "印", "宿", "白", "怀", "蒲", "邰", "从", "鄂", "索", "咸", "籍", "赖", "卓", "蔺", "屠", "蒙", "池", "乔", "阴", "郁", "胥", "能", "苍", "双", "闻", "莘", "党", "翟", "谭", "贡", "劳", "逄", "姬", "申", "扶", "堵", "冉", "宰", "郦", "雍", "郤", "璩", "桑", "桂", "濮", "牛", "寿", "通", "边", "扈", "燕", "冀", "郏", "浦", "尚", "农", "温", "别", "庄", "晏", "柴", "瞿", "阎", "充", "慕", "连", "茹", "习", "宦", "艾", "鱼", "容", "向", "古", "易", "慎", "戈", "廖", "庾", "终", "暨", "居", "衡", "步", "都", "耿", "满", "弘", "匡", "国", "文", "寇", "广", "禄", "阙", "东", "欧", "殳", "沃", "利", "蔚", "越", "夔", "隆", "师", "巩", "厍", "聂", "晁", "勾", "敖", "融", "冷", "訾", "辛", "阚", "那", "简", "饶", "空", "曾", "毋", "沙", "乜", "养", "鞠", "须", "丰", "巢", "关", "蒯", "相", "查", "后", "荆", "红", "游", "竺", "权", "逑", "盖", "益", "桓", "公", "仉", "督", "晋", "楚", "阎", "法", "汝", "鄢", "涂", "钦", "岳", "帅", "缑", "亢", "况", "后", "有", "琴", "归", "海", "墨", "哈", "谯", "笪", "年", "爱", "阳", "佟", "商", "牟", "佘", "佴", "伯", "赏", "万俟", "司马", "上官", "欧阳", "夏侯", "诸葛", "闻人", "东方", "赫连", "皇甫", "尉迟", "公羊", "澹台", "公冶", "宗政", "濮阳", "淳于", "单于", "太叔", "申屠", "公孙", "仲孙", "轩辕", "令狐", "锺离", "宇文", "长孙", "慕容", "鲜于", "闾丘", "司徒", "司空", "丌官", "司寇", "子车", "微生", "颛孙", "端木", "巫马", "公西", "漆雕", "乐正", "壤驷", "公良", "拓拔", "夹谷", "宰父", "谷梁", "段干", "百里", "东郭", "南门", "呼延", "羊舌", "梁丘", "左丘", "东门", "西门", "南宫" };

		/// <summary>
		/// 随机获取一个32位整数
		/// </summary>
		/// <param name="minValue">随机范围的最小值. 默认 <see cref="int.MinValue"/></param>
		/// <param name="maxValue">随机范围的最大值 (不包含此值). 默认 <see cref="int.MaxValue"/></param>
		/// <returns>一个32位整数</returns>
		public static int RandomInt32 (int minValue = int.MinValue, int maxValue = int.MaxValue)
		{
			return _random.Next (minValue, maxValue);
		}
		/// <summary>
		/// 随机获取一个64位整数
		/// </summary>
		/// <param name="minValue">随机范围的最小值. 默认 <see cref="long.MinValue"/></param>
		/// <param name="maxValue">随机范围的最大值 (不包含此值). 默认 <see cref="long.MaxValue"/></param>
		/// <returns>一个64位整数</returns>
		public static long RandomInt64 (long minValue = long.MinValue, long maxValue = long.MaxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException ("minValue 不得大于 maxValue");
			}

			return minValue + (long)((maxValue - minValue) * _random.NextDouble ());
		}
		/// <summary>
		/// 随机获取一个姓名
		/// </summary>
		/// <returns>姓名字符串</returns>
		public static string RandomName ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.Append (_surnameArray[RandomInt32 (0, _areaArray.Length)]);

			int nameLength = RandomInt32 (1, 3);

			// 获取名
			for (int i = 0; i < nameLength; i++)
			{
				builder.Append (RandomChar ());
			}
			return builder.ToString ();
		}
		/// <summary>
		/// 随机获取一个昵称
		/// </summary>
		/// <returns>随机一个昵称</returns>
		public static string RandomNick ()
		{
			int charCount = RandomInt32 (0, 11);    // 字符个数
			int symbolCount = RandomInt32 (0, 3);   // 特殊符号个数

			StringBuilder builder = new StringBuilder ();

			for (int i = 0, sCount = 0; i < charCount; i++)
			{
				if (sCount <= symbolCount && RandomBoolean ())
				{
					builder.Append (RandomSymbol ());
				}
				else
				{
					builder.Append (RandomChar ());
				}
			}
			return builder.ToString ();
		}
		/// <summary>
		/// 随机获取一个群名称
		/// </summary>
		/// <returns>获取的群名称</returns>
		public static string RandomGroupName ()
		{
			int count = RandomInt32 (0, 10);
			StringBuilder builder = new StringBuilder ();
			for (int i = 0; i < count; i++)
			{
				builder.Append (RandomChar ());
			}
			builder.Append ("交流群");
			return builder.ToString ();
		}
		/// <summary>
		/// 随机获取一个汉字
		/// </summary>
		/// <returns>随机的汉字</returns>
		public static string RandomChar ()
		{
			int byteArea = RandomInt32 (16, 88) + 160;
			int byteCode = RandomInt32 (1, 95) + 160;

			byte byte1 = Convert.ToByte (byteArea);
			byte byte2 = Convert.ToByte (byteCode);

			return Encoding.GetEncoding ("GB2312").GetString (new byte[] { byte1, byte2 });
		}
		/// <summary>
		/// 随机获取一个特殊符号
		/// </summary>
		/// <returns>特殊符号</returns>
		public static string RandomSymbol ()
		{
			int byteArea = RandomInt32 (1, 10) + 160;
			int byteCode = RandomInt32 (1, 95) + 160;

			byte byte1 = Convert.ToByte (byteArea);
			byte byte2 = Convert.ToByte (byteCode);

			return Encoding.GetEncoding ("GB2312").GetString (new byte[] { byte1, byte2 });
		}
		/// <summary>
		/// 随机获取一个地址字符串
		/// </summary>
		/// <returns>地址字符串</returns>
		public static string RandomArea ()
		{
			return _areaArray[RandomInt32 (0, _areaArray.Length)];
		}
		/// <summary>
		/// 随机获取一个指定类型的枚举值
		/// </summary>
		/// <typeparam name="T">随机的枚举类型</typeparam>
		/// <returns>这个类型随机的枚举值</returns>
		public static T RandomEnum<T> ()
			where T : Enum
		{
			Array enumArray = Enum.GetValues (typeof (T));
			return (T)enumArray.GetValue (RandomInt32 (0, enumArray.Length));
		}
		/// <summary>
		/// 随机获取一个布尔值
		/// </summary>
		/// <returns>随机布尔值</returns>
		public static bool RandomBoolean ()
		{
			return RandomInt32 (0, 2) == 0;
		}
		/// <summary>
		/// 随机返回多个参数的任意值
		/// </summary>
		/// <typeparam name="T">要返回的值类型</typeparam>
		/// <param name="values">要返回类型的值数组</param>
		/// <returns>随机返回的值</returns>
		public static T RandomElement<T> (params T[] values)
		{
			int index = RandomInt32 (0, values.Length);
			return values[index];
		}
		/// <summary>
		/// 随机获取一个时间日期
		/// </summary>
		/// <param name="minValue">随机范围最小值. 默认: <see cref="DateTime.MinValue"/></param>
		/// <param name="maxValue">随机范围最大值 (不包含此值). 默认: <see cref="DateTime.MaxValue"/></param>
		/// <returns>一个日期时间值</returns>
		public static DateTime RandomDateTime (DateTime? minValue = null, DateTime? maxValue = null)
		{
			if (minValue == null)
			{
				minValue = DateTime.MinValue;
			}

			if (maxValue == null)
			{
				maxValue = DateTime.MaxValue;
			}

			long tick = RandomInt64 (minValue.Value.Ticks, maxValue.Value.Ticks);
			return new DateTime (tick);
		}


	}
}
